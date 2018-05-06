using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer
{
	public class DataLayerFieldAttribute : Attribute
	{
		public DataLayerFieldAttribute(string fieldName, string fieldType)
		{
			FieldName = fieldName;
			FieldType = fieldType;
			AllowNull = false;
			UniqueKeyGroup = -1;
		}

		public DataLayerFieldAttribute(string fieldName, string fieldType, bool allowNull)
		{
			FieldName = fieldName;
			FieldType = fieldType;
			AllowNull = allowNull;
			UniqueKeyGroup = -1;
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

		public int UniqueKeyGroup
		{
			get;
			set;
		}
	}

	public class DataLayerPrimaryKeyFieldAttribute : DataLayerFieldAttribute
	{
		public DataLayerPrimaryKeyFieldAttribute(string fieldName, string fieldType)
			: base(fieldName, fieldType)
		{
			IsIdentity = false;
		}

		public DataLayerPrimaryKeyFieldAttribute(string fieldName, string fieldType, bool allowNull)
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

	public class DataLayerForeignKeyFieldAttribute : DataLayerFieldAttribute
	{
		public DataLayerForeignKeyFieldAttribute(string fieldName, string fieldType, string targetTableName, string targetFieldName)
			: base(fieldName, fieldType)
		{
			TargetTableName = targetTableName;
			TargetFieldName = targetFieldName;
		}

		public DataLayerForeignKeyFieldAttribute(string fieldName, string fieldType, string targetTableName, string targetFieldName, bool allowNull)
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

	public class DataLayerViewFieldAttribute : DataLayerFieldAttribute
	{
		public DataLayerViewFieldAttribute(string fieldName, string fieldType)
			: base(fieldName, fieldType)
		{
		}

		public DataLayerViewFieldAttribute(string fieldName, string fieldType, bool allowNull)
			: base(fieldName, fieldType, allowNull)
		{
		}
	}
}

