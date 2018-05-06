using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
    public static class ApplicationEvents
    {
        public static event AccountTransactionSelectionChangedEventHandler AccountTransactionSelectionChanged;
        public static event CashBookTransactionSelectionChangedEventHandler CashBookTransactionSelectionChanged;
        public static event AccountTransactionCreatedEventHandler AccountTransactionCreated;
        public static event CashBookTransactionCreatedEventHandler CashBookTransactionCreated;
        public static event AccountTransactionDeletedEventHandler AccountTransactionDeleted;
        public static event CashBookTransactionDeletedEventHandler CashBookTransactionDeleted;
        public static event AccountTransactionUpdatedEventHandler AccountTransactionUpdated;
        public static event CashBookTransactionUpdatedEventHandler CashBookTransactionUpdated;
        public static event VerificationEventHandler VerificationChanged;
        public static event VerificationDeletedEventHandler VerificationDeleted;
        public static event AccountingYearChangedEventHandler AccountingYearChanged;

        public static void OnAccountTransactionSelectionChanged(int accountTransactionNo, int verificationNo)
        {
            var eventArgs = new AccountTransactionEventArgs(accountTransactionNo, verificationNo);

            if (AccountTransactionSelectionChanged != null) {
                AccountTransactionSelectionChanged(null, eventArgs);
            }

            LastAccountTransactionSelectionChangedEventArgs = eventArgs;
        }

        public static void OnCashBookTransactionSelectionChanged(int cashBookTransactionNo, int verificationNo)
        {
            var eventArgs = new CashBookTransactionEventArgs(cashBookTransactionNo, verificationNo);

            if (CashBookTransactionSelectionChanged != null) {
                CashBookTransactionSelectionChanged(null, eventArgs);
            }

            LastCashBookTransactionSelectionChangedEventArgs = eventArgs;
        }

        public static void OnAccountTransactionCreated(int accountTransactionNo, int verificationNo)
        {
            if (AccountTransactionCreated != null) {
                AccountTransactionCreated(null, new AccountTransactionEventArgs(accountTransactionNo, verificationNo));
            }
        }

        public static void OnCashBookTransactionCreated(int cashBookTransactionNo, int verificationNo)
        {
            if (CashBookTransactionCreated != null) {
                CashBookTransactionCreated(null, new CashBookTransactionEventArgs(cashBookTransactionNo, verificationNo));
            }
        }

        public static void OnAccountTransactionDeleted(int accountTransactionNo, int verificationNo)
        {
            if (AccountTransactionDeleted != null) {
                AccountTransactionDeleted(null, new AccountTransactionEventArgs(accountTransactionNo, verificationNo));
            }
        }

        public static void OnCashBookTransactionDeleted(int cashBookTransactionNo, int verificationNo)
        {
            if (CashBookTransactionDeleted != null) {
                CashBookTransactionDeleted(null, new CashBookTransactionEventArgs(cashBookTransactionNo, verificationNo));
            }
        }

        public static void OnAccountTransactionUpdated(int accountTransactionNo, int verificationNo)
        {
            if (AccountTransactionUpdated != null) {
                AccountTransactionUpdated(null, new AccountTransactionEventArgs(accountTransactionNo, verificationNo));
            }
        }

        public static void OnCashBookTransactionUpdated(int cashBookTransactionNo, int verificationNo)
        {
            if (CashBookTransactionUpdated != null) {
                CashBookTransactionUpdated(null, new CashBookTransactionEventArgs(cashBookTransactionNo, verificationNo));
            }
        }

        public static void OnVerificationChanged(Verification verification)
        {
            if (VerificationChanged != null) {
                VerificationChanged(null, new VerificationEventArgs(verification));
            }
        }

        public static void OnVerificationDeleted(int verificationNo)
        {
            if (VerificationDeleted != null)
            {
                VerificationDeleted(null, new VerificationDeletedEventArgs(verificationNo));
            }
        }

        public static void OnAccountingYearChanged(int fromYear, int toYear)
        {
            if (AccountingYearChanged != null) {
                AccountingYearChanged(null, new AccountingYearChangedEventArgs(fromYear, toYear));
            }
        }

        public static AccountTransactionEventArgs LastAccountTransactionSelectionChangedEventArgs
        {
            get;
            private set;
        }

        public static CashBookTransactionEventArgs LastCashBookTransactionSelectionChangedEventArgs
        {
            get;
            private set;
        }
    }

    public class AccountTransactionEventArgs : EventArgs
    {
        public AccountTransactionEventArgs(int accountTransactionNo, int verificationNo)
        {
            AccountTransactionNo = accountTransactionNo;
            VerificationNo = verificationNo;
        }

        public int AccountTransactionNo
        {
            get;
            private set;
        }

        public int VerificationNo
        {
            get;
            private set;
        }
    }

    public class CashBookTransactionEventArgs : EventArgs
    {
        public CashBookTransactionEventArgs(int cashBookTransactionNo, int verificationNo)
        {
            CashBookTransactionNo = cashBookTransactionNo;
            VerificationNo = verificationNo;
        }

        public int CashBookTransactionNo
        {
            get;
            private set;
        }

        public int VerificationNo
        {
            get;
            private set;
        }
    }

    public class VerificationEventArgs : EventArgs
    {
        public VerificationEventArgs(Verification verification)
        {
            Verification = verification;
        }

        public Verification Verification
        {
            get;
            private set;
        }
    }

    public class VerificationDeletedEventArgs : EventArgs
    {
        public VerificationDeletedEventArgs(int verificationNo)
        {
            VerificationNo = verificationNo;
        }

        public int VerificationNo
        {
            get;
            private set;
        }
    }

    public class AccountingYearChangedEventArgs : EventArgs
    {
        public AccountingYearChangedEventArgs(int fromYear, int toYear)
        {
            FromYear = fromYear;
            ToYear = toYear;
        }

        public int FromYear
        {
            get;
            private set;
        }

        public int ToYear
        {
            get;
            private set;
        }
    }

    public delegate void AccountTransactionSelectionChangedEventHandler(object sender, AccountTransactionEventArgs e);

    public delegate void AccountTransactionCreatedEventHandler(object sender, AccountTransactionEventArgs e);

    public delegate void CashBookTransactionCreatedEventHandler(object sender, CashBookTransactionEventArgs e);

    public delegate void CashBookTransactionSelectionChangedEventHandler(object sender, CashBookTransactionEventArgs e);

    public delegate void AccountTransactionDeletedEventHandler(object sender, AccountTransactionEventArgs e);

    public delegate void CashBookTransactionDeletedEventHandler(object sender, CashBookTransactionEventArgs e);

    public delegate void AccountTransactionUpdatedEventHandler(object sender, AccountTransactionEventArgs e);

    public delegate void CashBookTransactionUpdatedEventHandler(object sender, CashBookTransactionEventArgs e);

    public delegate void VerificationEventHandler(object sender, VerificationEventArgs e);

    public delegate void VerificationDeletedEventHandler(object sender, VerificationDeletedEventArgs e);

    public delegate void AccountingYearChangedEventHandler(object sender, AccountingYearChangedEventArgs e);
}
