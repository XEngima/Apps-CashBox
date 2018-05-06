using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer
{
    public class BackupException : ApplicationException
    {
        public BackupException()
        {
        }

        public BackupException(string message)
            :base(message)
        {
        }

        public BackupException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
