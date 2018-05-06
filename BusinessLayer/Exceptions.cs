using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.BusinessLayer
{
    public class BusinessLayerException : ApplicationException
    {
        public BusinessLayerException()
            : base()
        {
        }

        public BusinessLayerException(string message)
            : base(message)
        {
        }

        public BusinessLayerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class NotConnectedToReleaseDatabaseException : BusinessLayerException
    {
        public NotConnectedToReleaseDatabaseException()
            : base()
        {
        }

        public NotConnectedToReleaseDatabaseException(string message)
            : base(message)
        {
        }

        public NotConnectedToReleaseDatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

