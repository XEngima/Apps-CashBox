using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBase.DataLayer.Kernel;

namespace EasyBase.DataLayer.StoredProcedures
{
    [StoredProcedure(DatabaseVersion = 3)]
    internal class CalculateBalance : StoredProcedure
    {
        public override string SqlString
        {
            get {
                return @"
                    select
	                    (
		                    select isnull(SUM(Amount), 0)
		                    from AccountTransactions
		                    where [Type] <> 1
	                    )
	                    -
	                    (
		                    select isnull(SUM(Amount), 0)
		                    from CashBookTransactions
                            where Amount<0
	                    )
	                    -
	                    (
		                    select isnull(SUM(Amount), 0)
		                    from CashBookTransactions
                            where Amount > 0
	                    )";
            }
        }
    }
}
