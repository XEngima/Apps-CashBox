using EasyBase.BusinessLayer;
using EasyBase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBase.BusinessLayer
{
    public class DataCache
    {
        public DataCache()
        {
        }

        private void FillVerification(Verification verification)
        {
            var accountTransactions = AccountTransactions.Where(t => t.VerificationNo == verification.No).ToList();
            var cashbookTransactions = CashBookTransactions.Where(t => t.VerificationNo == verification.No).ToList();

            var accountTransactionCollection = new AccountTransactionCollection();
            foreach (var accountTransaction in accountTransactions)
            {
                accountTransactionCollection.Add(accountTransaction);
            }

            var cashbookTransactionCollection = new CashBookTransactionCollection();
            foreach (var cashbookTransaction in cashbookTransactions)
            {
                cashbookTransactionCollection.Add(cashbookTransaction);
            }

            verification.AccountTransactions = accountTransactionCollection;
            verification.CashBookTransactions = cashbookTransactionCollection;
        }

        public void Load(int year)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                Settings = core.GetCashBoxSettings(CashBoxSettingsNo.CurrentApplicationNo);

                //Verifications = core.GetVerificationsByYear(year);
                Verifications = core.GetVerifications();

                //Condition isThisYear = new Condition(AccountTransaction.fTransactionTime, CompareOperator.GreaterThanOrEqualTo, new DateTime(year, 1, 1), DateTimeResolution.Day);
                //AccountTransactions = core.GetAccountTransactions(isThisYear);
                AccountTransactions = core.GetAccountTransactions();

                //isThisYear = new Condition(CashBookTransaction.fTransactionTime, CompareOperator.GreaterThanOrEqualTo, new DateTime(year, 1, 1), DateTimeResolution.Day);
                //CashBookTransactions = core.GetCashBookTransactions(isThisYear);
                CashBookTransactions = core.GetCashBookTransactions();

                Condition isEarlier = new Condition(AccountTransaction.fTransactionTime, CompareOperator.LessThan, new DateTime(year, 1, 1));
                EarlierAccountTransactions = core.GetAccountTransactions(isEarlier);

                isEarlier = new Condition(CashBookTransaction.fTransactionTime, CompareOperator.LessThan, new DateTime(year, 1, 1));
                EarlierCashBookTransactions = core.GetCashBookTransactions(isEarlier);

                foreach (var verification in Verifications)
                {
                    FillVerification(verification);
                }
            }
        }

        private VerificationCollection Verifications
        {
            get; set;
        }

        private AccountTransactionCollection AccountTransactions
        {
            get; set;
        }

        private CashBookTransactionCollection CashBookTransactions
        {
            get; set;
        }

        private AccountTransactionCollection EarlierAccountTransactions
        {
            get; set;
        }

        private CashBookTransactionCollection EarlierCashBookTransactions
        {
            get; set;
        }

        public Verification GetLastVerification()
        {
            return Verifications.OrderByDescending(v => v.No).FirstOrDefault();
        }

        public List<Verification> GetUnbalancedAndEmptyVerifications()
        {
            return (from v in Verifications
                    where v.IsEmpty || !v.IsBalanced
                    select v).ToList();
        }

        public Verification GetVerification(int no)
        {
            return Verifications.FirstOrDefault(v => v.No == no);
        }

        public CashBoxSettings Settings { get; private set; }

        public void Save(CashBoxSettings settings)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();
                core.Save(settings);
                Settings = settings;
            }
        }

        public AccountTransaction GetAccountTransaction(int no)
        {
            return AccountTransactions.FirstOrDefault(t => t.No == no);
        }

        public decimal CalculateAccountBalance(int accountNo)
        {
            Account account = null;
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();
                account = core.GetAccount(accountNo);
            }

            //var earlierTransactions = EarlierAccountTransactions.Where(t => t.AccountNo == accountNo).ToList();
            var transactions = AccountTransactions.Where(t => t.AccountNo == accountNo).ToList();

            //var earlierTotalAmount = earlierTransactions.Sum(t => t.Amount);
            var transactionsTotalAmount = transactions.Sum(t => t.Amount);

            //return account.BalanceBroughtForwardAmount + earlierTotalAmount + transactionsTotalAmount;
            return account.BalanceBroughtForwardAmount + transactionsTotalAmount;
        }

        public void Save(Verification verification)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                bool newVerification = verification.No == 0;
                core.Save(verification);

                FillVerification(verification);

                if (newVerification)
                {
                    Verifications.Add(verification);
                }
                else
                {
                    for (int i = 0; i < Verifications.Count(); i++)
                    {
                        if (Verifications[i].No == verification.No)
                        {
                            Verifications[i] = verification;
                            break;
                        }
                    }

                    for (int i = 0; i < AccountTransactions.Count(); i++)
                    {
                        if (AccountTransactions[i].VerificationNo == verification.No)
                        {
                            AccountTransactions[i].SetTransactionTime(verification.Date);
                            AccountTransactions[i].SetAccountingDate(verification.AccountingDate);
                            break;
                        }
                    }
                    for (int i = 0; i < CashBookTransactions.Count(); i++)
                    {
                        if (CashBookTransactions[i].VerificationNo == verification.No)
                        {
                            CashBookTransactions[i].SetTransactionTime(verification.Date);
                            CashBookTransactions[i].SetAccountingDate(verification.AccountingDate);
                            break;
                        }
                    }
                }
            }
        }

        public void Save(AccountTransaction accountTransaction)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                bool newTransaction = accountTransaction.No == 0;
                core.Save(accountTransaction);

                if (newTransaction)
                {
                    AccountTransactions.Add(accountTransaction);
                }
                else
                {
                    for (int i = 0; i < AccountTransactions.Count(); i++)
                    {
                        if (AccountTransactions[i].No == accountTransaction.No)
                        {
                            AccountTransactions[i] = accountTransaction;
                            break;
                        }
                    }
                }

                FillVerification(Verifications.First(v => v.No == accountTransaction.VerificationNo));
            }
        }

        public void Save(CashBookTransaction cashbookTransaction)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                bool newVerification = cashbookTransaction.No == 0;
                core.Save(cashbookTransaction);

                if (newVerification)
                {
                    CashBookTransactions.Add(cashbookTransaction);
                }
                else
                {
                    for (int i = 0; i < CashBookTransactions.Count(); i++)
                    {
                        if (CashBookTransactions[i].No == cashbookTransaction.No)
                        {
                            CashBookTransactions[i] = cashbookTransaction;
                            break;
                        }
                    }
                }

                FillVerification(Verifications.First(v => v.No == cashbookTransaction.VerificationNo));
            }
        }

        public CashBookTransaction GetCashBookTransaction(int no)
        {
            return CashBookTransactions.FirstOrDefault(t => t.No == no);
        }

        public void DeleteVerification(int no)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                Verification verification = Verifications.FirstOrDefault(v => v.No == no);

                if (verification != null)
                {
                    core.DeleteVerification(no);
                    Verifications.Remove(verification);
                }
            }
        }

        public void DeleteAccountTransaction(int no)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                AccountTransaction transaction = AccountTransactions.FirstOrDefault(t => t.No == no);

                if (transaction != null)
                {
                    Verification verification = Verifications.FirstOrDefault(v => v.No == transaction.VerificationNo);

                    core.DeleteAccountTransaction(no);
                    AccountTransactions.Remove(transaction);

                    FillVerification(verification);
                }
            }
        }

        public void DeleteCashBookTransaction(int no)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                var transaction = CashBookTransactions.FirstOrDefault(t => t.No == no);

                if (transaction != null)
                {
                    Verification verification = Verifications.FirstOrDefault(v => v.No == transaction.VerificationNo);

                    core.DeleteCashBookTransaction(no);
                    CashBookTransactions.Remove(transaction);

                    FillVerification(verification);
                }
            }
        }

        public void DeleteAccountTransactionsByVerificationNo(int verificationNo)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                var transactions = AccountTransactions.Where(t => t.VerificationNo == verificationNo).ToList();

                if (transactions.Count() > 0)
                {
                    var verification = Verifications.FirstOrDefault(v => v.No == transactions[0].VerificationNo);

                    core.DeleteAccountTransactionsByVerificationNo(verificationNo);

                    foreach (var transaction in transactions)
                    {
                        AccountTransactions.Remove(transaction);
                    }

                    FillVerification(verification);
                }
            }
        }

        public void DeleteCashBookTransactionsByVerificationNo(int verificationNo)
        {
            using (var core = new StandardBusinessLayer(this))
            {
                core.Connect();

                var transactions = CashBookTransactions.Where(t => t.VerificationNo == verificationNo).ToList();

                if (transactions.Count() > 0)
                {
                    var verification = Verifications.FirstOrDefault(v => v.No == transactions[0].VerificationNo);

                    core.DeleteCashBookTransactionsByVerificationNo(verificationNo);

                    foreach (var transaction in transactions)
                    {
                        CashBookTransactions.Remove(transaction);
                    }

                    FillVerification(verification);
                }
            }
        }
    }
}
