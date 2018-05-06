using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer.Kernel
{
    public class StoredProcedureAttribute : Attribute
    {
        public int DatabaseVersion
        {
            get;
            set;
        }
    }
}
