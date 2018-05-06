using System;

namespace EasyBase.Classes
{
    public enum CashBoxDateType
    {
        TransactionDate = 1,
        AccountingDate = 2
    }

    public enum CategoryType
    {
        Expense = 1,
        Income = 2
    }

    public enum AccountType
    {
        Asset = 1,
        Debt = 2
    }

    public enum AccountTagType
    {
        ExactAmount = 1,
        PercentOfRest = 2
    }

    public enum TagAccountSnapshotReason
    {
        AccountTransactionAdded = 1,
        AccountTransactionUpdated = 2,
        DefinitionChangedByUser = 3
    }

    public enum LogItemType
    {
        AccountTagRecalculated = 1
    }

    public enum CashBoxSettingsNo
    {
        CurrentApplicationNo = 1
    }
}
