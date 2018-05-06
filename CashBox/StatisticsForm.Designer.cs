namespace CashBox
{
    partial class StatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.monthlyViewTabPage = new System.Windows.Forms.TabPage();
            this.monthlyOverviewChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.monthlyOverviewMonthToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.monthBalanceTabPage = new System.Windows.Forms.TabPage();
            this.monthlyBalanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.monthlyBalanceYearToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.accountOverviewTabPage = new System.Windows.Forms.TabPage();
            this.accountChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.accountTypeToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.tagAccountOverviewTabPage = new System.Windows.Forms.TabPage();
            this.tagAccountChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.totalWealthTabPage = new System.Windows.Forms.TabPage();
            this.totalWealthChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.yearlyViewTabPage = new System.Windows.Forms.TabPage();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.yearlyOverviewMonthToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.yearlyOverviewChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl.SuspendLayout();
            this.monthlyViewTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyOverviewChart)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.monthBalanceTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyBalanceChart)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.accountOverviewTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accountChart)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tagAccountOverviewTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tagAccountChart)).BeginInit();
            this.totalWealthTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalWealthChart)).BeginInit();
            this.yearlyViewTabPage.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearlyOverviewChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.monthlyViewTabPage);
            this.tabControl.Controls.Add(this.yearlyViewTabPage);
            this.tabControl.Controls.Add(this.monthBalanceTabPage);
            this.tabControl.Controls.Add(this.accountOverviewTabPage);
            this.tabControl.Controls.Add(this.tagAccountOverviewTabPage);
            this.tabControl.Controls.Add(this.totalWealthTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1082, 593);
            this.tabControl.TabIndex = 0;
            // 
            // monthlyViewTabPage
            // 
            this.monthlyViewTabPage.Controls.Add(this.monthlyOverviewChart);
            this.monthlyViewTabPage.Controls.Add(this.toolStrip3);
            this.monthlyViewTabPage.Location = new System.Drawing.Point(4, 22);
            this.monthlyViewTabPage.Name = "monthlyViewTabPage";
            this.monthlyViewTabPage.Size = new System.Drawing.Size(1074, 567);
            this.monthlyViewTabPage.TabIndex = 2;
            this.monthlyViewTabPage.Text = "Månadsöversikt";
            this.monthlyViewTabPage.UseVisualStyleBackColor = true;
            // 
            // monthlyOverviewChart
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX.Maximum = 1.6D;
            chartArea1.AxisX.Minimum = 0.4D;
            chartArea1.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea1.CursorX.Interval = 0D;
            chartArea1.Name = "ChartArea1";
            this.monthlyOverviewChart.ChartAreas.Add(chartArea1);
            this.monthlyOverviewChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.monthlyOverviewChart.Legends.Add(legend1);
            this.monthlyOverviewChart.Location = new System.Drawing.Point(0, 25);
            this.monthlyOverviewChart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.monthlyOverviewChart.Name = "monthlyOverviewChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.monthlyOverviewChart.Series.Add(series1);
            this.monthlyOverviewChart.Size = new System.Drawing.Size(1074, 542);
            this.monthlyOverviewChart.TabIndex = 2;
            this.monthlyOverviewChart.Text = "chart1";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monthlyOverviewMonthToolStripComboBox});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1074, 25);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // monthlyOverviewMonthToolStripComboBox
            // 
            this.monthlyOverviewMonthToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthlyOverviewMonthToolStripComboBox.Name = "monthlyOverviewMonthToolStripComboBox";
            this.monthlyOverviewMonthToolStripComboBox.Size = new System.Drawing.Size(76, 25);
            this.monthlyOverviewMonthToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.monthToolStripComboBox_SelectedIndexChanged);
            // 
            // monthBalanceTabPage
            // 
            this.monthBalanceTabPage.Controls.Add(this.monthlyBalanceChart);
            this.monthBalanceTabPage.Controls.Add(this.toolStrip4);
            this.monthBalanceTabPage.Location = new System.Drawing.Point(4, 22);
            this.monthBalanceTabPage.Name = "monthBalanceTabPage";
            this.monthBalanceTabPage.Size = new System.Drawing.Size(1074, 567);
            this.monthBalanceTabPage.TabIndex = 3;
            this.monthBalanceTabPage.Text = "Månadsbalans";
            this.monthBalanceTabPage.UseVisualStyleBackColor = true;
            // 
            // monthlyBalanceChart
            // 
            chartArea3.Name = "ChartArea1";
            this.monthlyBalanceChart.ChartAreas.Add(chartArea3);
            this.monthlyBalanceChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.monthlyBalanceChart.Legends.Add(legend3);
            this.monthlyBalanceChart.Location = new System.Drawing.Point(0, 25);
            this.monthlyBalanceChart.Name = "monthlyBalanceChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.monthlyBalanceChart.Series.Add(series3);
            this.monthlyBalanceChart.Size = new System.Drawing.Size(1074, 542);
            this.monthlyBalanceChart.TabIndex = 0;
            this.monthlyBalanceChart.Text = "chart1";
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monthlyBalanceYearToolStripComboBox});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(1074, 25);
            this.toolStrip4.TabIndex = 2;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // monthlyBalanceYearToolStripComboBox
            // 
            this.monthlyBalanceYearToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthlyBalanceYearToolStripComboBox.Name = "monthlyBalanceYearToolStripComboBox";
            this.monthlyBalanceYearToolStripComboBox.Size = new System.Drawing.Size(76, 25);
            this.monthlyBalanceYearToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.monthlyBalanceYearToolStripComboBox_SelectedIndexChanged);
            // 
            // accountOverviewTabPage
            // 
            this.accountOverviewTabPage.Controls.Add(this.accountChart);
            this.accountOverviewTabPage.Controls.Add(this.toolStrip1);
            this.accountOverviewTabPage.Location = new System.Drawing.Point(4, 22);
            this.accountOverviewTabPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.accountOverviewTabPage.Name = "accountOverviewTabPage";
            this.accountOverviewTabPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.accountOverviewTabPage.Size = new System.Drawing.Size(1074, 567);
            this.accountOverviewTabPage.TabIndex = 0;
            this.accountOverviewTabPage.Text = "Kontoöversikt";
            this.accountOverviewTabPage.UseVisualStyleBackColor = true;
            // 
            // accountChart
            // 
            chartArea4.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea4.AxisX.Maximum = 1.6D;
            chartArea4.AxisX.Minimum = 0.4D;
            chartArea4.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea4.CursorX.Interval = 0D;
            chartArea4.Name = "ChartArea1";
            this.accountChart.ChartAreas.Add(chartArea4);
            this.accountChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.accountChart.Legends.Add(legend4);
            this.accountChart.Location = new System.Drawing.Point(2, 27);
            this.accountChart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.accountChart.Name = "accountChart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series4.IsXValueIndexed = true;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.accountChart.Series.Add(series4);
            this.accountChart.Size = new System.Drawing.Size(1070, 538);
            this.accountChart.TabIndex = 1;
            this.accountChart.Text = "chart1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountTypeToolStripComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1070, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // accountTypeToolStripComboBox
            // 
            this.accountTypeToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountTypeToolStripComboBox.Name = "accountTypeToolStripComboBox";
            this.accountTypeToolStripComboBox.Size = new System.Drawing.Size(151, 25);
            this.accountTypeToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.accountTypeToolStripComboBox_SelectedIndexChanged);
            // 
            // tagAccountOverviewTabPage
            // 
            this.tagAccountOverviewTabPage.Controls.Add(this.tagAccountChart);
            this.tagAccountOverviewTabPage.Location = new System.Drawing.Point(4, 22);
            this.tagAccountOverviewTabPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tagAccountOverviewTabPage.Name = "tagAccountOverviewTabPage";
            this.tagAccountOverviewTabPage.Size = new System.Drawing.Size(1074, 567);
            this.tagAccountOverviewTabPage.TabIndex = 4;
            this.tagAccountOverviewTabPage.Text = "Märkningskonton";
            this.tagAccountOverviewTabPage.UseVisualStyleBackColor = true;
            // 
            // tagAccountChart
            // 
            chartArea5.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea5.AxisX.Maximum = 1.6D;
            chartArea5.AxisX.Minimum = 0.4D;
            chartArea5.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea5.CursorX.Interval = 0D;
            chartArea5.Name = "ChartArea1";
            this.tagAccountChart.ChartAreas.Add(chartArea5);
            this.tagAccountChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Name = "Legend1";
            this.tagAccountChart.Legends.Add(legend5);
            this.tagAccountChart.Location = new System.Drawing.Point(0, 0);
            this.tagAccountChart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tagAccountChart.Name = "tagAccountChart";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series5.IsXValueIndexed = true;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.tagAccountChart.Series.Add(series5);
            this.tagAccountChart.Size = new System.Drawing.Size(1074, 567);
            this.tagAccountChart.TabIndex = 2;
            this.tagAccountChart.Text = "chart1";
            // 
            // totalWealthTabPage
            // 
            this.totalWealthTabPage.Controls.Add(this.totalWealthChart);
            this.totalWealthTabPage.Controls.Add(this.toolStrip2);
            this.totalWealthTabPage.Location = new System.Drawing.Point(4, 22);
            this.totalWealthTabPage.Name = "totalWealthTabPage";
            this.totalWealthTabPage.Size = new System.Drawing.Size(1074, 567);
            this.totalWealthTabPage.TabIndex = 1;
            this.totalWealthTabPage.Text = "Total förmögenhet";
            this.totalWealthTabPage.UseVisualStyleBackColor = true;
            // 
            // totalWealthChart
            // 
            chartArea6.Name = "ChartArea1";
            this.totalWealthChart.ChartAreas.Add(chartArea6);
            this.totalWealthChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Name = "Legend1";
            this.totalWealthChart.Legends.Add(legend6);
            this.totalWealthChart.Location = new System.Drawing.Point(0, 25);
            this.totalWealthChart.Name = "totalWealthChart";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.totalWealthChart.Series.Add(series6);
            this.totalWealthChart.Size = new System.Drawing.Size(1074, 542);
            this.totalWealthChart.TabIndex = 1;
            this.totalWealthChart.Text = "chart1";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1074, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // yearlyViewTabPage
            // 
            this.yearlyViewTabPage.Controls.Add(this.yearlyOverviewChart);
            this.yearlyViewTabPage.Controls.Add(this.toolStrip5);
            this.yearlyViewTabPage.Location = new System.Drawing.Point(4, 22);
            this.yearlyViewTabPage.Name = "yearlyViewTabPage";
            this.yearlyViewTabPage.Size = new System.Drawing.Size(1074, 567);
            this.yearlyViewTabPage.TabIndex = 5;
            this.yearlyViewTabPage.Text = "Årsöversikt";
            this.yearlyViewTabPage.UseVisualStyleBackColor = true;
            // 
            // toolStrip5
            // 
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yearlyOverviewMonthToolStripComboBox});
            this.toolStrip5.Location = new System.Drawing.Point(0, 0);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(1074, 25);
            this.toolStrip5.TabIndex = 2;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // yearlyOverviewMonthToolStripComboBox
            // 
            this.yearlyOverviewMonthToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearlyOverviewMonthToolStripComboBox.Name = "yearlyOverviewMonthToolStripComboBox";
            this.yearlyOverviewMonthToolStripComboBox.Size = new System.Drawing.Size(76, 25);
            this.yearlyOverviewMonthToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.yearlyOverviewMonthToolStripComboBox_SelectedIndexChanged);
            // 
            // yearlyOverviewChart
            // 
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisX.Maximum = 1.6D;
            chartArea2.AxisX.Minimum = 0.4D;
            chartArea2.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea2.CursorX.Interval = 0D;
            chartArea2.Name = "ChartArea1";
            this.yearlyOverviewChart.ChartAreas.Add(chartArea2);
            this.yearlyOverviewChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.yearlyOverviewChart.Legends.Add(legend2);
            this.yearlyOverviewChart.Location = new System.Drawing.Point(0, 25);
            this.yearlyOverviewChart.Margin = new System.Windows.Forms.Padding(2);
            this.yearlyOverviewChart.Name = "yearlyOverviewChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.yearlyOverviewChart.Series.Add(series2);
            this.yearlyOverviewChart.Size = new System.Drawing.Size(1074, 542);
            this.yearlyOverviewChart.TabIndex = 3;
            this.yearlyOverviewChart.Text = "chart1";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 593);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "StatisticsForm";
            this.Text = "Statistik";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.monthlyViewTabPage.ResumeLayout(false);
            this.monthlyViewTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyOverviewChart)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.monthBalanceTabPage.ResumeLayout(false);
            this.monthBalanceTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyBalanceChart)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.accountOverviewTabPage.ResumeLayout(false);
            this.accountOverviewTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accountChart)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tagAccountOverviewTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tagAccountChart)).EndInit();
            this.totalWealthTabPage.ResumeLayout(false);
            this.totalWealthTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalWealthChart)).EndInit();
            this.yearlyViewTabPage.ResumeLayout(false);
            this.yearlyViewTabPage.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearlyOverviewChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage accountOverviewTabPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart accountChart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox accountTypeToolStripComboBox;
        private System.Windows.Forms.TabPage totalWealthTabPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart totalWealthChart;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.TabPage monthlyViewTabPage;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripComboBox monthlyOverviewMonthToolStripComboBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart monthlyOverviewChart;
        private System.Windows.Forms.TabPage monthBalanceTabPage;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.DataVisualization.Charting.Chart monthlyBalanceChart;
        private System.Windows.Forms.ToolStripComboBox monthlyBalanceYearToolStripComboBox;
        private System.Windows.Forms.TabPage tagAccountOverviewTabPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart tagAccountChart;
        private System.Windows.Forms.TabPage yearlyViewTabPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart yearlyOverviewChart;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripComboBox yearlyOverviewMonthToolStripComboBox;
    }
}