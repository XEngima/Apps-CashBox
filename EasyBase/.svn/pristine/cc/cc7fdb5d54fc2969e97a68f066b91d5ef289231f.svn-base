using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EasyBase.Classes;

namespace EasyBase.DataLayer
{
	public class EasyBaseSqlConnection : IEasyBaseConnection
	{
		//------------------------------------------------------------------------------------------
		#region Fields

		private string _connectionString;
		private System.Data.SqlClient.SqlConnection _connection;
		private SqlTransaction _transaction;

		#endregion
		//------------------------------------------------------------------------------------------
		#region Construction/Destruction

		/// <summary>
		/// Skapar ett databaskopplingsobjekt.
		/// </summary>
		/// <param name="connectionString">Kopplingssträng till databasen.</param>
		internal EasyBaseSqlConnection(string connectionString)
		{
			_connectionString = connectionString;
			_connection = null;
			_transaction = null;
		}

		#endregion
		//------------------------------------------------------------------------------------------
		#region Public Methods

		/// <summary>
		/// Öppnar en koppling till databasen.
		/// </summary>
		public void Open()
		{
			if (_connection != null) {
				throw new DatabaseException("Database connection already open.");
			}

		    try
		    {
		        _connection = new System.Data.SqlClient.SqlConnection(_connectionString);
		        _connection.Open();
		    }
		    catch (SqlException)
		    {
		        _connection = null;
		        throw;
		    }
		}

	    public SqlConnection SqlConnection
	    {
            get { return _connection; }
	    }

		/// <summary>
		/// Stänger kopplingen till databasen.
		/// </summary>
		public void Close()
		{
			try {
				_connection.Close();
			}
			finally {
				_connection = null;
			}
		}

		public void EnlistTransaction(System.Transactions.Transaction transaction)
		{
			_connection.EnlistTransaction(transaction);
		}

		/// <summary>
		/// Öppnar en koppling till databasen och initierar en transaktion med IsolationLevel.ReadCommitted.
		/// </summary>
		public void BeginTransaction()
		{
			BeginTransaction(IsolationLevel.ReadCommitted);
		}

		/// <summary>
		/// Initierar en transaktion.
		/// </summary>
		/// <param name="isolationLevel">Isolation level.</param>
		public void BeginTransaction(IsolationLevel isolationLevel)
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			if (_transaction != null) {
				throw new DatabaseException("Transaction is already open.");
			}

			_transaction = _connection.BeginTransaction(isolationLevel);
		}

		/// <summary>
		/// Slutför transaktionen i databasen och stänger kopplingen.
		/// </summary>
		public void Commit()
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			if (_transaction == null) {
				throw new ConnectionException("Connection is not a transaction.");
			}

			try {
				_transaction.Commit();
			}
			finally {
				_transaction = null;
			}
		}

		/// <summary>
		/// Hämtar huruvida anslutningen är en transaktion.
		/// </summary>
		public bool IsTransaction
		{
			get { return _transaction != null; }
		}

		/// <summary>
		/// Ångrar ändringarna i databasen och stänger kopplingen.
		/// </summary>
		public void Rollback()
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			if (_transaction == null) {
				throw new ConnectionException("Connection is not a transaction.");
			}

			try {
				_transaction.Rollback();
			}
			finally {
				_transaction = null;
			}
		}

		/// <summary>
		/// Hämtar en datatabell från databasen utifrån en SQL-sats.
		/// </summary>
		/// <param name="sql">SQL-satsen.</param>
		/// <param name="parameters">Parametrar till SQL-satsen.</param>
		/// <returns>En datatabell.</returns>
		public void GetTable(DataTable dataTable, string sql, params object[] parameters)
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			try {
				SqlCommand cmd = new SqlCommand();
				SqlDataAdapter da;

				cmd.Connection = _connection;
				cmd.Transaction = _transaction;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql;

				int startIndex = 0;
				foreach (object parameter in parameters) {
					startIndex = sql.IndexOf("@", startIndex) + 1;
					int paramIndex = 0;
					StringBuilder sb = new StringBuilder();
					foreach (char c in sql.Substring(startIndex)) {
						if (char.IsLetterOrDigit(c)) {
							sb.Append(c);
							paramIndex++;
						}
						else {
							break;
						}
					}

					cmd.Parameters.Add(new SqlParameter("@" + sb.ToString(), parameter));
				}

				da = new SqlDataAdapter(cmd);
				da.Fill(dataTable);
			}
			catch (Exception ex) {
				throw ConvertToThrowableException(ex);
			}

			return;
		}

		public object GetScalar(string sql, params object[] parameters)
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			try {
				SqlCommand cmd = new SqlCommand();

				cmd.Connection = _connection;
				cmd.Transaction = _transaction;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql;

				int startIndex = 0;
				foreach (object parameter in parameters) {
					startIndex = sql.IndexOf("@", startIndex) + 1;
					int paramIndex = 0;
					StringBuilder sb = new StringBuilder();
					foreach (char c in sql.Substring(startIndex)) {
						if (char.IsLetterOrDigit(c)) {
							sb.Append(c);
							paramIndex++;
						}
						else {
							break;
						}
					}

					cmd.Parameters.Add(new SqlParameter("@" + sb.ToString(), parameter));
				}

				return cmd.ExecuteScalar();
			}
			catch (Exception ex) {
				throw ConvertToThrowableException(ex);
			}
		}

		/// <summary>
		/// Hämtar ett skalarvärde från databsen utifrån en SQL-sats.
		/// </summary>
		/// <param name="sql">SQL-satsen.</param>
		/// <returns>Ett skalarvärde.</returns>
		public object GetScalar(string sql)
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			SqlCommand cmd = new SqlCommand();

			cmd.Connection = _connection;
			cmd.Transaction = _transaction;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = sql;

			return cmd.ExecuteScalar();
		}

		/// <summary>
		/// Lägger till data i databasen.
		/// </summary>
		/// <param name="sql">En SQL-sats.</param>E
		public void Insert(string sql)
		{
			Insert(sql, false);
		}

		/// <summary>
		/// Lägger till data i databasen.
		/// </summary>
		/// <param name="sql">En SQL-sats.</param>
		/// <param name="returnIdentityID">true if the new identity ID is to be returned, otherwise false (latter is more optimized).</param>
		/// <returns>New identity ID for inserted row (if returnIdentityID is true).</returns>
		public int Insert(string sql, bool returnIdentityID)
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			SqlCommand cmd = new SqlCommand();

			cmd.Connection = _connection;
			if (_transaction != null)
			{
				cmd.Transaction = _transaction;
			}

			cmd.CommandType = CommandType.Text;
			cmd.CommandText = sql;
			cmd.ExecuteNonQuery();

			if (returnIdentityID)
			{
				cmd.CommandText = "SELECT @@IDENTITY";
				object obj = cmd.ExecuteScalar();
				return int.Parse(obj.ToString());
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Uppdaterar databasen utifrån en SQL-sats.
		/// </summary>
		/// <param name="sql">SQL-satsen.</param>
		/// <returns>Antalet uppdaterade rader.</returns>
		public int Update(string sql, params object[] parameters)
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			SqlCommand cmd = new SqlCommand();

			cmd.Connection = _connection;
			if (_transaction != null)
			{
				cmd.Transaction = _transaction;
			}
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = sql;

			int startIndex = 0;
			foreach (object parameter in parameters)
			{
				startIndex = sql.IndexOf("@", startIndex) + 1;
				int paramIndex = 0;
				StringBuilder sb = new StringBuilder(16);
				foreach (char c in sql.Substring(startIndex))
				{
					if (char.IsLetterOrDigit(c))
					{
						sb.Append(c);
						paramIndex++;
					}
					else
					{
						break;
					}
				}

				cmd.Parameters.Add(new SqlParameter("@" + sb.ToString(), parameter));
			}

			return cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Kör en SQL-sats i databasen.
		/// </summary>
		/// <param name="sql">SQL-satsen.</param>
		/// <returns>Antalet uppdaterade rader.</returns>
		public int Execute(string sql, params object[] parameters)
		{
			if (_connection == null) {
				throw new DatabaseException("Connection is not open.");
			}

			try {
				SqlCommand cmd = new SqlCommand();

				cmd.Connection = _connection;
				if (_transaction != null) {
					cmd.Transaction = _transaction;
				}
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql;

				int startIndex = 0;
				foreach (object parameter in parameters) {

					startIndex = sql.IndexOf("@", startIndex) + 1;
					int paramIndex = 0;
					StringBuilder sb = new StringBuilder(16);
					foreach (char c in sql.Substring(startIndex)) {
						if (char.IsLetterOrDigit(c)) {
							sb.Append(c);
							paramIndex++;
						}
						else {
							break;
						}
					}

					cmd.Parameters.Add(new SqlParameter("@" + sb.ToString(), parameter ?? DBNull.Value));
				}

				return cmd.ExecuteNonQuery();
			}
			catch (Exception ex) {
				throw ConvertToThrowableException(ex);
			}
		}

		/// <summary>
		/// Tar bort från databasen utifrån en SQL-sats.
		/// </summary>
		/// <param name="sql">SQL-satsen.</param>
		/// <returns>Antal påverkade rader.</returns>
		public int Delete(string sql, params object[] parameters)
		{
			return Update(sql);
		}

		#endregion
		//------------------------------------------------------------------------------------------

		private Exception ConvertToThrowableException(Exception ex)
		{
			if (ex is SqlException) {
				SqlException sqlEx = (SqlException)ex;

				foreach (SqlError sqlError in sqlEx.Errors) {
					if (sqlError.Number == 53 || sqlError.Number == 121) {
						return new InternetConnectionLostException("The connection to Internet was lost. Check your Internet connection and try again.", sqlEx, sqlEx.Errors);
					}
					else if (sqlError.Number >= 4060 && sqlError.Number <= 4064 || sqlError.Number >= 18456 && sqlError.Number <= 18461) {
						return new DatabaseConnectionLostException("Failed to login to the database.", sqlEx, sqlEx.Errors);
					}
					else if (sqlError.Number == 2627) {
						return new UniqueKeyConflictException("Unique key conflict in database.", ex);
					}
					else if (sqlError.Number == 547) {
						return new ForeignKeyConflictException("Foreign key conflict in database.", ex);
					}
					else if (sqlError.Number == -2 || sqlError.Number == 1205 || sqlError.Number == 3928) {
						Rollback();
						return new ConnectionDeadlockException("Transaction was deadlocked and has been chosen as the deadlock victim.", ex);
					}
					else if (sqlError.Number == 847 || sqlError.Number == 5245 || sqlError.Number == 17197 || sqlError.Number == 17830) {
						return new ConnectionTimeoutException("Database process was timed out!", ex);
					}
				}

				//if (sqlEx.ErrorCode == -2146232060) {
				//    return new InternetConnectionLostException("The connection to Internet was lost. Check your Internet connection and try again.", sqlEx, sqlEx.Errors);
				//}

				return new ConnectionException("An unknown error concerning the connection to the database or Internet occurred.", sqlEx, sqlEx.Errors);
			}
			else {
				ConnectionException unEx = new ConnectionException("An unknown error concerning the connection to the database occurred.", ex);
				return unEx;
			}
		}
	}
}
