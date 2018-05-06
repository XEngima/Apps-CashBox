using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using EasyBase.Classes;

namespace EasyBase.DataLayer
{
	public class DatabaseSchema
	{
		public DatabaseSchema(string xmlString)
		{
			Tables = new List<TableSchema>();
			XmlString = xmlString;
			ParseXml(xmlString);
		}

		public override bool Equals(object obj)
		{
			DatabaseSchema database2 = obj as DatabaseSchema;

			if (database2 == null) {
				return false;
			}

			return this == database2;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			hashCode = Tables.Count * 1000000;

			foreach (TableSchema table in Tables) {
				hashCode += table.GetHashCode();
			}

			return hashCode;
		}

		public static bool operator ==(DatabaseSchema database1, DatabaseSchema database2)
		{
            if ((object)database1 == null && (object)database2 == null) {
                return true;
            }

			bool isEqual = true;

			// Om databaserna har olika antal tabeller så är det olika
			if (database1.Tables.Count != database2.Tables.Count) {
				return false;
			}

			// Kolla igenom alla databastabeller och jämför dem
			foreach (TableSchema table1 in database1.Tables) {
				TableSchema table2 = database2.GetTable(table1.TableName);

				// Om tabellen inte finns i den andra databasen
				if (table2 == null) {
					return false;
				}

				// Om tabellerna är olika så returnera false
				if (table1 != table2) {
					return false;
				}
			}

			return isEqual;
		}

		public static bool operator !=(DatabaseSchema database1, DatabaseSchema database2)
		{
			return !(database1 == database2);
		}

		/// <summary>
		/// Adds a field to a unique key in a list of unique keys. If the unique key is not in the list, a new unique key will be added to the list.
		/// </summary>
		/// <param name="uniqueKeys">List of unique keys.</param>
		/// <param name="tableName">Name of the current database table.</param>
		/// <param name="uniqueKeyGroup">The group ID of the unique key within its entity.</param>
		/// <param name="field">Field to att do unique key.</param>
		private void AddToUniqueKeys(List<UniqueKey> uniqueKeys, string tableName, int uniqueKeyGroup, FieldSchema field)
		{
			foreach (UniqueKey uniqueKey in uniqueKeys) {
				if (uniqueKey.TableName == tableName && uniqueKey.UniqueKeyGroup == uniqueKeyGroup) {
					uniqueKey.Fields.Add(field);
					return;
				}
			}

			// If we get here, there was no appropriate unique key in collection, so we need to add one
			UniqueKey newUniqueKey = new UniqueKey(tableName, uniqueKeyGroup);
			newUniqueKey.Fields.Add(field);
			uniqueKeys.Add(newUniqueKey);
		}

        /// <summary>
        /// Gets the database version.
        /// </summary>
        public int Version
        {
            get;
            private set;
        }

		private void ParseXml(string xmlString)
		{
			XmlReader reader = XmlReader.Create(new StringReader(xmlString));

            reader.ReadToFollowing("Database");
            if (reader.MoveToAttribute("Version")) {
                int version;
                if (int.TryParse(reader.Value, out version)) {
                    Version = version;
                }
                else {
                    throw new DatabaseSchemaException("Database version tag is unreadable.");
                }
            }
            else {
                throw new DatabaseSchemaException("Database tag is missing its version attribute.");
            }

			while (reader.ReadToFollowing("Table")) {
				string tableName = "";
				bool isView = false;
				List<FieldSchema> fields = new List<FieldSchema>();
				List<UniqueKey> uniqueKeys = new List<UniqueKey>();

				while (reader.MoveToNextAttribute()) {
					string attributeName = reader.Name;

					switch (attributeName) {
						case "Name":
							tableName = reader.Value;
							break;
						case "IsView":
							isView = (reader.Value == "true");
							break;
						default:
							throw new NotImplementedException("'" + attributeName + "' on table '" + tableName + "' is not a valid table attribute.");
					}
				}

				if (tableName == "") {
					throw new DatabaseSchemaException("Table must have a name.");
				}

				while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
					;

				if (reader.NodeType != XmlNodeType.EndElement && !reader.EOF) {
					if (reader.Name == "Fields") {

						while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
							;

						while (reader.NodeType != XmlNodeType.EndElement && !reader.EOF) {
							FieldSchema field;

							if (reader.Name == "PrimaryKeyField") {

								string fieldName = "";
								string fieldType = "";
								bool allowNull = false;
								bool isIdentity = false;

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
										case "IsIdentity":
											isIdentity = Convert.ToBoolean(reader.Value);
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on table '" + tableName + "' is not a valid primary key field attribute.");
									}
								}

								if (fieldName == "") {
									throw new DatabaseSchemaException("Primary key field for table '" + tableName + "' must have a name.");
								}
								if (fieldType == "") {
									throw new DatabaseSchemaException("Primary key field '" + fieldName + "' on table '" + tableName + "' must have a type.");
								}

								field = new PrimaryKeyFieldSchema(tableName, fieldName, fieldType, allowNull, isIdentity);
							}
							else if (reader.Name == "ForeignKeyField") {

								string fieldName = "";
								string fieldType = "";
								string targetTableName = "";
								string targetFieldName = "";
								bool allowNull = false;
								List<int> uniqueKeyGroups = new List<int>();

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "TargetTable":
											targetTableName = reader.Value;
											break;
										case "TargetField":
											targetFieldName = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
										case "UniqueKeyGroup":
											string[] keyGroups = reader.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
											foreach (string sKeyGroup in keyGroups) {
												uniqueKeyGroups.Add(int.Parse(sKeyGroup));
											}
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on table '" + tableName + "' is not a valid foreign key field attribute.");
									}
								}


								if (fieldName == "") {
									throw new DatabaseSchemaException("Foreign key field for table '" + tableName + "' must have a name.");
								}
								if (fieldType == "") {
									throw new DatabaseSchemaException("Foreign key field '" + fieldName + "' on table '" + tableName + "' must have a type.");
								}
								if (targetTableName == "") {
									throw new DatabaseSchemaException("Foreign key field '" + fieldName + "' on table '" + tableName + "' must have a target table.");
								}
								if (targetFieldName == "") {
									throw new DatabaseSchemaException("Foreign key field '" + fieldName + "' on table '" + tableName + "' must have a target field.");
								}

								field = new ForeignKeyFieldSchema(tableName, fieldName, fieldType, targetTableName, targetFieldName, allowNull);

								if (uniqueKeyGroups.Count > 0) {
									foreach (int uniqueKeyGroup in uniqueKeyGroups) {
										AddToUniqueKeys(uniqueKeys, tableName, uniqueKeyGroup, field);
									}
								}
							}
							else if (reader.Name == "Field") {

								string fieldName = "";
								string fieldType = "";
								bool allowNull = false;
								List<int> uniqueKeyGroups = new List<int>();

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
										case "UniqueKeyGroup":
											string[] keyGroups = reader.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
											foreach (string sKeyGroup in keyGroups) {
												uniqueKeyGroups.Add(int.Parse(sKeyGroup));
											}
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on table '" + tableName + "' is not a valid field attribute.");
									}
								}

								if (fieldName == "") {
									throw new DatabaseSchemaException("Field on table '" + tableName + "' must have a name.");
								}
								if (fieldType == "") {
									throw new DatabaseSchemaException("Field '" + fieldName + "' table '" + tableName + "' must have a type.");
								}

								field = new FieldSchema(tableName, fieldName, fieldType, allowNull);

								if (uniqueKeyGroups.Count > 0) {
									foreach (int uniqueKeyGroup in uniqueKeyGroups) {
										AddToUniqueKeys(uniqueKeys, tableName, uniqueKeyGroup, field);
									}
								}
							}
							else if (reader.Name == "ViewField") {

								string fieldName = "";
								string fieldType = "";
								bool allowNull = false;

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on table '" + tableName + "' is not a valid field attribute.");
									}
								}

								if (fieldName == "") {
									throw new DatabaseSchemaException("Field on table '" + tableName + "' must have a name.");
								}
								if (fieldType == "") {
									throw new DatabaseSchemaException("Field '" + fieldName + "' table '" + tableName + "' must have a type.");
								}

								field = new ViewFieldSchema(tableName, fieldName, fieldType, allowNull);
							}
							else {
								throw new DatabaseSchemaException("When initiating table '" + tableName + "', tags '<PrimaryKeyField>, <ForeighKeyField> or <Field>' was expected, but got tag '<" + reader.Name + ">'");
							}

							fields.Add(field);

							while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
								;
						}


					}
					else {
						throw new DatabaseSchemaException("When initiating table '" + tableName + "', tag '<Fields>' was expected, but got tag '<" + reader.Name + ">'");
					}

					Tables.Add(new TableSchema(tableName, isView, fields.ToArray(), uniqueKeys.ToArray()));
				}
			}
		}

		public TableSchema GetTable(string tableName)
		{
			foreach (TableSchema table in Tables) {
				if (table.TableName == tableName) {
					return table;
				}
			}

			return null;
		}

		public string XmlString
		{
			get;
			private set;
		}

        public override string ToString()
        {
            return "Version == " + Version;
        }

		public List<TableSchema> Tables
		{
			get;
			private set;
		}

        public List<TableSchema> ViewTables
        {
            get {
                List<TableSchema> viewTables = new List<TableSchema>();

                foreach (var table in Tables) {
                    if (table.IsView) {
                        viewTables.Add(table);
                    }
                }

                return viewTables;
            }
        }

		public string[] GenerateSqlCreate()
		{
			List<string> sqls = new List<string>();

			sqls.Add("SET ANSI_NULLS ON;");
			sqls.Add("SET QUOTED_IDENTIFIER ON;");
			sqls.Add("SET ANSI_PADDING ON;");

			// Add tables

			foreach (TableSchema table in Tables) {
				sqls.Add(table.GenerateSqlCreateTable());
				sqls.AddRange(table.GenerateSqlCreateFields());
			}

			// Add constraints
			
			foreach (TableSchema table in Tables) {
				foreach (string sql in table.GenerateSqlCreateForeignKeys()) {
					sqls.Add(sql);
				}
			}

            // Add views

            //foreach (TableSchema table in Tables) {
            //    if (table.IsView) {
            //        sqls.AddRange(table.GenerateSqlCreateLatestView(Version));
            //    }
            //}

		    foreach (var updateCommand in GenerateSqlCreateAllViews())
		    {
                sqls.AddRange(updateCommand.SqlCommands);
            }

            return sqls.ToArray();
		}

		public string[] GenerateSqlCreateNoKeys(int Version)
		{
			List<string> sqls = new List<string>();

			sqls.Add("SET ANSI_NULLS ON;");
			sqls.Add("SET QUOTED_IDENTIFIER ON;");
			sqls.Add("SET ANSI_PADDING ON;");

			// Add tables

			foreach (TableSchema table in Tables) {
				sqls.AddRange(table.GenerateSqlCreateTableNoKeys());
				sqls.AddRange(table.GenerateSqlCreateFieldsNoKeys());
			}

			return sqls.ToArray();
		}

        /// <summary>
        /// Generates SQL to create tables and columns that are added since last version.
        /// </summary>
        /// <returns>The generated SQL in an SqlUpdateCommand.</returns>
        public SqlUpdateCommand GenerateSqlCreateDiff()
        {
            List<string> sqls = new List<string>();
            
            DatabaseSchema fromDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(Version - 1));

            // Loopa igenom alla tabeller i nya databasen
            foreach (var table in Tables) {
                var fromTable = fromDatabase.GetTable(table.TableName);

                if (fromTable == null) {
                    // Tabellen finns inte i gamla databasen, så den behöver skapas.

                    sqls.Add(table.GenerateSqlCreateTable());
                }
                else {
                    // Tabellen finns. Kolla igenom tabellen för att se om det har tillkommit några nya kolumner.

                    foreach (var field in table.AllFields) {
                        var fromField = fromTable.GetField(field.FieldName);

                        if (fromField == null) {
                            // Fältet fanns inte i den gamla databasen, så det behöver skapas.

                            sqls.AddRange(field.GenerateSqlCreateField());
                        }
                    }
                }
            }

            return new SqlUpdateCommand(sqls.ToArray());
        }

        /// <summary>
        /// Generates SQL to delete tables and columns that are deleted since last version.
        /// </summary>
        /// <returns>The generated SQL in an SQLUpdateCommand.</returns>
        public SqlUpdateCommand[] GenerateSqlDeleteDiff()
        {
            // Ta bort databaskolumner som har tagits bort
            List<SqlUpdateCommand> updateCommands = new List<SqlUpdateCommand>();

            DatabaseSchema fromDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(Version - 1));

            foreach (var fromTable in fromDatabase.Tables) {
                var toTable = GetTable(fromTable.TableName);

                if (toTable == null) {
                    updateCommands.Add(fromTable.GenerateSqlDropTableNoConstraints());
                }
                else {
                    // Kolla alla kolumner, om någon inte finns i den nya databasen ska den bort.
                    foreach (var fromField in fromTable.AllFields) {
                        FieldSchema field = toTable.GetField(fromField.FieldName);
                        
                        // Om fältet har tagits bort i den aktuella databasen, eller har omvandlats till ett vyfält i den nya, ska den bort från databasen.
                        if (field == null || field.IsViewField) {
                            updateCommands.Add(fromField.GenerateSqlDropDefaultValueConstraint());
                            updateCommands.Add(fromField.GenerateSqlDropFieldNoKeys());
                        }
                    }
                }
            }

            return updateCommands.ToArray();
        }

        /// <summary>
        /// Gets all table views that are changed between last version and current version.
        /// </summary>
        /// <returns>All names of changed table views as a string array.</returns>
        public string[] GetTableViewDiff()
        {
            List<string> viewTables = new List<string>();
            DatabaseSchema fromDatabase = new DatabaseSchema(DatabaseDefinition.GetDatabaseXmlDefinition(Version - 1));

            foreach (var viewTable in ViewTables) {
                var fromTable = fromDatabase.GetTable(viewTable.TableName);

                if (fromTable == null) {
                    viewTables.Add(viewTable.TableName);
                }
                else {
                    foreach (var field in viewTable.AllFields) {
                        if (fromTable.GetField(field.FieldName) == null) {
                            viewTables.Add(viewTable.TableName);
                        }
                    }
                }
            }

            foreach (var viewTable in fromDatabase.ViewTables) {
                var toTable = GetTable(viewTable.TableName);

                if (toTable == null) {
                    viewTables.Add(viewTable.TableName);
                }
                else {
                    foreach (var field in viewTable.AllFields) {
                        if (toTable.GetField(field.FieldName) == null) {
                            viewTables.Add(viewTable.TableName);
                        }
                    }
                }
            }

            return viewTables.ToArray();
        }

		public string[] GenerateSqlCreateKeys()
		{
			List<string> sqls = new List<string>();

			sqls.Add("SET ANSI_NULLS ON;");
			sqls.Add("SET QUOTED_IDENTIFIER ON;");
			sqls.Add("SET ANSI_PADDING ON;");

			// Add foreign keys

			foreach (TableSchema table in Tables) {
				foreach (string sql in table.GenerateSqlCreateForeignKeys()) {
					sqls.Add(sql);
				}
			}

			return sqls.ToArray();
		}

		public string[] GenerateSqlDropAll()
		{
			List<string> sqls = new List<string>();

			// Foreign keys

			foreach (TableSchema table in Tables) {
				foreach (ForeignKeyFieldSchema foreignKey in table.ForeignKeys) {
					StringBuilder sql = new StringBuilder();

					sql.AppendLine("if exists (");
					sql.AppendLine("  select 1");
					sql.AppendLine("  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS");
					sql.AppendLine("  where");
					sql.AppendLine("    CONSTRAINT_TYPE = 'FOREIGN KEY' and");
					sql.AppendLine("    TABLE_NAME = '" + table.TableName + "' and");
					sql.AppendLine("    CONSTRAINT_NAME = '" + foreignKey.SqlName + "')");
					sql.AppendLine("  alter table [" + table.TableName + "]");
					sql.AppendLine("  drop constraint [" + foreignKey.SqlName + "]");

					sqls.Add(sql.ToString());
				}
			}

			// Tables

			foreach (TableSchema table in Tables) {
				StringBuilder sql = new StringBuilder();

				sql.AppendLine("if exists (");
				sql.AppendLine("  select 1");
				sql.AppendLine("  from INFORMATION_SCHEMA.TABLES");
				sql.AppendLine("  where");
				sql.AppendLine("    TABLE_TYPE = 'BASE TABLE' and");
				sql.AppendLine("    TABLE_NAME = '" + table.TableName + "')");
				sql.AppendLine("  drop table [" + table.TableName + "]");

				sqls.Add(sql.ToString());
			}

			return sqls.ToArray();
		}

		public static string GetDefaultValue(string type, bool allowNull)
		{
			if (allowNull) {
				return "null";
			}
			else if (type == "int") {
				return "0";
			}
			else if (type == "bit") {
				return "0";
			}
			else if (type == "datetime") {
				return "'1901-01-01'";
			}
			else if (type == "smalldatetime") {
				return "'1901-01-01'";
			}
			else if (type == "money") {
				return "'0'";
			}
			else if (type.Length >= 7 && type.Substring(0, 7) == "varchar") {
				return "''";
			}
			else if (type.Length >= 8 && type.Substring(0, 8) == "nvarchar") {
				return "''";
			}
            else if (type.Substring(0, 7) == "decimal") {
                return "0";
            }

			throw new NotImplementedException("Default value for type " + type + " is not implemented.");
		}

        /// <summary>
        /// Generates SQL commands to drop all constraints in the database.
        /// </summary>
        /// <param name="version">Database version to generate drop commans for.</param>
        /// <returns>SQL commands as SqlUpdateCommand</returns>
        public SqlUpdateCommand[] GenerateSqlDropConstraints()
        {
            List<SqlUpdateCommand> updateCommands = new List<SqlUpdateCommand>();

            foreach (TableSchema table in Tables) {
                updateCommands.Add(new SqlUpdateCommand(table.GenerateSqlDropUniqueKeys()));
            }

            foreach (TableSchema table in Tables) {
                updateCommands.Add(new SqlUpdateCommand(table.GenerateSqlDropForeignKeys()));
            }

            foreach (TableSchema table in Tables) {
                if (table.PrimaryKey != null) {
                    updateCommands.Add(new SqlUpdateCommand(table.GenerateSqlDropPrimayKey()));
                }
            }

            return updateCommands.ToArray();
        }

	    private struct VersionScriptItem
	    {
	        public int CreatedInDbVersion;
	        public int CreateOrderIndex;
	        public string[] Scripts;
	    }

        public SqlUpdateCommand[] GenerateSqlCreateAllViews()
        {
            //List<DictionaryItem<int, string[]>> updateScripts = new List<DictionaryItem<int, string[]>>();
            List<SqlUpdateCommand> updateCommands = new List<SqlUpdateCommand>();
            List<VersionScriptItem> updateScripts = new List<VersionScriptItem>();

            foreach (var viewTable in ViewTables) {
                int createdInDatabaseVersion;
                int createOrderIndex;

                string[] scripts = viewTable.GenerateSqlCreateLatestView(Version, out createdInDatabaseVersion, out createOrderIndex);

                updateScripts.Add(new VersionScriptItem()
                                  {
                                      CreatedInDbVersion = createdInDatabaseVersion,
                                      CreateOrderIndex = createOrderIndex,
                                      Scripts = scripts
                                  } );
            }

            foreach (var updateScript in updateScripts.OrderBy(v => v.CreatedInDbVersion).ThenBy(v => v.CreateOrderIndex))
            {
                updateCommands.Add(new SqlUpdateCommand(updateScript.Scripts));
            }

            return updateCommands.ToArray();
        }

        /// <summary>
        /// Generates SQL commands to add all database constraints.
        /// </summary>
        public SqlUpdateCommand[] GenerateSqlAddDatabaseConstraints()
        {
            List<SqlUpdateCommand> updateCommands = new List<SqlUpdateCommand>();

            foreach (TableSchema table in Tables) {
                if (table.PrimaryKey != null) {
                    updateCommands.Add(new SqlUpdateCommand(table.GenerateSqlCreatePrimayKey()));
                }
            }

            foreach (TableSchema table in Tables) {
                updateCommands.Add(new SqlUpdateCommand(table.GenerateSqlCreateForeignKeys()));
            }

            foreach (TableSchema table in Tables) {
                updateCommands.Add(new SqlUpdateCommand(table.GenerateSqlCreateUniqueKeys()));
            }

            return updateCommands.ToArray();
        }
    }
}
