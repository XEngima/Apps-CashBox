using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer
{
	public class UniqueKey
	{
		public UniqueKey(string tableName, int uniqueKeyGroup)
		{
			TableName = tableName;
			Fields = new List<FieldSchema>();
			UniqueKeyGroup = uniqueKeyGroup;
		}

		public UniqueKey(string tableName, int uniqueKeyGroup, List<FieldSchema> fields)
		{
			TableName = tableName;
			Fields = fields;
			UniqueKeyGroup = uniqueKeyGroup;
		}

		public override bool Equals(object obj)
		{
			UniqueKey uniqueKey2 = obj as UniqueKey;

			if (uniqueKey2 == null) {
				return false;
			}

			return this == uniqueKey2;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;

			foreach (char c in TableName) {
				hashCode += (int)c;
			}

			hashCode += UniqueKeyGroup;

			foreach (FieldSchema field in Fields) {
				hashCode += field.GetHashCode();
			}

			return hashCode;
		}

		public static bool operator ==(UniqueKey uniqueKey1, UniqueKey uniqueKey2)
		{
            if ((object)uniqueKey1 == null && (object)uniqueKey2 == null) {
                return true;
            }
            else if ((object)uniqueKey1 == null || (object)uniqueKey2 == null) {
                return false;
            }

			if (uniqueKey1.Fields.Count != uniqueKey2.Fields.Count) {
				return false;
			}

			// Kolla igenom alla egenskaper och jämför dem
			if (uniqueKey1.TableName != uniqueKey2.TableName) {
				return false;
			}
			if (uniqueKey1.UniqueKeyGroup != uniqueKey2.UniqueKeyGroup) {
				return false;
			}

			foreach (FieldSchema field1 in uniqueKey1.Fields) {
				FieldSchema field2 = uniqueKey2.GetField(field1.FieldName);

				if (field1 != field2) {
					return false;
				}
			}

			return true;
		}

		public static bool operator !=(UniqueKey uniqueKey1, UniqueKey uniqueKey2)
		{
			return !(uniqueKey1 == uniqueKey2);
		}

		public FieldSchema GetField(string fieldName)
		{
			foreach (FieldSchema field in Fields) {
				if (field.FieldName == fieldName) {
					return field;
				}
			}

			return null;
		}

		public string TableName
		{
			get;
			private set;
		}

		public int UniqueKeyGroup
		{
			get;
			private set;
		}

		public List<FieldSchema> Fields
		{
			get;
			private set;
		}

		public string SqlName
		{
			get
			{
				StringBuilder uniqueKeyName = new StringBuilder();
				uniqueKeyName.Append("IX_" + TableName);

				foreach (FieldSchema field in Fields) {
					uniqueKeyName.Append("_" + field.FieldName);
				}

				return uniqueKeyName.ToString();
			}
		}

		public string GenerateSqlCreateConstraint()
		{
			StringBuilder sql = new StringBuilder();

			sql.AppendLine("if not exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE");
			sql.AppendLine("  where");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "' and ");
			sql.AppendLine("    CONSTRAINT_NAME = '" + SqlName + "')");

			StringBuilder fieldList = new StringBuilder();
			string comma = "";
			foreach (FieldSchema field in Fields) {
				fieldList.Append(comma + "[" + field.FieldName + "]" + " asc");
				comma = ", ";
			}

			sql.AppendLine("  alter table [" + TableName + "]  with check add constraint [" + SqlName + "] unique (" + fieldList + ");");

			return sql.ToString();
		}

		public string GenerateSqlDropConstraint()
		{
			StringBuilder sql = new StringBuilder();

			sql.AppendLine("if exists (");
			sql.AppendLine("  select 1");
			sql.AppendLine("  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS");
			sql.AppendLine("  where");
			sql.AppendLine("    CONSTRAINT_TYPE = 'UNIQUE' and");
			sql.AppendLine("    TABLE_NAME = '" + TableName + "' and");
			sql.AppendLine("    CONSTRAINT_NAME = '" + SqlName + "')");
			sql.AppendLine("  alter table [" + TableName + "]");
			sql.AppendLine("  drop constraint [" + SqlName + "]");

			//sql.AppendLine("alter table [" + TableName + "]  drop constraint [" + SqlName + "];");

			return sql.ToString();
		}
	}
}
