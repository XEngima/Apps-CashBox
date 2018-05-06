using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace EasyBase.DataLayer
{
	public class ConnectionException : ApplicationException
	{
		private ConnectionErrorNumber _errorNumber;
		private string _sqlString;

		public ConnectionException()
			: base()
		{
			_errorNumber = ConnectionErrorNumber.Unspecified;
		}

		public ConnectionException(string message)
			: base(message)
		{
			_errorNumber = ConnectionErrorNumber.Unspecified;
			SqlErrorNumbers = "";
		}

		public ConnectionException(string message, Exception innerException)
			: base(message, innerException)
		{
			_errorNumber = ConnectionErrorNumber.Unspecified;
		}

		public ConnectionException(string message, SqlException innerException, SqlErrorCollection sqlErrors)
			: base(message, innerException)
		{
			_errorNumber = ConnectionErrorNumber.Unspecified;

			if (sqlErrors != null) {
				StringBuilder sbSqlErrors = new StringBuilder();
				string comma = "";
				foreach (SqlError sqlError in sqlErrors) {
					sbSqlErrors.Append(comma);
					sbSqlErrors.Append(sqlError.Number);
					comma = ", ";
				}

				SqlErrorNumbers = sbSqlErrors.ToString();
			}
			else {
				SqlErrorNumbers = "";
			}
		}

		public string SqlErrorNumbers
		{
			get;
			private set;
		}

		public ConnectionErrorNumber ErrorNumber
		{
			get { return _errorNumber; }
			protected set { _errorNumber = value; }
		}

		public string SqlString
		{
			get { return _sqlString; }
		}

		internal void SetSqlString(string sql)
		{
			_sqlString = sql;
		}

		internal void SetErrorNumber(ConnectionErrorNumber errorNumber)
		{
			_errorNumber = errorNumber;
		}
	}

	public class DatabaseConnectionLostException : ConnectionException
	{
		public DatabaseConnectionLostException(string message, SqlException innerException, SqlErrorCollection sqlErrors)
			: base(message, innerException, sqlErrors)
		{
			ErrorNumber = ConnectionErrorNumber.DatabaseConnectionLost;
		}
	}

	public class InternetConnectionLostException : DatabaseConnectionLostException
	{
		public InternetConnectionLostException(string message, SqlException innerException, SqlErrorCollection sqlErrors)
			: base(message, innerException, sqlErrors)
		{
			ErrorNumber = ConnectionErrorNumber.InternetConnectionLost;
		}
	}

	public class UniqueKeyConflictException : ConnectionException
	{
		public UniqueKeyConflictException(string message, Exception innerException)
		{
			ErrorNumber = ConnectionErrorNumber.UniqueKeyConflict;
		}
	}

	public class ForeignKeyConflictException : ConnectionException
	{
		public ForeignKeyConflictException(string message, Exception innerException)
		{
			ErrorNumber = ConnectionErrorNumber.ForeignKeyConflict;
		}
	}

	public abstract class ConnectionUnstableException : ConnectionException
	{
		public ConnectionUnstableException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

	public class ConnectionDeadlockException : ConnectionUnstableException
	{
		public ConnectionDeadlockException(string message, Exception innerException)
			: base(message, innerException)
		{
			ErrorNumber = ConnectionErrorNumber.DeadlockException;
		}
	}

	public class ConnectionTimeoutException : ConnectionUnstableException
	{
		public ConnectionTimeoutException(string message, Exception innerException)
			: base(message, innerException)
		{
			ErrorNumber = ConnectionErrorNumber.Timeout;
		}
	}

	public class TimeStampException : ConnectionException
	{
		public TimeStampException(string message, Exception innerException)
			: base(message, innerException)
		{
			ErrorNumber = ConnectionErrorNumber.ViolationOfTimeStamp;
		}
	}

	public enum ConnectionErrorNumber
	{
		Unspecified = 0,
		UniqueKeyConflict = 1,
		ForeignKeyConflict = 2,
		DatabaseConnectionLost = 3,
		InternetConnectionLost = 4,
		DeadlockException = 5,
		Timeout = 6,
		ViolationOfTimeStamp = 7
	}
}
