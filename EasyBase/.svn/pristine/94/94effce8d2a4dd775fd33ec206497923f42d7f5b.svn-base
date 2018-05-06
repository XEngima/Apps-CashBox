using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer
{
	public class ForeignKeyFieldSchema : FieldSchema
	{
		public ForeignKeyFieldSchema(string tableName, string fieldName, string fieldType, string targetTableName, string targetFieldName, bool allowNull)
			:base(tableName, fieldName, fieldType, allowNull)
		{
			TargetTableName = targetTableName;
			TargetFieldName = targetFieldName;
		}

		public override bool Equals(object obj)
		{
			if (obj == null) {
				return false;
			}

			ForeignKeyFieldSchema field2 = obj as ForeignKeyFieldSchema;

			if ((object)field2 == null) {
				return false;
			}

			// Kolla igenom alla egenskaper och jämför dem
			if (TargetTableName != field2.TargetTableName) {
				return false;
			}
			if (TargetFieldName != field2.TargetFieldName) {
				return false;
			}

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;

			foreach (char c in TargetTableName) {
				hashCode += (int)c;
			}

			foreach (char c in TargetFieldName) {
				hashCode += (int)c;
			}

			return base.GetHashCode() + hashCode;
		}

		public static bool operator ==(ForeignKeyFieldSchema field1, ForeignKeyFieldSchema field2)
		{
            if ((object)field1 == null && (object)field2 == null) {
                return false;
            }
            else if ((object)field1 == null || (object)field2 == null) {
                return false;
            }

			return field1.Equals(field2);
		}

		public static bool operator !=(ForeignKeyFieldSchema field1, ForeignKeyFieldSchema field2)
		{
			return !(field1 == field2);
		}

		public string TargetTableName
		{
			get;
			private set;
		}

		public string TargetFieldName
		{
			get;
			private set;
		}

		public string SqlName
		{
			get { return "FK_" + TableName + "_" + FieldName + "_" + TargetTableName + "_" + TargetFieldName; }
		}

		public string[] GenerateSqlCreateConstraint()
		{
			List<string> sqls = new List<string>();
			StringBuilder sql = new StringBuilder();

			sql.AppendLine("if not exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS");
			sql.AppendLine("  where");
			sql.AppendLine("    CONSTRAINT_TYPE = 'FOREIGN KEY' and");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "' and");
			sql.AppendLine("    CONSTRAINT_NAME = '" + SqlName + "')");

			sql.AppendLine("alter table [" + TableName + "]  with check add constraint [" + SqlName + "] foreign key([" + FieldName + "])");
			sql.AppendLine("references [" + TargetTableName + "] ([" + TargetFieldName + "]);");
			sqls.Add(sql.ToString());

			sql = new StringBuilder();
			sql.AppendLine("alter table [" + TableName + "] check constraint [" + SqlName + "];");
			sqls.Add(sql.ToString());

			return sqls.ToArray();
		}

		public string[] GenerateSqlDropConstraint()
		{
			List<string> sqls = new List<string>();
			StringBuilder sql = new StringBuilder();

			sql.AppendLine("if exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS");
			sql.AppendLine("  where");
			sql.AppendLine("    CONSTRAINT_TYPE = 'FOREIGN KEY' and");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "' and");
			sql.AppendLine("    CONSTRAINT_NAME = '" + SqlName + "')");
			sql.AppendLine("  alter table [" + TableName + "]");
			sql.AppendLine("  drop constraint [" + SqlName + "]");

			sqls.Add(sql.ToString());
			return sqls.ToArray();
		}
	}
}
