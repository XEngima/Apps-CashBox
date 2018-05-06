using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBase.Classes;
using System.Data;
using System.Data.SqlClient;

namespace EasyBase.DataLayer
{
	internal static class DatabaseConversions
	{
        /// <summary>
        /// Converts the database after an update of the database schema.
        /// </summary>
        /// <param name="toVersion">Database version of the database to convert to.</param>
        /// <remarks>
        /// This must be updated for every new database version.
        /// </remarks>
        internal static SqlUpdateCommand ConvertDatabase(int toVersion)
        {
            if (toVersion == 3) {
                return new SqlUpdateCommand(ConvertDatabaseToVersion3);
            }
            else if (toVersion == 5) {
                return ConvertDatabaseToVersion5();
            }
            else if (toVersion == 7)
            {
                return ConvertDatabaseToVersion7();
            }
            else if (toVersion == 8)
            {
                return ConvertDatabaseToVersion8();
            }
            else if (toVersion == 9) {
                return ConvertDatabaseToVersion9();
            }

            return null;
        }

        private static SqlUpdateCommand ConvertDatabaseToVersion9()
        {
            string sql = "update Accounts set ShowInDiagram = 1; update Categories set ShowInDiagram = 1;";
            return new SqlUpdateCommand(sql);
        }

        private static SqlUpdateCommand ConvertDatabaseToVersion8()
        {
            string sql = "update Verifications set AccountingDate = Date";
            return new SqlUpdateCommand(sql);
        }

        private static SqlUpdateCommand ConvertDatabaseToVersion7()
        {
            string sql = "insert into CashBoxSettings values (" + (int)CashBoxSettingsNo.CurrentApplicationNo + ", " + CurrentApplication.DateTimeNow.Year + ")";
            return new SqlUpdateCommand(sql);
        }

        private static SqlUpdateCommand ConvertDatabaseToVersion5()
        {
            string sql = string.Format(@"
                update AccountTags
                set 
                    MoneyValue = (case when Type={0} then cast(Value as money) else 0 end),
                    RelativeValue = (case when Type={1} then cast(Value as decimal(18,9)) else 0 end);
                ",
                (int) AccountTagType.ExactAmount,
                (int) AccountTagType.PercentOfRest);

            return new SqlUpdateCommand(sql);
        }

        private static void ConvertDatabaseToVersion3(EasyBaseSqlConnection connection)
        {
            // Ta bort alla ingående saldo-rader och sätt värdet på kontot istället

            DataTable accountTable = new DataTable();

            string sql = "select No from Accounts where BalanceBroughtForwardAmount = 0";
            connection.GetTable(accountTable, sql);

            foreach (DataRow accountRow in accountTable.Rows) {
                int accountNo = (int)accountRow["No"];

                sql = "update Accounts set BalanceBroughtForwardAmount = (select isnull(sum(Amount), 0) from AccountTransactions where Type = 1 and AccountNo = " + accountNo + ") where No = " + accountNo;
                connection.Update(sql);
            }

            // Ta bort alla ingående balans-rader
            try {
                sql = "delete from AccountTransactions where Type = 1";
                connection.Delete(sql);
            }
            catch {
            }

            // Sätt rätt typ på tillgångs- och skuldkonton
            
            sql = @"
                update Accounts
                set Type = (
                    select case when (
                        select Accounts.BalanceBroughtForwardAmount + sum(Amount)
                        from AccountTransactions
                        where AccountNo = Accounts.No) < 0 then 1
                    else 2 end
                )";

            connection.Update(sql);

            DataTable accountTransactionTable = new DataTable();



            // Skapa verifikationer åt alla kontotransaktioner

            accountTransactionTable = new DataTable();
            bool alreadyDone = false;

            try {
                sql = "select * from AccountTransactions where SourceAccountTransactionNo is null order by TransactionTime asc, No asc";
                connection.GetTable(accountTransactionTable, sql);
            }
            catch {
                // Fältet SourceAccountTransactionNo finns inte, alltså har uppdateringen avbrutits en gång efter hela den här konverteringen...
                alreadyDone = true;
            }

            if (!alreadyDone) {
                sql = "delete from Verifications";
                connection.Delete(sql);

                int verificationNo = 1;
                int verificationSerialNumber = 1;
                int lastYear = 0;

                connection.Execute("set identity_insert Verifications on");

                foreach (DataRow accountTransactionDataRow in accountTransactionTable.Rows) {
                    int accountTransactionNo = (int)accountTransactionDataRow["No"];
                    DateTime time = (DateTime)accountTransactionDataRow["TransactionTime"];

                    if (time.Year > lastYear) {
                        verificationSerialNumber = 1;
                    }

                    sql = "insert into Verifications (No, Year, SerialNo, Date) values (" + verificationNo + ", " + time.Year + ", " + verificationSerialNumber + ", '" + time.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    connection.Insert(sql);

                    sql = "update AccountTransactions set VerificationNo = " + verificationNo + " where No = " + accountTransactionNo;
                    connection.Update(sql);

                    sql = "select No from CashBookTransactions where SourceAccountTransactionNo = " + accountTransactionNo;
                    DataTable cashBookTransactionDataTable = new DataTable();
                    connection.GetTable(cashBookTransactionDataTable, sql);

                    foreach (DataRow cashBookTransactionsDataRow in cashBookTransactionDataTable.Rows) {
                        int cashBookTransactionNo = (int)cashBookTransactionsDataRow["No"];
                        sql = "update CashBookTransactions set VerificationNo = " + verificationNo + " where No = " + cashBookTransactionNo;
                        connection.Update(sql);
                    }

                    sql = "select No from AccountTransactions where SourceAccountTransactionNo = " + accountTransactionNo;
                    DataTable accountTransactionDataTable2 = new DataTable();
                    connection.GetTable(accountTransactionDataTable2, sql);

                    foreach (DataRow accountTransactionsDataRow in accountTransactionDataTable2.Rows) {
                        int accountTransactionNo2 = (int)accountTransactionsDataRow["No"];
                        sql = "update accountTransactions set VerificationNo = " + verificationNo + " where No = " + accountTransactionNo2;
                        connection.Update(sql);
                    }

                    verificationNo++;
                    verificationSerialNumber++;
                    lastYear = time.Year;
                }

                connection.Execute("set identity_insert Verifications off");
            }
        }
    }
}
