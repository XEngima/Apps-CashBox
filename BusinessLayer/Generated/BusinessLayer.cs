using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EasyBase.Classes;
using EasyBase.DataLayer;

namespace EasyBase.BusinessLayer
{
    public partial class StandardBusinessLayer
    {
        public User GetUser(int no)
        {
            return Database.GetUser(no);
        }

        public User GetUserByEmail(string email)
        {
            return Database.GetUserByEmail(email);
        }

        public UserCollection GetUsersByName(string name)
        {
            return Database.GetUsersByName(name);
        }

        public UserCollection GetUsersByName(string name, SortOrder sortOrder)
        {
            return Database.GetUsersByName(name, sortOrder);
        }

        public UserCollection GetUsersByEmail(string email)
        {
            return Database.GetUsersByEmail(email);
        }

        public UserCollection GetUsersByEmail(string email, SortOrder sortOrder)
        {
            return Database.GetUsersByEmail(email, sortOrder);
        }

        public UserCollection GetUsersByPassword(string password)
        {
            return Database.GetUsersByPassword(password);
        }

        public UserCollection GetUsersByPassword(string password, SortOrder sortOrder)
        {
            return Database.GetUsersByPassword(password, sortOrder);
        }

        public UserCollection GetUsersByCreatedTime(DateTime createdTime)
        {
            return Database.GetUsersByCreatedTime(createdTime);
        }

        public UserCollection GetUsersByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            return Database.GetUsersByCreatedTime(createdTime, sortOrder);
        }

        public UserCollection GetUsers()
        {
            return Database.GetUsers();
        }
        public UserCollection GetUsers(Condition condition)
        {
            return Database.GetUsers(condition);
        }
        public DataTable GetUsersTable(params string[] sColumns)
        {
            return Database.GetUsersTable(sColumns);
        }
        public DataTable GetUsersTable(Condition condition, params string[] sColumns)
        {
            return Database.GetUsersTable(condition, sColumns);
        }
        public DataTable GetUsersTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetUsersTable(condition, sortOrder, sColumns);
        }
        public UserCollection GetUsers(SortOrder order)
        {
            return Database.GetUsers(order);
        }
        public UserCollection GetUsers(Condition condition, SortOrder sortOrder)
        {
            return Database.GetUsers(condition, sortOrder);
        }
        public int CountUsers()
        {
            return Database.CountUsers();
        }

        public int CountUsersByNo(int no)
        {
            return Database.CountUsersByNo(no);
        }

        public int CountUsersByName(string name)
        {
            return Database.CountUsersByName(name);
        }

        public int CountUsersByEmail(string email)
        {
            return Database.CountUsersByEmail(email);
        }

        public int CountUsersByPassword(string password)
        {
            return Database.CountUsersByPassword(password);
        }

        public int CountUsersByCreatedTime(DateTime createdTime)
        {
            return Database.CountUsersByCreatedTime(createdTime);
        }

        public int CountUsers(Condition condition)
        {
            return Database.CountUsers(condition);
        }

        public void Save(User user)
        {
            Database.Save(user);
        }

        public bool DeleteUser(int no)
        {
            return Database.DeleteUser(no);
        }

        public int DeleteAllUsers()
        {
            return Database.DeleteAllUsers();
        }

        public bool DeleteUserByEmail(string email)
        {
            return Database.DeleteUserByEmail(email);
        }

        public int DeleteUsers(Condition condition)
        {
            return Database.DeleteUsers(condition);
        }

        public Category GetCategory(int no)
        {
            return Database.GetCategory(no);
        }

        public CategoryCollection GetCategoriesByParentCategoryNo(int? parentCategoryNo)
        {
            return Database.GetCategoriesByParentCategoryNo(parentCategoryNo);
        }

        public CategoryCollection GetCategoriesByParentCategoryNo(int? parentCategoryNo, SortOrder sortOrder)
        {
            return Database.GetCategoriesByParentCategoryNo(parentCategoryNo, sortOrder);
        }

        public CategoryCollection GetCategoriesByType(CategoryType type)
        {
            return Database.GetCategoriesByType(type);
        }

        public CategoryCollection GetCategoriesByType(CategoryType type, SortOrder sortOrder)
        {
            return Database.GetCategoriesByType(type, sortOrder);
        }

        public CategoryCollection GetCategoriesByName(string name)
        {
            return Database.GetCategoriesByName(name);
        }

        public CategoryCollection GetCategoriesByName(string name, SortOrder sortOrder)
        {
            return Database.GetCategoriesByName(name, sortOrder);
        }

        public CategoryCollection GetCategoriesByIsArchived(bool isArchived)
        {
            return Database.GetCategoriesByIsArchived(isArchived);
        }

        public CategoryCollection GetCategoriesByIsArchived(bool isArchived, SortOrder sortOrder)
        {
            return Database.GetCategoriesByIsArchived(isArchived, sortOrder);
        }

        public CategoryCollection GetCategoriesByCreatedTime(DateTime createdTime)
        {
            return Database.GetCategoriesByCreatedTime(createdTime);
        }

        public CategoryCollection GetCategoriesByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            return Database.GetCategoriesByCreatedTime(createdTime, sortOrder);
        }

        public CategoryCollection GetCategoriesByShowInDiagram(bool showInDiagram)
        {
            return Database.GetCategoriesByShowInDiagram(showInDiagram);
        }

        public CategoryCollection GetCategoriesByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            return Database.GetCategoriesByShowInDiagram(showInDiagram, sortOrder);
        }

        public CategoryCollection GetCategories()
        {
            return Database.GetCategories();
        }
        public CategoryCollection GetCategories(Condition condition)
        {
            return Database.GetCategories(condition);
        }
        public DataTable GetCategoriesTable(params string[] sColumns)
        {
            return Database.GetCategoriesTable(sColumns);
        }
        public DataTable GetCategoriesTable(Condition condition, params string[] sColumns)
        {
            return Database.GetCategoriesTable(condition, sColumns);
        }
        public DataTable GetCategoriesTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetCategoriesTable(condition, sortOrder, sColumns);
        }
        public CategoryCollection GetCategories(SortOrder order)
        {
            return Database.GetCategories(order);
        }
        public CategoryCollection GetCategories(Condition condition, SortOrder sortOrder)
        {
            return Database.GetCategories(condition, sortOrder);
        }
        public int CountCategories()
        {
            return Database.CountCategories();
        }

        public int CountCategoriesByNo(int no)
        {
            return Database.CountCategoriesByNo(no);
        }

        public int CountCategoriesByParentCategoryNo(int? parentCategoryNo)
        {
            return Database.CountCategoriesByParentCategoryNo(parentCategoryNo);
        }

        public int CountCategoriesByType(CategoryType type)
        {
            return Database.CountCategoriesByType(type);
        }

        public int CountCategoriesByName(string name)
        {
            return Database.CountCategoriesByName(name);
        }

        public int CountCategoriesByIsArchived(bool isArchived)
        {
            return Database.CountCategoriesByIsArchived(isArchived);
        }

        public int CountCategoriesByCreatedTime(DateTime createdTime)
        {
            return Database.CountCategoriesByCreatedTime(createdTime);
        }

        public int CountCategoriesByShowInDiagram(bool showInDiagram)
        {
            return Database.CountCategoriesByShowInDiagram(showInDiagram);
        }

        public int CountCategories(Condition condition)
        {
            return Database.CountCategories(condition);
        }

        public void Save(Category category)
        {
            Database.Save(category);
        }

        public bool DeleteCategory(int no)
        {
            return Database.DeleteCategory(no);
        }

        public int DeleteAllCategories()
        {
            return Database.DeleteAllCategories();
        }

        public int DeleteCategoriesByParentCategoryNo(int? parentCategoryNo)
        {
            return Database.DeleteCategoriesByParentCategoryNo(parentCategoryNo);
        }

        public int DeleteCategories(Condition condition)
        {
            return Database.DeleteCategories(condition);
        }

        public Account GetAccount(int no)
        {
            return Database.GetAccount(no);
        }

        public Account GetFullAccount(int no)
        {
            Account account = Database.GetAccount(no);
            if (account == null) {
                return null;
            }
            account.AccountTransactions = Database.GetAccountTransactionsByAccountNo(account.No);
            return account;
        }

        public AccountCollection GetAccountsByUserNo(int userNo)
        {
            return Database.GetAccountsByUserNo(userNo);
        }

        public AccountCollection GetAccountsByUserNo(int userNo, SortOrder sortOrder)
        {
            return Database.GetAccountsByUserNo(userNo, sortOrder);
        }

        public AccountCollection GetAccountsByType(AccountType type)
        {
            return Database.GetAccountsByType(type);
        }

        public AccountCollection GetAccountsByType(AccountType type, SortOrder sortOrder)
        {
            return Database.GetAccountsByType(type, sortOrder);
        }

        public AccountCollection GetAccountsByName(string name)
        {
            return Database.GetAccountsByName(name);
        }

        public AccountCollection GetAccountsByName(string name, SortOrder sortOrder)
        {
            return Database.GetAccountsByName(name, sortOrder);
        }

        public AccountCollection GetAccountsByBalanceBroughtForwardAmount(decimal balanceBroughtForwardAmount)
        {
            return Database.GetAccountsByBalanceBroughtForwardAmount(balanceBroughtForwardAmount);
        }

        public AccountCollection GetAccountsByBalanceBroughtForwardAmount(decimal balanceBroughtForwardAmount, SortOrder sortOrder)
        {
            return Database.GetAccountsByBalanceBroughtForwardAmount(balanceBroughtForwardAmount, sortOrder);
        }

        public AccountCollection GetAccountsByIsArchived(bool isArchived)
        {
            return Database.GetAccountsByIsArchived(isArchived);
        }

        public AccountCollection GetAccountsByIsArchived(bool isArchived, SortOrder sortOrder)
        {
            return Database.GetAccountsByIsArchived(isArchived, sortOrder);
        }

        public AccountCollection GetAccountsByCreatedTime(DateTime createdTime)
        {
            return Database.GetAccountsByCreatedTime(createdTime);
        }

        public AccountCollection GetAccountsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            return Database.GetAccountsByCreatedTime(createdTime, sortOrder);
        }

        public AccountCollection GetAccountsByShowInDiagram(bool showInDiagram)
        {
            return Database.GetAccountsByShowInDiagram(showInDiagram);
        }

        public AccountCollection GetAccountsByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            return Database.GetAccountsByShowInDiagram(showInDiagram, sortOrder);
        }

        public AccountCollection GetAccountsByBalance(decimal balance)
        {
            return Database.GetAccountsByBalance(balance);
        }

        public AccountCollection GetAccountsByBalance(decimal balance, SortOrder sortOrder)
        {
            return Database.GetAccountsByBalance(balance, sortOrder);
        }

        public AccountCollection GetAccounts()
        {
            return Database.GetAccounts();
        }
        public AccountCollection GetAccounts(Condition condition)
        {
            return Database.GetAccounts(condition);
        }
        public DataTable GetAccountsTable(params string[] sColumns)
        {
            return Database.GetAccountsTable(sColumns);
        }
        public DataTable GetAccountsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetAccountsTable(condition, sColumns);
        }
        public DataTable GetAccountsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetAccountsTable(condition, sortOrder, sColumns);
        }
        public AccountCollection GetAccounts(SortOrder order)
        {
            return Database.GetAccounts(order);
        }
        public AccountCollection GetAccounts(Condition condition, SortOrder sortOrder)
        {
            return Database.GetAccounts(condition, sortOrder);
        }
        public int CountAccounts()
        {
            return Database.CountAccounts();
        }

        public int CountAccountsByNo(int no)
        {
            return Database.CountAccountsByNo(no);
        }

        public int CountAccountsByUserNo(int userNo)
        {
            return Database.CountAccountsByUserNo(userNo);
        }

        public int CountAccountsByType(AccountType type)
        {
            return Database.CountAccountsByType(type);
        }

        public int CountAccountsByName(string name)
        {
            return Database.CountAccountsByName(name);
        }

        public int CountAccountsByBalanceBroughtForwardAmount(decimal balanceBroughtForwardAmount)
        {
            return Database.CountAccountsByBalanceBroughtForwardAmount(balanceBroughtForwardAmount);
        }

        public int CountAccountsByIsArchived(bool isArchived)
        {
            return Database.CountAccountsByIsArchived(isArchived);
        }

        public int CountAccountsByCreatedTime(DateTime createdTime)
        {
            return Database.CountAccountsByCreatedTime(createdTime);
        }

        public int CountAccountsByShowInDiagram(bool showInDiagram)
        {
            return Database.CountAccountsByShowInDiagram(showInDiagram);
        }

        public int CountAccountsByBalance(decimal balance)
        {
            return Database.CountAccountsByBalance(balance);
        }

        public int CountAccounts(Condition condition)
        {
            return Database.CountAccounts(condition);
        }

        public void Save(Account account)
        {
            Database.Save(account);
        }

        public bool DeleteAccount(int no)
        {
            return Database.DeleteAccount(no);
        }

        public int DeleteAllAccounts()
        {
            return Database.DeleteAllAccounts();
        }

        public int DeleteAccountsByUserNo(int userNo)
        {
            return Database.DeleteAccountsByUserNo(userNo);
        }

        public int DeleteAccounts(Condition condition)
        {
            return Database.DeleteAccounts(condition);
        }

        public TagAccount GetTagAccount(int no)
        {
            return Database.GetTagAccount(no);
        }

        public TagAccount GetFullTagAccount(int no)
        {
            TagAccount tagAccount = Database.GetTagAccount(no);
            if (tagAccount == null) {
                return null;
            }
            tagAccount.TagAccountSnapshots = Database.GetTagAccountSnapshotsByTagAccountNo(tagAccount.No);
            return tagAccount;
        }

        public TagAccount GetTagAccountByName(string name)
        {
            return Database.GetTagAccountByName(name);
        }

        public TagAccountCollection GetTagAccountsByName(string name)
        {
            return Database.GetTagAccountsByName(name);
        }

        public TagAccountCollection GetTagAccountsByName(string name, SortOrder sortOrder)
        {
            return Database.GetTagAccountsByName(name, sortOrder);
        }

        public TagAccountCollection GetTagAccountsByIsArchived(bool isArchived)
        {
            return Database.GetTagAccountsByIsArchived(isArchived);
        }

        public TagAccountCollection GetTagAccountsByIsArchived(bool isArchived, SortOrder sortOrder)
        {
            return Database.GetTagAccountsByIsArchived(isArchived, sortOrder);
        }

        public TagAccountCollection GetTagAccountsByCreatedTime(DateTime createdTime)
        {
            return Database.GetTagAccountsByCreatedTime(createdTime);
        }

        public TagAccountCollection GetTagAccountsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            return Database.GetTagAccountsByCreatedTime(createdTime, sortOrder);
        }

        public TagAccountCollection GetTagAccountsByAmount(decimal amount)
        {
            return Database.GetTagAccountsByAmount(amount);
        }

        public TagAccountCollection GetTagAccountsByAmount(decimal amount, SortOrder sortOrder)
        {
            return Database.GetTagAccountsByAmount(amount, sortOrder);
        }

        public TagAccountCollection GetTagAccounts()
        {
            return Database.GetTagAccounts();
        }
        public TagAccountCollection GetTagAccounts(Condition condition)
        {
            return Database.GetTagAccounts(condition);
        }
        public DataTable GetTagAccountsTable(params string[] sColumns)
        {
            return Database.GetTagAccountsTable(sColumns);
        }
        public DataTable GetTagAccountsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetTagAccountsTable(condition, sColumns);
        }
        public DataTable GetTagAccountsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetTagAccountsTable(condition, sortOrder, sColumns);
        }
        public TagAccountCollection GetTagAccounts(SortOrder order)
        {
            return Database.GetTagAccounts(order);
        }
        public TagAccountCollection GetTagAccounts(Condition condition, SortOrder sortOrder)
        {
            return Database.GetTagAccounts(condition, sortOrder);
        }
        public int CountTagAccounts()
        {
            return Database.CountTagAccounts();
        }

        public int CountTagAccountsByNo(int no)
        {
            return Database.CountTagAccountsByNo(no);
        }

        public int CountTagAccountsByName(string name)
        {
            return Database.CountTagAccountsByName(name);
        }

        public int CountTagAccountsByIsArchived(bool isArchived)
        {
            return Database.CountTagAccountsByIsArchived(isArchived);
        }

        public int CountTagAccountsByCreatedTime(DateTime createdTime)
        {
            return Database.CountTagAccountsByCreatedTime(createdTime);
        }

        public int CountTagAccountsByAmount(decimal amount)
        {
            return Database.CountTagAccountsByAmount(amount);
        }

        public int CountTagAccounts(Condition condition)
        {
            return Database.CountTagAccounts(condition);
        }

        public void Save(TagAccount tagAccount)
        {
            Database.Save(tagAccount);
        }

        public bool DeleteTagAccount(int no)
        {
            return Database.DeleteTagAccount(no);
        }

        public int DeleteAllTagAccounts()
        {
            return Database.DeleteAllTagAccounts();
        }

        public bool DeleteTagAccountByName(string name)
        {
            return Database.DeleteTagAccountByName(name);
        }

        public int DeleteTagAccounts(Condition condition)
        {
            return Database.DeleteTagAccounts(condition);
        }

        public AccountTag GetAccountTag(int no)
        {
            return Database.GetAccountTag(no);
        }

        public AccountTagCollection GetAccountTagsByTagAccountNoAndAccountNo(int tagAccountNo, int accountNo)
        {
            return Database.GetAccountTagsByTagAccountNoAndAccountNo(tagAccountNo, accountNo);
        }

        public AccountTagCollection GetAccountTagsByTagAccountNoAndAccountNo(int tagAccountNo, int accountNo, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByTagAccountNoAndAccountNo(tagAccountNo, accountNo, sortOrder);
        }

        public AccountTagCollection GetAccountTagsByTagAccountNo(int tagAccountNo)
        {
            return Database.GetAccountTagsByTagAccountNo(tagAccountNo);
        }

        public AccountTagCollection GetAccountTagsByTagAccountNo(int tagAccountNo, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByTagAccountNo(tagAccountNo, sortOrder);
        }

        public AccountTagCollection GetAccountTagsByAccountNo(int accountNo)
        {
            return Database.GetAccountTagsByAccountNo(accountNo);
        }

        public AccountTagCollection GetAccountTagsByAccountNo(int accountNo, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByAccountNo(accountNo, sortOrder);
        }

        public AccountTagCollection GetAccountTagsByType(AccountTagType type)
        {
            return Database.GetAccountTagsByType(type);
        }

        public AccountTagCollection GetAccountTagsByType(AccountTagType type, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByType(type, sortOrder);
        }

        public AccountTagCollection GetAccountTagsByMoneyValue(decimal moneyValue)
        {
            return Database.GetAccountTagsByMoneyValue(moneyValue);
        }

        public AccountTagCollection GetAccountTagsByMoneyValue(decimal moneyValue, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByMoneyValue(moneyValue, sortOrder);
        }

        public AccountTagCollection GetAccountTagsByRelativeValue(decimal relativeValue)
        {
            return Database.GetAccountTagsByRelativeValue(relativeValue);
        }

        public AccountTagCollection GetAccountTagsByRelativeValue(decimal relativeValue, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByRelativeValue(relativeValue, sortOrder);
        }

        public AccountTagCollection GetAccountTagsByTagAccountName(string tagAccountName)
        {
            return Database.GetAccountTagsByTagAccountName(tagAccountName);
        }

        public AccountTagCollection GetAccountTagsByTagAccountName(string tagAccountName, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByTagAccountName(tagAccountName, sortOrder);
        }

        public AccountTagCollection GetAccountTagsByAmount(decimal amount)
        {
            return Database.GetAccountTagsByAmount(amount);
        }

        public AccountTagCollection GetAccountTagsByAmount(decimal amount, SortOrder sortOrder)
        {
            return Database.GetAccountTagsByAmount(amount, sortOrder);
        }

        public AccountTagCollection GetAccountTags()
        {
            return Database.GetAccountTags();
        }
        public AccountTagCollection GetAccountTags(Condition condition)
        {
            return Database.GetAccountTags(condition);
        }
        public DataTable GetAccountTagsTable(params string[] sColumns)
        {
            return Database.GetAccountTagsTable(sColumns);
        }
        public DataTable GetAccountTagsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetAccountTagsTable(condition, sColumns);
        }
        public DataTable GetAccountTagsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetAccountTagsTable(condition, sortOrder, sColumns);
        }
        public AccountTagCollection GetAccountTags(SortOrder order)
        {
            return Database.GetAccountTags(order);
        }
        public AccountTagCollection GetAccountTags(Condition condition, SortOrder sortOrder)
        {
            return Database.GetAccountTags(condition, sortOrder);
        }
        public int CountAccountTags()
        {
            return Database.CountAccountTags();
        }

        public int CountAccountTagsByNo(int no)
        {
            return Database.CountAccountTagsByNo(no);
        }

        public int CountAccountTagsByTagAccountNo(int tagAccountNo)
        {
            return Database.CountAccountTagsByTagAccountNo(tagAccountNo);
        }

        public int CountAccountTagsByAccountNo(int accountNo)
        {
            return Database.CountAccountTagsByAccountNo(accountNo);
        }

        public int CountAccountTagsByType(AccountTagType type)
        {
            return Database.CountAccountTagsByType(type);
        }

        public int CountAccountTagsByMoneyValue(decimal moneyValue)
        {
            return Database.CountAccountTagsByMoneyValue(moneyValue);
        }

        public int CountAccountTagsByRelativeValue(decimal relativeValue)
        {
            return Database.CountAccountTagsByRelativeValue(relativeValue);
        }

        public int CountAccountTagsByTagAccountName(string tagAccountName)
        {
            return Database.CountAccountTagsByTagAccountName(tagAccountName);
        }

        public int CountAccountTagsByAmount(decimal amount)
        {
            return Database.CountAccountTagsByAmount(amount);
        }

        public int CountAccountTags(Condition condition)
        {
            return Database.CountAccountTags(condition);
        }

        public void Save(AccountTag accountTag)
        {
            Database.Save(accountTag);
        }

        public bool DeleteAccountTag(int no)
        {
            return Database.DeleteAccountTag(no);
        }

        public int DeleteAllAccountTags()
        {
            return Database.DeleteAllAccountTags();
        }

        public int DeleteAccountTagsByTagAccountNo(int tagAccountNo)
        {
            return Database.DeleteAccountTagsByTagAccountNo(tagAccountNo);
        }

        public int DeleteAccountTagsByAccountNo(int accountNo)
        {
            return Database.DeleteAccountTagsByAccountNo(accountNo);
        }

        public bool DeleteAccountTagsByTagAccountNoAndAccountNo(int tagAccountNo, int accountNo)
        {
            return Database.DeleteAccountTagsByTagAccountNoAndAccountNo(tagAccountNo, accountNo);
        }

        public int DeleteAccountTags(Condition condition)
        {
            return Database.DeleteAccountTags(condition);
        }

        public Verification GetVerification(int no)
        {
            return Database.GetVerification(no);
        }

        //public Verification GetFullVerification(int no)
        //{
        //    Verification verification = Database.GetVerification(no);
        //    if (verification == null) {
        //        return null;
        //    }
        //    verification.AccountTransactions = Database.GetAccountTransactionsByVerificationNo(verification.No);
        //    verification.CashBookTransactions = Database.GetCashBookTransactionsByVerificationNo(verification.No);
        //    return verification;
        //}

        public Verification GetVerificationByYearAndSerialNo(int year, int serialNo)
        {
            return Database.GetVerificationByYearAndSerialNo(year, serialNo);
        }

        public VerificationCollection GetVerificationsByYear(int year)
        {
            return Database.GetVerificationsByYear(year);
        }

        public VerificationCollection GetVerificationsByYear(int year, SortOrder sortOrder)
        {
            return Database.GetVerificationsByYear(year, sortOrder);
        }

        public VerificationCollection GetVerificationsBySerialNo(int serialNo)
        {
            return Database.GetVerificationsBySerialNo(serialNo);
        }

        public VerificationCollection GetVerificationsBySerialNo(int serialNo, SortOrder sortOrder)
        {
            return Database.GetVerificationsBySerialNo(serialNo, sortOrder);
        }

        public VerificationCollection GetVerificationsByDate(DateTime date)
        {
            return Database.GetVerificationsByDate(date);
        }

        public VerificationCollection GetVerificationsByDate(DateTime date, SortOrder sortOrder)
        {
            return Database.GetVerificationsByDate(date, sortOrder);
        }

        public VerificationCollection GetVerificationsByAccountingDate(DateTime accountingDate)
        {
            return Database.GetVerificationsByAccountingDate(accountingDate);
        }

        public VerificationCollection GetVerificationsByAccountingDate(DateTime accountingDate, SortOrder sortOrder)
        {
            return Database.GetVerificationsByAccountingDate(accountingDate, sortOrder);
        }

        public VerificationCollection GetVerifications()
        {
            return Database.GetVerifications();
        }
        public VerificationCollection GetVerifications(Condition condition)
        {
            return Database.GetVerifications(condition);
        }
        public DataTable GetVerificationsTable(params string[] sColumns)
        {
            return Database.GetVerificationsTable(sColumns);
        }
        public DataTable GetVerificationsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetVerificationsTable(condition, sColumns);
        }
        public DataTable GetVerificationsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetVerificationsTable(condition, sortOrder, sColumns);
        }
        public VerificationCollection GetVerifications(SortOrder order)
        {
            return Database.GetVerifications(order);
        }
        public VerificationCollection GetVerifications(Condition condition, SortOrder sortOrder)
        {
            return Database.GetVerifications(condition, sortOrder);
        }
        public int CountVerifications()
        {
            return Database.CountVerifications();
        }

        public int CountVerificationsByNo(int no)
        {
            return Database.CountVerificationsByNo(no);
        }

        public int CountVerificationsByYear(int year)
        {
            return Database.CountVerificationsByYear(year);
        }

        public int CountVerificationsBySerialNo(int serialNo)
        {
            return Database.CountVerificationsBySerialNo(serialNo);
        }

        public int CountVerificationsByDate(DateTime date)
        {
            return Database.CountVerificationsByDate(date);
        }

        public int CountVerificationsByAccountingDate(DateTime accountingDate)
        {
            return Database.CountVerificationsByAccountingDate(accountingDate);
        }

        public int CountVerifications(Condition condition)
        {
            return Database.CountVerifications(condition);
        }

        public void Save(Verification verification)
        {
            Database.Save(verification);
        }

        public bool DeleteVerification(int no)
        {
            return Database.DeleteVerification(no);
        }

        public int DeleteAllVerifications()
        {
            return Database.DeleteAllVerifications();
        }

        public bool DeleteVerificationByYearAndSerialNo(int year, int serialNo)
        {
            return Database.DeleteVerificationByYearAndSerialNo(year, serialNo);
        }

        public int DeleteVerifications(Condition condition)
        {
            return Database.DeleteVerifications(condition);
        }

        public AccountTransaction GetAccountTransaction(int no)
        {
            return Database.GetAccountTransaction(no);
        }

        public AccountTransaction GetFullAccountTransaction(int no)
        {
            AccountTransaction accountTransaction = Database.GetAccountTransaction(no);
            if (accountTransaction == null) {
                return null;
            }
            accountTransaction.Account = Database.GetAccount(accountTransaction.No);
            accountTransaction.Verification = Database.GetVerification(accountTransaction.No);
            return accountTransaction;
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNoAndAccountNoAndVerificationNo(int userNo, int accountNo, int verificationNo)
        {
            return Database.GetAccountTransactionsByUserNoAndAccountNoAndVerificationNo(userNo, accountNo, verificationNo);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNoAndAccountNoAndVerificationNo(int userNo, int accountNo, int verificationNo, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByUserNoAndAccountNoAndVerificationNo(userNo, accountNo, verificationNo, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNo(int userNo)
        {
            return Database.GetAccountTransactionsByUserNo(userNo);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNo(int userNo, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByUserNo(userNo, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountNo(int accountNo)
        {
            return Database.GetAccountTransactionsByAccountNo(accountNo);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountNo(int accountNo, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByAccountNo(accountNo, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationNo(int verificationNo)
        {
            return Database.GetAccountTransactionsByVerificationNo(verificationNo);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationNo(int verificationNo, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByVerificationNo(verificationNo, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByAmount(decimal amount)
        {
            return Database.GetAccountTransactionsByAmount(amount);
        }

        public AccountTransactionCollection GetAccountTransactionsByAmount(decimal amount, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByAmount(amount, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByNote(string note)
        {
            return Database.GetAccountTransactionsByNote(note);
        }

        public AccountTransactionCollection GetAccountTransactionsByNote(string note, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByNote(note, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByCreatedTime(DateTime createdTime)
        {
            return Database.GetAccountTransactionsByCreatedTime(createdTime);
        }

        public AccountTransactionCollection GetAccountTransactionsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByCreatedTime(createdTime, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            return Database.GetAccountTransactionsByVerificationSerialNo(verificationSerialNo);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationSerialNo(int verificationSerialNo, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByVerificationSerialNo(verificationSerialNo, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByTransactionTime(DateTime transactionTime)
        {
            return Database.GetAccountTransactionsByTransactionTime(transactionTime);
        }

        public AccountTransactionCollection GetAccountTransactionsByTransactionTime(DateTime transactionTime, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByTransactionTime(transactionTime, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountingDate(DateTime accountingDate)
        {
            return Database.GetAccountTransactionsByAccountingDate(accountingDate);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountingDate(DateTime accountingDate, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByAccountingDate(accountingDate, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountName(string accountName)
        {
            return Database.GetAccountTransactionsByAccountName(accountName);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountName(string accountName, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByAccountName(accountName, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserName(string userName)
        {
            return Database.GetAccountTransactionsByUserName(userName);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserName(string userName, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByUserName(userName, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactionsByShowInDiagram(bool showInDiagram)
        {
            return Database.GetAccountTransactionsByShowInDiagram(showInDiagram);
        }

        public AccountTransactionCollection GetAccountTransactionsByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            return Database.GetAccountTransactionsByShowInDiagram(showInDiagram, sortOrder);
        }

        public AccountTransactionCollection GetAccountTransactions()
        {
            return Database.GetAccountTransactions();
        }
        public AccountTransactionCollection GetAccountTransactions(Condition condition)
        {
            return Database.GetAccountTransactions(condition);
        }
        public DataTable GetAccountTransactionsTable(params string[] sColumns)
        {
            return Database.GetAccountTransactionsTable(sColumns);
        }
        public DataTable GetAccountTransactionsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetAccountTransactionsTable(condition, sColumns);
        }
        public DataTable GetAccountTransactionsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetAccountTransactionsTable(condition, sortOrder, sColumns);
        }
        public AccountTransactionCollection GetAccountTransactions(SortOrder order)
        {
            return Database.GetAccountTransactions(order);
        }
        public AccountTransactionCollection GetAccountTransactions(Condition condition, SortOrder sortOrder)
        {
            return Database.GetAccountTransactions(condition, sortOrder);
        }
        public int CountAccountTransactions()
        {
            return Database.CountAccountTransactions();
        }

        public int CountAccountTransactionsByNo(int no)
        {
            return Database.CountAccountTransactionsByNo(no);
        }

        public int CountAccountTransactionsByUserNo(int userNo)
        {
            return Database.CountAccountTransactionsByUserNo(userNo);
        }

        public int CountAccountTransactionsByAccountNo(int accountNo)
        {
            return Database.CountAccountTransactionsByAccountNo(accountNo);
        }

        public int CountAccountTransactionsByVerificationNo(int verificationNo)
        {
            return Database.CountAccountTransactionsByVerificationNo(verificationNo);
        }

        public int CountAccountTransactionsByAmount(decimal amount)
        {
            return Database.CountAccountTransactionsByAmount(amount);
        }

        public int CountAccountTransactionsByNote(string note)
        {
            return Database.CountAccountTransactionsByNote(note);
        }

        public int CountAccountTransactionsByCreatedTime(DateTime createdTime)
        {
            return Database.CountAccountTransactionsByCreatedTime(createdTime);
        }

        public int CountAccountTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            return Database.CountAccountTransactionsByVerificationSerialNo(verificationSerialNo);
        }

        public int CountAccountTransactionsByTransactionTime(DateTime transactionTime)
        {
            return Database.CountAccountTransactionsByTransactionTime(transactionTime);
        }

        public int CountAccountTransactionsByAccountingDate(DateTime accountingDate)
        {
            return Database.CountAccountTransactionsByAccountingDate(accountingDate);
        }

        public int CountAccountTransactionsByAccountName(string accountName)
        {
            return Database.CountAccountTransactionsByAccountName(accountName);
        }

        public int CountAccountTransactionsByUserName(string userName)
        {
            return Database.CountAccountTransactionsByUserName(userName);
        }

        public int CountAccountTransactionsByShowInDiagram(bool showInDiagram)
        {
            return Database.CountAccountTransactionsByShowInDiagram(showInDiagram);
        }

        public int CountAccountTransactions(Condition condition)
        {
            return Database.CountAccountTransactions(condition);
        }

        public void Save(AccountTransaction accountTransaction)
        {
            Database.Save(accountTransaction);
        }

        public bool DeleteAccountTransaction(int no)
        {
            return Database.DeleteAccountTransaction(no);
        }

        public int DeleteAllAccountTransactions()
        {
            return Database.DeleteAllAccountTransactions();
        }

        public int DeleteAccountTransactionsByUserNo(int userNo)
        {
            return Database.DeleteAccountTransactionsByUserNo(userNo);
        }

        public int DeleteAccountTransactionsByAccountNo(int accountNo)
        {
            return Database.DeleteAccountTransactionsByAccountNo(accountNo);
        }

        public int DeleteAccountTransactionsByVerificationNo(int verificationNo)
        {
            return Database.DeleteAccountTransactionsByVerificationNo(verificationNo);
        }

        public bool DeleteAccountTransactionsByUserNoAndAccountNoAndVerificationNo(int userNo, int accountNo, int verificationNo)
        {
            return Database.DeleteAccountTransactionsByUserNoAndAccountNoAndVerificationNo(userNo, accountNo, verificationNo);
        }

        public int DeleteAccountTransactions(Condition condition)
        {
            return Database.DeleteAccountTransactions(condition);
        }

        public TagAccountSnapshot GetTagAccountSnapshot(int no)
        {
            return Database.GetTagAccountSnapshot(no);
        }

        public TagAccountSnapshot GetFullTagAccountSnapshot(int no)
        {
            TagAccountSnapshot tagAccountSnapshot = Database.GetTagAccountSnapshot(no);
            if (tagAccountSnapshot == null) {
                return null;
            }
            tagAccountSnapshot.TagAccount = Database.GetTagAccount(tagAccountSnapshot.No);
            return tagAccountSnapshot;
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNoAndTagAccountNo(int userNo, int tagAccountNo)
        {
            return Database.GetTagAccountSnapshotsByUserNoAndTagAccountNo(userNo, tagAccountNo);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNoAndTagAccountNo(int userNo, int tagAccountNo, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshotsByUserNoAndTagAccountNo(userNo, tagAccountNo, sortOrder);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNo(int userNo)
        {
            return Database.GetTagAccountSnapshotsByUserNo(userNo);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNo(int userNo, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshotsByUserNo(userNo, sortOrder);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByTagAccountNo(int tagAccountNo)
        {
            return Database.GetTagAccountSnapshotsByTagAccountNo(tagAccountNo);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByTagAccountNo(int tagAccountNo, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshotsByTagAccountNo(tagAccountNo, sortOrder);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByBalanceAmount(decimal balanceAmount)
        {
            return Database.GetTagAccountSnapshotsByBalanceAmount(balanceAmount);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByBalanceAmount(decimal balanceAmount, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshotsByBalanceAmount(balanceAmount, sortOrder);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByReason(TagAccountSnapshotReason reason)
        {
            return Database.GetTagAccountSnapshotsByReason(reason);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByReason(TagAccountSnapshotReason reason, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshotsByReason(reason, sortOrder);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByCreatedTime(DateTime createdTime)
        {
            return Database.GetTagAccountSnapshotsByCreatedTime(createdTime);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshotsByCreatedTime(createdTime, sortOrder);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByAccountName(string accountName)
        {
            return Database.GetTagAccountSnapshotsByAccountName(accountName);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByAccountName(string accountName, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshotsByAccountName(accountName, sortOrder);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshots()
        {
            return Database.GetTagAccountSnapshots();
        }
        public TagAccountSnapshotCollection GetTagAccountSnapshots(Condition condition)
        {
            return Database.GetTagAccountSnapshots(condition);
        }
        public DataTable GetTagAccountSnapshotsTable(params string[] sColumns)
        {
            return Database.GetTagAccountSnapshotsTable(sColumns);
        }
        public DataTable GetTagAccountSnapshotsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetTagAccountSnapshotsTable(condition, sColumns);
        }
        public DataTable GetTagAccountSnapshotsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetTagAccountSnapshotsTable(condition, sortOrder, sColumns);
        }
        public TagAccountSnapshotCollection GetTagAccountSnapshots(SortOrder order)
        {
            return Database.GetTagAccountSnapshots(order);
        }
        public TagAccountSnapshotCollection GetTagAccountSnapshots(Condition condition, SortOrder sortOrder)
        {
            return Database.GetTagAccountSnapshots(condition, sortOrder);
        }
        public int CountTagAccountSnapshots()
        {
            return Database.CountTagAccountSnapshots();
        }

        public int CountTagAccountSnapshotsByNo(int no)
        {
            return Database.CountTagAccountSnapshotsByNo(no);
        }

        public int CountTagAccountSnapshotsByUserNo(int userNo)
        {
            return Database.CountTagAccountSnapshotsByUserNo(userNo);
        }

        public int CountTagAccountSnapshotsByTagAccountNo(int tagAccountNo)
        {
            return Database.CountTagAccountSnapshotsByTagAccountNo(tagAccountNo);
        }

        public int CountTagAccountSnapshotsByBalanceAmount(decimal balanceAmount)
        {
            return Database.CountTagAccountSnapshotsByBalanceAmount(balanceAmount);
        }

        public int CountTagAccountSnapshotsByReason(TagAccountSnapshotReason reason)
        {
            return Database.CountTagAccountSnapshotsByReason(reason);
        }

        public int CountTagAccountSnapshotsByCreatedTime(DateTime createdTime)
        {
            return Database.CountTagAccountSnapshotsByCreatedTime(createdTime);
        }

        public int CountTagAccountSnapshotsByAccountName(string accountName)
        {
            return Database.CountTagAccountSnapshotsByAccountName(accountName);
        }

        public int CountTagAccountSnapshots(Condition condition)
        {
            return Database.CountTagAccountSnapshots(condition);
        }

        public void Save(TagAccountSnapshot tagAccountSnapshot)
        {
            Database.Save(tagAccountSnapshot);
        }

        public bool DeleteTagAccountSnapshot(int no)
        {
            return Database.DeleteTagAccountSnapshot(no);
        }

        public int DeleteAllTagAccountSnapshots()
        {
            return Database.DeleteAllTagAccountSnapshots();
        }

        public int DeleteTagAccountSnapshotsByUserNo(int userNo)
        {
            return Database.DeleteTagAccountSnapshotsByUserNo(userNo);
        }

        public int DeleteTagAccountSnapshotsByTagAccountNo(int tagAccountNo)
        {
            return Database.DeleteTagAccountSnapshotsByTagAccountNo(tagAccountNo);
        }

        public bool DeleteTagAccountSnapshotsByUserNoAndTagAccountNo(int userNo, int tagAccountNo)
        {
            return Database.DeleteTagAccountSnapshotsByUserNoAndTagAccountNo(userNo, tagAccountNo);
        }

        public int DeleteTagAccountSnapshots(Condition condition)
        {
            return Database.DeleteTagAccountSnapshots(condition);
        }

        public CashBookTransaction GetCashBookTransaction(int no)
        {
            return Database.GetCashBookTransaction(no);
        }

        public CashBookTransaction GetFullCashBookTransaction(int no)
        {
            CashBookTransaction cashBookTransaction = Database.GetCashBookTransaction(no);
            if (cashBookTransaction == null) {
                return null;
            }
            cashBookTransaction.Verification = Database.GetVerification(cashBookTransaction.No);
            return cashBookTransaction;
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(int userNo, int categoryNo, int verificationNo)
        {
            return Database.GetCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(userNo, categoryNo, verificationNo);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(int userNo, int categoryNo, int verificationNo, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(userNo, categoryNo, verificationNo, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNo(int userNo)
        {
            return Database.GetCashBookTransactionsByUserNo(userNo);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNo(int userNo, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByUserNo(userNo, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryNo(int categoryNo)
        {
            return Database.GetCashBookTransactionsByCategoryNo(categoryNo);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryNo(int categoryNo, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByCategoryNo(categoryNo, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationNo(int verificationNo)
        {
            return Database.GetCashBookTransactionsByVerificationNo(verificationNo);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationNo(int verificationNo, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByVerificationNo(verificationNo, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAmount(decimal amount)
        {
            return Database.GetCashBookTransactionsByAmount(amount);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAmount(decimal amount, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByAmount(amount, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByNote(string note)
        {
            return Database.GetCashBookTransactionsByNote(note);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByNote(string note, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByNote(note, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCreatedTime(DateTime createdTime)
        {
            return Database.GetCashBookTransactionsByCreatedTime(createdTime);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByCreatedTime(createdTime, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            return Database.GetCashBookTransactionsByVerificationSerialNo(verificationSerialNo);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationSerialNo(int verificationSerialNo, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByVerificationSerialNo(verificationSerialNo, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByTransactionTime(DateTime transactionTime)
        {
            return Database.GetCashBookTransactionsByTransactionTime(transactionTime);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByTransactionTime(DateTime transactionTime, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByTransactionTime(transactionTime, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAccountingDate(DateTime accountingDate)
        {
            return Database.GetCashBookTransactionsByAccountingDate(accountingDate);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAccountingDate(DateTime accountingDate, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByAccountingDate(accountingDate, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryName(string categoryName)
        {
            return Database.GetCashBookTransactionsByCategoryName(categoryName);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryName(string categoryName, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByCategoryName(categoryName, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByShowInDiagram(bool showInDiagram)
        {
            return Database.GetCashBookTransactionsByShowInDiagram(showInDiagram);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactionsByShowInDiagram(showInDiagram, sortOrder);
        }

        public CashBookTransactionCollection GetCashBookTransactions()
        {
            return Database.GetCashBookTransactions();
        }
        public CashBookTransactionCollection GetCashBookTransactions(Condition condition)
        {
            return Database.GetCashBookTransactions(condition);
        }
        public DataTable GetCashBookTransactionsTable(params string[] sColumns)
        {
            return Database.GetCashBookTransactionsTable(sColumns);
        }
        public DataTable GetCashBookTransactionsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetCashBookTransactionsTable(condition, sColumns);
        }
        public DataTable GetCashBookTransactionsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetCashBookTransactionsTable(condition, sortOrder, sColumns);
        }
        public CashBookTransactionCollection GetCashBookTransactions(SortOrder order)
        {
            return Database.GetCashBookTransactions(order);
        }
        public CashBookTransactionCollection GetCashBookTransactions(Condition condition, SortOrder sortOrder)
        {
            return Database.GetCashBookTransactions(condition, sortOrder);
        }
        public int CountCashBookTransactions()
        {
            return Database.CountCashBookTransactions();
        }

        public int CountCashBookTransactionsByNo(int no)
        {
            return Database.CountCashBookTransactionsByNo(no);
        }

        public int CountCashBookTransactionsByUserNo(int userNo)
        {
            return Database.CountCashBookTransactionsByUserNo(userNo);
        }

        public int CountCashBookTransactionsByCategoryNo(int categoryNo)
        {
            return Database.CountCashBookTransactionsByCategoryNo(categoryNo);
        }

        public int CountCashBookTransactionsByVerificationNo(int verificationNo)
        {
            return Database.CountCashBookTransactionsByVerificationNo(verificationNo);
        }

        public int CountCashBookTransactionsByAmount(decimal amount)
        {
            return Database.CountCashBookTransactionsByAmount(amount);
        }

        public int CountCashBookTransactionsByNote(string note)
        {
            return Database.CountCashBookTransactionsByNote(note);
        }

        public int CountCashBookTransactionsByCreatedTime(DateTime createdTime)
        {
            return Database.CountCashBookTransactionsByCreatedTime(createdTime);
        }

        public int CountCashBookTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            return Database.CountCashBookTransactionsByVerificationSerialNo(verificationSerialNo);
        }

        public int CountCashBookTransactionsByTransactionTime(DateTime transactionTime)
        {
            return Database.CountCashBookTransactionsByTransactionTime(transactionTime);
        }

        public int CountCashBookTransactionsByAccountingDate(DateTime accountingDate)
        {
            return Database.CountCashBookTransactionsByAccountingDate(accountingDate);
        }

        public int CountCashBookTransactionsByCategoryName(string categoryName)
        {
            return Database.CountCashBookTransactionsByCategoryName(categoryName);
        }

        public int CountCashBookTransactionsByShowInDiagram(bool showInDiagram)
        {
            return Database.CountCashBookTransactionsByShowInDiagram(showInDiagram);
        }

        public int CountCashBookTransactions(Condition condition)
        {
            return Database.CountCashBookTransactions(condition);
        }

        public void Save(CashBookTransaction cashBookTransaction)
        {
            Database.Save(cashBookTransaction);
        }

        public bool DeleteCashBookTransaction(int no)
        {
            return Database.DeleteCashBookTransaction(no);
        }

        public int DeleteAllCashBookTransactions()
        {
            return Database.DeleteAllCashBookTransactions();
        }

        public int DeleteCashBookTransactionsByUserNo(int userNo)
        {
            return Database.DeleteCashBookTransactionsByUserNo(userNo);
        }

        public int DeleteCashBookTransactionsByCategoryNo(int categoryNo)
        {
            return Database.DeleteCashBookTransactionsByCategoryNo(categoryNo);
        }

        public int DeleteCashBookTransactionsByVerificationNo(int verificationNo)
        {
            return Database.DeleteCashBookTransactionsByVerificationNo(verificationNo);
        }

        public bool DeleteCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(int userNo, int categoryNo, int verificationNo)
        {
            return Database.DeleteCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(userNo, categoryNo, verificationNo);
        }

        public int DeleteCashBookTransactions(Condition condition)
        {
            return Database.DeleteCashBookTransactions(condition);
        }

        public LogItem GetLogItem(int no)
        {
            return Database.GetLogItem(no);
        }

        public LogItemCollection GetLogItemsByUserNoAndVerificationNoAndAccountNo(int? userNo, int? verificationNo, int? accountNo)
        {
            return Database.GetLogItemsByUserNoAndVerificationNoAndAccountNo(userNo, verificationNo, accountNo);
        }

        public LogItemCollection GetLogItemsByUserNoAndVerificationNoAndAccountNo(int? userNo, int? verificationNo, int? accountNo, SortOrder sortOrder)
        {
            return Database.GetLogItemsByUserNoAndVerificationNoAndAccountNo(userNo, verificationNo, accountNo, sortOrder);
        }

        public LogItemCollection GetLogItemsByUserNo(int? userNo)
        {
            return Database.GetLogItemsByUserNo(userNo);
        }

        public LogItemCollection GetLogItemsByUserNo(int? userNo, SortOrder sortOrder)
        {
            return Database.GetLogItemsByUserNo(userNo, sortOrder);
        }

        public LogItemCollection GetLogItemsByVerificationNo(int? verificationNo)
        {
            return Database.GetLogItemsByVerificationNo(verificationNo);
        }

        public LogItemCollection GetLogItemsByVerificationNo(int? verificationNo, SortOrder sortOrder)
        {
            return Database.GetLogItemsByVerificationNo(verificationNo, sortOrder);
        }

        public LogItemCollection GetLogItemsByAccountNo(int? accountNo)
        {
            return Database.GetLogItemsByAccountNo(accountNo);
        }

        public LogItemCollection GetLogItemsByAccountNo(int? accountNo, SortOrder sortOrder)
        {
            return Database.GetLogItemsByAccountNo(accountNo, sortOrder);
        }

        public LogItemCollection GetLogItemsByType(LogItemType type)
        {
            return Database.GetLogItemsByType(type);
        }

        public LogItemCollection GetLogItemsByType(LogItemType type, SortOrder sortOrder)
        {
            return Database.GetLogItemsByType(type, sortOrder);
        }

        public LogItemCollection GetLogItemsByDescription(string description)
        {
            return Database.GetLogItemsByDescription(description);
        }

        public LogItemCollection GetLogItemsByDescription(string description, SortOrder sortOrder)
        {
            return Database.GetLogItemsByDescription(description, sortOrder);
        }

        public LogItemCollection GetLogItemsByLogTime(DateTime logTime)
        {
            return Database.GetLogItemsByLogTime(logTime);
        }

        public LogItemCollection GetLogItemsByLogTime(DateTime logTime, SortOrder sortOrder)
        {
            return Database.GetLogItemsByLogTime(logTime, sortOrder);
        }

        public LogItemCollection GetLogItems()
        {
            return Database.GetLogItems();
        }
        public LogItemCollection GetLogItems(Condition condition)
        {
            return Database.GetLogItems(condition);
        }
        public DataTable GetLogItemsTable(params string[] sColumns)
        {
            return Database.GetLogItemsTable(sColumns);
        }
        public DataTable GetLogItemsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetLogItemsTable(condition, sColumns);
        }
        public DataTable GetLogItemsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetLogItemsTable(condition, sortOrder, sColumns);
        }
        public LogItemCollection GetLogItems(SortOrder order)
        {
            return Database.GetLogItems(order);
        }
        public LogItemCollection GetLogItems(Condition condition, SortOrder sortOrder)
        {
            return Database.GetLogItems(condition, sortOrder);
        }
        public int CountLogItems()
        {
            return Database.CountLogItems();
        }

        public int CountLogItemsByNo(int no)
        {
            return Database.CountLogItemsByNo(no);
        }

        public int CountLogItemsByUserNo(int? userNo)
        {
            return Database.CountLogItemsByUserNo(userNo);
        }

        public int CountLogItemsByVerificationNo(int? verificationNo)
        {
            return Database.CountLogItemsByVerificationNo(verificationNo);
        }

        public int CountLogItemsByAccountNo(int? accountNo)
        {
            return Database.CountLogItemsByAccountNo(accountNo);
        }

        public int CountLogItemsByType(LogItemType type)
        {
            return Database.CountLogItemsByType(type);
        }

        public int CountLogItemsByDescription(string description)
        {
            return Database.CountLogItemsByDescription(description);
        }

        public int CountLogItemsByLogTime(DateTime logTime)
        {
            return Database.CountLogItemsByLogTime(logTime);
        }

        public int CountLogItems(Condition condition)
        {
            return Database.CountLogItems(condition);
        }

        public void Save(LogItem logItem)
        {
            Database.Save(logItem);
        }

        public bool DeleteLogItem(int no)
        {
            return Database.DeleteLogItem(no);
        }

        public int DeleteAllLogItems()
        {
            return Database.DeleteAllLogItems();
        }

        public int DeleteLogItemsByUserNo(int? userNo)
        {
            return Database.DeleteLogItemsByUserNo(userNo);
        }

        public int DeleteLogItemsByVerificationNo(int? verificationNo)
        {
            return Database.DeleteLogItemsByVerificationNo(verificationNo);
        }

        public int DeleteLogItemsByAccountNo(int? accountNo)
        {
            return Database.DeleteLogItemsByAccountNo(accountNo);
        }

        public bool DeleteLogItemsByUserNoAndVerificationNoAndAccountNo(int? userNo, int? verificationNo, int? accountNo)
        {
            return Database.DeleteLogItemsByUserNoAndVerificationNoAndAccountNo(userNo, verificationNo, accountNo);
        }

        public int DeleteLogItems(Condition condition)
        {
            return Database.DeleteLogItems(condition);
        }

        public CashBoxSettings GetCashBoxSettings(CashBoxSettingsNo no)
        {
            return Database.GetCashBoxSettings(no);
        }

        public CashBoxSettingsCollection GetCashBoxSettingsByAccountingYear(int accountingYear)
        {
            return Database.GetCashBoxSettingsByAccountingYear(accountingYear);
        }

        public CashBoxSettingsCollection GetCashBoxSettingsByAccountingYear(int accountingYear, SortOrder sortOrder)
        {
            return Database.GetCashBoxSettingsByAccountingYear(accountingYear, sortOrder);
        }

        public CashBoxSettingsCollection GetCashBoxSettings()
        {
            return Database.GetCashBoxSettings();
        }
        public CashBoxSettingsCollection GetCashBoxSettings(Condition condition)
        {
            return Database.GetCashBoxSettings(condition);
        }
        public DataTable GetCashBoxSettingsTable(params string[] sColumns)
        {
            return Database.GetCashBoxSettingsTable(sColumns);
        }
        public DataTable GetCashBoxSettingsTable(Condition condition, params string[] sColumns)
        {
            return Database.GetCashBoxSettingsTable(condition, sColumns);
        }
        public DataTable GetCashBoxSettingsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            return Database.GetCashBoxSettingsTable(condition, sortOrder, sColumns);
        }
        public CashBoxSettingsCollection GetCashBoxSettings(SortOrder order)
        {
            return Database.GetCashBoxSettings(order);
        }
        public CashBoxSettingsCollection GetCashBoxSettings(Condition condition, SortOrder sortOrder)
        {
            return Database.GetCashBoxSettings(condition, sortOrder);
        }
        public int CountCashBoxSettings()
        {
            return Database.CountCashBoxSettings();
        }

        public int CountCashBoxSettingsByNo(CashBoxSettingsNo no)
        {
            return Database.CountCashBoxSettingsByNo(no);
        }

        public int CountCashBoxSettingsByAccountingYear(int accountingYear)
        {
            return Database.CountCashBoxSettingsByAccountingYear(accountingYear);
        }

        public int CountCashBoxSettings(Condition condition)
        {
            return Database.CountCashBoxSettings(condition);
        }

        public void Save(CashBoxSettings cashBoxSettings)
        {
            Database.Save(cashBoxSettings);
        }

        public bool DeleteCashBoxSettings(CashBoxSettingsNo no)
        {
            return Database.DeleteCashBoxSettings(no);
        }

        public int DeleteAllCashBoxSettings()
        {
            return Database.DeleteAllCashBoxSettings();
        }

        public int DeleteCashBoxSettings(Condition condition)
        {
            return Database.DeleteCashBoxSettings(condition);
        }
    }
}
