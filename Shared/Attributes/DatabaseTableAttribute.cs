using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace EasyBase.Classes
{
	public class DatabaseTableAttribute : Attribute
	{
		public DatabaseTableAttribute(string tableName)
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

		public DatabaseFieldAttribute[] Fields
		{
			get
			{
				List<DatabaseFieldAttribute> databaseFieldAttributes = new List<DatabaseFieldAttribute>();
				Assembly assembly = Assembly.GetExecutingAssembly();

				// Loopa igenom alla klasser i assemblyt
				foreach (Type type in assembly.GetTypes()) {
					object[] attributes = type.GetCustomAttributes(typeof(DatabaseTableAttribute), false);

					// Om klassen är en databastabell
					if (attributes.Length > 0) {
						DatabaseTableAttribute attribute = (DatabaseTableAttribute)attributes[0];

						// Om klassen är aktuell databastabell
						if (attribute.TableName == TableName) {
							PropertyInfo[] properties = type.GetProperties();
							

							// Loopa igenom klassens alla fält
							foreach (PropertyInfo property in properties) {
								object[] fieldAttributes = property.GetCustomAttributes(typeof(DatabaseFieldAttribute), true);

								// Om fältet är ett databasfält
								if (fieldAttributes.Length > 0) {
									DatabaseFieldAttribute databaseFieldAttribute = (DatabaseFieldAttribute)fieldAttributes[0];
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
