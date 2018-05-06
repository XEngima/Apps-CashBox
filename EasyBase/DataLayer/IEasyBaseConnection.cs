using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace EasyBase.DataLayer
{
	public interface IEasyBaseConnection
	{
		void EnlistTransaction(Transaction transaction);
	}
}
