using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBase.BusinessLayer;
using EasyBase.Classes;

namespace CashBox
{
    public class Guess
    {
        public Guess (AccountTransaction accountTransaction, CashBookTransaction cashBookTransaction)
        {
            AccountTransaction = accountTransaction;
            CashBookTransaction = cashBookTransaction;
        }

        public AccountTransaction AccountTransaction
        {
            get;
            private set;
        }

        public CashBookTransaction CashBookTransaction
        {
            get;
            private set;
        }

        public void ClearAccountTransaction()
        {
            AccountTransaction = null;
        }

        public void ClearCashBookTransaction()
        {
            CashBookTransaction = null;
        }
    }

    public class Guesser
    {
        private VerificationCollection _verifications;
        private AccountTransactionCollection _accountTransactions;
        private CashBookTransactionCollection _cashBookTransactions;
        private readonly List<Guess> _guesses;

        public Guesser(DataCache dataCache)
        {
            DataCache = dataCache;
            Load();
            _guesses = new List<Guess>();
            CurrentGuessIndex = -1;
        }

        private DataCache DataCache { get; set; }

        public Guess[] Guesses
        {
            get { return _guesses.ToArray(); }
        }

        public Guess CurrentGuess
        {
            get
            {
                if (CurrentGuessIndex >= 0)
                {
                    return _guesses.Count > 0 ? _guesses[CurrentGuessIndex] : null;
                }

                return null;
            }
        }

        public int CurrentGuessIndex { get; set; }

        private void Load()
        {
            using (StandardBusinessLayer core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                SortOrder byDate = new SortOrder(Verification.fDate, OrderDirection.Ascending);
                _verifications = core.GetVerifications(byDate);

                byDate = new SortOrder(AccountTransaction.fTransactionTime, OrderDirection.Ascending);
                _accountTransactions = core.GetAccountTransactions();

                byDate = new SortOrder(CashBookTransaction.fTransactionTime, OrderDirection.Ascending);
                _cashBookTransactions = core.GetCashBookTransactions();
            }
        }

        public void Guess(int accountNo, decimal amount, string note)
        {
            var duplicateCheckList = new List<string>();
            ClearGuess();
            note = note.Trim();

            if (amount == 0 && note.Trim() == "")
            {
                return;
            }

            var accountTransactionNumbers = new List<int>();

            // Start search for accounts that match both account number and note
            foreach (var accountTransaction in _accountTransactions.Where(a => a.AccountNo == accountNo).OrderByDescending(a => a.TransactionTime))
            {
                if (accountTransaction.Amount == amount && accountTransaction.Note.ToLower() == note.ToLower())
                {
                    var cashBookTransaction = _cashBookTransactions.OrderBy(c => c.TransactionTime).FirstOrDefault(c => c.VerificationNo == accountTransaction.VerificationNo);

                    if (cashBookTransaction != null)
                    {
                        if (!duplicateCheckList.Contains(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower()))
                        {
                            duplicateCheckList.Add(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower());
                            _guesses.Add(new Guess(accountTransaction, cashBookTransaction));
                            accountTransactionNumbers.Add(accountTransaction.No);

                            if (_guesses.Count > 3) { return; }

                            break;
                        }
                    }
                }
            }

            // If not found, search other accounts
            if (accountTransactionNumbers.Count == 0)
            {
                foreach (
                    var accountTransaction in
                        _accountTransactions.Where(a => a.AccountNo != accountNo)
                            .OrderByDescending(a => a.TransactionTime))
                {
                    if (accountTransaction.Amount == amount && accountTransaction.Note.ToLower() == note.ToLower())
                    {
                        var cashBookTransaction =
                            _cashBookTransactions.OrderBy(c => c.TransactionTime)
                                .FirstOrDefault(c => c.VerificationNo == accountTransaction.VerificationNo);

                        if (cashBookTransaction != null)
                        {
                            if (!duplicateCheckList.Contains(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower()))
                            {
                                duplicateCheckList.Add(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower());
                                _guesses.Add(new Guess(accountTransaction, cashBookTransaction));
                                accountTransactionNumbers.Add(accountTransaction.No);

                                if (_guesses.Count > 3) { return; }

                                break;
                            }
                        }
                    }
                }
            }

            bool hasMatchingAmount = _accountTransactions.Find(x => x.Amount == amount) != null;

            // Then look for matches for note

            if (note.Trim() != "")
            {
                bool noteMatchFound = false;
                foreach (
                    var accountTransaction in
                        _accountTransactions.Where(a => a.AccountNo == accountNo)
                            .OrderByDescending(a => a.TransactionTime))
                {
                    if (accountTransaction.Note.ToLower().Contains(note.ToLower()) && !accountTransactionNumbers.Contains(accountTransaction.No))
                    {
                        var cashBookTransaction =
                            _cashBookTransactions.OrderBy(c => c.TransactionTime)
                                .FirstOrDefault(c => c.VerificationNo == accountTransaction.VerificationNo);

                        if (cashBookTransaction != null)
                        {
                            if (!duplicateCheckList.Contains(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower()))
                            {
                                duplicateCheckList.Add(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower());
                                _guesses.Add(new Guess(accountTransaction, cashBookTransaction));
                                accountTransactionNumbers.Add(accountTransaction.No);

                                if (_guesses.Count > 3) { return; }

                                if (amount != 0 && hasMatchingAmount)
                                {
                                    //noteMatchFound = true;
                                    //break;
                                }
                            }
                        }
                    }
                }

                // If not found, search other accounts
                if (!noteMatchFound)
                {
                    foreach (
                        var accountTransaction in
                            _accountTransactions.Where(a => a.AccountNo != accountNo)
                                .OrderByDescending(a => a.TransactionTime))
                    {
                        if (accountTransaction.Note.ToLower().Contains(note.ToLower()) &&
                            !accountTransactionNumbers.Contains(accountTransaction.No))
                        {
                            var cashBookTransaction =
                                _cashBookTransactions.OrderBy(c => c.TransactionTime)
                                    .FirstOrDefault(c => c.VerificationNo == accountTransaction.VerificationNo);

                            if (cashBookTransaction != null)
                            {
                                if (!duplicateCheckList.Contains(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower()))
                                {
                                    duplicateCheckList.Add(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower());
                                    _guesses.Add(new Guess(accountTransaction, cashBookTransaction));
                                    accountTransactionNumbers.Add(accountTransaction.No);

                                    if (_guesses.Count > 3) { return; }

                                    if (amount != 0 && hasMatchingAmount)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Then look for matches for amount

            if (amount != 0)
            {
                foreach (
                    var accountTransaction in
                        _accountTransactions.Where(a => a.AccountNo == accountNo)
                            .OrderByDescending(a => a.TransactionTime))
                {
                    if (accountTransaction.Amount == amount &&
                        !accountTransactionNumbers.Contains(accountTransaction.No))
                    {
                        var cashBookTransaction =
                            _cashBookTransactions.OrderBy(c => c.TransactionTime)
                                .FirstOrDefault(c => c.VerificationNo == accountTransaction.VerificationNo);

                        if (cashBookTransaction != null)
                        {
                            if (!duplicateCheckList.Contains(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower()))
                            {
                                duplicateCheckList.Add(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower());
                                _guesses.Add(new Guess(accountTransaction, cashBookTransaction));
                                accountTransactionNumbers.Add(accountTransaction.No);

                                if (_guesses.Count > 3) { return; }
                            }
                        }
                    }
                }

                // If not found, search other accounts
                foreach (
                    var accountTransaction in
                        _accountTransactions.Where(a => a.AccountNo != accountNo)
                            .OrderByDescending(a => a.TransactionTime))
                {
                    if (accountTransaction.Amount == amount &&
                        !accountTransactionNumbers.Contains(accountTransaction.No))
                    {
                        var cashBookTransaction =
                            _cashBookTransactions.OrderBy(c => c.TransactionTime)
                                .FirstOrDefault(c => c.VerificationNo == accountTransaction.VerificationNo);

                        if (cashBookTransaction != null)
                        {
                            if (!duplicateCheckList.Contains(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower()))
                            {
                                duplicateCheckList.Add(accountTransaction.Note.ToLower() + ", " + cashBookTransaction.Note.ToLower());
                                _guesses.Add(new Guess(accountTransaction, cashBookTransaction));
                                accountTransactionNumbers.Add(accountTransaction.No);

                                if (_guesses.Count > 3) { return; }

                                break;
                            }
                        }
                    }
                }
            }
        }

        public void ClearGuess()
        {
            _guesses.Clear();
        }
    }
}
