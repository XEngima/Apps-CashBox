using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBaseGenerator
{
    public class GlobalEnumCollection : List<GlobalEnum>
    {
        public GlobalEnum this[string name]
        {
            get
            {
                foreach (var globalEnum in this)
                {
                    if (globalEnum.Name == name)
                    {
                        return globalEnum;
                    }
                }

                return null;
            }
        }
    }
}
