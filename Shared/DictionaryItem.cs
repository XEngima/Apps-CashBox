using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
    public struct DictionaryItem<TKey, TValue>
    {
        public TKey Key
        {
            get;
            set;
        }

        public TValue Value
        {
            get;
            set;
        }
    }

}
