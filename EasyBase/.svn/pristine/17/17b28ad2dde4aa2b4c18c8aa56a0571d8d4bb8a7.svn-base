using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
	public class ForeignKeyField : Field
	{
		public ForeignKeyField(string entityName, string fieldName, string databaseType, string targetTableName, string targetFieldName, bool allowNull, bool isParentConnection, int[] uniqueKeyGroup)
			: base(entityName, fieldName, databaseType, allowNull, uniqueKeyGroup)
		{
			TargetTableName = targetTableName;
			TargetFieldName = targetFieldName;
            IsParentConnection = isParentConnection;
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

        public bool IsParentConnection
        {
            get;
            private set;
        }

		protected override string GenerateDataLayerClassPropertyAttribute()
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

			return "        [ForeignKeyField(\"" + FieldName + "\", \"" + DatabaseType + "\", \"" + TargetTableName + "\", \"" + TargetFieldName + "\"" + sAllowNull + sUniqueKeyGroups + ")]";
		}
	}
}
