using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
    public class GlobalEnum
    {
        public GlobalEnum(string name)
        {
            Name = name;
            Values = new List<string>();
        }

        public string Name
        {
            get;
            private set;
        }

        public List<string> Values
        {
            get;
            private set;
        }
    }
}
