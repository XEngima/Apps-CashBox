using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
	public class UniqueKey
	{
		public UniqueKey(int uniqueKeyGroup)
		{
			Fields = new List<Field>();
			UniqueKeyGroup = uniqueKeyGroup;
		}

		public UniqueKey(int uniqueKeyGroup, List<Field> fields)
		{
			Fields = fields;
			UniqueKeyGroup = uniqueKeyGroup;
		}

		public int UniqueKeyGroup
		{
			get;
			private set;
		}

		public List<Field> Fields
		{
			get;
			private set;
		}
	}
}
