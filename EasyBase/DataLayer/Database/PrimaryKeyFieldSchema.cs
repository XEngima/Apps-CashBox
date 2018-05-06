using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer
{
	public class PrimaryKeyFieldSchema : FieldSchema
	{
		public PrimaryKeyFieldSchema(string tableName, string fieldName, string fieldType, bool allowNull, bool isIdentity)
			: base(tableName, fieldName, fieldType, allowNull)
		{
			IsIdentity = isIdentity;
		}

		public bool IsIdentity
		{
			get;
			private set;
		}

		public override bool Equals(object obj)
		{
			if (obj == null) {
				return false;
			}

			PrimaryKeyFieldSchema field2 = obj as PrimaryKeyFieldSchema;

			if ((object)field2 == null) {
				return false;
			}

			// Kolla igenom alla egenskaper och jämför dem
			if (IsIdentity != field2.IsIdentity) {
				return false;
			}

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			if (IsIdentity) {
				hashCode += 1;
			}
			return base.GetHashCode() + hashCode;
		}

		public static bool operator ==(PrimaryKeyFieldSchema field1, PrimaryKeyFieldSchema field2)
		{
            if ((object)field1 == null && (object)field2 == null) {
                return true;
            }
            else if ((object)field1 == null || (object)field2 == null) {
                return false;
            }

			return field1.Equals(field2);
		}

		public static bool operator !=(PrimaryKeyFieldSchema field1, PrimaryKeyFieldSchema field2)
		{
			return !(field1 == field2);
		}

		public string GenerateSqlCreateConstraint()
		{
			StringBuilder sql = new StringBuilder();

			sql.AppendLine("if not exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS");
			sql.AppendLine("  where");
			sql.AppendLine("    CONSTRAINT_TYPE = 'PRIMARY KEY' and");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "' and");
			sql.AppendLine("    CONSTRAINT_NAME = '" + "PK_" + TableName + "')");
			sql.AppendLine("alter table [" + TableName + "]");
			sql.AppendLine("add constraint [PK_" + TableName + "] primary key clustered");
			sql.AppendLine("  ([" + FieldName + "] asc);");

			return sql.ToString();
		}

		public string GenerateSqlDropConstraint()
		{
			StringBuilder sql = new StringBuilder();

			sql.AppendLine("if exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS");
			sql.AppendLine("  where");
			sql.AppendLine("    CONSTRAINT_TYPE = 'PRIMARY KEY' and");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "' and");
			sql.AppendLine("    CONSTRAINT_NAME = '" + "PK_" + TableName + "')");
			sql.AppendLine("  alter table [" + TableName + "]");
			sql.AppendLine("  drop constraint [" + "PK_" + TableName + "]");

			return sql.ToString();
		}
	}
}
