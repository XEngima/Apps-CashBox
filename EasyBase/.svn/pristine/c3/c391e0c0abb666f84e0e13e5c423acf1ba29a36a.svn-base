using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace EasyBase.DataLayer
{
	public class DataLayerTableAttribute : Attribute
	{
		public DataLayerTableAttribute(string tableName)
		{
			TableName = tableName;
		}

		public string TableName
		{
			get;
			private set;
		}

		public bool IsView
		{
			get;
			set;
		}

		public DataLayerFieldAttribute[] Fields
		{
			get
			{
				List<DataLayerFieldAttribute> databaseFieldAttributes = new List<DataLayerFieldAttribute>();
				Assembly assembly = Assembly.GetExecutingAssembly();

				// Loopa igenom alla klasser i assemblyt
				foreach (Type type in assembly.GetTypes()) {
					object[] attributes = type.GetCustomAttributes(typeof(DataLayerTableAttribute), false);

					// Om klassen är en databastabell
					if (attributes.Length > 0) {
						DataLayerTableAttribute attribute = (DataLayerTableAttribute)attributes[0];

						// Om klassen är aktuell databastabell
						if (attribute.TableName == TableName) {
							PropertyInfo[] properties = type.GetProperties();
							

							// Loopa igenom klassens alla fält
							foreach (PropertyInfo property in properties) {
								object[] fieldAttributes = property.GetCustomAttributes(typeof(DataLayerFieldAttribute), true);

								// Om fältet är ett databasfält
								if (fieldAttributes.Length > 0) {
									DataLayerFieldAttribute databaseFieldAttribute = (DataLayerFieldAttribute)fieldAttributes[0];
									databaseFieldAttributes.Add(databaseFieldAttribute);
								}
							}

							return databaseFieldAttributes.ToArray();
						}
					}
				}

				return databaseFieldAttributes.ToArray();
			}
		}
	}
}
