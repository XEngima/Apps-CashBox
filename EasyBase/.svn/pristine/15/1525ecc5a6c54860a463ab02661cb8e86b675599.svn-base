using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
	public class Entity
	{
		public const bool DefaultAllowNull = false;
		public const bool DefaultIsIdentity = false;

		public Entity(EasyBaseSystem database, string nameSingular, string namePlural, bool isView, Field[] fields, UniqueKey[] uniqueKeys, string[] parentNames)
		{
            Database = database;

			NameSingular = nameSingular;
			NamePlural = namePlural;
			IsView = isView;

			Fields = new List<Field>();
			ForeignKeys = new List<ForeignKeyField>();
			ViewFields = new List<ViewField>();
            ParentNames = parentNames;
            ChildNames = new List<string>();

			bool primaryKeyAdded = false;

			foreach (Field field in fields) {
				if (field is PrimaryKeyField) {
					if (primaryKeyAdded) {
						throw new ApplicationException("Entity '" + nameSingular + "' has two primary key fields. Only one is allowed.");
					}

					primaryKeyAdded = true;
					PrimaryKey = (PrimaryKeyField)field;
				}
				else if (field is ForeignKeyField) {
					ForeignKeys.Add((ForeignKeyField)field);
				}
				else if (field is ViewField) {
					ViewFields.Add((ViewField)field);
				}
				else {
					Fields.Add(field);
				}
			}

			UniqueKeys = new List<UniqueKey>();

			foreach (UniqueKey uniqueKey in uniqueKeys) {
				UniqueKeys.Add(uniqueKey);
			}
		}

        private EasyBaseSystem Database
        {
            get;
            set;
        }

		public string NameSingular
		{
			get;
			private set;
		}

		public string NamePlural
		{
			get;
			private set;
		}

		public bool IsView
		{
			get;
			private set;
		}

		public PrimaryKeyField PrimaryKey
		{
			get;
			private set;
		}

		public List<ForeignKeyField> ForeignKeys
		{
			get;
			private set;
		}

        public bool HasParents
        {
            get { return ParentNames.Length > 0; }
        }

        public string[] ParentNames
        {
            get;
            private set;
        }

        public Entity[] Parents
        {
            get
            {
                List<Entity> parents = new List<Entity>();

                foreach (string parentName in ParentNames) {
                    parents.Add(Database.Entities.Find(e => e.NameSingular == parentName));
                }

                return parents.ToArray();
            }
        }

        public bool HasChildren
        {
            get { return ChildNames.Count > 0; }
        }

        public List<string> ChildNames
        {
            get;
            private set;
        }

        public Entity[] Children
        {
            get
            {
                List<Entity> children = new List<Entity>();

                foreach (string childName in ChildNames) {
                    children.Add(Database.Entities.Find(e => e.NameSingular == childName));
                }

                return children.ToArray();
            }
        }

        public bool ForeignKeysFormUniqueKey
		{
			get
			{
				List<int> keyGroupsToTest = new List<int>();
				foreach (ForeignKeyField foreignKey in ForeignKeys) {
					if (foreignKey.UniqueKeyGroups != null) {
						foreach (int uniqueKeyGroup in foreignKey.UniqueKeyGroups) {
							if (!keyGroupsToTest.Contains(uniqueKeyGroup)) {
								keyGroupsToTest.Add(uniqueKeyGroup);
							}
						}
					}
				}

				foreach (int keyGroup in keyGroupsToTest) {
					bool usedByAllForeignKeys = true;

					foreach (ForeignKeyField field in ForeignKeys) {
						if (!field.UniqueKeyGroups.Contains(keyGroup)) {
							usedByAllForeignKeys = false;
						}
					}

					if (usedByAllForeignKeys) {
						bool usedByAnotherField = false;
						foreach (Field field in Fields) {
							if (field.UniqueKeyGroups.Contains(keyGroup)) {
								usedByAnotherField = true;
								break;
							}
						}

						if (!usedByAnotherField) {
							return true;
						}
					}
				}

				return false;
			}
		}

		public List<Field> Fields
		{
			get;
			private set;
		}

		public List<ViewField> ViewFields
		{
			get;
			private set;
		}

		public List<Field> TableFields
		{
			get
			{
				List<Field> allFields = new List<Field>();

				if (PrimaryKey != null) {
					allFields.Add(PrimaryKey);
				}

				if (ForeignKeys.Count > 0) {
					allFields.AddRange(ForeignKeys);
				}

				if (Fields.Count > 0) {
					allFields.AddRange(Fields);
				}

				return allFields;
			}
		}

		public List<Field> AllFields
		{
			get
			{
				List<Field> allFields = new List<Field>();

				if (PrimaryKey != null) {
					allFields.Add(PrimaryKey);
				}

				if (ForeignKeys.Count > 0) {
					allFields.AddRange(ForeignKeys);
				}

				if (Fields.Count > 0) {
					allFields.AddRange(Fields);
				}

				if (ViewFields.Count > 0) {
					allFields.AddRange(ViewFields);
				}

				return allFields;
			}
		}

		public List<UniqueKey> UniqueKeys
		{
			get;
			private set;
		}

		public string BusinessClassName
		{
			get { return NameSingular; }
		}

		public string GenerateDataClass()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("    [DatabaseTable(\"" + NamePlural + "\"" + (IsView ? ", IsView = true" : "") + ")]");
			code.AppendLine("    public partial class " + NameSingular);
			code.AppendLine("    {");

			// Field Constants

			foreach (Field field in AllFields) {
				code.AppendLine("        public const string f" + field.FieldName + " = \"" + field.FieldName + "\";");
			}
			code.AppendLine();

			// Konstruction/Destruction

			string dataRowTypeName = NameSingular + "DataRow";
			string dataRowVarName = EasyBaseSystem.ToCamelCase(NameSingular + "DataRow");

			// Konstructor with a data row as argument

			code.AppendLine("        internal " + NameSingular + "(" + dataRowTypeName + " " + dataRowVarName + ")");
			code.AppendLine("        {");

			foreach (Field field in AllFields) {
				code.AppendLine("            " + field.FieldName + " = " + dataRowVarName + "." + field.FieldName + ";");
			}

            // Parent Properties

            if (HasParents) {
                foreach (string parentName in ParentNames) {
                    code.AppendLine("            " + parentName + " = null;");
                }
            }

            // Child Properties

            if (HasChildren) {
                foreach (string childName in ChildNames) {
                    string childPluralName = Database.Entities.Find(e => e.NameSingular == childName).NamePlural;

                    code.AppendLine("            " + childPluralName + " = null;");
                }
            }

			code.AppendLine("        }");
			code.AppendLine();

			// Konstructor with real arguments

			code.Append("        public " + NameSingular + "(");

			string comma = "";

			foreach (Field field in TableFields) {
				PrimaryKeyField primaryKeyField = field as PrimaryKeyField;

				if (primaryKeyField == null || !primaryKeyField.IsIdentity) { // If field is not a primary key field with identity
					code.Append(comma);

					code.Append(field.FieldType);

					code.Append(" ");
					code.Append(EasyBaseSystem.ToCamelCase(field.FieldName));

					comma = ", ";
				}
			}

            code.AppendLine(")");

			code.AppendLine("        {");

			foreach (Field field in AllFields) {
				code.Append("            ");
				code.Append(field.FieldName);
				code.Append(" = ");

				PrimaryKeyField primaryKeyField = field as PrimaryKeyField;

				if (field is ViewField) {
					code.Append(EasyBaseSystem.GetDefaultFieldValue(field.FieldType));
				}
				else if (primaryKeyField == null || !primaryKeyField.IsIdentity) { // If field is not a primary key field with identity
					code.Append(EasyBaseSystem.ToCamelCase(field.FieldName));
				}
				else {
					code.Append(EasyBaseSystem.GetDefaultFieldValue(field.FieldType));
				}

				code.AppendLine(";");
			}

            // Parent Properties

            if (HasParents) {
                foreach (string parentName in ParentNames) {
                    code.AppendLine("            " + parentName + " = null;");
                }
            }

            // Child Properties

            if (HasChildren) {
                foreach (string childName in ChildNames) {
                    string childPluralName = Database.Entities.Find(e => e.NameSingular == childName).NamePlural;

                    code.AppendLine("            " + childPluralName + " = null;");
                }
            }

            code.AppendLine("        }");

            // Parent Properties

            if (HasParents) {
                foreach (string parentName in ParentNames) {
                    code.AppendLine();
                    code.AppendLine("        public " + parentName + " " + parentName);
                    code.AppendLine("        {");
                    code.AppendLine("            get;");
                    code.AppendLine("            set;");
                    code.AppendLine("        }");
                }
            }

            // Child Properties

            if (HasChildren) {
                foreach (string childName in ChildNames) {
                    string childPluralName = Database.Entities.Find(e => e.NameSingular == childName).NamePlural;

                    code.AppendLine();
                    code.AppendLine("        public " + childName + "Collection " + childPluralName);
                    code.AppendLine("        {");
                    code.AppendLine("            get;");
                    code.AppendLine("            set;");
                    code.AppendLine("        }");
                }
            }

			// Properties

			foreach (Field field in AllFields) {
				code.AppendLine();
				code.Append(field.GenerateDataClassProperty());
			}

			// ToString()

			code.AppendLine();

			StringBuilder propertyList = new StringBuilder();
			comma = "";
			foreach (Field field in AllFields) {
				propertyList.Append(comma);
				propertyList.Append("\"");
				propertyList.Append(field.FieldName);
				propertyList.Append("=\" + ");
				propertyList.Append(field.FieldName);

				comma = " + \", \" + ";
			}

			code.AppendLine("        public override string ToString()");
			code.AppendLine("        {");
			code.AppendLine("            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(\"OverridedToString\");");
			code.AppendLine("            if (methodInfo != null) {");
			code.AppendLine("                return (string)methodInfo.Invoke(this, null);");
			code.AppendLine("            }");
			code.AppendLine("            else {");
			code.AppendLine("                return " + propertyList + ";");
			code.AppendLine("            }");
			code.AppendLine("        }");
			code.AppendLine("    }");

			return code.ToString();
		}

		public string GenerateEnums()
		{
			StringBuilder code = new StringBuilder();
			bool firstEnum = true;

			foreach (Field field in AllFields) {
				if (field.HasEnumValues2 && field.GlobalEnumName == "") {
					if (!firstEnum) {
						code.AppendLine();
					}

					code.Append(field.GenerateEnum());

					firstEnum = false;
				}
			}

			return code.ToString();
		}

		public string GenerateBusinessClass()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("    public partial class " + BusinessClassName);
			code.AppendLine("    {");

			// Field Constants

			foreach (Field field in AllFields) {
				code.AppendLine("        public const string f" + field.FieldName + " = \"" + field.FieldName + "\";");
			}
			code.AppendLine();

			// Konstruktion/Destruction

			string dataVarName = EasyBaseSystem.ToCamelCase(BusinessClassName);

			code.AppendLine("        public " + BusinessClassName + "(" + NameSingular + " " + dataVarName + ")");
			code.AppendLine("        {");

			foreach (Field field in AllFields) {
				code.AppendLine("            " + field.FieldName + " = " + dataVarName + "." + field.FieldName + ";");
			}

			code.AppendLine("        }");
			code.AppendLine();

			code.Append("        public " + BusinessClassName + "(");

			string comma = "";

			foreach (Field field in AllFields) {
				if (!(field is PrimaryKeyField)) {
					code.Append(comma);
					code.Append(field.FieldType);
					code.Append(" ");
					code.Append(EasyBaseSystem.ToCamelCase(field.FieldName));

					comma = ", ";
				}
			}

			code.AppendLine(")");

			code.AppendLine("        {");

			foreach (Field field in AllFields) {
				code.Append("            ");
				code.Append(field.FieldName);
				code.Append(" = ");

				if (field is PrimaryKeyField) {
					code.Append(EasyBaseSystem.GetDefaultDatabaseValue(field.FieldType, field.AllowNull));
				}
				else {
					code.Append(EasyBaseSystem.ToCamelCase(field.FieldName));
				}

				code.AppendLine(";");
			}

			code.AppendLine("        }");

			// Properties

			foreach (Field field in AllFields) {
				code.AppendLine();
				code.Append(field.GenerateBusinessClassProperty());
			}

			// ToString()

			code.AppendLine();

			StringBuilder propertyList = new StringBuilder();
			comma = "";
			foreach (Field field in AllFields) {
				propertyList.Append(comma);
				propertyList.Append("\"");
				propertyList.Append(field.FieldName);
				propertyList.Append("=\" + ");
				propertyList.Append(field.FieldName);

				comma = " + \", \" + ";
			}

			code.AppendLine("        public override string ToString()");
			code.AppendLine("        {");
			code.AppendLine("            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(\"OverridedToString\");");
			code.AppendLine("            if (methodInfo != null) {");
			code.AppendLine("                return (string)methodInfo.Invoke(this, null);");
			code.AppendLine("            }");
			code.AppendLine("            else {");
			code.AppendLine("                return " + propertyList + ";");
			code.AppendLine("            }");
			code.AppendLine("        }");

			code.AppendLine("    }");

			return code.ToString();
		}

		public string GenerateDataTableClass()
		{
			StringBuilder code = new StringBuilder();

			// DataRow

			code.AppendLine("    public class " + NameSingular + "DataRow : DataRow");
			code.AppendLine("    {");

			code.AppendLine("        internal " + NameSingular + "DataRow(DataRowBuilder builder)");
			code.AppendLine("            :base(builder)");
			code.AppendLine("        {");
			code.AppendLine("        }");

			// Properties

			foreach (Field field in AllFields) {
				code.AppendLine();
				code.Append(field.GenerateDataRowProperty());
			}

			code.AppendLine("    }");

			code.AppendLine();

			// DataTable

			code.AppendLine("    public class " + NameSingular + "Table : DataTable");
			code.AppendLine("    {");
			code.AppendLine("        protected override Type GetRowType()");
			code.AppendLine("        {");
			code.AppendLine("            return typeof(" + NameSingular + "DataRow);");
			code.AppendLine("        }");
			code.AppendLine();
			code.AppendLine("        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)");
			code.AppendLine("        {");
			code.AppendLine("            return new " + NameSingular + "DataRow(builder);");
			code.AppendLine("        }");
			code.AppendLine();
			code.AppendLine("        public new " + NameSingular + "DataRow NewRow()");
			code.AppendLine("        {");
			code.AppendLine("            return (" + NameSingular + "DataRow)base.NewRow();");
			code.AppendLine("        }");
			code.AppendLine();
			code.AppendLine("        public " + NameSingular + "DataRow this[int index]");
			code.AppendLine("        {");
			code.AppendLine("            get { return (" + NameSingular + "DataRow)Rows[index]; }");
			code.AppendLine("        }");
			code.AppendLine("    }");

			return code.ToString();
		}

		public string GenerateGetDataMethods()
		{
			StringBuilder code = new StringBuilder();
			bool firstMethod = true;
			string dataTableVarName;
			string comma;

			#region Get Record By Primary Key

			if (PrimaryKey != null) {
				string varName = EasyBaseSystem.ToCamelCase(PrimaryKey.FieldName);
				dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

				code.AppendLine("        public " + NameSingular + " Get" + NameSingular + "(" + PrimaryKey.FieldType + " " + varName + ")");
				code.AppendLine("        {");

				code.AppendLine("            " + NameSingular + "Table " + dataTableVarName + " = new " + NameSingular + "Table();");

				code.Append("            string sql = \"select ");

				comma = "";
				foreach (Field field in AllFields) {
					code.Append(comma);
					code.Append(field.FieldName);
					comma = ", ";
				}

				code.Append(" from ");
				code.Append(NamePlural);

				if (IsView) {
					code.Append("View");
				}

				code.Append(" where ");
				code.Append(PrimaryKey.FieldName);
				code.Append(" = @");
				code.Append(varName);
				code.AppendLine("\";");

				code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql, " + varName + ");");
				code.AppendLine();
				code.AppendLine("            if (" + dataTableVarName + ".Rows.Count > 0) {");
				code.AppendLine("                return new " + NameSingular + "(" + dataTableVarName + "[0]);");
				code.AppendLine("            }");
				code.AppendLine();
				code.AppendLine("            return null;");
				code.AppendLine("        }");

				firstMethod = false;
			}

			#endregion

			#region Get Records By Foreign Keys

			if (ForeignKeys.Count > 1) {
				if (!ForeignKeysFormUniqueKey) {

					dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

					if (!firstMethod) {
						code.AppendLine();
					}

					code.Append("        public " + NameSingular + "Collection Get" + NamePlural + "By");

					string and = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						code.Append(and + field.FieldName);
						and = "And";
					}

					code.Append("(");

					comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
						code.Append(comma + field.FieldType + " " + varName);
						comma = ", ";
					}

					code.AppendLine(")");

					code.AppendLine("        {");
					code.Append("            return Get" + NamePlural + "By");

					and = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						code.Append(and + field.FieldName);
						and = "And";
					}

					code.Append("(");

					comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
						code.Append(comma + varName);
						comma = ", ";
					}

					code.AppendLine(", null);");

					code.AppendLine("        }");

					firstMethod = false;
				}
			}

			#endregion

			#region Get Records By Foreign Keys Using Order

			if (ForeignKeys.Count > 1) {
				if (!ForeignKeysFormUniqueKey) {

					dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

					if (!firstMethod) {
						code.AppendLine();
					}

					code.Append("        public " + NameSingular + "Collection Get" + NamePlural + "By");

					string and = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						code.Append(and + field.FieldName);
						and = "And";
					}

					code.Append("(");

					comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
						code.Append(comma + field.FieldType + " " + varName);
						comma = ", ";
					}

					code.AppendLine(", SortOrder sortOrder)");

					code.AppendLine("        {");
					code.AppendLine("            " + NameSingular + "Table " + dataTableVarName + " = new " + NameSingular + "Table();");

					code.Append("            string sql = \"select ");

					comma = "";
					foreach (Field field in AllFields) {
						code.Append(comma);
						code.Append(field.FieldName);
						comma = ", ";
					}

					code.Append(" from ");
					code.Append(NamePlural);

					if (IsView) {
						code.Append("View");
					}

					code.Append(" where ");

					and = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);

						code.Append(and);
						code.Append(field.FieldName);
						code.Append(" = @");
						code.Append(varName);
						and = " and ";
					}

					code.AppendLine("\";");
					code.AppendLine();
					code.AppendLine("            if (sortOrder != null) {");
					code.AppendLine("                sql += \" order by \" + sortOrder.ToSqlString();");
					code.AppendLine("            }");
					code.AppendLine();

					code.Append("            Connection.GetTable(" + dataTableVarName + ", sql, ");

					comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);

						code.Append(comma);
						code.Append(varName);
						comma = ", ";
					}

					code.AppendLine(");");
					code.AppendLine();
					code.AppendLine("            if (" + dataTableVarName + ".Rows.Count > 0) {");
					code.AppendLine("                return " + NameSingular + "Collection.Create" + NameSingular + "Collection(" + dataTableVarName + ");");
					code.AppendLine("            }");
					code.AppendLine();
					code.AppendLine("            return new " + NameSingular + "Collection();");

					code.AppendLine("        }");

					firstMethod = false;
				}
			}

			#endregion

			#region Get Record By Unique Keys

			foreach (UniqueKey uniqueKey in UniqueKeys) {
				dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

				if (!firstMethod) {
					code.AppendLine();
				}

				code.Append("        public " + NameSingular + " Get" + NameSingular + "By");

				string and = "";
				foreach (Field field in uniqueKey.Fields) {
					code.Append(and + field.FieldName);
					and = "And";
				}

				code.Append("(");

				comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
					code.Append(comma + field.FieldType + " " + varName);
					comma = ", ";
				}

				code.AppendLine(")");

				code.AppendLine("        {");
				code.AppendLine("            " + NameSingular + "Table " + dataTableVarName + " = new " + NameSingular + "Table();");

				code.Append("            string sql = \"select ");

				comma = "";
				foreach (Field field in AllFields) {
					code.Append(comma);
					code.Append(field.FieldName);
					comma = ", ";
				}

				code.Append(" from ");
				code.Append(NamePlural);

				if (IsView) {
					code.Append("View");
				}

				code.Append(" where ");

				and = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);

					code.Append(and);
					code.Append(field.FieldName);
					code.Append(" = @");
					code.Append(varName);
					and = " and ";
				}

				code.AppendLine("\";");

				code.Append("            Connection.GetTable(" + dataTableVarName + ", sql, ");

				comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);

					code.Append(comma);
					code.Append(varName);
					comma = ", ";
				}
				
				code.AppendLine(");");
				code.AppendLine();
				code.AppendLine("            if (" + dataTableVarName + ".Rows.Count > 0) {");
				code.AppendLine("                return new " + NameSingular + "(" + dataTableVarName + "[0]);");
				code.AppendLine("            }");
				code.AppendLine();
				code.AppendLine("            return null;");

				code.AppendLine("        }");

				firstMethod = false;
			}

			#endregion

			#region Get Records By Each Field

			string dataTableTypeName;

			foreach (Field field in AllFields) {
				if (!(field is PrimaryKeyField)) {

					dataTableTypeName = NameSingular + "Table";
					dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

					if (!firstMethod) {
						code.AppendLine();
					}

					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);

					code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "By" + field.FieldName + "(" + field.FieldType + " " + varName + ")");
					code.AppendLine("        {");
					code.AppendLine("            return Get" + NamePlural + "By" + field.FieldName + "(" + varName + ", null);");
					code.AppendLine("        }");

					firstMethod = false;
				}
			}

			#endregion

			#region Get Records By Each Field Using Order

			foreach (Field field in AllFields) {
				if (!(field is PrimaryKeyField)) {

					dataTableTypeName = NameSingular + "Table";
					dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

					if (!firstMethod) {
						code.AppendLine();
					}

					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);

					code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "By" + field.FieldName + "(" + field.FieldType + " " + varName + ", SortOrder sortOrder)");
					code.AppendLine("        {");
					code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
					code.Append("            string sql = \"select ");

					comma = "";
					foreach (Field entityField in AllFields) {
						code.Append(comma);
						code.Append(entityField.FieldName);
						comma = ", ";
					}

					code.Append(" from ");
					code.Append(NamePlural);

					if (IsView) {
						code.Append("View");
					}

					code.Append(" where ");
					code.Append(field.FieldName);
					code.Append(" = @");
					code.Append(varName);
					code.AppendLine("\";");

					code.AppendLine();
					code.AppendLine("            if (sortOrder != null) {");
					code.AppendLine("                sql += \" order by \" + sortOrder.ToSqlString();");
					code.AppendLine("            }");
					code.AppendLine();

					code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql, " + varName + ");");
					code.AppendLine("            return " + NameSingular + "Collection.Create" + NameSingular + "Collection(" + dataTableVarName + ");");
					code.AppendLine("        }");

					firstMethod = false;

				}
			}

			#endregion

			#region Get All Records General

			dataTableTypeName = NameSingular + "Table";
			dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "()");
			code.AppendLine("        {");
			code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
			code.Append("            string sql = \"select ");

			comma = "";
			foreach (Field entityField in AllFields) {
				code.Append(comma);
				code.Append(entityField.FieldName);
				comma = ", ";
			}

			code.Append(" from ");
			code.Append(NamePlural);

			if (IsView) {
				code.Append("View");
			}

			code.AppendLine("\";");

			code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql);");
			code.AppendLine("            return " + NameSingular + "Collection.Create" + NameSingular + "Collection(" + dataTableVarName + ");");
			code.Append("        }");

			firstMethod = false;

			#endregion

			#region Get Records Using Condition

			dataTableTypeName = NameSingular + "Table";
			dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "(Condition condition)");
			code.AppendLine("        {");
			code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
			code.Append("            string sql = \"select ");

			comma = "";
			foreach (Field entityField in AllFields) {
				code.Append(comma);
				code.Append(entityField.FieldName);
				comma = ", ";
			}

			code.Append(" from ");
			code.Append("[");
			code.Append(NamePlural);

			if (IsView) {
				code.Append("View");
			}
			code.Append("]");

			code.AppendLine(" where \" + condition.ToSqlString();");

			code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql);");
			code.AppendLine("            return " + NameSingular + "Collection.Create" + NameSingular + "Collection(" + dataTableVarName + ");");
			code.Append("        }");

			firstMethod = false;

			#endregion

			#region Get Records Using Sort Order

			dataTableTypeName = NameSingular + "Table";
			dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "(SortOrder sortOrder)");
			code.AppendLine("        {");
			code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
			code.Append("            string sql = \"select ");

			comma = "";
			foreach (Field entityField in AllFields) {
				code.Append(comma);
				code.Append(entityField.FieldName);
				comma = ", ";
			}

			code.Append(" from ");
			code.Append("[");
			code.Append(NamePlural);

			if (IsView) {
				code.Append("View");
			}
			code.Append("]");

			code.AppendLine(" order by \" + sortOrder.ToSqlString();");

			code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql);");
			code.AppendLine("            return " + NameSingular + "Collection.Create" + NameSingular + "Collection(" + dataTableVarName + ");");
			code.Append("        }");

			firstMethod = false;

			#endregion

			#region Get Records Using Condition And Order

			dataTableTypeName = NameSingular + "Table";
			dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "(Condition condition, SortOrder sortOrder)");
			code.AppendLine("        {");
			code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
			code.Append("            string sql = \"select ");

			comma = "";
			foreach (Field entityField in AllFields) {
				code.Append(comma);
				code.Append(entityField.FieldName);
				comma = ", ";
			}

			code.Append(" from ");
			code.Append("[");
			code.Append(NamePlural);

			if (IsView) {
				code.Append("View");
			}
			code.Append("]");

			code.AppendLine(" where \" + condition.ToSqlString() + \" order by \" + sortOrder.ToSqlString();");

			code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql);");
			code.AppendLine("            return " + NameSingular + "Collection.Create" + NameSingular + "Collection(" + dataTableVarName + ");");
			code.Append("        }");

			firstMethod = false;

			#endregion

            #region Get Table

            dataTableTypeName = NameSingular + "Table";
            dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

            if (!firstMethod)
            {
                code.AppendLine();
            }

            code.AppendLine("        public DataTable Get" + NamePlural + "Table(params string[] sColumns)");
            code.AppendLine("        {");
            code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
            code.AppendLine("            StringBuilder sql = new StringBuilder(\"select \");");
            code.AppendLine();
            code.AppendLine("            if (sColumns.Length > 0) {");
            code.AppendLine("                string comma = \"\";");
            code.AppendLine("                foreach (string sColumn in sColumns) {");
            code.AppendLine("                    sql.Append(comma);");
            code.AppendLine("                    sql.Append(sColumn);");
            code.AppendLine("                    comma = \",\";");
            code.AppendLine("                }");
            code.AppendLine("            }");
            code.AppendLine("            else {");
            code.AppendLine("                sql.Append(\"*\");");
            code.AppendLine("            }");
            code.AppendLine();
            code.Append("");
            code.Append("            sql.Append(\" from ");
            code.Append("[");
            code.Append(NamePlural);

            if (IsView)
            {
                code.Append("View");
            }
            code.AppendLine("];\");");

            code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql.ToString());");
            code.AppendLine("            return " + dataTableVarName + ";");
            code.Append("        }");

            firstMethod = false;

            #endregion

            #region Get Table Using Condition

            dataTableTypeName = NameSingular + "Table";
            dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

            if (!firstMethod) {
                code.AppendLine();
            }

            code.AppendLine("        public DataTable Get" + NamePlural + "Table(Condition condition, params string[] sColumns)");
            code.AppendLine("        {");
            code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
            code.AppendLine("            StringBuilder sql = new StringBuilder(\"select \");");
            code.AppendLine();
            code.AppendLine("            if (sColumns.Length > 0) {");
            code.AppendLine("                string comma = \"\";");
            code.AppendLine("                foreach (string sColumn in sColumns) {");
            code.AppendLine("                    sql.Append(comma);");
            code.AppendLine("                    sql.Append(sColumn);");
            code.AppendLine("                    comma = \",\";");
            code.AppendLine("                }");
            code.AppendLine("            }");
            code.AppendLine("            else {");
            code.AppendLine("                sql.Append(\"*\");");
            code.AppendLine("            }");
            code.AppendLine();
            code.Append("");
            code.Append("            sql.Append(\" from ");
            code.Append("[");
            code.Append(NamePlural);

            if (IsView) {
                code.Append("View");
            }
            code.Append("]");

            code.AppendLine(" where \" + condition.ToSqlString());");

            code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql.ToString());");
            code.AppendLine("            return " + dataTableVarName + ";");
            code.Append("        }");

            firstMethod = false;

            #endregion

            #region Get Table Using Condition and Sort Order

            dataTableTypeName = NameSingular + "Table";
            dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

            if (!firstMethod) {
                code.AppendLine();
            }

            code.AppendLine("        public DataTable Get" + NamePlural + "Table(Condition condition, SortOrder sortOrder, params string[] sColumns)");
            code.AppendLine("        {");
            code.AppendLine("            " + dataTableTypeName + " " + dataTableVarName + " = new " + dataTableTypeName + "();");
            code.AppendLine("            StringBuilder sql = new StringBuilder(\"select \");");
            code.AppendLine();
            code.AppendLine("            if (sColumns.Length > 0) {");
            code.AppendLine("                string comma = \"\";");
            code.AppendLine("                foreach (string sColumn in sColumns) {");
            code.AppendLine("                    sql.Append(comma);");
            code.AppendLine("                    sql.Append(sColumn);");
            code.AppendLine("                    comma = \",\";");
            code.AppendLine("                }");
            code.AppendLine("            }");
            code.AppendLine("            else {");
            code.AppendLine("                sql.Append(\"*\");");
            code.AppendLine("            }");
            code.AppendLine();
            code.Append("");
            code.Append("            sql.Append(\" from ");
            code.Append("[");
            code.Append(NamePlural);

            if (IsView) {
                code.Append("View");
            }
            code.Append("]");

            code.AppendLine(" where \" + condition.ToSqlString());");
            code.AppendLine("            sql.Append(\" order by \" + sortOrder.ToSqlString());");

            code.AppendLine("            Connection.GetTable(" + dataTableVarName + ", sql.ToString());");
            code.AppendLine("            return " + dataTableVarName + ";");
            code.Append("        }");

            firstMethod = false;

            #endregion

            return code.ToString();
		}

		public string GenerateSaveDataMethods()
		{
			StringBuilder code = new StringBuilder();
			string varName = EasyBaseSystem.ToCamelCase(NameSingular);

			StringBuilder commaNameArgumentList = new StringBuilder();
			string comma = "";
			foreach (Field field in AllFields) {
				if ((PrimaryKey != null && !PrimaryKey.IsIdentity) || (!(field is PrimaryKeyField) && !(field is ViewField))) {
					commaNameArgumentList.Append(comma + field.FieldName);
					comma = ", ";
				}
			}

			StringBuilder commaParamArgumentList = new StringBuilder();
			comma = "";
			foreach (Field field in AllFields) {
				if ((PrimaryKey != null && !PrimaryKey.IsIdentity) || (!(field is PrimaryKeyField) && !(field is ViewField))) {
					commaParamArgumentList.Append(comma + "@" + EasyBaseSystem.ToCamelCase(field.FieldName));
					comma = ", ";
				}
			}

			StringBuilder commaVarArgumentList = new StringBuilder();
			comma = "";
			foreach (Field field in AllFields) {
				if (!(field is PrimaryKeyField) && !(field is ViewField)) {
					commaVarArgumentList.Append(comma + varName + "." + field.FieldName);
					comma = ", ";
				}
			}

			StringBuilder commaAssignmentArgumentList = new StringBuilder();
			comma = "";
			foreach (Field field in AllFields) {
				if (!(field is PrimaryKeyField) && !(field is ViewField)) {
					commaAssignmentArgumentList.Append(comma + field.FieldName + "=@" + EasyBaseSystem.ToCamelCase(field.FieldName));
					comma = ", ";
				}
			}

			if (PrimaryKey != null) {
				if (PrimaryKey.IsIdentity) {

					code.AppendLine("        public void Save(" + NameSingular + " " + varName + ")");
					code.AppendLine("        {");
					code.AppendLine("            if (" + varName + "." + PrimaryKey.FieldName + " == " + EasyBaseSystem.GetDefaultDatabaseValue(PrimaryKey.FieldType, PrimaryKey.AllowNull) + ") {");
					code.AppendLine("                string sql = \"insert into [" + NamePlural + "] (" + commaNameArgumentList + ") values (" + commaParamArgumentList + ")\";");
					code.AppendLine();
					code.AppendLine("                Connection.Execute(sql, " + commaVarArgumentList + ");");
					code.AppendLine("                " + varName + "." + PrimaryKey.FieldName + " = " + EasyBaseSystem.GetConversion(PrimaryKey.FieldType) + " (Connection.GetScalar(\"select @@identity\"));");
					code.AppendLine("            }");
					code.AppendLine("            else {");
					code.AppendLine("                string sql = \"update [" + NamePlural + "] set " + commaAssignmentArgumentList + " where [" + PrimaryKey.FieldName + "] = @" + EasyBaseSystem.ToCamelCase(PrimaryKey.FieldName) + "\";");
					code.AppendLine();
					code.AppendLine("                int affectedRows = Connection.Execute(sql, " + commaVarArgumentList + ", " + varName + "." + PrimaryKey.FieldName + ");");
					code.AppendLine("                if (affectedRows == 0) {");
					code.AppendLine("                    throw new DatabaseEntityDoesNotExistException(\"" + NameSingular + " \" + " + varName + "." + PrimaryKey.FieldName + " + \" does not exist in the database.\");");
					code.AppendLine("                }");
					code.AppendLine("            }");
					code.AppendLine("        }");

				}
				else {

					StringBuilder commaVarArgumentList2 = new StringBuilder();
					comma = "";
					foreach (Field field in AllFields) {
						if ((PrimaryKey != null && !PrimaryKey.IsIdentity) || (!(field is PrimaryKeyField) && !(field is ViewField))) {
							commaVarArgumentList2.Append(comma + varName + "." + field.FieldName);
							comma = ", ";
						}
					}

					code.AppendLine("        public void Save(" + NameSingular + " " + varName + ")");
					code.AppendLine("        {");
					code.AppendLine("            string sql = \"update [" + NamePlural + "] set " + commaAssignmentArgumentList + " where [" + PrimaryKey.FieldName + "] = @" + EasyBaseSystem.ToCamelCase(PrimaryKey.FieldName) + "\";");
					code.AppendLine();
					code.AppendLine("            int affectedRows = Connection.Execute(sql, " + commaVarArgumentList + ", " + varName + "." + PrimaryKey.FieldName + ");");
					code.AppendLine();
					code.AppendLine("            if (affectedRows == 0) {");
					code.AppendLine("                sql = \"insert into [" + NamePlural + "] (" + commaNameArgumentList + ") values (" + commaParamArgumentList + ")\";");
					code.AppendLine("                Connection.Execute(sql, " + commaVarArgumentList2 + ");");
					code.AppendLine("            }");
					code.AppendLine("        }");

				}
			}

			return code.ToString();
		}

		public string GenerateDeleteDataMethods()
		{
			StringBuilder code = new StringBuilder();
			bool firstMethod = true;

			// Primary Key Method

			if (PrimaryKey != null) {
				if (!firstMethod) {
					code.AppendLine();
				}

				code.AppendLine("        public bool Delete" + NameSingular + "(" + PrimaryKey.FieldType + " " + PrimaryKey.VarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            string sql = \"delete from " + NamePlural + " where " + PrimaryKey.FieldName + " = @" + PrimaryKey.VarName + "\";");
				code.AppendLine("            return Convert.ToInt32(Connection.Execute(sql, " + PrimaryKey.VarName + ")) > 0;");
				code.AppendLine("        }");

				firstMethod = false;
			}

            // Delete All Method

            // Foreign Key Method
            if (!firstMethod) {
                code.AppendLine();
            }

            code.AppendLine("        public int DeleteAll" + NamePlural + "()");
            code.AppendLine("        {");
            code.AppendLine("            string sql = \"delete from " + NamePlural + "\";");
            code.AppendLine("            return Convert.ToInt32(Connection.Execute(sql));");
            code.AppendLine("        }");

            firstMethod = false;

			foreach (ForeignKeyField foreignKey in ForeignKeys) {
				if (!firstMethod) {
					code.AppendLine();
				}

				code.AppendLine("        public int Delete" + NamePlural + "By" + foreignKey.FieldName + "(" + foreignKey.FieldType + " " + foreignKey.VarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            string sql = \"delete from " + NamePlural + " where " + foreignKey.FieldName + " = @" + foreignKey.VarName + "\";");
				code.AppendLine("            return Convert.ToInt32(Connection.Execute(sql, " + foreignKey.VarName + "));");
				code.AppendLine("        }");

				firstMethod = false;
			}

			if (!firstMethod) {
				code.AppendLine();
			}

			// Foreign keys

			if (ForeignKeys.Count > 1) {
				if (!ForeignKeysFormUniqueKey) {

					if (!firstMethod) {
						code.AppendLine();
					}

					StringBuilder andArgumentList = new StringBuilder();
					string and = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						andArgumentList.Append(and + field.FieldName);
						and = "And";
					}

					StringBuilder commaTypeArgumentList = new StringBuilder();
					string comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
						commaTypeArgumentList.Append(comma + field.FieldType + " " + varName);
						comma = ", ";
					}

					StringBuilder commaNameArgumentList = new StringBuilder();
					comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
						commaNameArgumentList.Append(comma + varName);
						comma = ", ";
					}

					StringBuilder sqlArgumentList = new StringBuilder();
					and = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						sqlArgumentList.Append(and + field.FieldName + " = @" + field.VarName);
						and = " and ";
					}

					code.AppendLine("        public bool Delete" + NamePlural + "By" + andArgumentList + "(" + commaTypeArgumentList + ")");
					code.AppendLine("        {");
					code.AppendLine("            string sql = \"delete from " + NamePlural + " where " + sqlArgumentList + "\";");
					code.AppendLine("            return Convert.ToInt32(Connection.Execute(sql, " + commaNameArgumentList + ")) == 1;");
					code.AppendLine("        }");

					firstMethod = false;
				}
			}

			// Unique keys

			foreach (UniqueKey uniqueKey in UniqueKeys) {
				if (!firstMethod) {
					code.AppendLine();
				}

				StringBuilder andArgumentList = new StringBuilder();
				string and = "";
				foreach (Field field in uniqueKey.Fields) {
					andArgumentList.Append(and + field.FieldName);
					and = "And";
				}

				StringBuilder commaTypeArgumentList = new StringBuilder();
				string comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
					commaTypeArgumentList.Append(comma + field.FieldType + " " + varName);
					comma = ", ";
				}

				StringBuilder commaNameArgumentList = new StringBuilder();
				comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
					commaNameArgumentList.Append(comma + varName);
					comma = ", ";
				}

				StringBuilder sqlArgumentList = new StringBuilder();
				and = "";
				foreach (Field field in uniqueKey.Fields) {
					sqlArgumentList.Append(and + field.FieldName + " = @" + field.VarName);
					and = " and ";
				}

				code.AppendLine("        public bool Delete" + NameSingular + "By" + andArgumentList + "(" + commaTypeArgumentList + ")");
				code.AppendLine("        {");
				code.AppendLine("            string sql = \"delete from " + NamePlural + " where " + sqlArgumentList + "\";");
				code.AppendLine("            return Convert.ToInt32(Connection.Execute(sql, " + commaNameArgumentList + ")) == 1;");
				code.AppendLine("        }");

				firstMethod = false;
			}

			// Condition Delete Methods

			code.AppendLine("        public int Delete" + NamePlural + "(Condition condition)");
			code.AppendLine("        {");
			code.AppendLine("            string sql = \"delete from " + NamePlural + " where \" + condition.ToSqlString();");
			code.AppendLine("            return Convert.ToInt32(Connection.Execute(sql));");
			code.AppendLine("        }");

			return code.ToString();
		}

		public string GenerateCountDataMethods()
		{
			StringBuilder code = new StringBuilder();
			bool firstMethod = true;
			string tableName = IsView ? NamePlural + "View" : NamePlural;

            // CountAll Methods

            code.AppendLine("        public int Count" + NamePlural + "()");
            code.AppendLine("        {");
            code.AppendLine("            string sql = \"select count(*) from " + tableName + "\";");
            code.AppendLine("            return Convert.ToInt32(Connection.GetScalar(sql));");
            code.AppendLine("        }");

			// CountBy Methods

			foreach (Field field in AllFields) {
				code.AppendLine();

				code.AppendLine("        public int Count" + NamePlural + "By" + field.FieldName + "(" + field.FieldType + " " + field.VarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            string sql = \"select count(*) from " + tableName + " where " + field.FieldName + " = @" + field.VarName + "\";");
				code.AppendLine("            return Convert.ToInt32(Connection.GetScalar(sql, " + field.VarName + "));");
				code.AppendLine("        }");

				firstMethod = false;
			}

			code.AppendLine();

			code.AppendLine("        public int Count" + NamePlural + "(Condition condition)");
			code.AppendLine("        {");
			code.AppendLine("            string sql = \"select count(*) from " + tableName + " where \" + condition.ToSqlString();");
			code.AppendLine("            return Convert.ToInt32(Connection.GetScalar(sql));");
			code.AppendLine("        }");

			return code.ToString();
		}

		public string GenerateSaveBusinessMethods()
		{
			StringBuilder code = new StringBuilder();

			if (PrimaryKey != null) {
				string varName = EasyBaseSystem.ToCamelCase(NameSingular);

				code.AppendLine("        public void Save(" + NameSingular + " " + varName + ")");
				code.AppendLine("        {");
				code.AppendLine("            Database.Save(" + varName + ");");
				code.AppendLine("        }");
			}

			return code.ToString();
		}

		public string GenerateDeleteBusinessMethods()
		{
			StringBuilder code = new StringBuilder();
			bool firstMethod = true;

			// Primary Key Methods

			if (PrimaryKey != null) {
				if (!firstMethod) {
					code.AppendLine();
				}

				code.AppendLine("        public bool Delete" + NameSingular + "(" + PrimaryKey.FieldType + " " + PrimaryKey.VarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            return Database.Delete" + NameSingular + "(" + PrimaryKey.VarName + ");");
				code.AppendLine("        }");

				firstMethod = false;
			}

            // Delete All Methods
            if (!firstMethod) {
                code.AppendLine();
            }

            code.AppendLine("        public int DeleteAll" + NamePlural + "()");
            code.AppendLine("        {");
            code.AppendLine("            return Database.DeleteAll" + NamePlural + "();");
            code.AppendLine("        }");

            firstMethod = false;

			// Foreign Key Methods

			foreach (ForeignKeyField foreignKey in ForeignKeys) {
				if (!firstMethod) {
					code.AppendLine();
				}

				code.AppendLine("        public int Delete" + NamePlural + "By" + foreignKey.FieldName + "(" + foreignKey.FieldType + " " + foreignKey.VarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            return Database.Delete" + NamePlural + "By" + foreignKey.FieldName + "(" + foreignKey.VarName + ");");
				code.AppendLine("        }");

				firstMethod = false;
			}

			// Foreign Key

			if (ForeignKeys.Count > 1) {
				if (!ForeignKeysFormUniqueKey) {
					if (!firstMethod) {
						code.AppendLine();
					}

					StringBuilder andArgumentList = new StringBuilder();
					string and = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						andArgumentList.Append(and + field.FieldName);
						and = "And";
					}

					StringBuilder commaTypeArgumentList = new StringBuilder();
					string comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
						commaTypeArgumentList.Append(comma + field.FieldType + " " + varName);
						comma = ", ";
					}

					StringBuilder commaNameArgumentList = new StringBuilder();
					comma = "";
					foreach (ForeignKeyField field in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
						commaNameArgumentList.Append(comma + varName);
						comma = ", ";
					}

					code.AppendLine("        public bool Delete" + NamePlural + "By" + andArgumentList + "(" + commaTypeArgumentList + ")");
					code.AppendLine("        {");
					code.AppendLine("            return Database.Delete" + NamePlural + "By" + andArgumentList + "(" + commaNameArgumentList + ");");
					code.AppendLine("        }");

					firstMethod = false;
				}
			}

			// Unique keys, Like: DeleteUser(int no);

			foreach (UniqueKey uniqueKey in UniqueKeys) {
				if (!firstMethod) {
					code.AppendLine();
				}

				StringBuilder andArgumentList = new StringBuilder();
				string and = "";
				foreach (Field field in uniqueKey.Fields) {
					andArgumentList.Append(and + field.FieldName);
					and = "And";
				}

				StringBuilder commaTypeArgumentList = new StringBuilder();
				string comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
					commaTypeArgumentList.Append(comma + field.FieldType + " " + varName);
					comma = ", ";
				}

				StringBuilder commaNameArgumentList = new StringBuilder();
				comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
					commaNameArgumentList.Append(comma + varName);
					comma = ", ";
				}

				code.AppendLine("        public bool Delete" + NameSingular + "By" + andArgumentList + "(" + commaTypeArgumentList + ")");
				code.AppendLine("        {");
				code.AppendLine("            return Database.Delete" + NameSingular + "By" + andArgumentList + "(" + commaNameArgumentList + ");");
				code.AppendLine("        }");

				firstMethod = false;
			}

			// Rest of methods

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public int Delete" + NamePlural + "(Condition condition)");
			code.AppendLine("        {");
			code.AppendLine("            return Database.Delete" + NamePlural + "(condition);");
			code.AppendLine("        }");

			return code.ToString();
		}

		public string GenerateCountBusinessMethods()
		{
			StringBuilder code = new StringBuilder();

            code.AppendLine("        public int Count" + NamePlural + "()");
            code.AppendLine("        {");
            code.AppendLine("            return Database.Count" + NamePlural + "();");
            code.AppendLine("        }");

            // CountBy Methods

			foreach (Field field in AllFields) {
				code.AppendLine();

				code.AppendLine("        public int Count" + NamePlural + "By" + field.FieldName + "(" + field.FieldType + " " + field.VarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            return Database.Count" + NamePlural + "By" + field.FieldName + "(" + field.VarName + ");");
				code.AppendLine("        }");
			}

			code.AppendLine();

			// Condition Methods

			code.AppendLine("        public int Count" + NamePlural + "(Condition condition)");
			code.AppendLine("        {");
			code.AppendLine("            return Database.Count" + NamePlural + "(condition);");
			code.AppendLine("        }");

			return code.ToString();
		}

		public string GenerateGetBusinessMethods()
		{
			StringBuilder code = new StringBuilder();
			bool firstMethod = true;

			string classVarName = EasyBaseSystem.ToCamelCase(BusinessClassName);

			// Primary key method, like GetUser(int no)

			if (PrimaryKey != null) {
				string fieldVarName = EasyBaseSystem.ToCamelCase(PrimaryKey.FieldName);

				code.AppendLine("        public " + BusinessClassName + " Get" + NameSingular + "(" + PrimaryKey.FieldType + " " + fieldVarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            return Database.Get" + NameSingular + "(" + fieldVarName + ");");
				code.AppendLine("        }");

                // Primary key full method, like GetFullUser(int no)

                if (HasParents || HasChildren) {
                    code.AppendLine();
                    code.AppendLine("        public " + BusinessClassName + " GetFull" + NameSingular + "(" + PrimaryKey.FieldType + " " + fieldVarName + ")");
                    code.AppendLine("        {");
                    code.AppendLine("            " + BusinessClassName + " " + classVarName + " = Database.Get" + NameSingular + "(" + fieldVarName + ");");
                    code.AppendLine("            if (" + classVarName + " == null) {");
                    code.AppendLine("                return null;");
                    code.AppendLine("            }");

                    if (HasParents) {
                        foreach (ForeignKeyField foreignKeyField in ForeignKeys) {
                            if (foreignKeyField.IsParentConnection) {
                                Entity parent = Parents.First(e => e.NamePlural == foreignKeyField.TargetTableName);
                                code.AppendLine("            " + classVarName + "." + parent.NameSingular + " = Database.Get" + parent.NameSingular + "(" + classVarName + "." + foreignKeyField.TargetFieldName + ");");
                            }
                        }
                    }

                    if (HasChildren) {
                        foreach (Entity child in Children) {
                            code.AppendLine("            " + classVarName + "." + child.NamePlural + " = Database.Get" + child.NamePlural + "By" + NameSingular + PrimaryKey.FieldName + "(" + classVarName + "." + PrimaryKey.FieldName + ");");
                        }
                    }

                    code.AppendLine("            return " + classVarName + ";");
                    code.AppendLine("        }");
                }

				firstMethod = false;
			}

			// Foreign keys
			if (ForeignKeys.Count > 1) {
				if (!ForeignKeysFormUniqueKey) {
					if (!firstMethod) {
						code.AppendLine();
					}

					StringBuilder andArgumentList = new StringBuilder();
					string and = "";
					foreach (ForeignKeyField foreignKey in ForeignKeys) {
						andArgumentList.Append(and + foreignKey.FieldName);
						and = "And";
					}

					StringBuilder commaTypeArgumentList = new StringBuilder();
					string comma = "";
					foreach (ForeignKeyField foreignKey in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(foreignKey.FieldName);
						commaTypeArgumentList.Append(comma + foreignKey.FieldType + " " + varName);
						comma = ", ";
					}

					StringBuilder commaNameArgumentList = new StringBuilder();
					comma = "";
					foreach (ForeignKeyField foreignKey in ForeignKeys) {
						string varName = EasyBaseSystem.ToCamelCase(foreignKey.FieldName);
						commaNameArgumentList.Append(comma + varName);
						comma = ", ";
					}

					code.AppendLine("        public " + BusinessClassName + "Collection Get" + NamePlural + "By" + andArgumentList + "(" + commaTypeArgumentList + ")");
					code.AppendLine("        {");
					code.AppendLine("            return Database.Get" + NamePlural + "By" + andArgumentList + "(" + commaNameArgumentList + ");");
					code.AppendLine("        }");

					code.AppendLine();

					code.AppendLine("        public " + BusinessClassName + "Collection Get" + NamePlural + "By" + andArgumentList + "(" + commaTypeArgumentList + ", SortOrder sortOrder)");
					code.AppendLine("        {");
					code.AppendLine("            return Database.Get" + NamePlural + "By" + andArgumentList + "(" + commaNameArgumentList + ", sortOrder);");
					code.AppendLine("        }");
				}
			}

			// Unique keys, Like: GetUser(int no);

			foreach (UniqueKey uniqueKey in UniqueKeys) {
				if (!firstMethod) {
					code.AppendLine();
				}

				StringBuilder andArgumentList = new StringBuilder();
				string and = "";
				foreach (Field field in uniqueKey.Fields) {
					andArgumentList.Append(and + field.FieldName);
					and = "And";
				}

				StringBuilder commaTypeArgumentList = new StringBuilder();
				string comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
					commaTypeArgumentList.Append(comma + field.FieldType + " " + varName);
					comma = ", ";
				}

				StringBuilder commaNameArgumentList = new StringBuilder();
				comma = "";
				foreach (Field field in uniqueKey.Fields) {
					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);
					commaNameArgumentList.Append(comma + varName);
					comma = ", ";
				}

				code.AppendLine("        public " + BusinessClassName + " Get" + NameSingular + "By" + andArgumentList + "(" + commaTypeArgumentList + ")");
				code.AppendLine("        {");
				code.AppendLine("            return Database.Get" + NameSingular + "By" + andArgumentList + "(" + commaNameArgumentList + ");");
				code.AppendLine("        }");

				firstMethod = false;
			}

			// All methods that return a collection, like: GetUsersByName(string name)

			foreach (Field field in AllFields) {
				if (!(field is PrimaryKeyField)) {

					string dataTableTypeName = NameSingular + "Table";
					string dataTableVarName = EasyBaseSystem.ToCamelCase(NameSingular + "Table");

					if (!firstMethod) {
						code.AppendLine();
					}

					string varName = EasyBaseSystem.ToCamelCase(field.FieldName);

					code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "By" + field.FieldName + "(" + field.FieldType + " " + varName + ")");
					code.AppendLine("        {");
					code.AppendLine("            return Database.Get" + NamePlural + "By" + field.FieldName + "(" + varName + ");");
					code.AppendLine("        }");

					code.AppendLine();

					code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "By" + field.FieldName + "(" + field.FieldType + " " + varName + ", SortOrder sortOrder)");
					code.AppendLine("        {");
					code.AppendLine("            return Database.Get" + NamePlural + "By" + field.FieldName + "(" + varName + ", sortOrder);");
					code.AppendLine("        }");

					firstMethod = false;
				}
			}

			// Methods like: GetUsers();

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "()");
			code.AppendLine("        {");
			code.AppendLine("            return Database.Get" + NamePlural + "();");
			code.Append("        }");

			firstMethod = false;

			// Methods like: GetUsers(Contition condition);

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "(Condition condition)");
			code.AppendLine("        {");
			code.AppendLine("            return Database.Get" + NamePlural + "(condition);");
			code.Append("        }");

			firstMethod = false;

            // Methods like: GetUsersTable(params string[] sColumns);

            if (!firstMethod) {
                code.AppendLine();
            }

            code.AppendLine("        public DataTable Get" + NamePlural + "Table(params string[] sColumns)");
            code.AppendLine("        {");
            code.AppendLine("            return Database.Get" + NamePlural + "Table(sColumns);");
            code.Append("        }");

            firstMethod = false;

            // Methods like: GetUsersTable(Contition condition, params string[] sColumns);

            if (!firstMethod)
            {
                code.AppendLine();
            }

            code.AppendLine("        public DataTable Get" + NamePlural + "Table(Condition condition, params string[] sColumns)");
            code.AppendLine("        {");
            code.AppendLine("            return Database.Get" + NamePlural + "Table(condition, sColumns);");
            code.Append("        }");

            firstMethod = false;

            // Methods like: GetUsersTable(Contition condition, SortOrder, params string[] sColumns);

            if (!firstMethod) {
                code.AppendLine();
            }

            code.AppendLine("        public DataTable Get" + NamePlural + "Table(Condition condition, SortOrder sortOrder, params string[] sColumns)");
            code.AppendLine("        {");
            code.AppendLine("            return Database.Get" + NamePlural + "Table(condition, sortOrder, sColumns);");
            code.Append("        }");

            firstMethod = false;

            // Methods like: GetUsers(SortOrder order);

            if (!firstMethod) {
                code.AppendLine();
            }

            code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "(SortOrder order)");
            code.AppendLine("        {");
            code.AppendLine("            return Database.Get" + NamePlural + "(order);");
            code.Append("        }");

            firstMethod = false;

            // Methods like: GetUsers(Contition condition, SortOrder order);

			if (!firstMethod) {
				code.AppendLine();
			}

			code.AppendLine("        public " + NameSingular + "Collection Get" + NamePlural + "(Condition condition, SortOrder sortOrder)");
			code.AppendLine("        {");
			code.AppendLine("            return Database.Get" + NamePlural + "(condition, sortOrder);");
			code.Append("        }");

			firstMethod = false;

			return code.ToString();
		}

		public string GenerateBusinessCollection()
		{
			StringBuilder code = new StringBuilder();
			string classVarName = EasyBaseSystem.ToCamelCase(BusinessClassName);

			code.AppendLine("    public class " + NameSingular + "Collection : List<" + BusinessClassName + ">");
			code.AppendLine("    {");
			code.AppendLine("        internal static " + NameSingular + "Collection Create" + NameSingular + "Collection(" + NameSingular + "Table " + classVarName + "Table)");
			code.AppendLine("        {");
			code.AppendLine("            " + NameSingular + "Collection " + classVarName + "Collection = new " + NameSingular + "Collection();");
			code.AppendLine();
			code.AppendLine("            foreach (" + NameSingular + "DataRow " + classVarName + "DataRow in " + classVarName + "Table.Rows) {");
			code.AppendLine("                " + classVarName + "Collection.Add(new " + NameSingular + "(" + classVarName + "DataRow));");
			code.AppendLine("            }");
			code.AppendLine();
			code.AppendLine("            return " + classVarName + "Collection;");
			code.AppendLine("        }");

			// Primary Key Method

			if (PrimaryKey != null) {
				code.AppendLine();
				code.AppendLine("        public " + NameSingular + " Get" + NameSingular + "(" + PrimaryKey.FieldType + " " + PrimaryKey.VarName + ")");
				code.AppendLine("        {");
				code.AppendLine("            foreach (" + NameSingular + " " + classVarName + " in this) {");
				code.AppendLine("                if (" + classVarName + "." + PrimaryKey.FieldName + " == " + PrimaryKey.VarName + ") {");
				code.AppendLine("                    return " + classVarName + ";");
				code.AppendLine("                }");
				code.AppendLine("            }");
				code.AppendLine();
				code.AppendLine("            return null;");
				code.AppendLine("        }");
			}

			// Unique Key Method
			foreach (UniqueKey uniqueKey in UniqueKeys) {
				StringBuilder andArgumentList = new StringBuilder();
				string and = "";
				foreach (Field field in uniqueKey.Fields) {
					andArgumentList.Append(and + field.FieldName);
					and = "And";
				}

				StringBuilder commaTypeArgumentList = new StringBuilder();
				string comma = "";
				foreach (Field field in uniqueKey.Fields) {
					commaTypeArgumentList.Append(comma + field.FieldType + " " + field.VarName);
					comma = ", ";
				}

				StringBuilder conditionArgumentList = new StringBuilder();
				and = "";
				foreach (Field field in uniqueKey.Fields) {
					conditionArgumentList.Append(and + classVarName + "." + field.FieldName + " == " + field.VarName);
					and = " && ";
				}

				code.AppendLine();
				code.AppendLine("        public " + NameSingular + " Get" + NameSingular + "By" + andArgumentList + "(" + commaTypeArgumentList + ")");
				code.AppendLine("        {");
				code.AppendLine("            foreach (" + NameSingular + " " + classVarName + " in this) {");
				code.AppendLine("                if (" + conditionArgumentList + ") {");
				code.AppendLine("                    return " + classVarName + ";");
				code.AppendLine("                }");
				code.AppendLine("            }");
				code.AppendLine();
				code.AppendLine("            return null;");
				code.AppendLine("        }");
			}

			code.AppendLine("    }");

			return code.ToString();
		}
	}
}
