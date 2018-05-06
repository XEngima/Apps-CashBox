using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBase.Classes;

namespace EasyBase.DataLayer
{
	public class TableSchema
	{
		public TableSchema(string tableName, bool isView, FieldSchema[] fieldSchemas, UniqueKey[] uniqueKeys)
		{
			TableName = tableName;
			IsView = isView;
			
			Fields = new List<FieldSchema>();
			ForeignKeys = new List<ForeignKeyFieldSchema>();

			bool primaryKeyAdded = false;

			foreach (FieldSchema fieldSchema in fieldSchemas) {
				if (fieldSchema is PrimaryKeyFieldSchema) {
					if (primaryKeyAdded) {
						throw new DatabaseSchemaException("Table '" + tableName + "' has two primary key fields. Only one is allowed.");
					}

					primaryKeyAdded = true;
					PrimaryKey = (PrimaryKeyFieldSchema)fieldSchema;
				}
				else if (fieldSchema is ForeignKeyFieldSchema) {
					ForeignKeys.Add((ForeignKeyFieldSchema)fieldSchema);
				}
				else {
					Fields.Add(fieldSchema);
				}
			}

			UniqueKeys = new List<UniqueKey>();

			foreach (UniqueKey uniqueKey in uniqueKeys) {
				UniqueKeys.Add(uniqueKey);
			}
		}

		public override bool Equals(object obj)
		{
			TableSchema table2 = obj as TableSchema;

			if (table2 == null) {
				return false;
			}

			return this == table2;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			hashCode = Fields.Count * 10000;

			foreach (char c in TableName) {
				hashCode += (int)c;
			}

			foreach (FieldSchema field in AllFields) {
				hashCode += field.GetHashCode();
			}

			return hashCode;
		}

		public static bool operator ==(TableSchema table1, TableSchema table2)
		{
            if ((object)table1 == null && (object)table2 == null) {
                return true;
            }
            else if ((object)table1 == null || (object)table2 == null) {
                return false;
            }

			// Om tabellerna har olika antal fält så är det olika
			if (table1.AllFields.Count != table2.AllFields.Count) {
				return false;
			}

			// Kolla igenom alla fält och jämför dem
			foreach (FieldSchema field1 in table1.AllFields) {
				FieldSchema field2 = table2.GetField(field1.FieldName);

				// Om fältet inte finns i den andra tabellen
				if (field2 == null) {
					return false;
				}

				// Om fälten är olika så returnera false
				if (field1 != field2) {
					return false;
				}
			}

			// Om tabellerna har olika antal unika nycklar så är de olika
			if (table1.UniqueKeys.Count != table2.UniqueKeys.Count) {
				return false;
			}

			// Kolla igenom alla unika nycklar och jämför dem
			foreach (UniqueKey uniqueKey1 in table1.UniqueKeys) {
				UniqueKey uniqueKey2 = table2.GetUniqueKey(uniqueKey1.UniqueKeyGroup);

				// Om den unika nyckeln inte finns i den andra tabellen
				if (uniqueKey2 == null) {
					return false;
				}

				// Om fälten är olika så returnera false
				if (uniqueKey1 != uniqueKey2) {
					return false;
				}
			}

			return true;
		}

		public static bool operator !=(TableSchema table1, TableSchema table2)
		{
			return !(table1 == table2);
		}

		public FieldSchema GetField(string fieldName)
		{
			foreach (FieldSchema field in AllFields) {
				if (field.FieldName == fieldName) {
					return field;
				}
			}

			return null;
		}

		public UniqueKey GetUniqueKey(int uniqueKeyGroup)
		{
			foreach (UniqueKey uniqueKey in UniqueKeys) {
				if (uniqueKey.UniqueKeyGroup == uniqueKeyGroup) {
					return uniqueKey;
				}
			}

			return null;
		}

		public bool IsView
		{
			get;
			private set;
		}

		public string TableName
		{
			get;
			private set;
		}

		public List<FieldSchema> Fields
		{
			get;
			private set;
		}

		public PrimaryKeyFieldSchema PrimaryKey
		{
			get;
			private set;
		}

		public List<ForeignKeyFieldSchema> ForeignKeys
		{
			get;
			private set;
		}

		public List<FieldSchema> DatabaseFields
		{
			get
			{
				List<FieldSchema> allFields = new List<FieldSchema>();

				if (PrimaryKey != null) {
					allFields.Add(PrimaryKey);
				}

				if (ForeignKeys.Count > 0) {
					allFields.AddRange(ForeignKeys);
				}

				foreach (FieldSchema field in Fields) {
					if (!field.IsViewField) {
						allFields.Add(field);
					}
				}

				return allFields;
			}
		}

		public List<FieldSchema> AllFields
		{
			get {
				List<FieldSchema> allFields = new List<FieldSchema>();

				if (PrimaryKey != null) {
					allFields.Add(PrimaryKey);
				}

				if (ForeignKeys.Count > 0) {
					allFields.AddRange(ForeignKeys);
				}

				if (Fields.Count > 0) {
					allFields.AddRange(Fields);
				}

				return allFields;
			}
		}

		public List<UniqueKey> UniqueKeys
		{
			get;
			private set;
		}

		public string GenerateSqlCreateTable()
		{
			StringBuilder sql = new StringBuilder();

			// Table

			sql.AppendLine("if not exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.TABLES");
			sql.AppendLine("  where");
			sql.AppendLine("    TABLE_TYPE = 'BASE TABLE' and");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "')");

			sql.AppendLine("create table [" + TableName + "] (");

			if (PrimaryKey != null) {
				string sNullable = PrimaryKey.AllowNull ? "null" : "not null";
				string sIdentity = PrimaryKey.IsIdentity ? " identity(1,1) " : " ";
				sql.AppendLine("  [" + PrimaryKey.FieldName + "] " + PrimaryKey.FieldType + sIdentity + sNullable + ",");
			}

			if (ForeignKeys.Count > 0) {
				foreach (var foreignKey in ForeignKeys) {
					string sNullable = foreignKey.AllowNull ? "null" : "not null";
					sql.AppendLine("  [" + foreignKey.FieldName + "] " + foreignKey.FieldType + " " + sNullable + ",");
				}
			}

			foreach (var field in Fields) {
				if (!(field is ViewFieldSchema)) {
					string sNullable = field.AllowNull ? "null" : "not null";
					sql.AppendLine("  [" + field.FieldName + "] " + field.FieldType + " " + sNullable + ",");
				}
			}

			//string comma = "";
			if (PrimaryKey != null) {
				sql.AppendLine("  constraint [PK_" + TableName + "] primary key clustered");
				sql.AppendLine("  (");
				sql.AppendLine("    [" + PrimaryKey.FieldName + "] ASC");
				sql.Append("  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]");

				//comma = ",";
			}

			// Unique key constraints

			/*
			foreach (UniqueKey uniqueKey in UniqueKeys) {
				if (comma != "") {
					sql.AppendLine(comma);
				}
				else {
					sql.AppendLine();
				}

				StringBuilder uniqueKeyName = new StringBuilder();
				uniqueKeyName.Append("[IX_" + TableName);

				foreach (FieldSchema field in uniqueKey.Fields) {
					uniqueKeyName.Append("_" + field.FieldName);
				}

				sql.AppendLine("  constraint " + uniqueKeyName + "] unique nonclustered");

				sql.AppendLine("  (");

				string innerComma = "";
				foreach (FieldSchema field in uniqueKey.Fields) {
					if (innerComma != "") {
						sql.AppendLine(innerComma);
					}

					sql.Append("    [" + field.FieldName + "] ASC");
					innerComma = ", ";
				}

				if (innerComma != "") {
					sql.AppendLine();
				}

				sql.AppendLine("  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]");

				comma = ",";
			}*/

			sql.AppendLine(") on [primary];");

			// Separate unique key constraints

			foreach (UniqueKey uniqueKey in UniqueKeys) {
				sql.AppendLine(uniqueKey.GenerateSqlCreateConstraint());
			}

			return sql.ToString();
		}

		public string[] GenerateSqlCreateTableNoKeys()
		{
			List<string> sqls = new List<string>();
			StringBuilder sql = new StringBuilder();

			// Table

			sql.AppendLine("if not exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.TABLES");
			sql.AppendLine("  where");
			sql.AppendLine("    TABLE_TYPE = 'BASE TABLE' and");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "')");

			sql.AppendLine("create table [" + TableName + "] (");

			if (PrimaryKey != null) {
				string sNullable = PrimaryKey.AllowNull ? "null" : "not null";
				string sIdentity = PrimaryKey.IsIdentity ? " identity(1,1) " : " ";
				sql.AppendLine("  [" + PrimaryKey.FieldName + "] " + PrimaryKey.FieldType + sIdentity + sNullable + ",");
			}

			if (ForeignKeys.Count > 0) {
				foreach (var foreignKey in ForeignKeys) {
					string sNullable = foreignKey.AllowNull ? "null" : "not null";
					sql.AppendLine("  [" + foreignKey.FieldName + "] " + foreignKey.FieldType + " " + sNullable + ",");
				}
			}

			foreach (var field in Fields) {
				if (!(field is ViewFieldSchema)) {
					string sNullable = field.AllowNull ? "null" : "not null";
					sql.AppendLine("  [" + field.FieldName + "] " + field.FieldType + " " + sNullable + ",");
				}
			}

			sql.AppendLine(") on [primary];");
			sqls.Add(sql.ToString());

			return sqls.ToArray();
		}

        /// <summary>
        /// Generates SQL to delete table from database.
        /// </summary>
        /// <returns>The drop script as a SqlUpdateCommand.</returns>
        public SqlUpdateCommand GenerateSqlDropTableNoConstraints()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("if exists (");
            sql.AppendLine("  select 1");
            sql.AppendLine("  from INFORMATION_SCHEMA.TABLES");
            sql.AppendLine("  where");
            sql.AppendLine("    TABLE_TYPE = 'BASE TABLE' and");
            sql.AppendLine("    TABLE_NAME = '" + TableName + "')");

            sql.AppendLine("drop table [" + TableName + "];");

            return new SqlUpdateCommand(sql.ToString());
        }

        public SqlUpdateCommand GenerateSqlCreateView(int databaseVersion, out int createOrderIndex)
        {
            createOrderIndex = 0;
			List<string> sqls = new List<string>();

			if (IsView) {
                string viewSql = DatabaseDefinition.GetViewSql(TableName, databaseVersion, out createOrderIndex);

                if (viewSql != "")
                {
                    // If we update, the view may be created in a version before
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("if exists (select * from sys.views where object_id = object_id(N'[dbo].[" + TableName + "View]'))");
                    sql.Append("drop view [" + TableName + "View]");
                    sqls.Add(sql.ToString());

                    sql = new StringBuilder();
                    sql.AppendLine("create view [" + TableName + "View] as");
                    sql.Append(viewSql);
                    sqls.Add(sql.ToString());
                }
			}

			return new SqlUpdateCommand(sqls.ToArray());
		}

	    /// <summary>
	    /// Generates SQL to create the latest view until the specified database version.
	    /// </summary>
	    /// <param name="databaseVersion">The last database version to care about.</param>
	    /// <returns>A SQL command as a string. An empty string if no view definition could be found.</returns>
	    public string[] GenerateSqlCreateLatestView(int databaseVersion)
	    {
	        int createdInDatabaseVersion;
	        int createOrderIndex;
            return GenerateSqlCreateLatestView(databaseVersion, out createdInDatabaseVersion, out createOrderIndex);
	    }


	    /// <summary>
	    /// Generates SQL to create the latest view until the specified database version.
	    /// </summary>
	    /// <param name="databaseVersion">The last database version to care about.</param>
	    /// <param name="createdInDatabaseVersion">The database verision in which the view is defined.</param>
	    /// <returns>A SQL command as a string. An empty string if no view definition could be found.</returns>
        public string[] GenerateSqlCreateLatestView(int databaseVersion, out int createdInDatabaseVersion, out int createOrderIndex)
        {
            createdInDatabaseVersion = 0;
	        createOrderIndex = 0;

            List<string> sqls = new List<string>();
            string viewSql = "";

	        int version = 0;
            while (version <= databaseVersion)
            {
                version++;
                int createOrderIndexTemp;

                string tmpViewSql = DatabaseDefinition.GetViewSql(TableName, version, out createOrderIndexTemp);
                if (tmpViewSql != "") {
                    viewSql = tmpViewSql;
                    createdInDatabaseVersion = version;
                    createOrderIndex = createOrderIndexTemp;
                }
            }

            if (viewSql == "" && IsView) {
                throw new DatabaseSchemaException("View for table '" + TableName + "' is missing.");
            }

            if (viewSql != "") {
                // If we update, the view may be created in a version before
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("if exists (select * from sys.views where object_id = object_id(N'[dbo].[" + TableName + "View]'))");
                sql.Append("drop view [" + TableName + "View]");
                sqls.Add(sql.ToString());

                sql = new StringBuilder();
                sql.AppendLine("create view [" + TableName + "View] as");
                sql.Append(viewSql);
                sqls.Add(sql.ToString());
            }

            return sqls.ToArray();
        }

		public string[] GenerateSqlCreateFields()
		{
			List<string> sqls = new List<string>();

			foreach (FieldSchema field in AllFields) {
				sqls.AddRange(field.GenerateSqlCreateField());
			}

			return sqls.ToArray();
		}

		public string[] GenerateSqlCreateFieldsNoKeys()
		{
			List<string> sqls = new List<string>();

			foreach (FieldSchema field in DatabaseFields) {
				sqls.AddRange(field.GenerateSqlCreateField());
			}

			return sqls.ToArray();
		}

		public string GenerateSqlCreatePrimayKey()
		{
			string sql = "";

			if (PrimaryKey != null) {
				sql = PrimaryKey.GenerateSqlCreateConstraint();
			}

			return sql;
		}

		public string GenerateSqlDropPrimayKey()
		{
			string sql = "";

			if (PrimaryKey != null) {
				sql = PrimaryKey.GenerateSqlDropConstraint();
			}

			return sql;
		}

		public string[] GenerateSqlCreateForeignKeys()
		{
			List<string> sqls = new List<string>();

			if (ForeignKeys.Count > 0) {
				foreach (var foreignKey in ForeignKeys) {
					sqls.AddRange(foreignKey.GenerateSqlCreateConstraint());
				}
			}

			return sqls.ToArray();
		}

		public string[] GenerateSqlDropForeignKeys()
		{
			List<string> sqls = new List<string>();

			if (ForeignKeys.Count > 0) {
				foreach (var foreignKey in ForeignKeys) {
					sqls.AddRange(foreignKey.GenerateSqlDropConstraint());
				}
			}

			return sqls.ToArray();
		}

        public string GenerateSqlDropView()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("if exists (select * from sys.views where object_id = object_id(N'[dbo].[" + TableName + "View]'))");
            sql.Append("drop view [" + TableName + "View]");

            return sql.ToString();
        }

        public string[] GenerateSqlCreateUniqueKeys()
		{
			List<string> sqls = new List<string>();

			foreach (UniqueKey uniqueKey in UniqueKeys) {
				sqls.Add(uniqueKey.GenerateSqlCreateConstraint());
			}

			return sqls.ToArray();
		}

		public string[] GenerateSqlDropUniqueKeys()
		{
			List<string> sqls = new List<string>();

			foreach (UniqueKey uniqueKey in UniqueKeys) {
				sqls.Add(uniqueKey.GenerateSqlDropConstraint());
			}

			return sqls.ToArray();
		}

        public override string ToString()
        {
            return TableName;
        }
	}
}
