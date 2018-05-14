using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EasyBase.Classes;
using EasyBase.DataLayer;
using System.Transactions;

namespace EasyBase.BusinessLayer
{
	public partial class StandardBusinessLayer
	{
        public StandardBusinessLayer(DataCache dataCache)
        {
            Database = new StandardDataLayer();
            DataCache = dataCache;
        }

        public bool ConnectedToReleaseDatabase
        {
            get { return true; }
        }

        public DataCache DataCache
        {
            get; set;
        }

        public decimal CalculateBalance()
        {
            DataTable dt = Database.GetAccountTransactionsTable(new Condition(AccountTransaction.fNo, CompareOperator.GreaterThan, 0),
                AccountTransaction.fNo,
                AccountTransaction.fAmount);

            return Database.CalculateBalance();
        }

        public decimal CalculateTotalWealth()
        {
            return Database.CalculateTotalWealth();
        }

        //public decimal CalculateAccountBalance(int accountNo)
        //{
        //    return Database.CalculateAccountBalance(accountNo);
        //}

        public CategoryCollection GetSubCategories()
        {
            return Database.GetSubCategories();
        }

        private void AddCategoryAndSubCategories(DataRow categoryDataRow, List<DictionaryItem<int, string>> categoryList, int level, DataTable categoryDataTable)
        {
            categoryList.Add(new DictionaryItem<int, string>() { Key = (int)categoryDataRow[Category.fNo], Value = (new String('-', 4 * level)) + " " + (string)categoryDataRow[Category.fName] });

            foreach (DataRow row in categoryDataTable.Rows) {
                if (!(row[Category.fParentCategoryNo] is DBNull) && (int)row[Category.fParentCategoryNo] == (int)categoryDataRow[Category.fNo]) {
                    AddCategoryAndSubCategories(row, categoryList, level + 1, categoryDataTable);
                }
            }
        }

        public List<DictionaryItem<int, string>> GetCategoriesHierarchicalArchived()
        {
            var categoryList = new List<DictionaryItem<int, string>>();

            Condition notArchived = new Condition(Category.fIsArchived, CompareOperator.EqualTo, true);
            SortOrder byTypeAndThenName = new SortOrder(Category.fType, OrderDirection.Descending, new SortOrder(Category.fName, OrderDirection.Ascending));
            DataTable categoryDataTable = Database.GetCategoriesTable(notArchived, byTypeAndThenName, Category.fNo, Category.fName, Category.fParentCategoryNo);

            foreach (DataRow row in categoryDataTable.Rows)
            {
                if (row[Category.fParentCategoryNo] is DBNull)
                {
                    AddCategoryAndSubCategories(row, categoryList, 0, categoryDataTable);
                }
            }

            return categoryList;
        }

        public List<DictionaryItem<int, string>> GetCategoriesHierarchical()
        {
            var categoryList = new List<DictionaryItem<int, string>>();

            Condition notArchived = new Condition(Category.fIsArchived, CompareOperator.NotEqualTo, true);
            SortOrder byTypeAndThenName = new SortOrder(Category.fType, OrderDirection.Descending, new SortOrder(Category.fName, OrderDirection.Ascending));
            DataTable categoryDataTable = Database.GetCategoriesTable(notArchived, byTypeAndThenName, Category.fNo, Category.fName, Category.fParentCategoryNo);

            foreach (DataRow row in categoryDataTable.Rows) {
                if (row[Category.fParentCategoryNo] is DBNull) {
                    AddCategoryAndSubCategories(row, categoryList, 0, categoryDataTable);
                }
            }

            return categoryList;
        }

        public Verification GetFirstUnbalancedVerification(int year)
        {
            return Database.GetFirstUnbalancedVerification(year);
        }

        public decimal GetVerificationBalance(int verificationNo)
        {
            return Database.GetVerificationBalance(verificationNo);
        }

        /// <summary>
        /// Gets the last verification for the given year.
        /// </summary>
        /// <param name="year">The year to get the last verification from.</param>
        /// <returns>Last verification. null if there is no verification in the database for the given year.</returns>
        //public Verification GetLastVerification(int year)
        //{
        //    return Database.GetLastVerification(year);
        //}

        /// <summary>
        /// Creates a new verification (next verification) for the given year.
        /// </summary>
        /// <param name="year">The year to create the next verification in.</param>
        /// <returns>Next verification</returns>
        public Verification GetNextVerification(int year)
        {
            Verification verification = DataCache.GetLastVerification();
            Verification newVerification;

            if (verification != null)
            {
                newVerification = new Verification(year, verification.SerialNo + 1, CurrentApplication.DateTimeNow, CurrentApplication.DateTimeNow);
            }
            else
            {
                newVerification = new Verification(year, 1, CurrentApplication.DateTimeNow, CurrentApplication.DateTimeNow);
            }
            
            DataCache.Save(newVerification);
            return newVerification;
        }

        public Verification CreateNewVerification(int year)
        {
            Verification verification;

            // Om ingen verifikation är laddad
            if (CurrentApplication.CurrentVerification == null) {
                verification = DataCache.GetLastVerification();
                //verification = GetLastVerification(year);

                if (verification == null) {
                    verification = GetNextVerification(year);
                    //CurrentApplication.CurrentVerification = GetFullVerification(verification.No);
                    CurrentApplication.CurrentVerification = DataCache.GetVerification(verification.No);
                    return CurrentApplication.CurrentVerification;
                }
                else {
                    //CurrentApplication.CurrentVerification = GetFullVerification(verification.No);
                    CurrentApplication.CurrentVerification = DataCache.GetVerification(verification.No);
                    return CurrentApplication.CurrentVerification;
                }
            }

            // En verifikation är laddad

            if (!CurrentApplication.CurrentVerification.HasTransactions) {
                throw new VerificationException("En ny affärshändelse kan inte skapas eftersom den aktuella affärshändelsen (" + CurrentApplication.CurrentVerification + ") är tom.");
            }

            if (!CurrentApplication.CurrentVerification.IsBalanced) {
                throw new VerificationException("En ny affärshändelse kan inte skapas eftersom nuvarande affärshändelse inte är balanserad.");
            }

            verification = GetNextVerification(year);
            CurrentApplication.CurrentVerification = DataCache.GetVerification(verification.No);

            return CurrentApplication.CurrentVerification;
        }

        public void DoAccountTransaction(DateTime date, decimal amount, int sourceAccountNo, string sourceNote, int targetAccountNo, string targetNote)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionHelper.Options(System.Transactions.IsolationLevel.ReadCommitted))) {
                Database.TheConnection.EnlistTransaction(Transaction.Current);

                Verification verification = GetNextVerification(date.Year);
                
                AccountTransaction sourceTransaction = new AccountTransaction(CurrentApplication.UserNo, sourceAccountNo, verification.No, verification.Date, verification.AccountingDate,
                    -amount, sourceNote, CurrentApplication.DateTimeNow);

                DataCache.Save(sourceTransaction);

                AccountTransaction targetTransaction = new AccountTransaction(CurrentApplication.UserNo, targetAccountNo, verification.No, verification.Date, verification.AccountingDate,
                    amount, targetNote, CurrentApplication.DateTimeNow);

                DataCache.Save(targetTransaction);

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Gets the earliest verificate date in the database.
        /// </summary>
        /// <param name="dateType">Type of date.</param>
        /// <returns>The earliest account transaction date as a DateTime. null if no account transaction date exists.</returns>
        public DateTime? GetEarliestVerificationDate(CashBoxDateType dateType)
        {
            return Database.GetEarliestVerificationDate(dateType);
        }

        /// <summary>
        /// Gets the latest verificate date in the database.
        /// </summary>
        /// <param name="dateType">Type of date.</param>
        /// <returns>The earliest account transaction date as a DateTime. null if no account transaction date exists.</returns>
        public DateTime? GetLatestVerificationDate(CashBoxDateType dateType)
        {
            return Database.GetLatestVerificationDate(dateType);
        }

        /// <summary>
        /// Gets the earliest transaction time for a specified account transaction in the database.
        /// </summary>
        /// <returns>The earliest account transaction date as a DateTime. null if no account transaction date exists.</returns>
        public DateTime? GetFirstAccountTransactionDate(int accountNo)
        {
            return Database.GetFirstAccountTransactionDate(accountNo);
        }

	    public decimal GetLatestTagAccountSnapshot(int tagAccountNo)
	    {
	        return Database.GetLatestTagAccountSnapshot(tagAccountNo);
	    }

	    //public int[] GetUnbalancedVerificationNumbers()
     //   {
     //       return Database.GetUnbalancedVerificationNumbers();
     //   }

        /// <summary>
        /// Deletes a verification and all its its associated account- and cash book transactions from the database.
        /// </summary>
        /// <param name="verificationNo">The number of the verification to be deleted.</param>
        /// <exception cref="InvalidOperationException">Thrown if the verification was not the last one in the database.</exception>
        public void DeleteVerificationAndAssociatedTransactions(int verificationNo)
        {
            Verification verification = Database.GetVerification(verificationNo);
            Verification lastVerification = Database.GetLastVerification(verification.Year);

            if (verification.No != lastVerification.No) {
                throw new VerificationException("Du försöker ta bort en annan än den senaste verifikationen. Detta är inte tillåtet eftersom det skulle skapa luckor i verifikationsserien.");
            }

            var accountTransactions = GetAccountTransactionsByVerificationNo(verificationNo);
            bool accountHasTags = accountTransactions.Count > 0 && CountAccountTagsByAccountNo(accountTransactions[0].AccountNo) > 0;

            if (accountTransactions.Any(at => at.Amount != 0) && accountHasTags)
            {
                throw new VerificationException("Du försöker ta bort en kontotransaktion från ett konto som har kontomärkningar och vars belopp inte är 0. Detta är inte tillåtet eftersom befintliga kontomärkningar kan bli felaktiga. Sätt transaktionsbelopp till 0 för berörda kontotransaktioner och försök igen.");
            }

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionHelper.Options(System.Transactions.IsolationLevel.ReadCommitted))) {
                Database.TheConnection.EnlistTransaction(Transaction.Current);

                DataCache.DeleteCashBookTransactionsByVerificationNo(verification.No);
                DataCache.DeleteAccountTransactionsByVerificationNo(verification.No);
                DeleteLogItemsByVerificationNo(verification.No);
                DataCache.DeleteVerification(lastVerification.No);

                transactionScope.Complete();
            }

            int currentAccountingYear = GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo).AccountingYear;
            //CurrentApplication.CurrentVerification = GetFullVerification(GetLastVerification(currentAccountingYear).No);
            CurrentApplication.CurrentVerification = DataCache.GetVerification(DataCache.GetLastVerification().No);
        }

        /// <summary>
        /// Gets all account names and their balances.
        /// </summary>
        /// <param name="untilDay">Include transactions until this day.</param>
        /// <returns>A data table with account names and their balances.</returns>
        public DataTable GetAccountBalances(DateTime untilDay)
        {
            return Database.GetAccountBalances(untilDay);
        }

        /// <summary>
        /// Gets all tag account names and their balances.
        /// </summary>
        /// <param name="untilDay">Include transactions until this day.</param>
        /// <returns>A data table with account names and their balances.</returns>
        public DataTable GetTagAccountBalances()
        {
            return Database.GetTagAccountBalances();
        }

        public DataTable GetTotalWealthPerDate(DateTime firstDate, DateTime lastDate)
        {
            return Database.GetTotalWealthPerDate(firstDate, lastDate);
        }

        public DataTable GetTotalWealthPerMonth(DateTime firstDate, DateTime lastDate)
        {
            return Database.GetTotalWealthPerMonth(firstDate, lastDate);
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
            return Database.GetSpendingsPerCategory(categoryType, date, onlyMonth);
        }

        /// <summary>
        /// Gets the monthly balance for a whole year.
        /// </summary>
        /// <param name="year">The year to get monthly balance for.</param>
        /// <param name="categoryType">Category type, income or expense.</param>
        /// <returns>A data table with columns Month (on format "yyyy-MM") and Amount.</returns>
        public DataTable GetMonthlyBalance(DateTime year, CategoryType categoryType)
        {
            return Database.GetMonthlyBalance(year, categoryType);
        }

        public decimal GetTotalBalanceBrougntForward()
        {
            return Database.GetTotalBalanceBrougntForward();
        }

        public AccountTransaction AddAccountTransaction(int verificationNo, DateTime verificationDate, DateTime accountingDate, int userNo, int accountNo, decimal amount,
            string note, TagHandlerAction tagHandlerAction, int accountTagNo)
        {
            Verification verification;
            AccountTransaction transaction;
            Account account = GetAccount(accountNo);

            if (verificationNo > 0) {
                verification = DataCache.GetVerification(verificationNo);
            }
            else {
                verification = CreateNewVerification(verificationDate.Year);
            }

            AccountTagCollection accountTags = GetAccountTagsByAccountNo(accountNo);

            List<LogItem> logItems;
            RecalculateAccountNoValues(verificationNo, amount, tagHandlerAction, accountTagNo, accountTags, account, out logItems);

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionHelper.Options(System.Transactions.IsolationLevel.ReadCommitted)))
            {
                Database.TheConnection.EnlistTransaction(Transaction.Current);

                verification.Date = DanielEiserman.DateAndTime.Date.FloorDateTime(verificationDate);
                verification.AccountingDate = DanielEiserman.DateAndTime.Date.FloorDateTime(accountingDate);
                DataCache.Save(verification);

                if (verification.Date.Year != verificationDate.Year) {
                    throw new InvalidOperationException("Datumet på verifikationen stämmer inte överens med dess värde.");
                }

                transaction = new AccountTransaction(userNo, accountNo, verification.No, verification.Date, verification.AccountingDate, amount, note,
                                                     CurrentApplication.DateTimeNow);
                DataCache.Save(transaction);

                foreach (var accountTag in accountTags)
                {
                    Save(accountTag);
                }

                foreach (var accountTag in accountTags)
                {
                    CreateTagAccountSnapshot(accountTag.TagAccountNo, TagAccountSnapshotReason.AccountTransactionAdded);
                }

                foreach (var logItem in logItems)
                {
                    logItem.VerificationNo = verification.No;
                    Save(logItem);
                }

                transactionScope.Complete();
            }

            // Alert all listeners that a transaction was created
            ApplicationEvents.OnAccountTransactionCreated(transaction.No, transaction.VerificationNo);

            // Update the new verification
            CurrentApplication.CurrentVerification = DataCache.GetVerification(verification.No);

            return transaction;
        }

	    public void CreateTagAccountSnapshot(int tagAccountNo, TagAccountSnapshotReason reason)
	    {
	        var tagAccount = GetTagAccount(tagAccountNo);
	        var latestSnapshotBalanceAmount = GetLatestTagAccountSnapshot(tagAccountNo);

	        var newSnapshot = new TagAccountSnapshot(CurrentApplication.UserNo, tagAccountNo, tagAccount.Amount, reason,
	            CurrentApplication.DateTimeNow);

	        if (latestSnapshotBalanceAmount != newSnapshot.BalanceAmount)
	        {
	            Save(newSnapshot);
	        }
	    }

	    private static void RecalculateAccountNoValues(int verificationNo, decimal amount,
	        TagHandlerAction tagHandlerAction, int accountTagNo, AccountTagCollection accountTags, Account account,
	        out List<LogItem> logItems)
	    {
	        AccountTag currentAccountTag = accountTags.Find(at => at.No == accountTagNo);

	        decimal totalAbsoluteAmount = 0;
	        decimal totalRelativePercent = 0;

	        foreach (var accountTag in accountTags)
	        {
	            if (accountTag.Type == AccountTagType.PercentOfRest)
	            {
	                totalRelativePercent += accountTag.RelativeValue;
	            }
	            else if (accountTag.Type == AccountTagType.ExactAmount)
	            {
	                totalAbsoluteAmount += accountTag.Amount;
	            }
	        }

	        logItems = new List<LogItem>();
	        string logText;

	        if (tagHandlerAction == TagHandlerAction.Untagged)
	        {
	            decimal newTotalRelativeAndUntaggedAmount = account.Balance + account.BalanceBroughtForwardAmount - totalAbsoluteAmount + amount;
	            var relativeAccountTags = accountTags.Where(at => at.Type == AccountTagType.PercentOfRest);

	            // Recalculate percent values (if there is anything left in the account after the withdrawal)
	            if (newTotalRelativeAndUntaggedAmount > 0)
	            {
	                foreach (var accountTag in relativeAccountTags)
	                {
	                    logText = string.Format("Relative account tag {0} changed value from {1} to ", accountTag.No,
	                        accountTag.RelativeValue.ToString("0.#########"));

	                    accountTag.RelativeValue = Math.Round(accountTag.Amount/newTotalRelativeAndUntaggedAmount, 9);

                        if (accountTag.RelativeValue < 0)
                        {
                            accountTag.RelativeValue = 0;
                        }
                        else if (accountTag.RelativeValue > 1)
                        {
                            accountTag.RelativeValue = 1;
                        }

                        logText += accountTag.RelativeValue.ToString("0.#########") + ".";

	                    logItems.Add(new LogItem(CurrentApplication.UserNo, verificationNo, account.No,
	                        LogItemType.AccountTagRecalculated, logText, CurrentApplication.DateTimeNow));
	                }
	            }
	        }
	        else if (tagHandlerAction == TagHandlerAction.Specified)
	        {
	            decimal newTotalRelativeAndUntaggedAmount = account.Balance + account.BalanceBroughtForwardAmount - totalAbsoluteAmount + amount;
	            var relativeAccountTags = accountTags.Where(at => at.Type == AccountTagType.PercentOfRest);
	            decimal totalRelativePercentAfter = 0m;

	            if (currentAccountTag.Type == AccountTagType.ExactAmount)
	            {
	                logText = string.Format("Absolute account tag {0} changed value from {1} to {2}.", currentAccountTag.No,
                        currentAccountTag.MoneyValue.ToString("0.####"), (currentAccountTag.MoneyValue + amount).ToString("0.####"));

                    currentAccountTag.MoneyValue += amount;

	                logItems.Add(new LogItem(CurrentApplication.UserNo, verificationNo, account.No,
	                    LogItemType.AccountTagRecalculated, logText, CurrentApplication.DateTimeNow));
	            }
	            else
	            {
	                // Recalculate percent values (if there is anything left in the account after the withdrawal)
	                if (newTotalRelativeAndUntaggedAmount > 0)
	                {
	                    foreach (var accountTag in relativeAccountTags)
	                    {
	                        decimal accountTagAmount = accountTag.Amount;
	                        if (accountTag.No == currentAccountTag.No)
	                        {
	                            accountTagAmount += amount;
	                        }

	                        logText = string.Format("Relative account tag {0} changed value from {1} to ", accountTag.No,
	                            accountTag.RelativeValue.ToString("0.#########"));

                            accountTag.RelativeValue = Math.Round(accountTagAmount / newTotalRelativeAndUntaggedAmount, 9);

                            if (accountTag.RelativeValue < 0)
	                        {
                                accountTag.RelativeValue = 0;
	                        }
                            else if (accountTag.RelativeValue > 1)
                            {
                                accountTag.RelativeValue = 1;
                            }

                            totalRelativePercentAfter += accountTag.RelativeValue;

                            logText += accountTag.RelativeValue.ToString("0.#########") + ".";

	                        logItems.Add(new LogItem(CurrentApplication.UserNo, verificationNo, account.No,
	                            LogItemType.AccountTagRecalculated, logText, CurrentApplication.DateTimeNow));
	                    }
	                }

	                // If the total percent for all relative account tags was 100 percent before transaction,
	                // then make sure that they are 100 percent after.
	                if (totalRelativePercent == 1 || totalRelativePercentAfter > 1)
	                {
	                    decimal diff = totalRelativePercentAfter - totalRelativePercent;
	                    var highestAccountTag = relativeAccountTags.OrderByDescending(at => at.Amount).First();

	                    logText = string.Format("Relative account tag {0} value was adjusted from {1} to ", highestAccountTag.No,
	                        highestAccountTag.RelativeValue.ToString("0.#########"));

                        highestAccountTag.RelativeValue -= diff;

	                    if (diff > 0)
	                    {
                            logText += highestAccountTag.RelativeValue.ToString("0.#########") + ".";

	                        logItems.Add(new LogItem(CurrentApplication.UserNo, verificationNo, account.No,
	                            LogItemType.AccountTagRecalculated, logText, CurrentApplication.DateTimeNow));
	                    }
	                }
	            }
	        }
	        else if (tagHandlerAction == TagHandlerAction.Split)
	        {
	            // Do nothing - alla account tag percent values are still correct
	        }
	        else
	        {
	            throw new NotImplementedException("TagType action " + tagHandlerAction + " is not implemented.");
	        }

	        decimal totalUntaggedAndRelativeAmount = account.BalanceBroughtForwardAmount + account.Balance -
	                                                 totalAbsoluteAmount;

	        // Some validation
	        if (amount < 0)
	        {
	            if (tagHandlerAction == TagHandlerAction.Specified)
	            {
	                if (Math.Abs(amount) > currentAccountTag.Amount)
	                {
	                    if (currentAccountTag.Type == AccountTagType.ExactAmount)
	                    {
                            throw new MoneyTagException(
                                "Uttaget kan inte genomföras eftersom dess värde överstiger värdet för den valda märkningen som tillåter en transaktion på " +
                                currentAccountTag.Amount.ToString(CurrentApplication.MoneyDisplayFormat) +
                                " kr. Om du vill genomföra transaktionen måste du först ändra på märkningen.");
                        }
	                    else // If relative amount
	                    {
                            // Om det finns flera relativa kontotaggar med tillräckligt värde på så är det ok, annars inte.
	                        if (Math.Abs(amount) > totalUntaggedAndRelativeAmount)
	                        {
                                throw new MoneyTagException(
                                    "Uttaget kan inte genomföras eftersom dess värde överstiger värdet för samtliga relativa märkningar inklusive omärkt belopp på " +
                                    totalUntaggedAndRelativeAmount.ToString(CurrentApplication.MoneyDisplayFormat) +
                                    " kr. Om du vill genomföra transaktionen måste du först ändra på märkningen.");
                            }
	                    }
	                }
	            }
	            else
	            {
	                if (tagHandlerAction == TagHandlerAction.Split)
	                {
	                    if (Math.Abs(amount) > account.Balance + account.BalanceBroughtForwardAmount)
	                    {
	                        throw new MoneyTagException(
	                            "Uttaget kan inte genomföras eftersom dess värde överstiger det totala värdet för alla relativa märkningar (" +
	                            totalUntaggedAndRelativeAmount.ToString(CurrentApplication.MoneyDisplayFormat) +
	                            " kr). Om du vill genomföra transaktionen måste du först ändra på märkningen.");
	                    }
	                }
	            }
	        }

	        if (account.BalanceBroughtForwardAmount + account.Balance < 0 || account.Type == AccountType.Debt)
	        {
	            if (tagHandlerAction != TagHandlerAction.Untagged)
	            {
	                throw new MoneyTagException(
	                    "Du kan inte välja en märkning på ett skuldkonto eller på ett konto med negativt belopp.");
	            }
	        }
	    }

	    public CashBookTransaction AddCashBookTransaction(int verificationNo, DateTime verificationDate, DateTime accountingDate, int userNo, int categoryNo, decimal amount,
            string note)
        {
            Verification verification;
            CashBookTransaction transaction;

            if (verificationNo > 0)
            {
                verification = DataCache.GetVerification(verificationNo);
            }
            else
            {
                verification = CreateNewVerification(verificationDate.Year);
            }

            if (verification.Date.Year != verificationDate.Year)
            {
                throw new InvalidOperationException("Datumet på verifikationen stämmer inte överens med dess värde.");
            }

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionHelper.Options(System.Transactions.IsolationLevel.ReadCommitted)))
            {
                Database.TheConnection.EnlistTransaction(Transaction.Current);

                verification.Date = DanielEiserman.DateAndTime.Date.FloorDateTime(verificationDate);
                verification.AccountingDate = DanielEiserman.DateAndTime.Date.FloorDateTime(accountingDate);
                DataCache.Save(verification);

                transaction = new CashBookTransaction(userNo, categoryNo, verification.No, verification.Date, verification.AccountingDate, amount,
                                                                          note, CurrentApplication.DateTimeNow);
                DataCache.Save(transaction);

                transactionScope.Complete();
            }

            // Alert all listeners that a transaction was created
            ApplicationEvents.OnCashBookTransactionCreated(transaction.No, transaction.VerificationNo);

            // Update the new verification
            CurrentApplication.CurrentVerification = DataCache.GetVerification(verification.No);

            return transaction;
        }

        public AccountTransaction UpdateAccountTransaction(int accountTransactionNo, DateTime verificationDate, DateTime accountingDate, int userNo, int accountNo, decimal amount,
            string note, TagHandlerAction tagHandlerAction, int accountTagNo)
        {
            Account account = GetAccount(accountNo);
            //AccountTransaction transaction = GetAccountTransaction(accountTransactionNo);
            AccountTransaction transaction = DataCache.GetAccountTransaction(accountTransactionNo);
            decimal changeInAmount = amount - transaction.Amount;

            transaction.UserNo = userNo;
            transaction.AccountNo = accountNo;
            transaction.Amount = amount;
            transaction.Note = note;

            Verification verification = DataCache.GetVerification(transaction.VerificationNo);
            //Verification verification = GetVerification(transaction.VerificationNo);
            verification.Date = DanielEiserman.DateAndTime.Date.FloorDateTime(verificationDate);
            verification.AccountingDate = DanielEiserman.DateAndTime.Date.FloorDateTime(accountingDate);

            AccountTagCollection accountTags = GetAccountTagsByAccountNo(accountNo);

            List<LogItem> logItems;
            RecalculateAccountNoValues(verification.No, changeInAmount, tagHandlerAction, accountTagNo, accountTags, account, out logItems);

            if (verification.Date.Year != verificationDate.Year)
            {
                throw new InvalidOperationException("Datumet på verifikationen stämmer inte överens med dess värde.");
            }

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionHelper.Options(System.Transactions.IsolationLevel.ReadCommitted)))
            {
                Database.TheConnection.EnlistTransaction(Transaction.Current);

                DataCache.Save(verification);
                DataCache.Save(transaction);

                foreach (var accountTag in accountTags)
                {
                    Save(accountTag);
                }

                foreach (var accountTag in accountTags)
                {
                    CreateTagAccountSnapshot(accountTag.TagAccountNo, TagAccountSnapshotReason.AccountTransactionUpdated);
                }

                foreach (var logItem in logItems)
                {
                    logItem.VerificationNo = verification.No;
                    Save(logItem);
                }

                transactionScope.Complete();
            }

            // Alert all listeners that a transaction was updated
            ApplicationEvents.OnAccountTransactionUpdated(accountTransactionNo, verification.No);

            return transaction;
        }

        public CashBookTransaction UpdateCashBookTransaction(int cashBookTransactionNo, DateTime verificationDate, DateTime accountingDate, int userNo, int categoryNo, decimal amount,
            string note)
        {
            CashBookTransaction transaction = GetCashBookTransaction(cashBookTransactionNo);
            transaction.UserNo = userNo;
            transaction.CategoryNo = categoryNo;
            transaction.Amount = amount;
            transaction.Note = note;

            Verification verification = DataCache.GetVerification(transaction.VerificationNo);
            //Verification verification = GetVerification(transaction.VerificationNo);
            verification.Date = DanielEiserman.DateAndTime.Date.FloorDateTime(verificationDate);
            verification.AccountingDate = DanielEiserman.DateAndTime.Date.FloorDateTime(accountingDate);

            if (verification.Date.Year != verificationDate.Year || verification.AccountingDate.Year != verificationDate.Year)
            {
                throw new InvalidOperationException("Datumet på verifikationen stämmer inte överens med dess värde.");
            }

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionHelper.Options(System.Transactions.IsolationLevel.ReadCommitted)))
            {
                Database.TheConnection.EnlistTransaction(Transaction.Current);

                DataCache.Save(verification);
                DataCache.Save(transaction);

                transactionScope.Complete();
            }

            // Alert all listeners that a transaction was updated
            ApplicationEvents.OnCashBookTransactionUpdated(cashBookTransactionNo, verification.No);

            return transaction;
        }

        public VerificationCollection GetUnbalancedAndEmptyVerifications(int accountingYear)
        {
            return Database.GetUnbalancedAndEmptyVerifications(accountingYear);
        }

        /// <summary>
        /// Gets the total cash book amount for a specified category and all its sub categories.
        /// </summary>
        /// <param name="month">A date time with month precision.</param>
        /// <param name="categoryNo">Category number.</param>
        /// <returns>The total amount as a decimal.</returns>
        public decimal GetTotalCashBookAmountRecursiveCategories(DateTime month, int categoryNo)
        {
            return Database.GetTotalCashBookAmountRecursiveCategories(month, categoryNo);
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
            return Database.GetCashBookTransactions(month, userNo, categoryNo, dateType);
        }

	    public decimal GetAccountBalanceBroughtForward(int accountNo, int beforeYear)
	    {
	        return Database.GetAccountBalanceBroughtForward(accountNo, beforeYear);
	    }
	}
}
