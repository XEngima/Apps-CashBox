using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyBase.DataLayer
{
	public class FieldSchema
	{
		public FieldSchema(string tableName, string fieldName, string fieldType, bool allowNull)
		{
			TableName = tableName;
			FieldName = fieldName;
			FieldType = fieldType;
			AllowNull = allowNull;
		}

		public override bool Equals(object obj)
		{
			if (obj == null) {
				return false;
			}

			FieldSchema field2 = obj as FieldSchema;

			// Kolla igenom alla egenskaper och jämför dem
			if (TableName != field2.TableName) {
				return false;
			}
			if (FieldName != field2.FieldName) {
				return false;
			}
			if (FieldType != field2.FieldType) {
				return false;
			}
			if (AllowNull != field2.AllowNull) {
				return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;

			foreach (char c in FieldName) {
				hashCode += (int)c;
			}

			foreach (char c in FieldType) {
				hashCode += (int)c;
			}

			if (AllowNull) {
				hashCode += 1;
			}

			return hashCode;
		}

		public static bool operator ==(FieldSchema field1, FieldSchema field2)
		{
            if ((object)field1 == null && (object)field2 == null) {
                return true;
            }
			else if ((object)field1 == null || (object)field2 == null) {
				return false;
			}

			return field1.Equals(field2) && field2.Equals(field1);
		}

		public static bool operator !=(FieldSchema field1, FieldSchema field2)
		{
			return !(field1 == field2);
		}

		public string TableName
		{
			get;
			private set;
		}

		public string FieldName
		{
			get;
			private set;
		}

		public string FieldType
		{
			get;
			private set;
		}

		public bool AllowNull
		{
			get;
			private set;
		}

		public bool IsViewField
		{
			get { return this is ViewFieldSchema; }
		}

        public virtual SqlUpdateCommand GenerateSqlDropDefaultValueConstraint()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("if exists (");
            sql.AppendLine("  select 1");
            sql.AppendLine("  from INFORMATION_SCHEMA.COLUMNS");
            sql.AppendLine("  where");
            sql.AppendLine("    TABLE_NAME = '" + TableName + "' and");
            sql.AppendLine("    COLUMN_NAME = '" + FieldName + "' and");
            sql.AppendLine("    COLUMN_DEFAULT is not null)");
            sql.AppendLine("  begin");
            sql.AppendLine("    alter table [" + TableName + "] drop constraint [DF_" + TableName + "_" + FieldName + "];");
            sql.AppendLine("  end;");

            return new SqlUpdateCommand(sql.ToString());
        }

		public virtual string[] GenerateSqlCreateField()
		{
			List<string> sqls = new List<string>();
			StringBuilder sql = new StringBuilder();

			string sNull = AllowNull ? "null" : "not null";

			sql.AppendLine("if not exists (select 1 from INFORMATION_SCHEMA.COLUMNS");
			sql.AppendLine("  where TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + FieldName + "')");
			sql.AppendLine("  begin");
			sql.AppendLine("    alter table [" + TableName + "] add [" + FieldName + "] " + FieldType + ";");

			sql.AppendLine("  end;");

			sqls.Add(sql.ToString());

			if (!(this is PrimaryKeyFieldSchema)) {
				sql = new StringBuilder();
				sql.AppendLine("if not exists (");
				sql.AppendLine("  select 1");
				sql.AppendLine("  from INFORMATION_SCHEMA.COLUMNS");
				sql.AppendLine("  where");
				sql.AppendLine("    TABLE_NAME = '" + TableName + "' and");
				sql.AppendLine("    COLUMN_NAME = '" + FieldName + "' and");
				sql.AppendLine("    COLUMN_DEFAULT is not null)");
				sql.AppendLine("  begin");
				sql.AppendLine("    alter table [" + TableName + "] add constraint [DF_" + TableName + "_" + FieldName + "] default ((" + DatabaseSchema.GetDefaultValue(FieldType, AllowNull) + ")) for [" + FieldName + "];");
				sql.AppendLine("  end;");
				sqls.Add(sql.ToString());

				if (!AllowNull) {
					sqls.Add("update [" + TableName + "] set [" + FieldName + "] = " + DatabaseSchema.GetDefaultValue(FieldType, AllowNull) + " where [" + FieldName + "] is null;");
				}
			}

			sqls.Add("  alter table [" + TableName + "] alter column [" + FieldName + "] " + FieldType + " " + sNull + ";");

			return sqls.ToArray();
		}

        public virtual SqlUpdateCommand GenerateSqlDropFieldNoKeys()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("if exists (select 1 from INFORMATION_SCHEMA.COLUMNS");
            sql.AppendLine("  where TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + FieldName + "')");
            sql.AppendLine("  begin");
            sql.AppendLine("    alter table [" + TableName + "] drop column [" + FieldName + "];");

            sql.AppendLine("  end;");

            return new SqlUpdateCommand(sql.ToString());
        }

		public string SerializeData(object oData)
		{
			if (FieldType == "bit") {
				if (oData is DBNull) {
					return "n";
				}
				else {
					return ((bool)oData) ? "1" : "0";
				}
			}
			else if (FieldType == "int") {
				if (oData is DBNull) {
					return "¤";
				}
				else {
					return Convert.ToInt32(oData).ToString() + "¤";
				}
			}
			else if (FieldType == "real") {
				if (oData is DBNull) {
					return "¤";
				}
				else {
					return Convert.ToDouble(oData).ToString().Replace(",", ".") + "¤";
				}
			}
			else if (FieldType == "money") {
				if (oData is DBNull) {
					return "¤";
				}
				else {
					return Convert.ToDecimal(oData).ToString("0.####").Replace(",", ".") + "¤";
				}
			}
            else if (Regex.IsMatch(FieldType, @"^decimal\(\d+,\d+\)$")) { // decimal (x,y)
                if (oData is DBNull)
                {
                    return "¤";
                }
                else
                {
                    return Convert.ToDecimal(oData).ToString().Replace(",", ".") + "¤";
                }
            }
            else if (FieldType == "datetime" || FieldType == "smalldatetime")
            {
				if (oData is DBNull) {
					return "¤";
				}
				else {
					return ((DateTime)oData).ToBinary().ToString() + "¤";
				}
			}
			else if (FieldType.Contains("varchar")) {
				if (oData is DBNull) {
					return "¤";
				}
				else {
					return "\"" + oData.ToString().Replace("¤", "\\¤").Replace("[", "\\[") +"\"¤";
				}
			}

			throw new NotImplementedException("Typen " + FieldType + " tas inte om hand i SerializeData.");
		}

		public string DeSerializeData(string value)
		{
			if (FieldType == "bit") {
				if (value == "n") {
					return "null";
				}
				else {
					return value;
				}
			}
            else if (FieldType == "int" || FieldType == "real" || FieldType == "money" || Regex.IsMatch(FieldType, @"^decimal\(\d+,\d+\)$"))
            {
				if (value == "") {
					return "null";
				}
				else {
					return value;
				}
			}
			else if (FieldType == "datetime") {
				if (value == "") {
					return "null";
				}
				else {
					DateTime dateTime = DateTime.FromBinary(long.Parse(value));
					return "'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss:fff") + "'";
				}
			}
			else if (FieldType == "smalldatetime") {
				if (value == "") {
					return "null";
				}
				else {
					DateTime dateTime = DateTime.FromBinary(long.Parse(value));
					return "'" + dateTime.ToString("yyyy-MM-dd HH:mm") + "'";
				}
			}
			else if (FieldType.Contains("varchar")) {
				if (value == "") {
					return "null";
				}
				else {
					string s = value.Substring(1, value.Length - 2);
					s = s.Replace("\\¤", "¤").Replace("\\[", "[").Replace("'", "''").Replace("\r\n", "' + char(13) + char(10) + '").Replace("\n", "' + char(10) + '");
					return "'" + s + "'";
				}
			}

			throw new NotImplementedException("Typen " + FieldType + " tas inte om hand i DeSerializeData.");
		}

		public string[] SplitDataString(string columnData)
		{
			List<string> data = new List<string>();

			if (FieldType == "bit") {
				foreach (char c in columnData) {
					data.Add(c.ToString());
				}
				return data.ToArray();
			}
            else if (FieldType == "int" || FieldType == "real" || FieldType == "money" || FieldType == "datetime" || FieldType == "smalldatetime" || Regex.IsMatch(FieldType, @"^decimal\(\d+,\d+\)$"))
            {
				string[] array = columnData.Split("¤".ToCharArray());
				return array;
			}
			else if (FieldType.Contains("varchar")) {
				string[] array = columnData.Split("¤".ToCharArray());
				return array;
			}

			throw new NotImplementedException("Typen " + FieldType + " tas inte om hand i SplitDataString.");
		}

        public override string ToString()
        {
            return FieldName;
        }
	}
}
