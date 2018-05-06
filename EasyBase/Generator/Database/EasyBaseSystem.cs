using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace EasyBaseGenerator
{
    /// <summary>
    /// Klass som beskriver hela entitetssystemet som byggs upp utifrån XML-definitionsfilen.
    /// </summary>
	public class EasyBaseSystem
	{
        /// <summary>
        /// Skapar ett EasyBaseSystem-objekt och bygger upp det enligt XML-definitionsfilen.
        /// </summary>
        /// <param name="xmlString">XML-definitionsfilen som en textsträng.</param>
		public EasyBaseSystem(string xmlString)
		{
            Enums = new GlobalEnumCollection();
			Entities = new List<Entity>();
			ParseXml(xmlString);
		}

        public GlobalEnumCollection Enums
        {
            get;
            private set;
        }

		public List<Entity> Entities
		{
			get;
			private set;
		}

		/// <summary>
		/// Adds a field to a unique key in a list of unique keys. If the unique key is not in the list, a new unique key will be added to the list.
		/// </summary>
		/// <param name="uniqueKeys">List of unique keys.</param>
		/// <param name="uniqueKeyGroup">The group ID of the unique key within its entity.</param>
		/// <param name="field">Field to att do unique key.</param>
		private void AddToUniqueKeys(List<UniqueKey> uniqueKeys, int uniqueKeyGroup, Field field)
		{
			foreach (UniqueKey uniqueKey in uniqueKeys) {
				if (uniqueKey.UniqueKeyGroup == uniqueKeyGroup) {
					uniqueKey.Fields.Add(field);
					return;
				}
			}

			// If we get here, there was no appropriate unique key in collection, so we need to add one
			UniqueKey newUniqueKey = new UniqueKey(uniqueKeyGroup);
			newUniqueKey.Fields.Add(field);
			uniqueKeys.Add(newUniqueKey);
		}

        private bool IsGlobalEnum(string enumName)
        {
            if (Enums[enumName] != null)
            {
                return true;
            }

            return false;
        }
		
		private static string ToPlural(string singularWord)
		{
			if (singularWord.EndsWith("y")) {
				return singularWord.Substring(0, singularWord.Length - 1) + "ies";
			}

			return singularWord + "s";
		}

		/// <summary>
		/// Parses an XML string to a new Easy Base System.
		/// </summary>
		/// <remarks>Generator step 1.</remarks>
		/// <param name="xmlString">The XML string.</param>
		private void ParseXml(string xmlString)
		{
			// Read all entity names (singular and plural)
			XmlReader reader = XmlReader.Create(new StringReader(xmlString));

			XDocument doc = XDocument.Load(reader);

			var entityNames = (from entity in doc.Descendants("Entity")
							   select new
							   {
								   NameSingular = entity.Attribute("NameSingular").Value,
								   NamePlural = entity.Attributes("NamePlural").Count() > 0 ? entity.Attribute("NamePlural").Value : ToPlural(entity.Attribute("NameSingular").Value)
							   }).ToList();

			reader.Close();
			reader = XmlReader.Create(new StringReader(xmlString));

            // Read Enums
            doc = XDocument.Load(reader);

            var enums = (from e in doc.Descendants("Enum")
                         select e).ToList();

            // Create all global enums
            foreach (var e in enums)
            {
                string enumName = e.Attribute("Name").Value;
                Enums.Add(new GlobalEnum(enumName));

                var values = (from v in e.Descendants()
                              select v).ToList();

                // Create all possible values for current enum
                foreach (var value in values)
                {
                    Enums[enumName].Values.Add(value.Value);
                }
            }

            reader.Close();
            reader = XmlReader.Create(new StringReader(xmlString));

            // Read Entities
			while (reader.ReadToFollowing("Entity")) {
				string nameSingular = "";
				string namePlural = "";
				bool isView = false;
				List<Field> fields = new List<Field>();
				List<UniqueKey> uniqueKeys = new List<UniqueKey>();
                List<string> parentNames = new List<string>();

				while (reader.MoveToNextAttribute()) {
					string attributeName = reader.Name;

					switch (attributeName) {
						case "NameSingular":
							nameSingular = reader.Value;
							if (namePlural == "") {
								namePlural = ToPlural(nameSingular);
							}
							break;
						case "NamePlural":
							namePlural = reader.Value;
							break;
						case "IsView":
							isView = (reader.Value.ToLower() == "true");
							break;
						default:
							throw new NotImplementedException("'" + attributeName + "' on entity '" + nameSingular + "' is not a valid entity attribute.");
					}
				}

				if (nameSingular == "") {
					throw new ApplicationException("Entity must have a name.");
				}

				while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
					;

				if (reader.NodeType != XmlNodeType.EndElement && !reader.EOF) {
					if (reader.Name == "Fields") {

						while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
							;

						while (reader.NodeType != XmlNodeType.EndElement && !reader.EOF) {
							Field field;

							if (reader.Name == "PrimaryKeyField") {

								string fieldName = "";
								string fieldType = "";
								bool allowNull = false;
								bool isIdentity = false;
								string[] enumValues = null;
                                string globalEnumName = "";

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
										case "IsIdentity":
											isIdentity = Convert.ToBoolean(reader.Value);
											break;
										case "EnumValues":
                                            if (IsGlobalEnum(reader.Value)) {
                                                globalEnumName = reader.Value;
                                            }
											else {
                                                enumValues = reader.Value.Split("/".ToArray());
                                            }
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on entity '" + nameSingular + "' is not a valid primary key field attribute.");
									}
								}

								if (fieldName == "") {
									throw new ApplicationException("Primary key field for entity '" + nameSingular + "' must have a name.");
								}
								if (fieldType == "") {
									throw new ApplicationException("Primary key field '" + fieldName + "' on entity '" + nameSingular + "' must have a type.");
								}

								field = new PrimaryKeyField(nameSingular, fieldName, fieldType, allowNull, isIdentity);

                                if (globalEnumName != "") {
                                    field.GlobalEnumName = globalEnumName;
                                }
                                else {
                                    if (enumValues != null) {
                                        field.EnumValues = enumValues;
                                    }
                                }
							}
							else if (reader.Name == "ForeignKeyField") {

								string fieldName = "";
								string fieldType = "";
								string targetEntityName = "";
								string targetFieldName = "";
								bool allowNull = false;
                                bool isParentConnection = false;
								List<int> uniqueKeyGroups = new List<int>();

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "TargetEntity":
											targetEntityName = reader.Value;
											break;
										case "TargetField":
											targetFieldName = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
                                        case "IsParentConnection":
                                            isParentConnection = Convert.ToBoolean(reader.Value);
                                            break;
										case "UniqueKeyGroup":
											string[] keyGroups = reader.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
											foreach (string sKeyGroup in keyGroups) {
												uniqueKeyGroups.Add(int.Parse(sKeyGroup));
											}
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on entity '" + nameSingular + "' is not a valid foreign key field attribute.");
									}
								}


								if (fieldName == "") {
									throw new ApplicationException("Foreign key field for entity '" + nameSingular + "' must have a name.");
								}
								if (fieldType == "") {
									throw new ApplicationException("Foreign key field '" + fieldName + "' on entity '" + nameSingular + "' must have a type.");
								}
								if (targetEntityName == "") {
									throw new ApplicationException("Foreign key field '" + fieldName + "' on entity '" + nameSingular + "' must have a target entity.");
								}
								if (targetFieldName == "") {
									throw new ApplicationException("Foreign key field '" + fieldName + "' on entity '" + nameSingular + "' must have a target field.");
								}

								var tableName = entityNames.Find(a => a.NameSingular == targetEntityName).NamePlural;
								field = new ForeignKeyField(nameSingular, fieldName, fieldType, tableName, targetFieldName, allowNull, isParentConnection, uniqueKeyGroups.ToArray());

                                if (isParentConnection) {
                                    parentNames.Add(targetEntityName);
                                }

								if (uniqueKeyGroups.Count > 0) {
									foreach (int uniqueKeyGroup in uniqueKeyGroups) {
										AddToUniqueKeys(uniqueKeys, uniqueKeyGroup, field);
									}
								}
							}
							else if (reader.Name == "Field") {

								string fieldName = "";
								string fieldType = "";
								bool allowNull = false;
								List<int> uniqueKeyGroups = new List<int>();
								string[] enumValues = null;
								string globalEnumName = "";

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
										case "UniqueKeyGroup":
											string[] keyGroups = reader.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
											foreach (string sKeyGroup in keyGroups) {
												uniqueKeyGroups.Add(int.Parse(sKeyGroup));
											}
											break;
										case "EnumValues":
											if (IsGlobalEnum(reader.Value)) {
												globalEnumName = reader.Value;
											}
											else {
												enumValues = reader.Value.Split("/".ToArray());
											}
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on entity '" + nameSingular + "' is not a valid field attribute.");
									}
								}

								if (fieldName == "") {
									throw new ApplicationException("Field on entity '" + nameSingular + "' must have a name.");
								}
								if (fieldType == "") {
									throw new ApplicationException("Field '" + fieldName + "' entity '" + nameSingular + "' must have a type.");
								}

								field = new Field(nameSingular, fieldName, fieldType, allowNull, uniqueKeyGroups.ToArray());

								if (globalEnumName != "") {
									field.GlobalEnumName = globalEnumName;
								}
								else {
									if (enumValues != null) {
										field.EnumValues = enumValues;
									}
								}

								if (uniqueKeyGroups.Count > 0) {
									foreach (int uniqueKeyGroup in uniqueKeyGroups) {
										AddToUniqueKeys(uniqueKeys, uniqueKeyGroup, field);
									}
								}
							}
							else if (reader.Name == "ViewField") {

								string fieldName = "";
								string fieldType = "";
								bool allowNull = false;

								while (reader.MoveToNextAttribute()) {
									string attributeName = reader.Name;

									switch (attributeName) {
										case "Name":
											fieldName = reader.Value;
											break;
										case "Type":
											fieldType = reader.Value;
											break;
										case "AllowNull":
											allowNull = Convert.ToBoolean(reader.Value);
											break;
										default:
											throw new NotImplementedException("'" + attributeName + "' on field '" + fieldName + "' on entity '" + nameSingular + "' is not a valid field attribute.");
									}
								}

								if (fieldName == "") {
									throw new ApplicationException("Field on entity '" + nameSingular + "' must have a name.");
								}
								if (fieldType == "") {
									throw new ApplicationException("Field '" + fieldName + "' entity '" + nameSingular + "' must have a type.");
								}

								field = new ViewField(nameSingular, fieldName, fieldType, allowNull);
							}
							else {
								throw new ApplicationException("When initiating entity '" + nameSingular + "', tags '<PrimaryKeyField>, <ForeighKeyField>, <Field> or <ViewField>' was expected, but got tag '<" + reader.Name + ">'");
							}

							fields.Add(field);

							while (reader.Read() && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
								;
						}


					}
					else {
						throw new ApplicationException("When initiating entity '" + nameSingular + "', tag '<Fields>' was expected, but got tag '<" + reader.Name + ">'");
					}

					Entities.Add(new Entity(this, nameSingular, namePlural, isView, fields.ToArray(), uniqueKeys.ToArray(), parentNames.ToArray()));
				}
			}

            // Loopa igenom alla entiteter och sätt barn på alla föräldrar
            foreach (Entity entity in Entities) {
                if (entity.HasParents) {
                    foreach (string parentName in entity.ParentNames) {
                        Entity parent = Entities.Find(e => e.NameSingular == parentName);
                        parent.ChildNames.Add(entity.NameSingular);
                    }
                }
            }
		}

		public string GenerateDataClasses()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("using System;");
			code.AppendLine("using System.Text;");
			code.AppendLine("using System.Collections.Generic;");
			code.AppendLine("using System.Reflection;");
			code.AppendLine("using System.Runtime.CompilerServices;");
			code.AppendLine();
			code.AppendLine("[assembly: InternalsVisibleTo(\"EasyBase.DataLayer\")]");
			code.AppendLine();
			code.AppendLine("namespace EasyBase.Classes");
			code.AppendLine("{");

			bool firstLine = true;

			foreach (Entity entity in Entities) {
				if (!firstLine) {
					code.AppendLine();
				}

				code.Append(entity.GenerateDataClass());

				firstLine = false;
			}

			code.AppendLine("}");

			return code.ToString();
		}

		public string GenerateEnums()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("using System;");
			code.AppendLine();
			code.AppendLine("namespace EasyBase.Classes");
			code.AppendLine("{");

            bool firstEnum = true;
            foreach (GlobalEnum globalEnum in Enums)
            {
                if (!firstEnum)
                {
                    code.AppendLine();
                }

                code.AppendLine("    public enum " + globalEnum.Name);
                code.AppendLine("    {");

                int valueNo = 1;
                foreach (string enumValue in globalEnum.Values)
                {
                    string comma = "";
                    if (valueNo < globalEnum.Values.Count)
                    {
                        comma = ",";
                    }

                    code.AppendLine("        " + enumValue + " = " + valueNo + comma);
                    valueNo++;
                }

                code.AppendLine("    }");
                firstEnum = false;
            }

			foreach (Entity entity in Entities) {
				string enumCode = entity.GenerateEnums();

				if (enumCode != "") {
					code.AppendLine();
					code.Append(enumCode);
				}
			}

			code.AppendLine("}");

			return code.ToString();
		}

		public string GenerateBusinessClasses()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("using System;");
			code.AppendLine("using System.Collections.Generic;");
			code.AppendLine("using System.Text;");
			code.AppendLine("using EasyBase.DataLayer;");
			
			code.AppendLine();
			code.AppendLine("namespace EasyBase.BusinessLayer");
			code.AppendLine("{");

			bool firstLine = true;

			foreach (Entity entity in Entities) {
				if (!firstLine) {
					code.AppendLine();
				}

				code.Append(entity.GenerateBusinessClass());

				firstLine = false;
			}

			code.AppendLine("}");

			return code.ToString();
		}

		public string GenerateDataMethods()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("using System;");
			code.AppendLine("using System.Collections.Generic;");
            code.AppendLine("using System.Text;");
            code.AppendLine("using System.Data;");
            code.AppendLine("using EasyBase.Classes;");
			code.AppendLine();
			code.AppendLine("namespace EasyBase.DataLayer");
			code.AppendLine("{");
			code.AppendLine("    public partial class StandardDataLayer");
			code.AppendLine("    {");

			bool firstLine = true;

			foreach (Entity entity in Entities) {
				if (!firstLine) {
					code.AppendLine();
				}

				code.AppendLine(entity.GenerateGetDataMethods());
				code.AppendLine();
				code.Append(entity.GenerateCountDataMethods());
				code.AppendLine();
				code.Append(entity.GenerateSaveDataMethods());
				code.AppendLine();
				code.Append(entity.GenerateDeleteDataMethods());

				firstLine = false;
			}

			code.AppendLine("    }");
			code.AppendLine("}");

			return code.ToString();
		}

		public string GenerateBusinessMethods()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("using System;");
			code.AppendLine("using System.Collections.Generic;");
            code.AppendLine("using System.Text;");
            code.AppendLine("using System.Data;");
            code.AppendLine("using EasyBase.Classes;");
			code.AppendLine("using EasyBase.DataLayer;");
			code.AppendLine();
			code.AppendLine("namespace EasyBase.BusinessLayer");
			code.AppendLine("{");
			code.AppendLine("    public partial class StandardBusinessLayer");
			code.AppendLine("    {");

			bool firstLine = true;

			foreach (Entity entity in Entities) {
				if (!firstLine) {
					code.AppendLine();
				}

				code.Append(entity.GenerateGetBusinessMethods());
				code.AppendLine();
				code.Append(entity.GenerateCountBusinessMethods());
				code.AppendLine();
				code.Append(entity.GenerateSaveBusinessMethods());
				code.AppendLine();
				code.Append(entity.GenerateDeleteBusinessMethods());

				firstLine = false;
			}

			code.AppendLine("    }");
			code.AppendLine("}");

			return code.ToString();
		}

		public string GenerateBusinessCollections()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("using System;");
			code.AppendLine("using System.Text;");
			code.AppendLine("using System.Collections.Generic;");
			code.AppendLine();
			code.AppendLine("namespace EasyBase.Classes");
			code.AppendLine("{");

			bool firstLine = true;

			foreach (Entity entity in Entities) {
				if (!firstLine) {
					code.AppendLine();
				}

				code.Append(entity.GenerateBusinessCollection());

				firstLine = false;
			}

			code.AppendLine("}");

			return code.ToString();
		}

		public string GenerateDataTables()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("using System;");
			code.AppendLine("using System.Data;");
			code.AppendLine();

			code.AppendLine("namespace EasyBase.Classes");
			code.AppendLine("{");

			bool firstLine = true;

			foreach (Entity entity in Entities) {
				if (!firstLine) {
					code.AppendLine();
				}

				code.Append(entity.GenerateDataTableClass());

				firstLine = false;
			}

			code.AppendLine("}");

			return code.ToString();
		}

		public static string ToFieldType(string databaseType, bool nullable)
		{
			if (databaseType == "int" && !nullable) {
				return "int";
			}
			if (databaseType == "int" && nullable) {
				return "int?";
			}
			else if (databaseType.Length >= 7 && databaseType.Substring(0, 7) == "varchar" && !nullable) {
				return "string";
			}
			else if (databaseType.Length >= 7 && databaseType.Substring(0, 7) == "varchar" && nullable) {
				return "string?";
			}
			else if (databaseType.Length >= 8 && databaseType.Substring(0, 8) == "nvarchar" && !nullable) {
				return "string";
			}
			else if (databaseType.Length >= 8 && databaseType.Substring(0, 8) == "nvarchar" && nullable) {
				return "string?";
			}
			else if (databaseType == "bit" && !nullable) {
				return "bool";
			}
			else if (databaseType == "bit" && nullable) {
				return "bool?";
			}
			else if (databaseType == "datetime" && !nullable) {
				return "DateTime";
			}
			else if (databaseType == "datetime" && nullable) {
				return "DateTime?";
			}
			else if (databaseType == "smalldatetime" && !nullable) {
				return "DateTime";
			}
			else if (databaseType == "smalldatetime" && nullable) {
				return "DateTime?";
			}
			else if (databaseType == "money" && !nullable) {
				return "decimal";
			}
			else if (databaseType == "money" && nullable) {
				return "decimal?";
			}
            else if (Regex.IsMatch(databaseType, @"^decimal\(\d+,\d+\)$")) {
                return "decimal";
            }
			else {
				throw new NotImplementedException("Database type '" + databaseType + "' not supported.");
			}
		}

		public static string GetConversion(string databaseType)
		{
			if (databaseType == "int") {
				return "Convert.ToInt32";
			}
			else if (databaseType.Length >= 7 && databaseType.Substring(0, 7) == "varchar") {
				return "Convert.ToString";
			}
			else if (databaseType == "bit") {
				return "Convert.ToBoolean";
			}
			else if (databaseType == "datetime") {
				return "Convert.ToDateTime";
			}
			else {
				throw new NotImplementedException("Database type '" + databaseType + "' not supported.");
			}
		}

		public static string ToCamelCase(string varName)
		{
			string camelVarName;

			if (varName.Trim() == "") {
				throw new InvalidOperationException("Variable name must be something to camel case it!");
			}

			if (varName == varName.ToUpper()) {
				return varName.ToLower();
			}

			camelVarName = varName.Substring(0, 1).ToLower();

			if (varName.Length > 1) {
				camelVarName += varName.Substring(1);
			}

			if (varName.Trim() == camelVarName.Trim()) {
				throw new InvalidOperationException("Variable '" + varName + "' cannot be camel cased!");
			}

			return camelVarName;
		}

		public static string GetDefaultFieldValue(string fieldType)
		{
			if (fieldType.EndsWith("?")) {
				return "null";
			}
			else if (fieldType == "int") {
				return "0";
			}
			else if (fieldType == "bool") {
				return "false";
			}
			else if (fieldType == "DateTime") {
				return "new DateTime()";
			}
			else if (fieldType == "decimal") {
				return "0";
			}
			else if (fieldType == "string") {
				return "\"\"";
			}

			throw new NotImplementedException("Default value for type " + fieldType + " is not implemented.");
		}

		public static string GetDefaultDatabaseValue(string databaseType, bool allowNull)
		{
			if (allowNull) {
				return "null";
			}
			else if (databaseType == "int") {
				return "0";
			}
			else if (databaseType == "bit") {
				return "0";
			}
			else if (databaseType == "datetime") {
				return "'1901-01-01'";
			}
			else if (databaseType == "money") {
				return "0";
			}
			else if (databaseType.Length >= 7 && databaseType.Substring(0, 7) == "varchar") {
				return "''";
			}

			throw new NotImplementedException("Default value for type " + databaseType + " is not implemented.");
		}
	}
}
