using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using EasyBase.Classes;

namespace EasyBase.DataLayer
{
	public partial class StandardDataLayer
	{
        public override bool ConnectedToReleaseDatabase
        {
            get { return DataLayer.Settings.ConnectionString.Contains("Initial Catalog=CashBox;"); }
        }

        /// <summary>
        /// Inits the database on create.
        /// </summary>
        /// <remarks>
        /// This method should be updated with every new database version, and should always work for the last database version, i.e. there
        /// is no need to handle previous versions since this is an init method. All help methods start with "Init".
        /// </remarks>
        public override void InitDatabaseOnCreate()
        {
            CashBoxSettings settings = new CashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo, CurrentApplication.DateTimeNow.Year);
            Save(settings);

            Category category = new Category(null, CategoryType.Income, "Lön", false, CurrentApplication.DateTimeNow, true);
            Save(category);

            category = new Category(null, CategoryType.Income, "Korrigering", false, CurrentApplication.DateTimeNow, true);
            Save(category);

            category = new Category(null, CategoryType.Expense, "Mat", false, CurrentApplication.DateTimeNow, true);
            Save(category);
            Category foodCategory = category;

            category = new Category(foodCategory.No, CategoryType.Expense, "Mat", false, CurrentApplication.DateTimeNow, true);
            Save(category);
            category = new Category(foodCategory.No, CategoryType.Expense, "Utemat", false, CurrentApplication.DateTimeNow, true);
            Save(category);

            category = new Category(null, CategoryType.Expense, "Bil", false, CurrentApplication.DateTimeNow, true);
            Save(category);

            category = new Category(null, CategoryType.Expense, "Resor", false, CurrentApplication.DateTimeNow, true);
            Save(category);

            category = new Category(null, CategoryType.Expense, "Hus", false, CurrentApplication.DateTimeNow, true);
            Save(category);

            category = new Category(null, CategoryType.Expense, "Korrigering", false, CurrentApplication.DateTimeNow, true);
            Save(category);

            // Add calls to more Init-methods here...
        }

        public decimal CalculateBalance()
        {
            string sql = @"
                select
	                (
		                select isnull(sum(Amount), 0)
		                from AccountTransactions
	                )
	                -
	                (
		                select isnull(sum(Amount), 0)
		                from CashBookTransactions
	                )
";

            object oValue = Connection.GetScalar(sql);
            decimal value;
            bool success = decimal.TryParse(oValue.ToString(), out value);
            return (success ? value : 0);
        }

        public decimal CalculateTotalWealth()
        {
            string sql = @"
                select
                    (
                        select sum(BalanceBroughtForwardAmount)
                        from Accounts
                    )
                    +
	                (
		                select isnull(SUM(Amount), 0)
		                from AccountTransactions
	                )";

            object oValue = Connection.GetScalar(sql);
            decimal value;
            bool success = decimal.TryParse(oValue.ToString(), out value);
            return (success ? value : 0);
        }

        //public decimal CalculateAccountBalance(int accountNo)
        //{
        //    string sql = string.Format(@"
        //        select
        //            Accounts.BalanceBroughtForwardAmount +
	       //         (
		      //          select isnull(SUM(Amount), 0)
		      //          from
        //                    AccountTransactions
        //                where AccountNo = {0}
	       //         ) as BalanceAmount
        //        from Accounts
        //        where No = {0}", accountNo);

        //    return decimal.Parse(Connection.GetScalar(sql).ToString());
        //}

        public CategoryCollection GetSubCategories()
        {
            string sql = @"
                select *
                from Categories c1
                where
                    IsArchived <> 1 and
                    (
                        select count(*)
                        from Categories c2
                        where c2.ParentCategoryNo = c1.No
                    ) = 0
                order by
                    Type desc,
                    Name asc";

            CategoryTable categoryTable = new CategoryTable();
            Connection.GetTable(categoryTable, sql);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }

       // public int[] GetUnbalancedVerificationNumbers()
       // {
       //     string sql = @"
       //         select No
       //         from
       //             Verifications
       //         where
       //             (
       //                 select isnull(sum(Amount), 0)
       //                 from AccountTransactionsView
       //                 where
							//Year([TransactionTime]) = Verifications.Year and
							//VerificationSerialNo = Verifications.SerialNo
       //             )
       //             -
       //             (
       //                 select isnull(sum(Amount), 0)
       //                 from CashBookTransactionsView
       //                 where
							//Year([TransactionTime]) = Verifications.Year and
							//VerificationSerialNo = Verifications.SerialNo
       //             ) <> 0";

       //     DataTable dataTable = new DataTable();
       //     Connection.GetTable(dataTable, sql);

       //     return (from DataRow row in dataTable.Rows
       //             select (int)row["No"]).ToList().ToArray();
       // }

        public VerificationCollection GetUnbalancedAndEmptyVerifications(int accountingYear)
        {
            string sql = string.Format(@"
                select *
                from
                    Verifications
                where
                    Year = {0} and
                    (
                        (
                            select isnull(sum(Amount), 0)
                            from AccountTransactionsView
                            where
							    Year([TransactionTime]) = Verifications.Year and
							    VerificationSerialNo = Verifications.SerialNo
                        )
                        -
                        (
                            select isnull(sum(Amount), 0)
                            from CashBookTransactionsView
                            where
							    Year([TransactionTime]) = Verifications.Year and
							    VerificationSerialNo = Verifications.SerialNo
                        ) <> 0 or
                        (
                            select count(*)
                            from AccountTransactionsView
                            where
							    Year([TransactionTime]) = Verifications.Year and
							    VerificationSerialNo = Verifications.SerialNo
                        )
                        +
                        (
                            select count(*)
                            from CashBookTransactionsView
                            where
							    Year([TransactionTime]) = Verifications.Year and
							    VerificationSerialNo = Verifications.SerialNo
                        ) = 0
                    )", accountingYear);

            VerificationTable verificationTable = new VerificationTable();
            Connection.GetTable(verificationTable, sql);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }

        public Verification GetFirstUnbalancedVerification(int year)
        {
            string sql = string.Format(@"
                select top 1 *
                from
                    Verifications
                where
                    Year = {0} and
                    (
                        select isnull(sum(Amount), 0)
                        from AccountTransactionsView
                        where
							Year([TransactionTime]) = Verifications.Year and
							VerificationSerialNo = Verifications.SerialNo
                    )
                    -
                    (
                        select isnull(sum(Amount), 0)
                        from CashBookTransactionsView
                        where
							Year([TransactionTime]) = Verifications.Year and
							VerificationSerialNo = Verifications.SerialNo
                    ) <> 0
                order by
                    Verifications.No asc", year);

            VerificationTable verificationsTable = new VerificationTable();
            Connection.GetTable(verificationsTable, sql);

            if (verificationsTable.Rows.Count > 0) {
                return new Verification(verificationsTable[0]);
            }

            return null;
        }

        public decimal GetVerificationBalance(int verificationNo)
        {
            string sql = string.Format(@"
                select (
                    (
                        select isnull(sum(Amount), 0)
                        from AccountTransactions
                        where VerificationNo = {0}
                    )
                    -
                    (
                        select isnull(sum(Amount), 0)
                        from CashBookTransactions
                        where VerificationNo = {0}
                    )
                )", verificationNo);

            return Convert.ToDecimal(Connection.GetScalar(sql));
        }

        /// <summary>
        /// Gets the earliest verificate date in the database.
        /// </summary>
        /// <param name="dateType">Type of date.</param>
        /// <returns>The earliest account transaction date as a DateTime. null if no account transaction date exists.</returns>
        public DateTime? GetEarliestVerificationDate(CashBoxDateType dateType)
        {
            string dateField = dateType == CashBoxDateType.TransactionDate ? "[Date]" : "[AccountingDate]";

            string sql = string.Format(@"
                select top 1 {0}
                from Verifications
                order by {0} asc", dateField);
            
            object transactionTime = Connection.GetScalar(sql);
            return (transactionTime == null) ? (DateTime?)null : Convert.ToDateTime(transactionTime);
        }

        /// <summary>
        /// Gets the latest verificate date in the database.
        /// </summary>
        /// <param name="dateType">Type of date.</param>
        /// <returns>The earliest account transaction date as a DateTime. null if no account transaction date exists.</returns>
        public DateTime? GetLatestVerificationDate(CashBoxDateType dateType)
        {
            string dateField = dateType == CashBoxDateType.TransactionDate ? "[Date]" : "[AccountingDate]";

            string sql = string.Format(@"
                select top 1 {0}
                from Verifications
                order by {0} desc", dateField);

            object transactionTime = Connection.GetScalar(sql);
            return (transactionTime == null) ? (DateTime?)null : Convert.ToDateTime(transactionTime);
        }

        /// <summary>
        /// Gets the earliest transaction time for a specified account transaction in the database.
        /// </summary>
        /// <returns>The earliest account transaction date as a DateTime. null if no account transaction date exists.</returns>
        public DateTime? GetFirstAccountTransactionDate(int accountNo)
        {
            string sql = string.Format(@"
                select top 1 TransactionTime
                from AccountTransactions
                where AccountNo = {0}
                order by TransactionTime asc", accountNo);

            object transactionTime = Connection.GetScalar(sql);

            return (transactionTime == null) ? (DateTime?)null : Convert.ToDateTime(transactionTime);
        }

	    public decimal GetLatestTagAccountSnapshot(int tagAccountNo)
	    {
	        string sql = string.Format(@"
                select isnull((
                    select top 1 BalanceAmount
                    from TagAccountSnapshots
                    where TagAccountNo = {0}),0)", tagAccountNo);

	        return Convert.ToDecimal(Connection.GetScalar(sql));
	    }

        /// <summary>
        /// Gets all account names and their balances.
        /// </summary>
        /// <param name="untilDay">Include transactions until this day.</param>
        /// <returns>A data table with columns Name and Balance.</returns>
        public DataTable GetAccountBalances(DateTime untilDay)
        {
            string sql = string.Format(@"
                select
                    Accounts.Name,
                    Accounts.BalanceBroughtForwardAmount + (
                        select isnull(sum(AccountTransactionsView.Amount),0)
                        from AccountTransactionsView
                        where
                            AccountingDate < '{0}' and
                            AccountNo = Accounts.No
                    ) as Balance
                from
	                Accounts
                where
                    IsArchived = 0 and
                    ShowInDiagram = 1
                order by
	                Accounts.Name
                ", untilDay.AddDays(1).ToString("yyyy-MM-dd"));

            DataTable resultDataTable = new DataTable();
            Connection.GetTable(resultDataTable, sql);
            return resultDataTable;
        }

	    public DataTable GetTagAccountBalances()
	    {
	        string sql = string.Format(@"
                select
                    TagAccountsView.Name,
                    isnull(TagAccountsView.Amount, 0) as Balance
                from
                    TagAccountsView
                where
                    TagAccountsView.IsArchived = 0
                order by
                    TagAccountsView.Name");

            DataTable resultDataTable = new DataTable();
            Connection.GetTable(resultDataTable, sql);
            return resultDataTable;
        }

        public DataTable GetTotalWealthPerDate(DateTime firstDate, DateTime lastDate)
        {
            string sql = string.Format(@"
                select
	                convert(varchar(10), TransactionTime, 126) as [Date],
                    sum(Amount) as Amount
                from
                    AccountTransactionsView
                group by
	                convert(varchar(10), TransactionTime, 126)
                order by
	                convert(varchar(10), TransactionTime, 126)");

            DataTable resultDataTable = new DataTable();
            Connection.GetTable(resultDataTable, sql);
            return resultDataTable;
        }

        public DataTable GetTotalWealthPerMonth(DateTime firstDate, DateTime lastDate)
        {
            string sql = string.Format(@"
                select
	                convert(varchar(7), TransactionTime, 126) as [Date],
                    sum(Amount) as Amount
                from
                    AccountTransactionsView
                group by
	                convert(varchar(7), TransactionTime, 126)
                order by
	                convert(varchar(7), TransactionTime, 126)");

            DataTable resultDataTable = new DataTable();
            Connection.GetTable(resultDataTable, sql);
            return resultDataTable;
        }

	    /// <summary>
	    /// Gets total spendings per non archived category.
	    /// </summary>
	    /// <param name="categoryType">Type of category.</param>
	    /// <param name="date">Month or year to collect.</param>
	    /// <param name="onlyMonth">true if only the month in date will be collected. Otherwise the whole year's data is collected.</param>
	    /// <returns>A data table with columns No, ParentCategoryNo, Name, IsArchived, Amount and Children.</returns>
	    public DataTable GetSpendingsPerCategory(CategoryType categoryType, DateTime date, bool onlyMonth)
	    {
            string fromDate = date.Year.ToString() + "-01-01";
            string toDate = date.AddYears(1).Year.ToString() + "-01-01";

	        if (onlyMonth)
	        {
                fromDate = date.ToString("yyyy-MM") + "-01";
                toDate = date.AddMonths(1).ToString("yyyy-MM") + "-01";
            }

            string sql = string.Format(@"
                select
	                Categories.No,
	                Categories.ParentCategoryNo,
	                Categories.Name,
                    Categories.IsArchived,
	                (
						select isnull(sum(Amount), 0)
						from CashBookTransactionsView
						where
							CategoryNo = Categories.No and
							AccountingDate >= '{1}' and
							AccountingDate < '{2}'
	                ) as Amount,
	                count(c2.No) as Children
                from
	                Categories
	                left outer join Categories c2 on Categories.No = c2.ParentCategoryNo
                where
                    Categories.Type = {0} and
                    Categories.ShowInDiagram = 1
                group by
	                Categories.No,
	                Categories.Name,
                    Categories.IsArchived,
	                Categories.ParentCategoryNo
                ", (int)categoryType, fromDate, toDate);

            DataTable resultDataTable = new DataTable();
            Connection.GetTable(resultDataTable, sql);
            return resultDataTable;
        }

        public DataTable GetMonthlyBalance(DateTime year, CategoryType categoryType)
        {
            string sql = string.Format(@"
                select
	                convert(varchar(7), AccountingDate, 126) as [Month],
	                SUM(Amount) as Amount
                from
	                CashBookTransactionsView
                where
                    ShowInDiagram = 1 and
	                AccountingDate >= '{0}' and
	                AccountingDate < '{1}' and
	                (
		                select [Type]
		                from Categories
		                where [No] = CategoryNo
	                ) = {2}
                group by
	                convert(varchar(7), AccountingDate, 126)
                order by
                    convert(varchar(7), AccountingDate, 126) asc
            ", (new DateTime(year.Year, 1, 1)).ToString("yyyy-MM-dd"), (new DateTime(year.Year, 1, 1)).AddYears(1).ToString("yyyy-MM-dd"), (int)categoryType);

            DataTable resultDataTable = new DataTable();
            Connection.GetTable(resultDataTable, sql);
            return resultDataTable;
        }

        /// <summary>
        /// Gets the last verification for the given year.
        /// </summary>
        /// <param name="year">The year to get the last verification from.</param>
        /// <returns>Last verification. null if there is no verification in the database for the given year.</returns>
        public Verification GetLastVerification(int year)
        {
            string sql = string.Format(@"
                select top 1 *
                from Verifications
                where Year = {0}
                order by No desc", year);

            VerificationTable resultDataTable = new VerificationTable();
            Connection.GetTable(resultDataTable, sql);

            if (resultDataTable.Rows.Count > 0) {
                return new Verification(resultDataTable[0]);
            }
            else {
                return null;
            }
        }

        public decimal GetTotalBalanceBrougntForward()
        {
            string sql = @"
                select sum(BalanceBroughtForwardAmount)
                from Accounts";

            return (decimal)Connection.GetScalar(sql);
        }

        /// <summary>
        /// Gets the total cash book amount for a specified category and all its sub categories.
        /// </summary>
        /// <param name="month">A date time with month precision.</param>
        /// <param name="categoryNo">Category number.</param>
        /// <returns>The total amount as a decimal.</returns>
        public decimal GetTotalCashBookAmountRecursiveCategories(DateTime month, int categoryNo)
        {
            month = new DateTime(month.Year, month.Month, 1);
            string sql = string.Format(@"
                with c as
                (
	                select No, ParentCategoryNo, Name
	                from Categories
	                where No={0}

	                union all
	
	                select uc.No, uc.ParentCategoryNo, uc.Name
	                from c
	                join Categories as uc on uc.ParentCategoryNo = c.No
                ), a as
                (
	                select sum(amount) as Amount
	                from
		                CashBookTransactionsView
	                where
		                TransactionTime >= '{1}' and
		                TransactionTime < '{2}' and
		                CategoryNo in
		                (
			                select No from c
		                )
                )
                select * from a",
                                categoryNo, month.ToString("yyyyMMdd"), month.AddMonths(1).ToString("yyyyMMdd"));

            return Convert.ToDecimal(Connection.Execute(sql));
        }

        /// <summary>
        /// Gets cash book transactions by given parameters.
        /// </summary>
        /// <param name="month">A date time with month precision.</param>
        /// <param name="userNo">User number.</param>
        /// <param name="categoryNo">Category number.</param>
        /// <returns></returns>
        public CashBookTransactionCollection GetCashBookTransactionsTransactionTime(DateTime month, int userNo, int categoryNo)
        {
            month = new DateTime(month.Year, month.Month, 1);
            string sql = "";

            if (categoryNo > 0)
            {
                sql += string.Format(@"
                with c as
                (
	                select No, ParentCategoryNo, Name
	                from Categories
	                where No={0}

	                union all
	
	                select uc.No, uc.ParentCategoryNo, uc.Name
	                from c
	                join Categories as uc on uc.ParentCategoryNo = c.No
                )
	            select *
	            from
		            CashBookTransactionsView
	            where
		            TransactionTime >= '{1}' and
		            TransactionTime < '{2}' and
                    {3}
		            CategoryNo in
		            (
			            select No from c
		            )", categoryNo, month.ToString("yyyyMMdd"), month.AddMonths(1).ToString("yyyyMMdd"),
                      userNo > 0 ? " UserNo = " + userNo + " and " : "");
            }
            else
            {
                sql += string.Format(@"
	                select *
	                from
		                CashBookTransactionsView
	                where
                        {0}
		                TransactionTime >= '{1}' and
		                TransactionTime < '{2}'",
                            userNo > 0 ? " UserNo = " + userNo + " and " : "",
                            month.ToString("yyyyMMdd"),
                            month.AddMonths(1).ToString("yyyyMMdd"));
            }

            sql += string.Format(@"
                order by
	                TransactionTime desc,
                    No desc
                ");

            CashBookTransactionTable resultDataTable = new CashBookTransactionTable();
            Connection.GetTable(resultDataTable, sql);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(resultDataTable);
        }

        /// <summary>
        /// Gets cash book transactions by given parameters.
        /// </summary>
        /// <param name="month">A date time with month precision.</param>
        /// <param name="userNo">User number.</param>
        /// <param name="categoryNo">Category number.</param>
        /// <returns></returns>
        public CashBookTransactionCollection GetCashBookTransactionsAccountingTime(DateTime month, int userNo, int categoryNo)
        {
            month = new DateTime(month.Year, month.Month, 1);
            string sql = "";

            if (categoryNo > 0)
            {
                sql += string.Format(@"
                with c as
                (
	                select No, ParentCategoryNo, Name
	                from Categories
	                where No={0}

	                union all
	
	                select uc.No, uc.ParentCategoryNo, uc.Name
	                from c
	                join Categories as uc on uc.ParentCategoryNo = c.No
                )
	            select *
	            from
		            CashBookTransactionsView
	            where
		            AccountingDate >= '{1}' and
		            AccountingDate < '{2}' and
                    {3}
		            CategoryNo in
		            (
			            select No from c
		            )", categoryNo, month.ToString("yyyyMMdd"), month.AddMonths(1).ToString("yyyyMMdd"),
                      userNo > 0 ? " UserNo = " + userNo + " and " : "");
            }
            else
            {
                sql += string.Format(@"
	                select *
	                from
		                CashBookTransactionsView
	                where
                        {0}
		                AccountingDate >= '{1}' and
		                AccountingDate < '{2}'",
                            userNo > 0 ? " UserNo = " + userNo + " and " : "",
                            month.ToString("yyyyMMdd"),
                            month.AddMonths(1).ToString("yyyyMMdd"));
            }

            sql += string.Format(@"
                order by
	                AccountingDate desc,
                    No desc
                ");

            CashBookTransactionTable resultDataTable = new CashBookTransactionTable();
            Connection.GetTable(resultDataTable, sql);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(resultDataTable);
        }

        /// <summary>
        /// Gets cash book transactions by given parameters.
        /// </summary>
        /// <param name="month">A date time with month precision.</param>
        /// <param name="userNo">User number.</param>
        /// <param name="categoryNo">Category number.</param>
        /// <param name="dateType">Date Type.</param>
        /// <returns></returns>
        public CashBookTransactionCollection GetCashBookTransactions(DateTime month, int userNo, int categoryNo, CashBoxDateType dateType)
        {
            if (dateType == CashBoxDateType.TransactionDate)
            {
                return GetCashBookTransactionsTransactionTime(month, userNo, categoryNo);
            }
            else if (dateType == CashBoxDateType.AccountingDate)
            {
                return GetCashBookTransactionsAccountingTime(month, userNo, categoryNo);
            }
            else
            {
                throw new NotImplementedException("CashBoxDateType." + dateType + " is unknown.");
            }
        }

	    public decimal GetAccountBalanceBroughtForward(int accountNo, int beforeYear)
	    {
            string sql = string.Format(@"
                select (select isnull(sum(Amount), 0)
                from AccountTransactionsView
                where
                    AccountNo = {0} and
                    TransactionTime < '{1}-01-01')
                +
                (select isnull(BalanceBroughtForwardAmount, 0)
                from Accounts
                where No = {0})", accountNo, beforeYear);

            return Convert.ToDecimal(Connection.GetScalar(sql));
	    }
    }
}

