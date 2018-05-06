using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
	public class PrimaryKeyField : Field
	{
		public PrimaryKeyField(string entityName, string fieldName, string databaseType, bool allowNull, bool isIdentity)
			: base(entityName, fieldName, databaseType, allowNull, null)
		{
			IsIdentity = isIdentity;
		}

		public bool IsIdentity
		{
			get;
			private set;
		}

		protected override string GenerateDataLayerClassPropertyAttribute()
		{
			string sAllowNull = "";
			string sIsIdentity = "";

			if (AllowNull != Entity.DefaultAllowNull) {
				sAllowNull = ", " + (AllowNull ? "true" : "false");
			}

			if (IsIdentity != Entity.DefaultIsIdentity) {
				sIsIdentity = ", IsIdentity = " + (IsIdentity ? "true" : "false");
			}

			return "        [PrimaryKeyField(\"" + FieldName + "\", \"" + DatabaseType + "\"" + sAllowNull + sIsIdentity + ")]";
		}

		public override string GenerateDataClassProperty()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine(GenerateDataLayerClassPropertyAttribute());
			code.AppendLine("        public " + FieldType + " " + FieldName);
			code.AppendLine("        {");
			code.AppendLine("            get;");
			code.AppendLine("            internal set;");
			code.AppendLine("        }");

			return code.ToString();
		}

		public override string GenerateBusinessClassProperty()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("        public " + FieldType + " " + FieldName);
			code.AppendLine("        {");
			code.AppendLine("            get;");
			code.AppendLine("            internal set;");
			code.AppendLine("        }");

			return code.ToString();
		}
	}
}
