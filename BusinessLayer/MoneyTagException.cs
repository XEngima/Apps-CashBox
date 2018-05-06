using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EasyBase.BusinessLayer
{
    public class MoneyTagException : ApplicationException
    {
        public MoneyTagException(string message)
            : base(message)
        {
        }
    }
}
