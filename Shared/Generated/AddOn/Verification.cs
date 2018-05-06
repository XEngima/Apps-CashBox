using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DanielEiserman.DateAndTime;

namespace EasyBase.Classes
{
	public partial class Verification
	{
        public bool HasTransactions
        {
            get { return AccountTransactions.Count > 0 || CashBookTransactions.Count > 0; }
        }

        public bool IsBalanced
        {
            get { return (AccountTransactions.Sum(x => x.Amount) - CashBookTransactions.Sum(x => x.Amount) == 0); }
        }

	    public decimal Balance
	    {
            get { return CashBookTransactions.Sum(x => x.Amount) - AccountTransactions.Sum(x => x.Amount); }
        }

        public bool IsEmpty
        {
            get { return AccountTransactions.Count() == 0 && CashBookTransactions.Count() == 0; }
        }

        public string OverridedToString()
        {
            return Year.ToString() + "-" + SerialNo;
        }
	}
}
