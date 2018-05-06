using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
	public class Field
	{
		public Field(string entityName, string fieldName, string databaseType, bool allowNull, int[] uniqueKeyGroup)
		{
			EntityName = entityName;
			FieldName = fieldName;
			DatabaseType = databaseType;
			AllowNull = allowNull;
			UniqueKeyGroups = uniqueKeyGroup;
            GlobalEnumName = "";
		}

		public string EntityName
		{
			get;
			private set;
		}

		public string FieldName
		{
			get;
			private set;
		}

		public string VarName
		{
			get { return EasyBaseSystem.ToCamelCase(FieldName); }
		}

		public string FieldType
		{
			get
			{
				if (GlobalEnumName != "") {
					return GlobalEnumName;
				}
				else if (HasEnumValues2) {
					return EntityName + FieldName;
				}
				else {
					return EasyBaseSystem.ToFieldType(DatabaseType, AllowNull);
				}
			}
		}

		public string DatabaseType
		{
			get;
			private set;
		}

		public bool AllowNull
		{
			get;
			private set;
		}

		public int[] UniqueKeyGroups
		{
			get;
			private set;
		}

		public bool HasEnumValues2
		{
			get { return EnumValues != null || GlobalEnumName != ""; }
		}

        public string GlobalEnumName
        {
            get;
            set;
        }

		private string[] _enumValues;
		public string[] EnumValues
		{
			get { return _enumValues; }
			set
			{
				if (FieldType != "int") {
					throw new ApplicationException("Only fields of type int can have enums.");
				}
				_enumValues = value;
			}
		}

		protected virtual string GenerateDataLayerClassPropertyAttribute()
		{
			string sAllowNull = "";
			string sUniqueKeyGroups = "";

			if (AllowNull != Entity.DefaultAllowNull) {
				sAllowNull = ", " + (AllowNull ? "true" : "false");
			}

			if (UniqueKeyGroups.Length > 0) {
				sUniqueKeyGroups = ", UniqueKeyGroupsString = \"";

				string comma = "";
				foreach (int uniqueKeyGroup in UniqueKeyGroups) {
					sUniqueKeyGroups += comma + uniqueKeyGroup.ToString();
					comma = ",";
				}

				sUniqueKeyGroups += "\"";
			}

			return "        [DatabaseField(\"" + FieldName + "\", \"" + DatabaseType + "\"" + sAllowNull + sUniqueKeyGroups + ")]";
		}

		public virtual string GenerateDataClassProperty()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine(GenerateDataLayerClassPropertyAttribute());
			code.AppendLine("        public " + FieldType + " " + FieldName);
			code.AppendLine("        {");
			code.AppendLine("            get;");
			code.AppendLine("            set;");
			code.AppendLine("        }");

			return code.ToString();
		}

		public virtual string GenerateBusinessClassProperty()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("        public " + FieldType + " " + FieldName);
			code.AppendLine("        {");
			code.AppendLine("            get;");
			code.AppendLine("            set;");
			code.AppendLine("        }");

			return code.ToString();
		}

		public virtual string GenerateDataRowProperty()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("        public " + FieldType + " " + FieldName);
			code.AppendLine("        {");

			if (AllowNull) {
				code.AppendLine("            get { return this[\"" + FieldName + "\"] is DBNull ? null : (" + FieldType + ")this[\"" + FieldName + "\"]; }");
			}
			else {
				code.AppendLine("            get { return (" + FieldType + ")this[\"" + FieldName + "\"]; }");
			}
			code.AppendLine("        }");

			return code.ToString();
		}

		public virtual string GenerateEnum()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("    public enum " + EntityName + FieldName);
			code.Append("    {");

			int no = 1;
			string comma = "";
			foreach (string enumValue in EnumValues) {
				code.AppendLine(comma);
				code.Append("        " + enumValue + " = " + no);
				comma = ",";
				no++;
			}

			code.AppendLine();
			code.AppendLine("    }");

			return code.ToString();
		}
	}
}
