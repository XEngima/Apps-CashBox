using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator.Database
{
	public class UniqueKeyCollection : List<UniqueKey>
	{
		public UniqueKeyCollection()
			: base()
		{
		}

		public UniqueKey this[string name]
		{
			get
			{
				foreach (UniqueKey uniqueKey in this) {
					//if (uniqueKey.
				}

				return null;
			}
		}
	}
}
