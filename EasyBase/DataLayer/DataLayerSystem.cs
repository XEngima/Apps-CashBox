using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using EasyBase.Classes;

namespace EasyBase.DataLayer
{
	public partial class StandardDataLayer : BaseDataLayer, IDisposable
	{
        public event ProgressEventHandler Progress;

		public StandardDataLayer()
		{
			Connection = new EasyBaseSqlConnection(Settings.ConnectionString);
		}

        public void OnProgress(int currentValue, int targetValue, string message)
        {
            if (Progress != null) {
                Progress(this, new ProgressEventArgs(currentValue, targetValue, message));
            }
        }

		/// <summary>
		/// Opens a connection to the database.
		/// </summary>
		public void Connect()
		{
			Connect(false);
		}

		public IEasyBaseConnection TheConnection
		{
			get { return Connection; }
		}

        public void Dispose()
        {
            Disconnect();
        }

        public static bool OkForTesting
        {
            get { return (Settings.ConnectionString == Settings.TestConnectionString); }
        }

        /// <summary>
		/// Opens a connection to the database.
		/// </summary>
        /// <param name="overrideDatabaseSchemaCheck">true if you want the connection to open even if database has the wrong database version.</param>
		/// <exception cref="UpdatingDatabaseException">Thrown if the database currently are being updated.</exception>
		public void Connect(bool overrideDatabaseSchemaCheck)
		{
            if (ConnectedToReleaseDatabase && CurrentApplication.SkippedDays != 0)
            {
                throw new InvalidOperationException("Skipped days cannot be set in release database.");
            }

			if (!overrideDatabaseSchemaCheck && !CheckDatabase()) {
				throw new InvalidOperationException("The coded database and the saved database do not match.");
			}

			Connection.Open();

			if (!overrideDatabaseSchemaCheck && NeedToUpdateDatabase()) {
				Connection.Close();
				throw new NeedUpdateDatabaseException("Cannot connect since the database need to be updated!");
			}
		}

		/// <summary>
		/// Disconnects the database.
		/// </summary>
		public void Disconnect()
		{
			Connection.Close();
		}

		private EasyBaseSqlConnection Connection
		{
			get;
			set;
		}

        /// <summary>
		/// Bygger upp alla tabeller och vyer i en databas i enlighet med den senaste versionen. Databasen måste finnas innan den här funktionen används.
		/// </summary>
		public void BuildDatabase()
		{
            DropAllTablesInDatabase();

            DatabaseSchema database = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(DatabaseDefinition.DatabaseVersion));

            string[] sqls = database.GenerateSqlCreate();

            foreach (string sql in sqls) {
                Connection.Execute(sql);
            }

            // Write the database table
            string startSql = "insert into EasyBaseSystems values (" + (int)EasyBase.Classes.EasyBaseSystemNo.CurrentApplicationNo + ", " + DatabaseDefinition.DatabaseVersion + ", " + DatabaseDefinition.DatabaseVersion + ")";
            Connection.Insert(startSql);

            InitDatabaseOnCreate();
		}

        /// <summary>
        /// Skapar en tom databas med samma namn som i kopplingssträngen på webbservern.
        /// </summary>
	    public void CreateDatabaseFromScratch()
	    {
            string input = Settings.ConnectionString;
            string regex = "Initial Catalog=([^;]+)";

            Match match = Regex.Match(input, regex);
	        string databaseName = "";

            if (match.Success)
            {
                databaseName = match.Groups[1].Value;
            }

	        string connectionString = Regex.Replace(Settings.ConnectionString, "Initial Catalog=[^;]+", "Initial Catalog=master",
	            RegexOptions.IgnoreCase);

	        string sql = "create database " + databaseName;

	        EasyBaseSqlConnection connection = new EasyBaseSqlConnection(connectionString);
	        try
	        {
                connection.Open();
	            connection.Execute(sql);
	        }
	        finally
	        {
	            connection.Close();
	        }
	    }

	    /// <summary>
		/// Skapar en databas.
		/// </summary>
		/// <param name="version">Vilken databasversion som ska skapas.</param>
		private void CreateDatabaseNoConstraints(int version)
		{
			DropAllTablesInDatabase();

			DatabaseSchema database = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(version));

			//DatabaseSchema database = new DatabaseSchema(GetDatabaseDefinitionXml());

			string[] sqls = database.GenerateSqlCreateNoKeys(DatabaseDefinition.DatabaseVersion);

			foreach (string sql in sqls) {
				Connection.Execute(sql);
			}
		}

		/// <summary>
		/// Checks that the coded database and the current database schema match.
		/// </summary>
		/// <returns>true if the databases agree, otherwise false.</returns>
		public bool CheckDatabase()
		{
            var savedDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(DatabaseDefinition.DatabaseVersion));
			var codedDatabase = new DatabaseSchema(GetDatabaseDefinitionXml());

			return savedDatabase == codedDatabase;
		}

		public void DropAllTablesInDatabase()
		{
			var database = new DatabaseSchema(GetDatabaseDefinitionXml());
			string[] sqls = database.GenerateSqlDropAll();

		    const string dropForeignKeysSql = @"
                while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY'))
                begin
	                declare @sql nvarchar(2000)
	                SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
	                + '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']')
	                FROM information_schema.table_constraints
	                WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
	                exec (@sql)
                end";

		    Connection.Execute(dropForeignKeysSql);

			foreach (string sql in sqls) {
				Connection.Execute(sql);
			}
		}

		private static DatabaseTableAttribute[] GetAllTableAttributes()
		{
			var databaseTables = new List<DatabaseTableAttribute>();

			//Assembly assembly = Assembly.GetExecutingAssembly();
			Assembly assembly = Assembly.GetAssembly(typeof(EasyBaseSystem));
			foreach (Type type in assembly.GetTypes()) {
				object[] attributes = type.GetCustomAttributes(typeof(DatabaseTableAttribute), false);

				foreach (DatabaseTableAttribute attribute in attributes) {
					databaseTables.Add(attribute);
				}
			}

			return databaseTables.ToArray();
		}

		/// <summary>
		/// Gets the database definition.
		/// </summary>
		/// <returns>An XML file with the database definition as a string.</returns>
		public static string GetDatabaseDefinitionXml()
		{
			XElement databaseElement = new XElement("Database");
            databaseElement.Add(new XAttribute("Version", DatabaseDefinition.DatabaseVersion));
			XElement tablesElement = new XElement("Tables");

			foreach (DatabaseTableAttribute tableAttribute in GetAllTableAttributes()) {
				XElement tableElement = new XElement("Table");
				XElement fieldsElement = new XElement("Fields");

				tableElement.Add(new XAttribute("Name", tableAttribute.TableName));

				if (tableAttribute.IsView) {
					tableElement.Add(new XAttribute("IsView", true));
				}

				// Add primary keys

				foreach (DatabaseFieldAttribute fieldAttribute in tableAttribute.Fields) {
					PrimaryKeyFieldAttribute primaryKeyFieldAttribute = fieldAttribute as PrimaryKeyFieldAttribute;

					if (primaryKeyFieldAttribute != null) {

						XElement fieldElement = new XElement("PrimaryKeyField", new XAttribute("Name", fieldAttribute.FieldName), new XAttribute("Type", fieldAttribute.FieldType));

						if (fieldAttribute.AllowNull) {
							fieldElement.Add(new XAttribute("AllowNull", true));
						}

						if (primaryKeyFieldAttribute.IsIdentity) {
							fieldElement.Add(new XAttribute("IsIdentity", true));
						}

						fieldsElement.Add(fieldElement);

					}
				}

				// Add foreign keys

				foreach (DatabaseFieldAttribute fieldAttribute in tableAttribute.Fields) {
					ForeignKeyFieldAttribute foreignKey = fieldAttribute as ForeignKeyFieldAttribute;

					if (foreignKey != null) {
						XElement fieldElement = new XElement("ForeignKeyField", new XAttribute("Name", foreignKey.FieldName), new XAttribute("Type", foreignKey.FieldType), new XAttribute("TargetTable", foreignKey.TargetTableName), new XAttribute("TargetField", foreignKey.TargetFieldName));

						if (fieldAttribute.AllowNull) {
							fieldElement.Add(new XAttribute("AllowNull", true));
						}
						if (fieldAttribute.UniqueKeyGroups.Length > 0) {
							fieldElement.Add(new XAttribute("UniqueKeyGroup", fieldAttribute.UniqueKeyGroupsString));
						}

						fieldsElement.Add(fieldElement);
					}
				}

				// Add rest of fields

				foreach (DatabaseFieldAttribute fieldAttribute in tableAttribute.Fields) {
					if (!(fieldAttribute is PrimaryKeyFieldAttribute) && !(fieldAttribute is ForeignKeyFieldAttribute) && !(fieldAttribute is ViewFieldAttribute)) {
						XElement fieldElement = new XElement("Field", new XAttribute("Name", fieldAttribute.FieldName), new XAttribute("Type", fieldAttribute.FieldType));

						if (fieldAttribute.AllowNull) {
							fieldElement.Add(new XAttribute("AllowNull", true));
						}
						if (fieldAttribute.UniqueKeyGroups.Length > 0) {
							fieldElement.Add(new XAttribute("UniqueKeyGroup", fieldAttribute.UniqueKeyGroupsString));
						}

						fieldsElement.Add(fieldElement);
					}
				}

				// Add view fields

				foreach (DatabaseFieldAttribute fieldAttribute in tableAttribute.Fields) {
					ViewFieldAttribute viewField = fieldAttribute as ViewFieldAttribute;

					if (viewField != null) {
						XElement fieldElement = new XElement("ViewField", new XAttribute("Name", viewField.FieldName), new XAttribute("Type", viewField.FieldType));

						if (viewField.AllowNull) {
							fieldElement.Add(new XAttribute("AllowNull", true));
						}

						fieldsElement.Add(fieldElement);
					}
				}

				tableElement.Add(fieldsElement);
				tablesElement.Add(tableElement);
			}

			databaseElement.Add(tablesElement);

			return databaseElement.ToString().Replace("\"", "'");
		}

		public static string[] GetCreateSqlScript()
		{
			DatabaseSchema database = new DatabaseSchema(GetDatabaseDefinitionXml());
			return database.GenerateSqlCreate();
		}

		/// <summary>
		/// Checks whether the database need to be updated or not.
		/// </summary>
		/// <returns>true if the database needs update, otherwise false.</returns>
		public bool NeedToUpdateDatabase()
		{
			string sql =
				@"if exists (
					select 1
					from INFORMATION_SCHEMA.TABLES
					where
						TABLE_TYPE = 'BASE TABLE' and
						TABLE_NAME = 'EasyBaseSystems')
					select 1
				else
					select 0";

			int systemTableExists = (int)Connection.GetScalar(sql);
			if (systemTableExists != 1) {
				throw new DatabaseCorruptException("The database is corrupt. System table is missing.");
			}

			object oDatabaseVersion = Connection.GetScalar("select top 1 DatabaseVersion from EasyBaseSystems");

			if (oDatabaseVersion == null) {
				throw new DatabaseCorruptException("The database is corrupt.");
			}

			int databaseVersion = (int)oDatabaseVersion;

			if (databaseVersion < DatabaseDefinition.DatabaseVersion) {
				return true;
			}
			else if (databaseVersion > DatabaseDefinition.DatabaseVersion) {
				throw new DatabaseException("Erroneous database version. The database's version is " + databaseVersion + ", but the current system's version is " + DatabaseDefinition.DatabaseVersion + ".");
			}

			return false;
		}

		/// <summary>
		/// Updates the database to the latest database version.
		/// </summary>
		public void UpdateDatabase()
		{
            RunSqlUpdateCommands(CollectSqlUpdateDatabase(true));
		}

        private void RunSqlUpdateCommands(SqlUpdateCommand[] updateCommands)
        {
            // Kör alla skript
            foreach (SqlUpdateCommand command in updateCommands) {
                command.DoUpdate(Connection);
            }
        }

        private string[] GetUpdatedViewTables(int startVersion, int endVersion)
        {
            var viewTables = new List<string>();

            for (int version = startVersion + 1; version <= endVersion; version++) {
                var database = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(version));
                viewTables.AddRange(database.GetTableViewDiff());
            }

            return viewTables.ToArray();
        }

        private SqlUpdateCommand[] CollectSqlUpdateDatabase(bool databaseContainsConstraints)
        {
            return CollectSqlUpdateDatabase(databaseContainsConstraints, DatabaseDefinition.DatabaseVersion);
        }

	    private SqlUpdateCommand[] CollectSqlUpdateDatabase(bool databaseContainsConstraints, int toDatabaseVersion)
        {
            var updateCommands = new List<SqlUpdateCommand>();
            int startVersion = (int)Connection.GetScalar("select top 1 DatabaseVersion from EasyBaseSystems");

            if (toDatabaseVersion < startVersion) {
                throw new UpdatingDatabaseException("Du försöker att uppdatera databas med version " + startVersion + " till version " + toDatabaseVersion + ". Det är inte möjligt att nedgradera en databasversion på det sättet.");
            }

            var startDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(startVersion));
            var endDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(toDatabaseVersion));

            if (databaseContainsConstraints) {
                updateCommands.AddRange(startDatabase.GenerateSqlDropConstraints());
                updateCommands.AddRange(endDatabase.GenerateSqlDropConstraints()); // Måste med eftersom uppdateringen kan krascha halvvägs
            }

            updateCommands.AddRange(GenerateSqlUpdateDatabaseToVersionNoConstraints(toDatabaseVersion));

            updateCommands.AddRange(endDatabase.GenerateSqlAddDatabaseConstraints());

            // Uppdatera vyer
            updateCommands.AddRange(endDatabase.GenerateSqlCreateAllViews());

            // Bekräfta den nya versionen i databasen
            updateCommands.Add(new SqlUpdateCommand("update EasyBaseSystems set DatabaseVersion = " + toDatabaseVersion));

            return updateCommands.ToArray();
        }

		/// <summary>
		/// Updates the database to the latest database version. Database constraints must be removed before call to this function, and added again afterwards.
		/// </summary>
		/// <returns>Final database version as an integer.</returns>
		private SqlUpdateCommand[] GenerateSqlUpdateDatabaseToVersionNoConstraints(int toDatabaseVersion)
		{
            int version = (int)Connection.GetScalar("select top 1 DatabaseVersion from EasyBaseSystems");

            var updateCommands = new List<SqlUpdateCommand>();

			while (version < toDatabaseVersion) {
				updateCommands.AddRange(UpdateDatabaseOneVersionNoConstraints(version));
				version++;
			}

            return updateCommands.ToArray();
		}

		/// <summary>
		/// Generates SQL update commands (scripts) that update the database one step to a specified version. 
        /// Database constraints must be removed before call to this function, and added again afterwards.
		/// </summary>
        /// <param name="currentVersion">Current version of the database.</param>
        /// <exception cref="InvalidOperationException">Thrown if the database version too high.</exception>
		private SqlUpdateCommand[] UpdateDatabaseOneVersionNoConstraints(int currentVersion)
		{
            int toVersion = currentVersion + 1;
            if (toVersion > DatabaseDefinition.DatabaseVersion) {
                throw new InvalidOperationException("Database cannot be updated to a version greater than " + DatabaseDefinition.DatabaseVersion + ".");
            }

			// Hämta aktuellt databasschema
            DatabaseSchema fromDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(toVersion - 1));
            DatabaseSchema toDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(toVersion));

            List<SqlUpdateCommand> updateCommands = new List<SqlUpdateCommand>();

            // Add information about the update to the database
            updateCommands.Add(new SqlUpdateCommand("update EasyBaseSystems set UpdatingToVersion = " + toVersion));

            // Add update commands for new tables and columns
            updateCommands.Add(toDatabase.GenerateSqlCreateDiff());

            // Add developer update scripts/functions for the current database update
            SqlUpdateCommand databaseConversion = DatabaseConversions.ConvertDatabase(toVersion);
            if (databaseConversion != null) {
                updateCommands.Add(databaseConversion);
            }

            // Add update commands that deletes removed tables and columns
            updateCommands.AddRange(toDatabase.GenerateSqlDeleteDiff());

            return updateCommands.ToArray();
		}

        public void CreateBackup(string fullPath)
        {
            CreateBackup2(fullPath);
        }

		public void CreateBackup1(string fullPath)
		{
            // Current version of backup system
            int backupVersion = 1;

            string sql = "select top 1 [DatabaseVersion] from [EasyBaseSystems]";
            int databaseVersion = Convert.ToInt32(Connection.GetScalar(sql));

            if (databaseVersion != DatabaseDefinition.DatabaseVersion) {
                throw new DatabaseVersionException("Wrong database version. The database's version is " + databaseVersion + ", but the current version of the application is " + DatabaseDefinition.DatabaseVersion + ". Backup is not possible.");
            }

            DatabaseSchema savedDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(DatabaseDefinition.DatabaseVersion));

            StreamWriter streamWriter = new StreamWriter(fullPath);
            streamWriter.Write("[" + backupVersion + ";" + DatabaseDefinition.DatabaseVersion + "]");

            foreach (TableSchema table in savedDatabase.Tables) {
                StringBuilder commaSeparatedFields = new StringBuilder();
				string comma = "";
				foreach (FieldSchema field in table.DatabaseFields) {
					commaSeparatedFields.Append(comma + "[" + field.FieldName + "]");
					comma = ", ";
				}

				sql = "select " + commaSeparatedFields + " from [" + table.TableName + "]";
				DataTable dataTable = new DataTable();

				Connection.GetTable(dataTable, sql);

				streamWriter.Write("[" + table.TableName + ";" + dataTable.Rows.Count + "]");

				if (dataTable.Rows.Count > 0) {
					foreach (FieldSchema field in table.DatabaseFields) {
						streamWriter.Write("[" + field.FieldName + "]");

						foreach (DataRow dataRow in dataTable.Rows) {
							string data = field.SerializeData(dataRow[field.FieldName]);
							streamWriter.Write(data);
						}
					}
				}
			}

			streamWriter.Flush();
			streamWriter.Close();
		}

        public void CreateBackup2(string fullPath)
        {
            // Current version of backup system
            int backupVersion = 2;

            string sql = "select top 1 [DatabaseVersion] from [EasyBaseSystems]";
            int databaseVersion = Convert.ToInt32(Connection.GetScalar(sql));

            if (databaseVersion != DatabaseDefinition.DatabaseVersion) {
                throw new DatabaseVersionException("Wrong database version. The database's version is " + databaseVersion + ", but the current version of the application is " + DatabaseDefinition.DatabaseVersion + ". Backup is not possible.");
            }

            DatabaseSchema savedDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(DatabaseDefinition.DatabaseVersion));

            long totalRowsInDB = 0;

            sql = "select (";
            string plus = "";
            foreach (TableSchema table in savedDatabase.Tables) {
                sql = sql + plus + "(select count(*) from [" + table.TableName + "])";
                plus = "+";
            }

            sql += ")";

            totalRowsInDB = Convert.ToInt64(Connection.GetScalar(sql));

            StreamWriter streamWriter = new StreamWriter(fullPath);
            streamWriter.Write("[" + backupVersion + ";" + CurrentApplication.Name + ";" + DatabaseDefinition.DatabaseVersion + ";" + totalRowsInDB + "]");

            foreach (TableSchema table in savedDatabase.Tables) {
                StringBuilder commaSeparatedFields = new StringBuilder();
                string comma = "";
                foreach (FieldSchema field in table.DatabaseFields) {
                    commaSeparatedFields.Append(comma + "[" + field.FieldName + "]");
                    comma = ", ";
                }

                sql = "select " + commaSeparatedFields + " from [" + table.TableName + "]";
                DataTable dataTable = new DataTable();

                Connection.GetTable(dataTable, sql);

                streamWriter.Write("[" + table.TableName + ";" + dataTable.Rows.Count + "]");

                if (dataTable.Rows.Count > 0) {
                    foreach (FieldSchema field in table.DatabaseFields) {
                        streamWriter.Write("[" + field.FieldName + "]");

                        foreach (DataRow dataRow in dataTable.Rows) {
                            string data = field.SerializeData(dataRow[field.FieldName]);
                            streamWriter.Write(data);
                        }
                    }
                }
            }

            streamWriter.Flush();
            streamWriter.Close();
        }

        public void RestoreBackup(string fullPath, int toDatabaseVersion)
		{
			var reader = new StreamReader(fullPath);
			var header = new StringBuilder();

            int backupVersion = 0;
            int databaseVersion = 0;
            string databaseApplicationName = "";
            long totalRowsInDB = 0;

            try {
                // Read header
			    char token = ' ';
			    while (token != ']') {
				    token = (char)reader.Read();
				    if (token != '[' && token != ']') {
					    header.Append(token);
				    }
			    }

                string[] headerArray = header.ToString().Split(";".ToCharArray());
                backupVersion = int.Parse(headerArray[0]);
                databaseVersion = 0;
                databaseApplicationName = "";

                if (backupVersion == 1) {
                    databaseVersion = int.Parse(headerArray[1]);
                }
                else {
                    databaseApplicationName = headerArray[1];
                    databaseVersion = int.Parse(headerArray[2]);
                    totalRowsInDB = long.Parse(headerArray[3]);
                }
            }
            catch (Exception ex) {
                throw new BackupException("Backupfilens huvud har ett felaktigt format.", ex);
            }

            if (backupVersion >= 2) {
                if (databaseApplicationName != CurrentApplication.Name) {
                    throw new BackupException("Den aktuella backupen tillhör inte det här programmet. Backupen är en backup för programmet \"" + databaseApplicationName + "\".");
                }
            }

			// Create database
            var database = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(databaseVersion));
			CreateDatabaseNoConstraints(databaseVersion);

            // Call function that reads all table contents from the backup file.
            switch (backupVersion) {
                case 1:
                    RestoreBackup1(reader, database, backupVersion);
                    break;
                case 2:
                    RestoreBackup2(reader, database, backupVersion, totalRowsInDB);
                    break;
                default:
                    throw new NotImplementedException("Backup version " + backupVersion + " is not implemented.");
            }

			// Efter inläsning, uppdatera databasen till senaste versionen
            RunSqlUpdateCommands(CollectSqlUpdateDatabase(false, toDatabaseVersion));
		}

        public void RestoreBackup1(StreamReader reader, DatabaseSchema database, int backupVersion)
        {
            char token = ' ';
            char lastToken = ' ';
            var tableHeader = new StringBuilder();
            var fieldName = new StringBuilder();
            var sBackupVersion = new StringBuilder();
            var sDatabaseVersion = new StringBuilder();

            TableSchema currentTable = null;
            FieldSchema currentField = null;

            bool readingTableHeader = false;
            bool readingFieldName = false;
            string tableName = "";
            int rowCount = 0;
            var dataTable = new DataTable();
            bool readingFieldData = false;
            var fieldColumn = new StringBuilder();
            bool expectingColumnName = false;
            char lastLastToken = ' ';
            int fieldsRead = 0;
            int tableCount = 0;

            while (true) {
                if (!reader.EndOfStream) {
                    token = (char)reader.Read();
                }

                if (token == '[' && lastToken != '\\' && !readingTableHeader && !expectingColumnName && !readingFieldData) { // Start of table header
                    readingTableHeader = true;
                    tableHeader = new StringBuilder();
                }
                else if (token == '[' && lastToken != '\\' && !readingTableHeader && expectingColumnName) { // Start of table header
                    fieldName = new StringBuilder();
                    readingFieldName = true;
                    expectingColumnName = false;
                }
                else if (token == '[' && ((lastToken == '¤' && lastLastToken != '\\' && readingFieldData) || (readingFieldData && currentField.FieldType == "bit")) || reader.EndOfStream) { // End of column data
                    readingFieldData = false;
                    string sData;

                    // Liten fuling
                    if (reader.EndOfStream) {
                        fieldColumn.Append(token);
                    }

                    if (currentField.FieldType != "bit") {
                        sData = fieldColumn.ToString().Substring(0, fieldColumn.ToString().Length - 1);
                    }
                    else {
                        sData = fieldColumn.ToString();
                    }

                    string[] array = currentField.SplitDataString(sData);

                    for (int i = 0; i < rowCount; i++) {
                        dataTable.Rows[i][fieldName.ToString()] = array[i];
                    }

                    fieldsRead++;

                    if (fieldsRead == database.GetTable(tableName).DatabaseFields.Count) {

                        if (currentTable.PrimaryKey.IsIdentity) {
                            Connection.Execute("set identity_insert " + currentTable.TableName + " on;");
                        }

                        // Save the table in the database
                        foreach (DataRow dataRow in dataTable.Rows) {
                            string comma = "";
                            StringBuilder commaSeparatedFieldNames = new StringBuilder();
                            foreach (FieldSchema f in currentTable.DatabaseFields) {
                                commaSeparatedFieldNames.Append(comma + f.FieldName);
                                comma = ", ";
                            }

                            comma = "";
                            StringBuilder commaSeparatedFieldValues = new StringBuilder();
                            foreach (FieldSchema f in currentTable.DatabaseFields) {
                                commaSeparatedFieldValues.Append(comma + f.DeSerializeData((string)dataRow[f.FieldName]));
                                comma = ", ";
                            }

                            string sql = "insert into [" + currentTable.TableName + "] (" + commaSeparatedFieldNames + ") values (" + commaSeparatedFieldValues + ");";
                            Connection.Execute(sql);
                        }

                        if (currentTable.PrimaryKey.IsIdentity) {
                            Connection.Execute("set identity_insert " + currentTable.TableName + " off;");
                        }

                        tableCount += 1;
                        OnProgress(tableCount, database.Tables.Count(), "");

                        // Reset most values
                        fieldsRead = 0;
                        readingTableHeader = true;
                        tableHeader = new StringBuilder();
                    }
                    else {
                        readingFieldName = true;
                        fieldName = new StringBuilder();
                        fieldColumn = new StringBuilder();
                    }

                    if (reader.EndOfStream) {
                        break;
                    }
                }
                else if (token == ']' && lastToken != '\\' && readingTableHeader) { // End of table header
                    readingTableHeader = false;

                    string[] array = tableHeader.ToString().Split(";".ToCharArray());
                    tableName = array[0];
                    currentTable = database.GetTable(tableName);
                    rowCount = int.Parse(array[1]);

                    // Init data table
                    dataTable = new DataTable();

                    foreach (FieldSchema field in currentTable.DatabaseFields) {
                        dataTable.Columns.Add(field.FieldName, typeof(string));
                    }

                    for (int i = 1; i <= rowCount; i++) {
                        DataRow dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    if (rowCount > 0) {
                        fieldColumn = new StringBuilder();
                        expectingColumnName = true;
                    }
                }
                else if (token == ']' && lastToken != '\\' && readingFieldName) { // End of field header
                    readingFieldName = false;
                    readingFieldData = true;
                    currentField = currentTable.GetField(fieldName.ToString());
                }
                else if (readingTableHeader) {
                    tableHeader.Append(token);
                }
                else if (readingFieldName) {
                    fieldName.Append(token);
                }
                else if (readingFieldData) {
                    fieldColumn.Append(token);
                }

                lastLastToken = lastToken;
                lastToken = token;
            }

            reader.Close();

            if (tableCount < database.Tables.Count) {
                tableCount = database.Tables.Count;
                OnProgress(tableCount, database.Tables.Count, "");
            }

            // Uppdatera databasen till senaste versionen
            //RunSqlUpdateCommands(CollectSqlUpdateDatabase(false));
        }

        public void RestoreBackup2(StreamReader reader, DatabaseSchema database, int backupVersion, long totalRowsInDB)
        {
            char token = ' ';
            char lastToken = ' ';
            StringBuilder tableHeader = new StringBuilder();
            StringBuilder fieldName = new StringBuilder();
            StringBuilder sBackupVersion = new StringBuilder();
            StringBuilder sDatabaseVersion = new StringBuilder();

            TableSchema currentTable = null;
            FieldSchema currentField = null;

            bool readingTableHeader = false;
            bool readingFieldName = false;
            string tableName = "";
            int rowCount = 0;
            DataTable dataTable = new DataTable();
            bool readingFieldData = false;
            StringBuilder fieldColumn = new StringBuilder();
            bool expectingColumnName = false;
            char lastLastToken = ' ';
            int fieldsRead = 0;
            int tableCount = 0;

            while (true) {
                if (!reader.EndOfStream) {
                    token = (char)reader.Read();
                }

                if (token == '[' && lastToken != '\\' && !readingTableHeader && !expectingColumnName && !readingFieldData) { // Start of table header
                    readingTableHeader = true;
                    tableHeader = new StringBuilder();
                }
                else if (token == '[' && lastToken != '\\' && !readingTableHeader && expectingColumnName) { // Start of table header
                    fieldName = new StringBuilder();
                    readingFieldName = true;
                    expectingColumnName = false;
                }
                else if (token == '[' && ((lastToken == '¤' && lastLastToken != '\\' && readingFieldData) || (readingFieldData && currentField.FieldType == "bit")) || reader.EndOfStream) { // End of column data
                    readingFieldData = false;
                    string sData;

                    // Liten fuling
                    if (reader.EndOfStream) {
                        fieldColumn.Append(token);
                    }

                    if (currentField.FieldType != "bit") {
                        sData = fieldColumn.ToString().Substring(0, fieldColumn.ToString().Length - 1);
                    }
                    else {
                        sData = fieldColumn.ToString();
                    }

                    string[] array = currentField.SplitDataString(sData);

                    for (int i = 0; i < rowCount; i++) {
                        dataTable.Rows[i][fieldName.ToString()] = array[i];
                    }

                    fieldsRead++;

                    if (fieldsRead == database.GetTable(tableName).DatabaseFields.Count) {

                        if (currentTable.PrimaryKey.IsIdentity) {
                            Connection.Execute("set identity_insert " + currentTable.TableName + " on;");
                        }

                        // Save the table in the database
                        foreach (DataRow dataRow in dataTable.Rows) {
                            string comma = "";
                            StringBuilder commaSeparatedFieldNames = new StringBuilder();
                            foreach (FieldSchema f in currentTable.DatabaseFields) {
                                commaSeparatedFieldNames.Append(comma + f.FieldName);
                                comma = ", ";
                            }

                            comma = "";
                            StringBuilder commaSeparatedFieldValues = new StringBuilder();
                            foreach (FieldSchema f in currentTable.DatabaseFields) {
                                commaSeparatedFieldValues.Append(comma + f.DeSerializeData((string)dataRow[f.FieldName]));
                                comma = ", ";
                            }

                            string sql = "insert into [" + currentTable.TableName + "] (" + commaSeparatedFieldNames + ") values (" + commaSeparatedFieldValues + ");";
                            Connection.Execute(sql);
                        }

                        if (currentTable.PrimaryKey.IsIdentity) {
                            Connection.Execute("set identity_insert " + currentTable.TableName + " off;");
                        }

                        tableCount += 1;
//                      OnProgress(tableCount, database.Tables.Count());

                        // Reset most values
                        fieldsRead = 0;
                        readingTableHeader = true;
                        tableHeader = new StringBuilder();
                    }
                    else {
                        readingFieldName = true;
                        fieldName = new StringBuilder();
                        fieldColumn = new StringBuilder();
                    }

                    if (reader.EndOfStream) {
                        break;
                    }
                }
                else if (token == ']' && lastToken != '\\' && readingTableHeader) { // End of table header
                    readingTableHeader = false;

                    string[] array = tableHeader.ToString().Split(";".ToCharArray());
                    tableName = array[0];
                    currentTable = database.GetTable(tableName);
                    rowCount = int.Parse(array[1]);

                    OnProgress(tableCount, database.Tables.Count(), "Reading table '" + tableName + "'");

                    // Init data table
                    dataTable = new DataTable();

                    foreach (FieldSchema field in currentTable.DatabaseFields) {
                        dataTable.Columns.Add(field.FieldName, typeof(string));
                    }

                    for (int i = 1; i <= rowCount; i++) {
                        DataRow dataRow = dataTable.NewRow();
                        dataTable.Rows.Add(dataRow);
                    }

                    if (rowCount > 0) {
                        fieldColumn = new StringBuilder();
                        expectingColumnName = true;
                    }
                }
                else if (token == ']' && lastToken != '\\' && readingFieldName) { // End of field header
                    readingFieldName = false;
                    readingFieldData = true;
                    currentField = currentTable.GetField(fieldName.ToString());
                }
                else if (readingTableHeader) {
                    tableHeader.Append(token);
                }
                else if (readingFieldName) {
                    fieldName.Append(token);
                }
                else if (readingFieldData) {
                    fieldColumn.Append(token);
                }

                lastLastToken = lastToken;
                lastToken = token;
            }

            reader.Close();

            if (tableCount < database.Tables.Count) {
                tableCount = database.Tables.Count;
                OnProgress(tableCount, database.Tables.Count, "Restore completed!");
            }

            // Uppdatera databasen till senaste versionen
            //RunSqlUpdateCommands(CollectSqlUpdateDatabase(false));
        }
    }
}

