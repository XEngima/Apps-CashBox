using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
	public class DatabaseException : ApplicationException
	{
		public DatabaseException()
			: base()
		{
		}

		public DatabaseException(string message)
			: base(message)
		{
		}

		public DatabaseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

	public class DatabaseCorruptException : DatabaseException
	{
		public DatabaseCorruptException()
			: base()
		{
		}

		public DatabaseCorruptException(string message)
			: base(message)
		{
		}

		public DatabaseCorruptException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

    public class DatabaseVersionException : DatabaseException
    {
        public DatabaseVersionException()
            : base()
        {
        }

        public DatabaseVersionException(string message)
            : base(message)
        {
        }

        public DatabaseVersionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class UpdatingDatabaseException : DatabaseException
	{
		public UpdatingDatabaseException()
			: base()
		{
		}

		public UpdatingDatabaseException(string message)
			: base(message)
		{
		}

		public UpdatingDatabaseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

	public class NeedUpdateDatabaseException : DatabaseException
	{
		public NeedUpdateDatabaseException()
			: base()
		{
		}

		public NeedUpdateDatabaseException(string message)
			: base(message)
		{
		}

		public NeedUpdateDatabaseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

	public class DatabaseEntityDoesNotExistException : DatabaseException
	{
		public DatabaseEntityDoesNotExistException()
		{
		}

		public DatabaseEntityDoesNotExistException(string message)
			:base(message)
		{
		}

		public DatabaseEntityDoesNotExistException(string message, Exception innerException)
			:base(message, innerException)
		{
		}
	}
}
