using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
	public class ViewField : Field
	{
		public ViewField(string entityName, string fieldName, string databaseType, bool allowNull)
			: base(entityName, fieldName, databaseType, allowNull, null)
		{
		}

		protected override string GenerateDataLayerClassPropertyAttribute()
		{
			string sAllowNull = "";

			if (AllowNull != Entity.DefaultAllowNull) {
				sAllowNull = ", " + (AllowNull ? "true" : "false");
			}

			return "        [ViewField(\"" + FieldName + "\", \"" + DatabaseType + "\"" + sAllowNull + ")]";
		}

		public override string GenerateDataClassProperty()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine(GenerateDataLayerClassPropertyAttribute());
			code.AppendLine("        public " + FieldType + " " + FieldName);
			code.AppendLine("        {");
			code.AppendLine("            get;");
			code.AppendLine("            private set;");
			code.AppendLine("        }");

			return code.ToString();
		}

		public override string GenerateBusinessClassProperty()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("        public " + FieldType + " " + FieldName);
			code.AppendLine("        {");
			code.AppendLine("            get;");
			code.AppendLine("            private set;");
			code.AppendLine("        }");

			return code.ToString();
		}
	}
}
