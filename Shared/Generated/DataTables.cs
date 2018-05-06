using System;
using System.Data;

namespace EasyBase.Classes
{
    public class UserDataRow : DataRow
    {
        internal UserDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public string Name
        {
            get { return (string)this["Name"]; }
        }

        public string Email
        {
            get { return (string)this["Email"]; }
        }

        public string Password
        {
            get { return (string)this["Password"]; }
        }

        public DateTime CreatedTime
        {
            get { return (DateTime)this["CreatedTime"]; }
        }
    }

    public class UserTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(UserDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new UserDataRow(builder);
        }

        public new UserDataRow NewRow()
        {
            return (UserDataRow)base.NewRow();
        }

        public UserDataRow this[int index]
        {
            get { return (UserDataRow)Rows[index]; }
        }
    }

    public class CategoryDataRow : DataRow
    {
        internal CategoryDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int? ParentCategoryNo
        {
            get { return this["ParentCategoryNo"] is DBNull ? null : (int?)this["ParentCategoryNo"]; }
        }

        public CategoryType Type
        {
            get { return (CategoryType)this["Type"]; }
        }

        public string Name
        {
            get { return (string)this["Name"]; }
        }

        public bool IsArchived
        {
            get { return (bool)this["IsArchived"]; }
        }

        public DateTime CreatedTime
        {
            get { return (DateTime)this["CreatedTime"]; }
        }

        public bool ShowInDiagram
        {
            get { return (bool)this["ShowInDiagram"]; }
        }
    }

    public class CategoryTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(CategoryDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new CategoryDataRow(builder);
        }

        public new CategoryDataRow NewRow()
        {
            return (CategoryDataRow)base.NewRow();
        }

        public CategoryDataRow this[int index]
        {
            get { return (CategoryDataRow)Rows[index]; }
        }
    }

    public class AccountDataRow : DataRow
    {
        internal AccountDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int UserNo
        {
            get { return (int)this["UserNo"]; }
        }

        public AccountType Type
        {
            get { return (AccountType)this["Type"]; }
        }

        public string Name
        {
            get { return (string)this["Name"]; }
        }

        public decimal BalanceBroughtForwardAmount
        {
            get { return (decimal)this["BalanceBroughtForwardAmount"]; }
        }

        public bool IsArchived
        {
            get { return (bool)this["IsArchived"]; }
        }

        public DateTime CreatedTime
        {
            get { return (DateTime)this["CreatedTime"]; }
        }

        public bool ShowInDiagram
        {
            get { return (bool)this["ShowInDiagram"]; }
        }

        public decimal Balance
        {
            get { return (decimal)this["Balance"]; }
        }
    }

    public class AccountTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(AccountDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new AccountDataRow(builder);
        }

        public new AccountDataRow NewRow()
        {
            return (AccountDataRow)base.NewRow();
        }

        public AccountDataRow this[int index]
        {
            get { return (AccountDataRow)Rows[index]; }
        }
    }

    public class TagAccountDataRow : DataRow
    {
        internal TagAccountDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public string Name
        {
            get { return (string)this["Name"]; }
        }

        public bool IsArchived
        {
            get { return (bool)this["IsArchived"]; }
        }

        public DateTime CreatedTime
        {
            get { return (DateTime)this["CreatedTime"]; }
        }

        public decimal Amount
        {
            get { return (decimal)this["Amount"]; }
        }
    }

    public class TagAccountTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(TagAccountDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new TagAccountDataRow(builder);
        }

        public new TagAccountDataRow NewRow()
        {
            return (TagAccountDataRow)base.NewRow();
        }

        public TagAccountDataRow this[int index]
        {
            get { return (TagAccountDataRow)Rows[index]; }
        }
    }

    public class AccountTagDataRow : DataRow
    {
        internal AccountTagDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int TagAccountNo
        {
            get { return (int)this["TagAccountNo"]; }
        }

        public int AccountNo
        {
            get { return (int)this["AccountNo"]; }
        }

        public AccountTagType Type
        {
            get { return (AccountTagType)this["Type"]; }
        }

        public decimal MoneyValue
        {
            get { return (decimal)this["MoneyValue"]; }
        }

        public decimal RelativeValue
        {
            get { return (decimal)this["RelativeValue"]; }
        }

        public string TagAccountName
        {
            get { return (string)this["TagAccountName"]; }
        }

        public decimal Amount
        {
            get { return (decimal)this["Amount"]; }
        }
    }

    public class AccountTagTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(AccountTagDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new AccountTagDataRow(builder);
        }

        public new AccountTagDataRow NewRow()
        {
            return (AccountTagDataRow)base.NewRow();
        }

        public AccountTagDataRow this[int index]
        {
            get { return (AccountTagDataRow)Rows[index]; }
        }
    }

    public class VerificationDataRow : DataRow
    {
        internal VerificationDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int Year
        {
            get { return (int)this["Year"]; }
        }

        public int SerialNo
        {
            get { return (int)this["SerialNo"]; }
        }

        public DateTime Date
        {
            get { return (DateTime)this["Date"]; }
        }

        public DateTime AccountingDate
        {
            get { return (DateTime)this["AccountingDate"]; }
        }
    }

    public class VerificationTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(VerificationDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new VerificationDataRow(builder);
        }

        public new VerificationDataRow NewRow()
        {
            return (VerificationDataRow)base.NewRow();
        }

        public VerificationDataRow this[int index]
        {
            get { return (VerificationDataRow)Rows[index]; }
        }
    }

    public class AccountTransactionDataRow : DataRow
    {
        internal AccountTransactionDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int UserNo
        {
            get { return (int)this["UserNo"]; }
        }

        public int AccountNo
        {
            get { return (int)this["AccountNo"]; }
        }

        public int VerificationNo
        {
            get { return (int)this["VerificationNo"]; }
        }

        public decimal Amount
        {
            get { return (decimal)this["Amount"]; }
        }

        public string Note
        {
            get { return (string)this["Note"]; }
        }

        public DateTime CreatedTime
        {
            get { return (DateTime)this["CreatedTime"]; }
        }

        public int VerificationSerialNo
        {
            get { return (int)this["VerificationSerialNo"]; }
        }

        public DateTime TransactionTime
        {
            get { return (DateTime)this["TransactionTime"]; }
        }

        public DateTime AccountingDate
        {
            get { return (DateTime)this["AccountingDate"]; }
        }

        public string AccountName
        {
            get { return (string)this["AccountName"]; }
        }

        public string UserName
        {
            get { return (string)this["UserName"]; }
        }

        public bool ShowInDiagram
        {
            get { return (bool)this["ShowInDiagram"]; }
        }
    }

    public class AccountTransactionTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(AccountTransactionDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new AccountTransactionDataRow(builder);
        }

        public new AccountTransactionDataRow NewRow()
        {
            return (AccountTransactionDataRow)base.NewRow();
        }

        public AccountTransactionDataRow this[int index]
        {
            get { return (AccountTransactionDataRow)Rows[index]; }
        }
    }

    public class TagAccountSnapshotDataRow : DataRow
    {
        internal TagAccountSnapshotDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int UserNo
        {
            get { return (int)this["UserNo"]; }
        }

        public int TagAccountNo
        {
            get { return (int)this["TagAccountNo"]; }
        }

        public decimal BalanceAmount
        {
            get { return (decimal)this["BalanceAmount"]; }
        }

        public TagAccountSnapshotReason Reason
        {
            get { return (TagAccountSnapshotReason)this["Reason"]; }
        }

        public DateTime CreatedTime
        {
            get { return (DateTime)this["CreatedTime"]; }
        }

        public string AccountName
        {
            get { return (string)this["AccountName"]; }
        }
    }

    public class TagAccountSnapshotTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(TagAccountSnapshotDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new TagAccountSnapshotDataRow(builder);
        }

        public new TagAccountSnapshotDataRow NewRow()
        {
            return (TagAccountSnapshotDataRow)base.NewRow();
        }

        public TagAccountSnapshotDataRow this[int index]
        {
            get { return (TagAccountSnapshotDataRow)Rows[index]; }
        }
    }

    public class CashBookTransactionDataRow : DataRow
    {
        internal CashBookTransactionDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int UserNo
        {
            get { return (int)this["UserNo"]; }
        }

        public int CategoryNo
        {
            get { return (int)this["CategoryNo"]; }
        }

        public int VerificationNo
        {
            get { return (int)this["VerificationNo"]; }
        }

        public decimal Amount
        {
            get { return (decimal)this["Amount"]; }
        }

        public string Note
        {
            get { return (string)this["Note"]; }
        }

        public DateTime CreatedTime
        {
            get { return (DateTime)this["CreatedTime"]; }
        }

        public int VerificationSerialNo
        {
            get { return (int)this["VerificationSerialNo"]; }
        }

        public DateTime TransactionTime
        {
            get { return (DateTime)this["TransactionTime"]; }
        }

        public DateTime AccountingDate
        {
            get { return (DateTime)this["AccountingDate"]; }
        }

        public string CategoryName
        {
            get { return (string)this["CategoryName"]; }
        }

        public bool ShowInDiagram
        {
            get { return (bool)this["ShowInDiagram"]; }
        }
    }

    public class CashBookTransactionTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(CashBookTransactionDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new CashBookTransactionDataRow(builder);
        }

        public new CashBookTransactionDataRow NewRow()
        {
            return (CashBookTransactionDataRow)base.NewRow();
        }

        public CashBookTransactionDataRow this[int index]
        {
            get { return (CashBookTransactionDataRow)Rows[index]; }
        }
    }

    public class LogItemDataRow : DataRow
    {
        internal LogItemDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public int No
        {
            get { return (int)this["No"]; }
        }

        public int? UserNo
        {
            get { return this["UserNo"] is DBNull ? null : (int?)this["UserNo"]; }
        }

        public int? VerificationNo
        {
            get { return this["VerificationNo"] is DBNull ? null : (int?)this["VerificationNo"]; }
        }

        public int? AccountNo
        {
            get { return this["AccountNo"] is DBNull ? null : (int?)this["AccountNo"]; }
        }

        public LogItemType Type
        {
            get { return (LogItemType)this["Type"]; }
        }

        public string Description
        {
            get { return (string)this["Description"]; }
        }

        public DateTime LogTime
        {
            get { return (DateTime)this["LogTime"]; }
        }
    }

    public class LogItemTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(LogItemDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new LogItemDataRow(builder);
        }

        public new LogItemDataRow NewRow()
        {
            return (LogItemDataRow)base.NewRow();
        }

        public LogItemDataRow this[int index]
        {
            get { return (LogItemDataRow)Rows[index]; }
        }
    }

    public class CashBoxSettingsDataRow : DataRow
    {
        internal CashBoxSettingsDataRow(DataRowBuilder builder)
            :base(builder)
        {
        }

        public CashBoxSettingsNo No
        {
            get { return (CashBoxSettingsNo)this["No"]; }
        }

        public int AccountingYear
        {
            get { return (int)this["AccountingYear"]; }
        }
    }

    public class CashBoxSettingsTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(CashBoxSettingsDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new CashBoxSettingsDataRow(builder);
        }

        public new CashBoxSettingsDataRow NewRow()
        {
            return (CashBoxSettingsDataRow)base.NewRow();
        }

        public CashBoxSettingsDataRow this[int index]
        {
            get { return (CashBoxSettingsDataRow)Rows[index]; }
        }
    }
}
