using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace EasyBase.DataLayer
{
	public static class TransactionHelper
	{
		public static TransactionOptions Options(IsolationLevel isolationLevel)
		{
			return Options(isolationLevel, new TimeSpan(1, 0, 15));
		}

		public static TransactionOptions Options(IsolationLevel isolationLevel, TimeSpan timeout)
		{
			TransactionOptions scopeOptions = new TransactionOptions();
			scopeOptions.IsolationLevel = isolationLevel;
			scopeOptions.Timeout = timeout;

			return scopeOptions;
		}
	}
}
