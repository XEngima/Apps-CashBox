using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer
{
	public class ViewFieldSchema : FieldSchema
	{
		public ViewFieldSchema(string tableName, string fieldName, string fieldType, bool allowNull)
			: base(tableName, fieldName, fieldType, allowNull)
		{
		}

		public override bool Equals(object obj)
		{
			if (obj == null) {
				return false;
			}

			ViewFieldSchema field2 = obj as ViewFieldSchema;

			if ((object)field2 == null) {
				return false;
			}

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() + 159;
		}

		public static bool operator ==(ViewFieldSchema field1, ViewFieldSchema field2)
		{
            if ((object)field1 == null && (object)field2 == null) {
                return true;
            }
            else if ((object)field1 == null || (object)field2 == null) {
                return false;
            }

			return field1.Equals(field2);
		}

		public static bool operator !=(ViewFieldSchema field1, ViewFieldSchema field2)
		{
			return !(field1 == field2);
		}

		public override string[] GenerateSqlCreateField()
		{
			List<string> sqls = new List<string>();
			return sqls.ToArray();
		}
	}
}
