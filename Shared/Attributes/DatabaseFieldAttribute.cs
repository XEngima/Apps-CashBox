using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
	public class DatabaseFieldAttribute : Attribute
	{
		public DatabaseFieldAttribute(string fieldName, string fieldType)
		{
			FieldName = fieldName;
			FieldType = fieldType;
			AllowNull = false;
			UniqueKeyGroupsString = "";
		}

		public DatabaseFieldAttribute(string fieldName, string fieldType, bool allowNull)
		{
			FieldName = fieldName;
			FieldType = fieldType;
			AllowNull = allowNull;
			UniqueKeyGroupsString = "";
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
			set;
		}

		public string UniqueKeyGroupsString
		{
			get;
			set;
		}

		public int[] UniqueKeyGroups
		{
			get
			{
				List<int> uniqueKeyGroups = new List<int>();
				string[] uniqueKeyGroupStrings = UniqueKeyGroupsString.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

				foreach (string uniqueKeyGroupString in uniqueKeyGroupStrings) {
					uniqueKeyGroups.Add(int.Parse(uniqueKeyGroupString));
				}

				return uniqueKeyGroups.ToArray();
			}
		}
	}

	public class PrimaryKeyFieldAttribute : DatabaseFieldAttribute
	{
		public PrimaryKeyFieldAttribute(string fieldName, string fieldType)
			: base(fieldName, fieldType)
		{
			IsIdentity = false;
		}

		public PrimaryKeyFieldAttribute(string fieldName, string fieldType, bool allowNull)
			: base(fieldName, fieldType, allowNull)
		{
			IsIdentity = false;
		}

		public bool IsIdentity
		{
			get;
			set;
		}
	}

	public class ForeignKeyFieldAttribute : DatabaseFieldAttribute
	{
		public ForeignKeyFieldAttribute(string fieldName, string fieldType, string targetTableName, string targetFieldName)
			: base(fieldName, fieldType)
		{
			TargetTableName = targetTableName;
			TargetFieldName = targetFieldName;
		}

		public ForeignKeyFieldAttribute(string fieldName, string fieldType, string targetTableName, string targetFieldName, bool allowNull)
			: base(fieldName, fieldType, allowNull)
		{
			TargetTableName = targetTableName;
			TargetFieldName = targetFieldName;
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
	}

	public class ViewFieldAttribute : DatabaseFieldAttribute
	{
		public ViewFieldAttribute(string fieldName, string fieldType)
			: base(fieldName, fieldType)
		{
		}

		public ViewFieldAttribute(string fieldName, string fieldType, bool allowNull)
			: base(fieldName, fieldType, allowNull)
		{
		}
	}
}

