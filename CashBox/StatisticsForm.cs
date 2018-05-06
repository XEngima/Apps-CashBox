using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EasyBase.BusinessLayer;
using EasyBase.Classes;
using DanielEiserman.DateAndTime;

namespace CashBox
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm(DataCache dataCache)
        {
            InitializeComponent();
            DataCache = dataCache;
            FormLoading = true;
        }

        private DataCache DataCache { get; set; }

        private bool FormLoading
        {
            get;
            set;
        }

        private void LoadAccountBalance()
        {
            accountChart.Series.Clear();

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                DataTable accountDataTable = core.GetAccountBalances(CurrentApplication.DateTimeNow);

                int serieNo = 0;

                foreach (DataRow accountDataRow in accountDataTable.Rows) {
                    string name = (string)accountDataRow[Account.fName];
                    decimal balance = (decimal)accountDataRow["Balance"];

                    if ((int)accountTypeToolStripComboBox.ComboBox.SelectedValue == 1) { // Visa konton
                        if (balance >= 0) {
                            accountChart.Series.Add(name).Points.Add((double)balance);
                        }
                    }
                    else if ((int)accountTypeToolStripComboBox.ComboBox.SelectedValue == 2) { // Visa skulder
                        if (balance <= 0) {
                            accountChart.Series.Add(name).Points.Add((double)-balance);
                        }
                    }
                    else { // Visa konton och skulder
                        accountChart.Series.Add(name).Points.Add((double)balance);
                    }

                    serieNo++;
                }
            }
        }

        private void LoadTagAccountBalance()
        {
            tagAccountChart.Series.Clear();

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                DataTable tagAccountDataTable = core.GetTagAccountBalances();

                foreach (DataRow accountDataRow in tagAccountDataTable.Rows)
                {
                    string name = (string)accountDataRow[Account.fName];
                    decimal balance = (decimal)accountDataRow["Balance"];

                    tagAccountChart.Series.Add(name).Points.Add((double)balance);
                }
            }
        }

        private void LoadTotalWealth()
        {
            totalWealthChart.Series.Clear();

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                totalWealthChart.Series.Add("Dag för dag");
                totalWealthChart.Series["Dag för dag"].ChartType = SeriesChartType.Line;

                totalWealthChart.Series.Add("Månadsvis");
                totalWealthChart.Series["Månadsvis"].ChartType = SeriesChartType.Line;

                decimal balanceBroughtForward = core.GetTotalBalanceBrougntForward();

                DataTable totalWealthDataTable = core.GetTotalWealthPerDate(DateTime.MinValue, DateTime.MaxValue);
                decimal startAmount = 0;
                DateTime startDate = DateTime.MinValue;
                decimal endAmount = 0;

                if (totalWealthDataTable.Rows.Count > 0) {
                    DateTime firstDate = Convert.ToDateTime(totalWealthDataTable.Rows[0]["Date"]);
                    DateTime lastDate = Convert.ToDateTime(totalWealthDataTable.Rows[totalWealthDataTable.Rows.Count - 1]["Date"]);
                    decimal totalWealth = balanceBroughtForward;
                    int row = 0;

                    for (DateTime date = firstDate; date <= CurrentApplication.DateTimeNow; date = date.AddDays(1))
                    {
                        if (row < totalWealthDataTable.Rows.Count && DanielEiserman.DateAndTime.Date.FloorDateTime(date) == Convert.ToDateTime(totalWealthDataTable.Rows[row]["Date"]))
                        {
                            totalWealth += (decimal)totalWealthDataTable.Rows[row]["Amount"];
                            row++;
                        }

                        totalWealthChart.Series["Dag för dag"].Points.AddXY(date, totalWealth);

                        if (date == firstDate)
                        {
                            startAmount = totalWealth; // Save start amount, so we can use it in the monthly presentation below
                            startDate = firstDate;
                        }
                    }

                    endAmount = totalWealth;
                }

                totalWealthDataTable = core.GetTotalWealthPerMonth(DateTime.MinValue, DateTime.MaxValue);

                if (totalWealthDataTable.Rows.Count > 0)
                {
                    totalWealthChart.Series["Månadsvis"].Points.AddXY(startDate, startAmount);

                    DateTime firstDate = Convert.ToDateTime((string)totalWealthDataTable.Rows[0]["Date"] + "-01");
                    DateTime lastDate = Convert.ToDateTime(totalWealthDataTable.Rows[totalWealthDataTable.Rows.Count - 1]["Date"]);
                    decimal totalWealth = balanceBroughtForward;
                    int row = 0;

                    for (DateTime date = firstDate; date <= lastDate; date = date.AddMonths(1))
                    {
                        if (DanielEiserman.DateAndTime.Date.FloorDateTime(date) == Convert.ToDateTime(totalWealthDataTable.Rows[row]["Date"]))
                        {
                            totalWealth += (decimal)totalWealthDataTable.Rows[row]["Amount"];
                            if (date.AddMonths(1) < CurrentApplication.DateTimeNow)
                            {
                                totalWealthChart.Series["Månadsvis"].Points.AddXY(date.AddMonths(1), totalWealth);
                            }

                            row++;
                        }
                    }
                }
            }
        }

        private void LoadMonthlyBalance()
        {
            monthlyBalanceChart.Series.Clear();
            monthlyBalanceChart.ResetAutoValues();

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                monthlyBalanceChart.Series.Add("Inkomster");
                monthlyBalanceChart.Series["Inkomster"].ChartType = SeriesChartType.Column;

                monthlyBalanceChart.Series.Add("Utgifter");
                monthlyBalanceChart.Series["Utgifter"].ChartType = SeriesChartType.Column;

                DateTime year = (DateTime)monthlyBalanceYearToolStripComboBox.ComboBox.SelectedValue;

                DataTable yearBalanceIncomeDataTable = core.GetMonthlyBalance(year, CategoryType.Income);
                DataTable yearBalanceExpenseDataTable = core.GetMonthlyBalance(year, CategoryType.Expense);

                DateTime startDate = DateTime.MinValue;
                int incomeRow = 0;
                int expenseRow = 0;

                for (int month = 1; month <= 12; month++)
                {
                    decimal monthIncome = 0;
                    decimal monthExpense = 0;

                    if (yearBalanceIncomeDataTable.Rows.Count > 0 && incomeRow < yearBalanceIncomeDataTable.Rows.Count)
                    {
                        if ((string)yearBalanceIncomeDataTable.Rows[incomeRow]["Month"] == year.ToString("yyyy") + "-" + month.ToString("00"))
                        {
                            monthIncome = (decimal)yearBalanceIncomeDataTable.Rows[incomeRow]["Amount"];
                            incomeRow++;
                        }
                    }

                    if (yearBalanceExpenseDataTable.Rows.Count > 0 && expenseRow < yearBalanceExpenseDataTable.Rows.Count)
                    {
                        if ((string)yearBalanceExpenseDataTable.Rows[expenseRow]["Month"] == year.ToString("yyyy") + "-" + month.ToString("00"))
                        {
                            monthExpense = -(decimal)yearBalanceExpenseDataTable.Rows[expenseRow]["Amount"];
                            expenseRow++;
                        }
                    }

                    monthlyBalanceChart.Series["Inkomster"].Points.AddXY(month, monthIncome);
                    monthlyBalanceChart.Series["Utgifter"].Points.AddXY(month, monthExpense);
                }
            }
        }

        private decimal GetSubCategoriesAmount(int categoryNo, DataTable categorySpendingDataTable)
        {
            decimal subCategoriesAmount = 0;

            foreach (DataRow accountDataRow in categorySpendingDataTable.Rows)
            {
                int subCategoryNo = (int)accountDataRow["No"];
                int parentCategoryNo = accountDataRow["ParentCategoryNo"] is DBNull ? 0 : (int)accountDataRow["ParentCategoryNo"];
                decimal amount = (decimal)accountDataRow["Amount"];

                if (parentCategoryNo == categoryNo)
                {
                    subCategoriesAmount = decimal.Add(subCategoriesAmount, amount);
                    subCategoriesAmount = decimal.Add(subCategoriesAmount, GetSubCategoriesAmount(subCategoryNo, categorySpendingDataTable));
                }
            }

            return subCategoriesAmount;
        }

        private void LoadMonthlyOverview()
        {
            monthlyOverviewChart.Series.Clear();
            monthlyOverviewChart.ResetAutoValues();

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                DataTable categorySpendingDataTable = core.GetSpendingsPerCategory(CategoryType.Expense, (DateTime)monthlyOverviewMonthToolStripComboBox.ComboBox.SelectedValue, true);
                List<DictionaryItem<string, decimal>> archivedCategories = new List<DictionaryItem<string, decimal>>();

                int serieNo = 0;

                foreach (DataRow accountDataRow in categorySpendingDataTable.Rows)
                {
                    if (accountDataRow["ParentCategoryNo"] is DBNull)
                    {
                        int categoryNo = (int)accountDataRow["No"];
                        string name = (string)accountDataRow["Name"];
                        decimal amount = (decimal)accountDataRow["Amount"];
                        bool isArchived = (bool)accountDataRow["IsArchived"];

                        if (!isArchived) {
                            amount = decimal.Add(amount, GetSubCategoriesAmount(categoryNo, categorySpendingDataTable));
                            monthlyOverviewChart.Series.Add(name).Points.Add((double)-amount);
                            serieNo++;
                        }
                        else if (decimal.Compare(amount, 0) != 0) { // Om kategorin är arkiverad men innehåller pengar
                            amount = decimal.Add(amount, GetSubCategoriesAmount(categoryNo, categorySpendingDataTable));
                            archivedCategories.Add(new DictionaryItem<string, decimal>() { Key = name, Value = amount });
                        }
                    }
                }

                // Add archived categories at the end
                foreach (var archivedCategory in archivedCategories) {
                    monthlyOverviewChart.Series.Add(archivedCategory.Key).Points.Add((double)-archivedCategory.Value);
                    serieNo++;
                }
            }
        }

        private void LoadYearlyOverview()
        {
            yearlyOverviewChart.Series.Clear();
            yearlyOverviewChart.ResetAutoValues();

            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                DataTable categorySpendingDataTable = core.GetSpendingsPerCategory(CategoryType.Expense, (DateTime)yearlyOverviewMonthToolStripComboBox.ComboBox.SelectedValue, false);
                List<DictionaryItem<string, decimal>> archivedCategories = new List<DictionaryItem<string, decimal>>();

                int serieNo = 0;

                foreach (DataRow accountDataRow in categorySpendingDataTable.Rows) {
                    if (accountDataRow["ParentCategoryNo"] is DBNull) {
                        int categoryNo = (int)accountDataRow["No"];
                        string name = (string)accountDataRow["Name"];
                        decimal amount = (decimal)accountDataRow["Amount"];
                        bool isArchived = (bool)accountDataRow["IsArchived"];

                        if (!isArchived) {
                            amount = decimal.Add(amount, GetSubCategoriesAmount(categoryNo, categorySpendingDataTable));
                            yearlyOverviewChart.Series.Add(name).Points.Add((double)-amount);
                            serieNo++;
                        }
                        else if (decimal.Compare(amount, 0) != 0) { // Om kategorin är arkiverad men innehåller pengar
                            amount = decimal.Add(amount, GetSubCategoriesAmount(categoryNo, categorySpendingDataTable));
                            archivedCategories.Add(new DictionaryItem<string, decimal>() { Key = name, Value = amount });
                        }
                    }
                }

                // Add archived categories at the end
                foreach (var archivedCategory in archivedCategories) {
                    yearlyOverviewChart.Series.Add(archivedCategory.Key).Points.Add((double)-archivedCategory.Value);
                    serieNo++;
                }
            }
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            List<DictionaryItem<int, string>> accountTypeList = new List<DictionaryItem<int,string>>();

            accountTypeList.Add(new DictionaryItem<int, string>() { Key = 1, Value = "Visa tillgångar" });
            accountTypeList.Add(new DictionaryItem<int, string>() { Key = 2, Value = "Visa skulder" });
            accountTypeList.Add(new DictionaryItem<int, string>() { Key = 3, Value = "Visa tillgångar och skulder" });

            accountTypeToolStripComboBox.ComboBox.ValueMember = "Key";
            accountTypeToolStripComboBox.ComboBox.DisplayMember = "Value";
            accountTypeToolStripComboBox.ComboBox.DataSource = accountTypeList;

            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                CashBoxDateType dateType = CashBoxDateType.TransactionDate;

                DateTime? firstAccountTransactionDate = core.GetEarliestVerificationDate(dateType);
                Date firstDate = CurrentApplication.DateNow;

                if (firstAccountTransactionDate != null)
                {
                    firstDate = new Date((DateTime)firstAccountTransactionDate);
                }

                // Fix so that the date is the first in its month
                firstDate = new Date(firstDate.Year, firstDate.Month, 1);

                // Month
                var monthList = new List<DictionaryItem<DateTime, string>>();
                DateTime date = CurrentApplication.DateNow;

                while ((Date)date >= firstDate)
                {
                    monthList.Add(new DictionaryItem<DateTime, string>() { Key = date, Value = date.ToString("yyyy-MM") });
                    date = date.AddMonths(-1);
                }

                monthlyOverviewMonthToolStripComboBox.ComboBox.ValueMember = "Key";
                monthlyOverviewMonthToolStripComboBox.ComboBox.DisplayMember = "Value";
                monthlyOverviewMonthToolStripComboBox.ComboBox.DataSource = monthList;

                // Fix so that the date is the first in its year
                firstDate = new Date(firstDate.Year, 1, 1);

                // Year
                var yearList = new List<DictionaryItem<DateTime, string>>();
                date = CurrentApplication.DateNow;

                while ((Date)date >= firstDate) {
                    yearList.Add(new DictionaryItem<DateTime, string>() { Key = date, Value = date.ToString("yyyy") });
                    date = date.AddYears(-1);
                }

                monthlyBalanceYearToolStripComboBox.ComboBox.ValueMember = "Key";
                monthlyBalanceYearToolStripComboBox.ComboBox.DisplayMember = "Value";
                monthlyBalanceYearToolStripComboBox.ComboBox.DataSource = yearList;

                // Fix so that the date is the first in its year
                firstDate = new Date(firstDate.Year, 1, 1);

                // Year
                yearList = new List<DictionaryItem<DateTime, string>>();
                date = CurrentApplication.DateNow;

                while ((Date)date >= firstDate) {
                    yearList.Add(new DictionaryItem<DateTime, string>() { Key = date, Value = date.ToString("yyyy") });
                    date = date.AddYears(-1);
                }

                yearlyOverviewMonthToolStripComboBox.ComboBox.ValueMember = "Key";
                yearlyOverviewMonthToolStripComboBox.ComboBox.DisplayMember = "Value";
                yearlyOverviewMonthToolStripComboBox.ComboBox.DataSource = yearList;
            }

            LoadMonthlyOverview();
            LoadYearlyOverview();
            LoadMonthlyBalance();
            LoadAccountBalance();
            LoadTagAccountBalance();
            LoadTotalWealth();

            FormLoading = false;
        }

        private void accountTypeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading) {
                LoadAccountBalance();
            }
        }

        private void monthToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading) {
                LoadMonthlyOverview();
            }
        }

        private void monthlyBalanceYearToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading) {
                LoadMonthlyBalance();
            }
        }

        private void yearlyOverviewMonthToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormLoading) {
                LoadYearlyOverview();
            }
        }
    }
}
