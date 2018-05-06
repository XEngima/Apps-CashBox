using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
    public class VerificationException : Exception
    {
        public VerificationException(string message)
            : base(message)
        {
        }
    }
}
