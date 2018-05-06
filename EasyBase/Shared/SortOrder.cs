using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
	public class SortOrder
	{
		public SortOrder(string fieldName, OrderDirection direction, params SortOrder[] moreOrders)
		{
			FieldName = fieldName;
			Direction = direction;
			NextOrder = null;

			SortOrder currentOrder = this;
			foreach (SortOrder order in moreOrders) {
				currentOrder.NextOrder = order;
				currentOrder = order;
			}
		}

		public string FieldName
		{
			get;
			private set;
		}

		public OrderDirection Direction
		{
			get;
			private set;
		}

		public SortOrder NextOrder
		{
			get;
			set;
		}

		internal string ToSqlString()
		{
			string sql;
			sql = FieldName + " " + (Direction == OrderDirection.Ascending ? "asc" : "desc");

			if (NextOrder != null) {
				sql += ", " + NextOrder.ToSqlString();
			}

			return sql;
		}

        public static SortOrder Create(string fieldName, OrderDirection direction, params SortOrder[] moreOrders)
        {
            return new SortOrder(fieldName, direction, moreOrders);
        }
	}

	public enum OrderDirection
	{
		Ascending,
		Descending
	}
}
