using System;
using System.Collections.Generic;
using System.Text;
using DanielEiserman.DateAndTime;

namespace EasyBase.Classes
{
	public class Condition
	{
        private Condition()
        {
            AndConditions = new List<Condition>();
            OrConditions = new List<Condition>();
        }

		public Condition(string fieldName, CompareOperator compare, int value)
		{
			FieldName = fieldName;
			Operator = compare;
			Value = value;
			DateTimeResolution = DateTimeResolution.Exact;
			Negate = false;

			AndConditions = new List<Condition>();
			OrConditions = new List<Condition>();
		}

		public Condition(string fieldName, CompareOperator compare, DateTime value, DateTimeResolution resolution)
		{
			FieldName = fieldName;
			Operator = compare;
			Value = value;
			DateTimeResolution = resolution;
			Negate = false;

			AndConditions = new List<Condition>();
			OrConditions = new List<Condition>();
		}

		public Condition(string fieldName, CompareOperator compare, bool value)
		{
			FieldName = fieldName;
			Operator = compare;
			Value = value;
			DateTimeResolution = DateTimeResolution.Exact;
			Negate = false;

			AndConditions = new List<Condition>();
			OrConditions = new List<Condition>();
		}

		public Condition(string fieldName, CompareOperator compare, object value)
		{
			if (value == null && compare != CompareOperator.EqualTo && compare != CompareOperator.NotEqualTo) {
				throw new ArgumentException("Comparison with a null value can only be done with compare operator equal to (=) or not equal to (<>).");
			}

			FieldName = fieldName;
			Operator = compare;

			if (value != null && value.GetType().IsEnum) {
				Value = (int)value;
			}
			else {
				Value = value;
			}

			DateTimeResolution = DateTimeResolution.Exact;
			Negate = false;

			AndConditions = new List<Condition>();
			OrConditions = new List<Condition>();
		}

		public static Condition operator &(Condition condition1, Condition condition2)
		{
			//Condition newCondition1 = condition1.Clone();
			//Condition newCondition2 = condition2.Clone();

            Condition newCondition = new Condition();

            newCondition.AndConditions.Add(condition1);
            newCondition.AndConditions.Add(condition2);

			return newCondition;
		}

		public static Condition operator |(Condition condition1, Condition condition2)
		{
			//Condition newCondition1 = condition1.Clone();
			//Condition newCondition2 = condition2.Clone();

            Condition newCondition = new Condition();

			newCondition.OrConditions.Add(condition1);
            newCondition.OrConditions.Add(condition2);

			return newCondition;
		}

		private CompareOperator NotOperator
		{
			get {
				switch (Operator) {
					case CompareOperator.EqualTo:
						return CompareOperator.NotEqualTo;
					case CompareOperator.LessThan:
						return CompareOperator.GreaterThanOrEqualTo;
					case CompareOperator.LessThanOrEqualTo:
						return CompareOperator.GreaterThan;
					case CompareOperator.GreaterThanOrEqualTo:
						return CompareOperator.LessThan;
					case CompareOperator.GreaterThan:
						return CompareOperator.LessThanOrEqualTo;
					case CompareOperator.NotEqualTo:
						return CompareOperator.EqualTo;
					default:
						throw new NotImplementedException("Operator " + Operator + " does not have an implemented NOT operator.");
				}
			}
		}

		public bool Negate
		{
			get;
			set;
		}

		public Condition Clone()
		{
			Condition condition = new Condition(FieldName, Operator, Value);
			condition.Negate = Negate;
			condition.DateTimeResolution = DateTimeResolution;

			foreach (Condition andCondition in AndConditions) {
				condition.AndConditions.Add(andCondition.Clone());
			}
			foreach (Condition orCondition in OrConditions) {
				condition.OrConditions.Add(orCondition.Clone());
			}

			return condition;
		}

//		public static Condition operator !(Condition condition)
//		{
//			Condition notCondition = condition.Clone();
//			notCondition.Negate = !condition.Negate;
//			return notCondition;
//		}

		public string FieldName
		{
			get;
			private set;
		}

		public CompareOperator Operator
		{
			get;
			private set;
		}

		public object Value
		{
			get;
			private set;
		}

		public DateTimeResolution DateTimeResolution
		{
			get;
			set;
		}

		public List<Condition> OrConditions
		{
			get;
			set;
		}

		public List<Condition> AndConditions
		{
			get;
			set;
		}

		private string ToDatabaseOperator(CompareOperator comparer)
		{
			switch (comparer) {
				case CompareOperator.LessThan:
					return "<";
				case CompareOperator.LessThanOrEqualTo:
					return "<=";
				case CompareOperator.EqualTo:
					return "=";
				case CompareOperator.GreaterThan:
					return ">";
				case CompareOperator.GreaterThanOrEqualTo:
					return ">=";
				case CompareOperator.NotEqualTo:
					return "<>";
				default:
					throw new NotImplementedException("Jämförare är inte implementerad.");
			}
		}

		private string DatabaseValue
		{
			get
			{
				if (Value is int) {
					return Value.ToString();
				}
				else if (Value is bool) {
					return (bool)Value ? "1" : "0";
				}
				else if (Value is DateTime) {
					DateTime v = (DateTime)Value;
					return "'" + v.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
				}
				else if (Value is Date) {
					Date v = (Date)Value;
					return "'" + v.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
				}
				else if (Value is string) {
					return "'" + Value.ToString() + "'";
				}
				else {
					throw new NotImplementedException("Datatype inte implementerad i Condition.DatabaseValue.");
				}
			}
		}

		public string ToSqlString()
		{
			StringBuilder sql = new StringBuilder();

			if (Negate) {
				sql.Append("not ");
			}

			if (AndConditions.Count > 0) { // Omslut alla och:ar med en parentes, bara för att expliciera...
                string sAnd = "";
                foreach (Condition andCondition in AndConditions) {
                    sql.Append(sAnd + andCondition.ToSqlString());
                    sAnd = " and ";
                }
            }
            else if (OrConditions.Count > 0) {
                sql.Append("(");

                string sOr = "";
                foreach (Condition orCondition in OrConditions) {
                    sql.Append(sOr + orCondition.ToSqlString());
                    sOr = " or ";
                }

                sql.Append(")");
            }
            else {
                if (Value == null) {
                    sql.Append(FieldName);

                    if (Operator == CompareOperator.EqualTo) {
                        sql.Append(" is null");
                    }
                    else if (Operator == CompareOperator.NotEqualTo) {
                        sql.Append(" is not null");
                    }
                    else {
                        throw new InvalidOperationException("Cannot compare null with anything else than Equal To (=) or Not Equal To (<>).");
                    }
                }
                else if (!(Value is DateTime) || (Value is DateTime && DateTimeResolution == DateTimeResolution.Exact)) {
                    sql.Append(FieldName);
                    sql.Append(ToDatabaseOperator(Operator));
                    sql.Append(DatabaseValue);
                }
                else { // Om det är DateTime med specialupplösning
                    DateTime v = (DateTime)Value;

                    if (DateTimeResolution == DateTimeResolution.Day) {
                        if (Operator == CompareOperator.LessThan) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.ToString("yyyy-MM-dd") + "'");
                        }
                        if (Operator == CompareOperator.LessThanOrEqualTo) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd") + "'");
                        }
                        if (Operator == CompareOperator.EqualTo) {
                            sql.Append("(");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.ToString("yyyy-MM-dd") + "'");
                            sql.Append(" and ");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd") + "'");
                            sql.Append(")");
                        }
                        if (Operator == CompareOperator.GreaterThanOrEqualTo) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.ToString("yyyy-MM-dd") + "'");
                        }
                        if (Operator == CompareOperator.GreaterThan) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd") + "'");
                        }
                        if (Operator == CompareOperator.NotEqualTo) {
                            sql.Append("(");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.ToString("yyyy-MM-dd") + "'");
                            sql.Append(" or ");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd") + "'");
                            sql.Append(")");
                        }
                    }
                    else if (DateTimeResolution == DateTimeResolution.Minute) {
                        if (Operator == CompareOperator.LessThan) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.LessThanOrEqualTo) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.EqualTo) {
                            sql.Append("(");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(" and ");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(")");
                        }
                        if (Operator == CompareOperator.GreaterThanOrEqualTo) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.GreaterThan) {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.NotEqualTo) {
                            sql.Append("(");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + v.ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(" or ");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + v.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(")");
                        }
                    }
                    else if (DateTimeResolution == Classes.DateTimeResolution.Month)
                    {
                        if (Operator == CompareOperator.LessThan)
                        {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.LessThanOrEqualTo)
                        {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).AddMonths(1).ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.EqualTo)
                        {
                            sql.Append("(");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(" and ");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).AddMonths(1).ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(")");
                        }
                        if (Operator == CompareOperator.GreaterThanOrEqualTo)
                        {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.GreaterThan)
                        {
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).AddMonths(1).ToString("yyyy-MM-dd HH:mm") + "'");
                        }
                        if (Operator == CompareOperator.NotEqualTo)
                        {
                            sql.Append("(");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.LessThan));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(" or ");
                            sql.Append(FieldName);
                            sql.Append(ToDatabaseOperator(CompareOperator.GreaterThanOrEqualTo));
                            sql.Append("'" + (new DateTime(v.Year, v.Month, 1)).AddMonths(1).ToString("yyyy-MM-dd HH:mm") + "'");
                            sql.Append(")");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("'" + DateTimeResolution.ToString() + "' är inte implementerat.");
                    }
                }
            }

			return sql.ToString();
		}

		public static Condition Interval(string fieldName, int fromValue, int toValue)
		{
			Condition condition = new Condition(fieldName, CompareOperator.GreaterThanOrEqualTo, fromValue);
			condition.AndConditions.Add(new Condition(fieldName, CompareOperator.LessThanOrEqualTo, toValue));
			return condition;
		}

		public static Condition Interval(string fieldName, DateTime fromValue, DateTime toValue, DateTimeResolution resolution)
		{
			Condition condition = new Condition(fieldName, CompareOperator.GreaterThanOrEqualTo, fromValue, resolution);
			condition.AndConditions.Add(new Condition(fieldName, CompareOperator.LessThanOrEqualTo, toValue, resolution));
			return condition;
		}

		private static Condition And(Condition firstCondition, params Condition[] conditions)
		{
			foreach (Condition condition in conditions) {
				firstCondition.AndConditions.Add(condition);
			}

			return firstCondition;
		}

		private static Condition Or(Condition firstCondition, params Condition[] conditions)
		{
			foreach (Condition condition in conditions) {
				firstCondition.OrConditions.Add(condition);
			}

			return firstCondition;
		}

        public override string ToString()
        {
            string sCondition = "";

            if (FieldName != null) {
                sCondition = FieldName + " " + ToDatabaseOperator(Operator) + " " + DatabaseValue;

                foreach (Condition andCondition in AndConditions) {
                    sCondition = sCondition + " and " + andCondition;
                }
                foreach (Condition orCondition in OrConditions) {
                    sCondition = sCondition + " or " + orCondition;
                }
            }
            else {
                string sAnd = "";

                foreach (Condition andCondition in AndConditions) {
                    sCondition = sCondition + sAnd + andCondition;
                    sAnd = " and ";
                }

                if (OrConditions.Count > 0) {
                    sCondition = sCondition + "(";
                    foreach (Condition orCondition in OrConditions) {
                        sCondition = sCondition + sAnd + orCondition;
                        sAnd = " or ";
                    }
                    sCondition = sCondition + ")";
                }
            }


            return sCondition;
        }
	}

	public enum DateTimeResolution
	{
		Year,
		Month,
		Day,
		Hour,
		Minute,
		Exact
	}

	public enum CompareOperator
	{
		LessThan,
		LessThanOrEqualTo,
		EqualTo,
		GreaterThan,
		GreaterThanOrEqualTo,
		NotEqualTo
	}
}
