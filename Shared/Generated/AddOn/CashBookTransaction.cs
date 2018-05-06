using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DanielEiserman.DateAndTime;

namespace EasyBase.Classes
{
	public partial class CashBookTransaction
	{
        public void SetTransactionTime(DateTime transactionTime)
        {
            TransactionTime = transactionTime;
        }

        public void SetAccountingDate(DateTime accountingDate)
        {
            AccountingDate = accountingDate;
        }
    }
}
