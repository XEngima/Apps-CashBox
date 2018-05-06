using System;
using System.Text;
using System.Collections.Generic;

namespace EasyBase.Classes
{
    public class UserCollection : List<User>
    {
        internal static UserCollection CreateUserCollection(UserTable userTable)
        {
            UserCollection userCollection = new UserCollection();

            foreach (UserDataRow userDataRow in userTable.Rows) {
                userCollection.Add(new User(userDataRow));
            }

            return userCollection;
        }

        public User GetUser(int no)
        {
            foreach (User user in this) {
                if (user.No == no) {
                    return user;
                }
            }

            return null;
        }

        public User GetUserByEmail(string email)
        {
            foreach (User user in this) {
                if (user.Email == email) {
                    return user;
                }
            }

            return null;
        }
    }

    public class CategoryCollection : List<Category>
    {
        internal static CategoryCollection CreateCategoryCollection(CategoryTable categoryTable)
        {
            CategoryCollection categoryCollection = new CategoryCollection();

            foreach (CategoryDataRow categoryDataRow in categoryTable.Rows) {
                categoryCollection.Add(new Category(categoryDataRow));
            }

            return categoryCollection;
        }

        public Category GetCategory(int no)
        {
            foreach (Category category in this) {
                if (category.No == no) {
                    return category;
                }
            }

            return null;
        }
    }

    public class AccountCollection : List<Account>
    {
        internal static AccountCollection CreateAccountCollection(AccountTable accountTable)
        {
            AccountCollection accountCollection = new AccountCollection();

            foreach (AccountDataRow accountDataRow in accountTable.Rows) {
                accountCollection.Add(new Account(accountDataRow));
            }

            return accountCollection;
        }

        public Account GetAccount(int no)
        {
            foreach (Account account in this) {
                if (account.No == no) {
                    return account;
                }
            }

            return null;
        }
    }

    public class TagAccountCollection : List<TagAccount>
    {
        internal static TagAccountCollection CreateTagAccountCollection(TagAccountTable tagAccountTable)
        {
            TagAccountCollection tagAccountCollection = new TagAccountCollection();

            foreach (TagAccountDataRow tagAccountDataRow in tagAccountTable.Rows) {
                tagAccountCollection.Add(new TagAccount(tagAccountDataRow));
            }

            return tagAccountCollection;
        }

        public TagAccount GetTagAccount(int no)
        {
            foreach (TagAccount tagAccount in this) {
                if (tagAccount.No == no) {
                    return tagAccount;
                }
            }

            return null;
        }

        public TagAccount GetTagAccountByName(string name)
        {
            foreach (TagAccount tagAccount in this) {
                if (tagAccount.Name == name) {
                    return tagAccount;
                }
            }

            return null;
        }
    }

    public class AccountTagCollection : List<AccountTag>
    {
        internal static AccountTagCollection CreateAccountTagCollection(AccountTagTable accountTagTable)
        {
            AccountTagCollection accountTagCollection = new AccountTagCollection();

            foreach (AccountTagDataRow accountTagDataRow in accountTagTable.Rows) {
                accountTagCollection.Add(new AccountTag(accountTagDataRow));
            }

            return accountTagCollection;
        }

        public AccountTag GetAccountTag(int no)
        {
            foreach (AccountTag accountTag in this) {
                if (accountTag.No == no) {
                    return accountTag;
                }
            }

            return null;
        }
    }

    public class VerificationCollection : List<Verification>
    {
        internal static VerificationCollection CreateVerificationCollection(VerificationTable verificationTable)
        {
            VerificationCollection verificationCollection = new VerificationCollection();

            foreach (VerificationDataRow verificationDataRow in verificationTable.Rows) {
                verificationCollection.Add(new Verification(verificationDataRow));
            }

            return verificationCollection;
        }

        public Verification GetVerification(int no)
        {
            foreach (Verification verification in this) {
                if (verification.No == no) {
                    return verification;
                }
            }

            return null;
        }

        public Verification GetVerificationByYearAndSerialNo(int year, int serialNo)
        {
            foreach (Verification verification in this) {
                if (verification.Year == year && verification.SerialNo == serialNo) {
                    return verification;
                }
            }

            return null;
        }
    }

    public class AccountTransactionCollection : List<AccountTransaction>
    {
        internal static AccountTransactionCollection CreateAccountTransactionCollection(AccountTransactionTable accountTransactionTable)
        {
            AccountTransactionCollection accountTransactionCollection = new AccountTransactionCollection();

            foreach (AccountTransactionDataRow accountTransactionDataRow in accountTransactionTable.Rows) {
                accountTransactionCollection.Add(new AccountTransaction(accountTransactionDataRow));
            }

            return accountTransactionCollection;
        }

        public AccountTransaction GetAccountTransaction(int no)
        {
            foreach (AccountTransaction accountTransaction in this) {
                if (accountTransaction.No == no) {
                    return accountTransaction;
                }
            }

            return null;
        }
    }

    public class TagAccountSnapshotCollection : List<TagAccountSnapshot>
    {
        internal static TagAccountSnapshotCollection CreateTagAccountSnapshotCollection(TagAccountSnapshotTable tagAccountSnapshotTable)
        {
            TagAccountSnapshotCollection tagAccountSnapshotCollection = new TagAccountSnapshotCollection();

            foreach (TagAccountSnapshotDataRow tagAccountSnapshotDataRow in tagAccountSnapshotTable.Rows) {
                tagAccountSnapshotCollection.Add(new TagAccountSnapshot(tagAccountSnapshotDataRow));
            }

            return tagAccountSnapshotCollection;
        }

        public TagAccountSnapshot GetTagAccountSnapshot(int no)
        {
            foreach (TagAccountSnapshot tagAccountSnapshot in this) {
                if (tagAccountSnapshot.No == no) {
                    return tagAccountSnapshot;
                }
            }

            return null;
        }
    }

    public class CashBookTransactionCollection : List<CashBookTransaction>
    {
        internal static CashBookTransactionCollection CreateCashBookTransactionCollection(CashBookTransactionTable cashBookTransactionTable)
        {
            CashBookTransactionCollection cashBookTransactionCollection = new CashBookTransactionCollection();

            foreach (CashBookTransactionDataRow cashBookTransactionDataRow in cashBookTransactionTable.Rows) {
                cashBookTransactionCollection.Add(new CashBookTransaction(cashBookTransactionDataRow));
            }

            return cashBookTransactionCollection;
        }

        public CashBookTransaction GetCashBookTransaction(int no)
        {
            foreach (CashBookTransaction cashBookTransaction in this) {
                if (cashBookTransaction.No == no) {
                    return cashBookTransaction;
                }
            }

            return null;
        }
    }

    public class LogItemCollection : List<LogItem>
    {
        internal static LogItemCollection CreateLogItemCollection(LogItemTable logItemTable)
        {
            LogItemCollection logItemCollection = new LogItemCollection();

            foreach (LogItemDataRow logItemDataRow in logItemTable.Rows) {
                logItemCollection.Add(new LogItem(logItemDataRow));
            }

            return logItemCollection;
        }

        public LogItem GetLogItem(int no)
        {
            foreach (LogItem logItem in this) {
                if (logItem.No == no) {
                    return logItem;
                }
            }

            return null;
        }
    }

    public class CashBoxSettingsCollection : List<CashBoxSettings>
    {
        internal static CashBoxSettingsCollection CreateCashBoxSettingsCollection(CashBoxSettingsTable cashBoxSettingsTable)
        {
            CashBoxSettingsCollection cashBoxSettingsCollection = new CashBoxSettingsCollection();

            foreach (CashBoxSettingsDataRow cashBoxSettingsDataRow in cashBoxSettingsTable.Rows) {
                cashBoxSettingsCollection.Add(new CashBoxSettings(cashBoxSettingsDataRow));
            }

            return cashBoxSettingsCollection;
        }

        public CashBoxSettings GetCashBoxSettings(CashBoxSettingsNo no)
        {
            foreach (CashBoxSettings cashBoxSettings in this) {
                if (cashBoxSettings.No == no) {
                    return cashBoxSettings;
                }
            }

            return null;
        }
    }
}
