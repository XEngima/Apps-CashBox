using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EasyBase.DataLayer")]

namespace EasyBase.Classes
{
    [DatabaseTable("Users")]
    public partial class User
    {
        public const string fNo = "No";
        public const string fName = "Name";
        public const string fEmail = "Email";
        public const string fPassword = "Password";
        public const string fCreatedTime = "CreatedTime";

        internal User(UserDataRow userDataRow)
        {
            No = userDataRow.No;
            Name = userDataRow.Name;
            Email = userDataRow.Email;
            Password = userDataRow.Password;
            CreatedTime = userDataRow.CreatedTime;
        }

        public User(string name, string email, string password, DateTime createdTime)
        {
            No = 0;
            Name = name;
            Email = email;
            Password = password;
            CreatedTime = createdTime;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [DatabaseField("Name", "varchar(128)")]
        public string Name
        {
            get;
            set;
        }

        [DatabaseField("Email", "varchar(128)", UniqueKeyGroupsString = "1")]
        public string Email
        {
            get;
            set;
        }

        [DatabaseField("Password", "varchar(128)")]
        public string Password
        {
            get;
            set;
        }

        [DatabaseField("CreatedTime", "datetime")]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "Name=" + Name + ", " + "Email=" + Email + ", " + "Password=" + Password + ", " + "CreatedTime=" + CreatedTime;
            }
        }
    }

    [DatabaseTable("Categories")]
    public partial class Category
    {
        public const string fNo = "No";
        public const string fParentCategoryNo = "ParentCategoryNo";
        public const string fType = "Type";
        public const string fName = "Name";
        public const string fIsArchived = "IsArchived";
        public const string fCreatedTime = "CreatedTime";
        public const string fShowInDiagram = "ShowInDiagram";

        internal Category(CategoryDataRow categoryDataRow)
        {
            No = categoryDataRow.No;
            ParentCategoryNo = categoryDataRow.ParentCategoryNo;
            Type = categoryDataRow.Type;
            Name = categoryDataRow.Name;
            IsArchived = categoryDataRow.IsArchived;
            CreatedTime = categoryDataRow.CreatedTime;
            ShowInDiagram = categoryDataRow.ShowInDiagram;
        }

        public Category(int? parentCategoryNo, CategoryType type, string name, bool isArchived, DateTime createdTime, bool showInDiagram)
        {
            No = 0;
            ParentCategoryNo = parentCategoryNo;
            Type = type;
            Name = name;
            IsArchived = isArchived;
            CreatedTime = createdTime;
            ShowInDiagram = showInDiagram;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [ForeignKeyField("ParentCategoryNo", "int", "Categories", "No", true)]
        public int? ParentCategoryNo
        {
            get;
            set;
        }

        [DatabaseField("Type", "int")]
        public CategoryType Type
        {
            get;
            set;
        }

        [DatabaseField("Name", "varchar(128)")]
        public string Name
        {
            get;
            set;
        }

        [DatabaseField("IsArchived", "bit")]
        public bool IsArchived
        {
            get;
            set;
        }

        [DatabaseField("CreatedTime", "datetime")]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        [DatabaseField("ShowInDiagram", "bit")]
        public bool ShowInDiagram
        {
            get;
            set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "ParentCategoryNo=" + ParentCategoryNo + ", " + "Type=" + Type + ", " + "Name=" + Name + ", " + "IsArchived=" + IsArchived + ", " + "CreatedTime=" + CreatedTime + ", " + "ShowInDiagram=" + ShowInDiagram;
            }
        }
    }

    [DatabaseTable("Accounts", IsView = true)]
    public partial class Account
    {
        public const string fNo = "No";
        public const string fUserNo = "UserNo";
        public const string fType = "Type";
        public const string fName = "Name";
        public const string fBalanceBroughtForwardAmount = "BalanceBroughtForwardAmount";
        public const string fIsArchived = "IsArchived";
        public const string fCreatedTime = "CreatedTime";
        public const string fShowInDiagram = "ShowInDiagram";
        public const string fBalance = "Balance";

        internal Account(AccountDataRow accountDataRow)
        {
            No = accountDataRow.No;
            UserNo = accountDataRow.UserNo;
            Type = accountDataRow.Type;
            Name = accountDataRow.Name;
            BalanceBroughtForwardAmount = accountDataRow.BalanceBroughtForwardAmount;
            IsArchived = accountDataRow.IsArchived;
            CreatedTime = accountDataRow.CreatedTime;
            ShowInDiagram = accountDataRow.ShowInDiagram;
            Balance = accountDataRow.Balance;
            AccountTransactions = null;
        }

        public Account(int userNo, AccountType type, string name, decimal balanceBroughtForwardAmount, bool isArchived, DateTime createdTime, bool showInDiagram)
        {
            No = 0;
            UserNo = userNo;
            Type = type;
            Name = name;
            BalanceBroughtForwardAmount = balanceBroughtForwardAmount;
            IsArchived = isArchived;
            CreatedTime = createdTime;
            ShowInDiagram = showInDiagram;
            Balance = 0;
            AccountTransactions = null;
        }

        public AccountTransactionCollection AccountTransactions
        {
            get;
            set;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [ForeignKeyField("UserNo", "int", "Users", "No")]
        public int UserNo
        {
            get;
            set;
        }

        [DatabaseField("Type", "int")]
        public AccountType Type
        {
            get;
            set;
        }

        [DatabaseField("Name", "varchar(128)")]
        public string Name
        {
            get;
            set;
        }

        [DatabaseField("BalanceBroughtForwardAmount", "money")]
        public decimal BalanceBroughtForwardAmount
        {
            get;
            set;
        }

        [DatabaseField("IsArchived", "bit")]
        public bool IsArchived
        {
            get;
            set;
        }

        [DatabaseField("CreatedTime", "datetime")]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        [DatabaseField("ShowInDiagram", "bit")]
        public bool ShowInDiagram
        {
            get;
            set;
        }

        [ViewField("Balance", "money")]
        public decimal Balance
        {
            get;
            private set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "UserNo=" + UserNo + ", " + "Type=" + Type + ", " + "Name=" + Name + ", " + "BalanceBroughtForwardAmount=" + BalanceBroughtForwardAmount + ", " + "IsArchived=" + IsArchived + ", " + "CreatedTime=" + CreatedTime + ", " + "ShowInDiagram=" + ShowInDiagram + ", " + "Balance=" + Balance;
            }
        }
    }

    [DatabaseTable("TagAccounts", IsView = true)]
    public partial class TagAccount
    {
        public const string fNo = "No";
        public const string fName = "Name";
        public const string fIsArchived = "IsArchived";
        public const string fCreatedTime = "CreatedTime";
        public const string fAmount = "Amount";

        internal TagAccount(TagAccountDataRow tagAccountDataRow)
        {
            No = tagAccountDataRow.No;
            Name = tagAccountDataRow.Name;
            IsArchived = tagAccountDataRow.IsArchived;
            CreatedTime = tagAccountDataRow.CreatedTime;
            Amount = tagAccountDataRow.Amount;
            TagAccountSnapshots = null;
        }

        public TagAccount(string name, bool isArchived, DateTime createdTime)
        {
            No = 0;
            Name = name;
            IsArchived = isArchived;
            CreatedTime = createdTime;
            Amount = 0;
            TagAccountSnapshots = null;
        }

        public TagAccountSnapshotCollection TagAccountSnapshots
        {
            get;
            set;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [DatabaseField("Name", "varchar(128)", UniqueKeyGroupsString = "1")]
        public string Name
        {
            get;
            set;
        }

        [DatabaseField("IsArchived", "bit")]
        public bool IsArchived
        {
            get;
            set;
        }

        [DatabaseField("CreatedTime", "datetime")]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        [ViewField("Amount", "money")]
        public decimal Amount
        {
            get;
            private set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "Name=" + Name + ", " + "IsArchived=" + IsArchived + ", " + "CreatedTime=" + CreatedTime + ", " + "Amount=" + Amount;
            }
        }
    }

    [DatabaseTable("AccountTags", IsView = true)]
    public partial class AccountTag
    {
        public const string fNo = "No";
        public const string fTagAccountNo = "TagAccountNo";
        public const string fAccountNo = "AccountNo";
        public const string fType = "Type";
        public const string fMoneyValue = "MoneyValue";
        public const string fRelativeValue = "RelativeValue";
        public const string fTagAccountName = "TagAccountName";
        public const string fAmount = "Amount";

        internal AccountTag(AccountTagDataRow accountTagDataRow)
        {
            No = accountTagDataRow.No;
            TagAccountNo = accountTagDataRow.TagAccountNo;
            AccountNo = accountTagDataRow.AccountNo;
            Type = accountTagDataRow.Type;
            MoneyValue = accountTagDataRow.MoneyValue;
            RelativeValue = accountTagDataRow.RelativeValue;
            TagAccountName = accountTagDataRow.TagAccountName;
            Amount = accountTagDataRow.Amount;
        }

        public AccountTag(int tagAccountNo, int accountNo, AccountTagType type, decimal moneyValue, decimal relativeValue)
        {
            No = 0;
            TagAccountNo = tagAccountNo;
            AccountNo = accountNo;
            Type = type;
            MoneyValue = moneyValue;
            RelativeValue = relativeValue;
            TagAccountName = "";
            Amount = 0;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [ForeignKeyField("TagAccountNo", "int", "TagAccounts", "No")]
        public int TagAccountNo
        {
            get;
            set;
        }

        [ForeignKeyField("AccountNo", "int", "Accounts", "No")]
        public int AccountNo
        {
            get;
            set;
        }

        [DatabaseField("Type", "int")]
        public AccountTagType Type
        {
            get;
            set;
        }

        [DatabaseField("MoneyValue", "money")]
        public decimal MoneyValue
        {
            get;
            set;
        }

        [DatabaseField("RelativeValue", "decimal(18,9)")]
        public decimal RelativeValue
        {
            get;
            set;
        }

        [ViewField("TagAccountName", "varchar(128)")]
        public string TagAccountName
        {
            get;
            private set;
        }

        [ViewField("Amount", "money")]
        public decimal Amount
        {
            get;
            private set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "TagAccountNo=" + TagAccountNo + ", " + "AccountNo=" + AccountNo + ", " + "Type=" + Type + ", " + "MoneyValue=" + MoneyValue + ", " + "RelativeValue=" + RelativeValue + ", " + "TagAccountName=" + TagAccountName + ", " + "Amount=" + Amount;
            }
        }
    }

    [DatabaseTable("Verifications")]
    public partial class Verification
    {
        public const string fNo = "No";
        public const string fYear = "Year";
        public const string fSerialNo = "SerialNo";
        public const string fDate = "Date";
        public const string fAccountingDate = "AccountingDate";

        internal Verification(VerificationDataRow verificationDataRow)
        {
            No = verificationDataRow.No;
            Year = verificationDataRow.Year;
            SerialNo = verificationDataRow.SerialNo;
            Date = verificationDataRow.Date;
            AccountingDate = verificationDataRow.AccountingDate;
            AccountTransactions = null;
            CashBookTransactions = null;
        }

        public Verification(int year, int serialNo, DateTime date, DateTime accountingDate)
        {
            No = 0;
            Year = year;
            SerialNo = serialNo;
            Date = date;
            AccountingDate = accountingDate;
            AccountTransactions = null;
            CashBookTransactions = null;
        }

        public AccountTransactionCollection AccountTransactions
        {
            get;
            set;
        }

        public CashBookTransactionCollection CashBookTransactions
        {
            get;
            set;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [DatabaseField("Year", "int", UniqueKeyGroupsString = "1")]
        public int Year
        {
            get;
            set;
        }

        [DatabaseField("SerialNo", "int", UniqueKeyGroupsString = "1")]
        public int SerialNo
        {
            get;
            set;
        }

        [DatabaseField("Date", "smalldatetime")]
        public DateTime Date
        {
            get;
            set;
        }

        [DatabaseField("AccountingDate", "smalldatetime")]
        public DateTime AccountingDate
        {
            get;
            set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "Year=" + Year + ", " + "SerialNo=" + SerialNo + ", " + "Date=" + Date + ", " + "AccountingDate=" + AccountingDate;
            }
        }
    }

    [DatabaseTable("AccountTransactions", IsView = true)]
    public partial class AccountTransaction
    {
        public const string fNo = "No";
        public const string fUserNo = "UserNo";
        public const string fAccountNo = "AccountNo";
        public const string fVerificationNo = "VerificationNo";
        public const string fAmount = "Amount";
        public const string fNote = "Note";
        public const string fCreatedTime = "CreatedTime";
        public const string fVerificationSerialNo = "VerificationSerialNo";
        public const string fTransactionTime = "TransactionTime";
        public const string fAccountingDate = "AccountingDate";
        public const string fAccountName = "AccountName";
        public const string fUserName = "UserName";
        public const string fShowInDiagram = "ShowInDiagram";

        internal AccountTransaction(AccountTransactionDataRow accountTransactionDataRow)
        {
            No = accountTransactionDataRow.No;
            UserNo = accountTransactionDataRow.UserNo;
            AccountNo = accountTransactionDataRow.AccountNo;
            VerificationNo = accountTransactionDataRow.VerificationNo;
            Amount = accountTransactionDataRow.Amount;
            Note = accountTransactionDataRow.Note;
            CreatedTime = accountTransactionDataRow.CreatedTime;
            VerificationSerialNo = accountTransactionDataRow.VerificationSerialNo;
            TransactionTime = accountTransactionDataRow.TransactionTime;
            AccountingDate = accountTransactionDataRow.AccountingDate;
            AccountName = accountTransactionDataRow.AccountName;
            UserName = accountTransactionDataRow.UserName;
            ShowInDiagram = accountTransactionDataRow.ShowInDiagram;
            Account = null;
            Verification = null;
        }

        public AccountTransaction(int userNo, int accountNo, int verificationNo, DateTime transactionTime, DateTime accountingDate, decimal amount, string note, DateTime createdTime)
        {
            No = 0;
            UserNo = userNo;
            AccountNo = accountNo;
            VerificationNo = verificationNo;
            Amount = amount;
            Note = note;
            CreatedTime = createdTime;
            VerificationSerialNo = 0;
            TransactionTime = transactionTime;
            AccountingDate = accountingDate;
            AccountName = "";
            UserName = "";
            ShowInDiagram = false;
            Account = null;
            Verification = null;
        }

        public Account Account
        {
            get;
            set;
        }

        public Verification Verification
        {
            get;
            set;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [ForeignKeyField("UserNo", "int", "Users", "No")]
        public int UserNo
        {
            get;
            set;
        }

        [ForeignKeyField("AccountNo", "int", "Accounts", "No")]
        public int AccountNo
        {
            get;
            set;
        }

        [ForeignKeyField("VerificationNo", "int", "Verifications", "No")]
        public int VerificationNo
        {
            get;
            set;
        }

        [DatabaseField("Amount", "money")]
        public decimal Amount
        {
            get;
            set;
        }

        [DatabaseField("Note", "varchar(1024)")]
        public string Note
        {
            get;
            set;
        }

        [DatabaseField("CreatedTime", "datetime")]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        [ViewField("VerificationSerialNo", "int")]
        public int VerificationSerialNo
        {
            get;
            private set;
        }

        [ViewField("TransactionTime", "datetime")]
        public DateTime TransactionTime
        {
            get;
            private set;
        }

        [ViewField("AccountingDate", "datetime")]
        public DateTime AccountingDate
        {
            get;
            private set;
        }

        [ViewField("AccountName", "varchar(128)")]
        public string AccountName
        {
            get;
            private set;
        }

        [ViewField("UserName", "varchar(128)")]
        public string UserName
        {
            get;
            private set;
        }

        [ViewField("ShowInDiagram", "bit")]
        public bool ShowInDiagram
        {
            get;
            private set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "UserNo=" + UserNo + ", " + "AccountNo=" + AccountNo + ", " + "VerificationNo=" + VerificationNo + ", " + "Amount=" + Amount + ", " + "Note=" + Note + ", " + "CreatedTime=" + CreatedTime + ", " + "VerificationSerialNo=" + VerificationSerialNo + ", " + "TransactionTime=" + TransactionTime + ", " + "AccountingDate=" + AccountingDate + ", " + "AccountName=" + AccountName + ", " + "UserName=" + UserName + ", " + "ShowInDiagram=" + ShowInDiagram;
            }
        }
    }

    [DatabaseTable("TagAccountSnapshots", IsView = true)]
    public partial class TagAccountSnapshot
    {
        public const string fNo = "No";
        public const string fUserNo = "UserNo";
        public const string fTagAccountNo = "TagAccountNo";
        public const string fBalanceAmount = "BalanceAmount";
        public const string fReason = "Reason";
        public const string fCreatedTime = "CreatedTime";
        public const string fAccountName = "AccountName";

        internal TagAccountSnapshot(TagAccountSnapshotDataRow tagAccountSnapshotDataRow)
        {
            No = tagAccountSnapshotDataRow.No;
            UserNo = tagAccountSnapshotDataRow.UserNo;
            TagAccountNo = tagAccountSnapshotDataRow.TagAccountNo;
            BalanceAmount = tagAccountSnapshotDataRow.BalanceAmount;
            Reason = tagAccountSnapshotDataRow.Reason;
            CreatedTime = tagAccountSnapshotDataRow.CreatedTime;
            AccountName = tagAccountSnapshotDataRow.AccountName;
            TagAccount = null;
        }

        public TagAccountSnapshot(int userNo, int tagAccountNo, decimal balanceAmount, TagAccountSnapshotReason reason, DateTime createdTime)
        {
            No = 0;
            UserNo = userNo;
            TagAccountNo = tagAccountNo;
            BalanceAmount = balanceAmount;
            Reason = reason;
            CreatedTime = createdTime;
            AccountName = "";
            TagAccount = null;
        }

        public TagAccount TagAccount
        {
            get;
            set;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [ForeignKeyField("UserNo", "int", "Users", "No")]
        public int UserNo
        {
            get;
            set;
        }

        [ForeignKeyField("TagAccountNo", "int", "TagAccounts", "No")]
        public int TagAccountNo
        {
            get;
            set;
        }

        [DatabaseField("BalanceAmount", "money")]
        public decimal BalanceAmount
        {
            get;
            set;
        }

        [DatabaseField("Reason", "int")]
        public TagAccountSnapshotReason Reason
        {
            get;
            set;
        }

        [DatabaseField("CreatedTime", "datetime")]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        [ViewField("AccountName", "varchar(128)")]
        public string AccountName
        {
            get;
            private set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "UserNo=" + UserNo + ", " + "TagAccountNo=" + TagAccountNo + ", " + "BalanceAmount=" + BalanceAmount + ", " + "Reason=" + Reason + ", " + "CreatedTime=" + CreatedTime + ", " + "AccountName=" + AccountName;
            }
        }
    }

    [DatabaseTable("CashBookTransactions", IsView = true)]
    public partial class CashBookTransaction
    {
        public const string fNo = "No";
        public const string fUserNo = "UserNo";
        public const string fCategoryNo = "CategoryNo";
        public const string fVerificationNo = "VerificationNo";
        public const string fAmount = "Amount";
        public const string fNote = "Note";
        public const string fCreatedTime = "CreatedTime";
        public const string fVerificationSerialNo = "VerificationSerialNo";
        public const string fTransactionTime = "TransactionTime";
        public const string fAccountingDate = "AccountingDate";
        public const string fCategoryName = "CategoryName";
        public const string fShowInDiagram = "ShowInDiagram";

        internal CashBookTransaction(CashBookTransactionDataRow cashBookTransactionDataRow)
        {
            No = cashBookTransactionDataRow.No;
            UserNo = cashBookTransactionDataRow.UserNo;
            CategoryNo = cashBookTransactionDataRow.CategoryNo;
            VerificationNo = cashBookTransactionDataRow.VerificationNo;
            Amount = cashBookTransactionDataRow.Amount;
            Note = cashBookTransactionDataRow.Note;
            CreatedTime = cashBookTransactionDataRow.CreatedTime;
            VerificationSerialNo = cashBookTransactionDataRow.VerificationSerialNo;
            TransactionTime = cashBookTransactionDataRow.TransactionTime;
            AccountingDate = cashBookTransactionDataRow.AccountingDate;
            CategoryName = cashBookTransactionDataRow.CategoryName;
            ShowInDiagram = cashBookTransactionDataRow.ShowInDiagram;
            Verification = null;
        }

        public CashBookTransaction(int userNo, int categoryNo, int verificationNo, decimal amount, string note, DateTime createdTime)
        {
            No = 0;
            UserNo = userNo;
            CategoryNo = categoryNo;
            VerificationNo = verificationNo;
            Amount = amount;
            Note = note;
            CreatedTime = createdTime;
            VerificationSerialNo = 0;
            TransactionTime = new DateTime();
            AccountingDate = new DateTime();
            CategoryName = "";
            ShowInDiagram = false;
            Verification = null;
        }

        public Verification Verification
        {
            get;
            set;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [ForeignKeyField("UserNo", "int", "Users", "No")]
        public int UserNo
        {
            get;
            set;
        }

        [ForeignKeyField("CategoryNo", "int", "Categories", "No")]
        public int CategoryNo
        {
            get;
            set;
        }

        [ForeignKeyField("VerificationNo", "int", "Verifications", "No")]
        public int VerificationNo
        {
            get;
            set;
        }

        [DatabaseField("Amount", "money")]
        public decimal Amount
        {
            get;
            set;
        }

        [DatabaseField("Note", "varchar(1024)")]
        public string Note
        {
            get;
            set;
        }

        [DatabaseField("CreatedTime", "datetime")]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        [ViewField("VerificationSerialNo", "int")]
        public int VerificationSerialNo
        {
            get;
            private set;
        }

        [ViewField("TransactionTime", "datetime")]
        public DateTime TransactionTime
        {
            get;
            private set;
        }

        [ViewField("AccountingDate", "datetime")]
        public DateTime AccountingDate
        {
            get;
            private set;
        }

        [ViewField("CategoryName", "varchar(128)")]
        public string CategoryName
        {
            get;
            private set;
        }

        [ViewField("ShowInDiagram", "bit")]
        public bool ShowInDiagram
        {
            get;
            private set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "UserNo=" + UserNo + ", " + "CategoryNo=" + CategoryNo + ", " + "VerificationNo=" + VerificationNo + ", " + "Amount=" + Amount + ", " + "Note=" + Note + ", " + "CreatedTime=" + CreatedTime + ", " + "VerificationSerialNo=" + VerificationSerialNo + ", " + "TransactionTime=" + TransactionTime + ", " + "AccountingDate=" + AccountingDate + ", " + "CategoryName=" + CategoryName + ", " + "ShowInDiagram=" + ShowInDiagram;
            }
        }
    }

    [DatabaseTable("LogItems")]
    public partial class LogItem
    {
        public const string fNo = "No";
        public const string fUserNo = "UserNo";
        public const string fVerificationNo = "VerificationNo";
        public const string fAccountNo = "AccountNo";
        public const string fType = "Type";
        public const string fDescription = "Description";
        public const string fLogTime = "LogTime";

        internal LogItem(LogItemDataRow logItemDataRow)
        {
            No = logItemDataRow.No;
            UserNo = logItemDataRow.UserNo;
            VerificationNo = logItemDataRow.VerificationNo;
            AccountNo = logItemDataRow.AccountNo;
            Type = logItemDataRow.Type;
            Description = logItemDataRow.Description;
            LogTime = logItemDataRow.LogTime;
        }

        public LogItem(int? userNo, int? verificationNo, int? accountNo, LogItemType type, string description, DateTime logTime)
        {
            No = 0;
            UserNo = userNo;
            VerificationNo = verificationNo;
            AccountNo = accountNo;
            Type = type;
            Description = description;
            LogTime = logTime;
        }

        [PrimaryKeyField("No", "int", IsIdentity = true)]
        public int No
        {
            get;
            internal set;
        }

        [ForeignKeyField("UserNo", "int", "Users", "No", true)]
        public int? UserNo
        {
            get;
            set;
        }

        [ForeignKeyField("VerificationNo", "int", "Verifications", "No", true)]
        public int? VerificationNo
        {
            get;
            set;
        }

        [ForeignKeyField("AccountNo", "int", "Accounts", "No", true)]
        public int? AccountNo
        {
            get;
            set;
        }

        [DatabaseField("Type", "int")]
        public LogItemType Type
        {
            get;
            set;
        }

        [DatabaseField("Description", "nvarchar(MAX)")]
        public string Description
        {
            get;
            set;
        }

        [DatabaseField("LogTime", "datetime")]
        public DateTime LogTime
        {
            get;
            set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "UserNo=" + UserNo + ", " + "VerificationNo=" + VerificationNo + ", " + "AccountNo=" + AccountNo + ", " + "Type=" + Type + ", " + "Description=" + Description + ", " + "LogTime=" + LogTime;
            }
        }
    }

    [DatabaseTable("CashBoxSettings")]
    public partial class CashBoxSettings
    {
        public const string fNo = "No";
        public const string fAccountingYear = "AccountingYear";

        internal CashBoxSettings(CashBoxSettingsDataRow cashBoxSettingsDataRow)
        {
            No = cashBoxSettingsDataRow.No;
            AccountingYear = cashBoxSettingsDataRow.AccountingYear;
        }

        public CashBoxSettings(CashBoxSettingsNo no, int accountingYear)
        {
            No = no;
            AccountingYear = accountingYear;
        }

        [PrimaryKeyField("No", "int")]
        public CashBoxSettingsNo No
        {
            get;
            internal set;
        }

        [DatabaseField("AccountingYear", "int")]
        public int AccountingYear
        {
            get;
            set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "AccountingYear=" + AccountingYear;
            }
        }
    }
}
