namespace BeanCounter
{
    partial class FrmReports
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
            if (disposing && (components != null))
            {
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
            this.gbReports = new System.Windows.Forms.GroupBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.gbForecaseOptions = new System.Windows.Forms.GroupBox();
            this.cbOverages = new System.Windows.Forms.CheckBox();
            this.nudOverages = new System.Windows.Forms.NumericUpDown();
            this.tbOverrideBalance = new System.Windows.Forms.TextBox();
            this.cbOverrideBalance = new System.Windows.Forms.CheckBox();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.btnLoadReport = new System.Windows.Forms.Button();
            this.cbCategoryName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDateRange = new System.Windows.Forms.ComboBox();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvReports = new System.Windows.Forms.DataGridView();
            this.bwBuildCashForecast = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslRows = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslAverage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMonthly = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.gbForecaseOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverages)).BeginInit();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbReports
            // 
            this.gbReports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbReports.Controls.Add(this.scMain);
            this.gbReports.Location = new System.Drawing.Point(12, 12);
            this.gbReports.Name = "gbReports";
            this.gbReports.Size = new System.Drawing.Size(698, 438);
            this.gbReports.TabIndex = 0;
            this.gbReports.TabStop = false;
            // 
            // scMain
            // 
            this.scMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(6, 11);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.gbForecaseOptions);
            this.scMain.Panel1.Controls.Add(this.gbOptions);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.dgvReports);
            this.scMain.Size = new System.Drawing.Size(686, 421);
            this.scMain.SplitterDistance = 112;
            this.scMain.TabIndex = 0;
            // 
            // gbForecaseOptions
            // 
            this.gbForecaseOptions.Controls.Add(this.cbOverages);
            this.gbForecaseOptions.Controls.Add(this.nudOverages);
            this.gbForecaseOptions.Controls.Add(this.tbOverrideBalance);
            this.gbForecaseOptions.Controls.Add(this.cbOverrideBalance);
            this.gbForecaseOptions.Location = new System.Drawing.Point(362, 0);
            this.gbForecaseOptions.Name = "gbForecaseOptions";
            this.gbForecaseOptions.Size = new System.Drawing.Size(239, 109);
            this.gbForecaseOptions.TabIndex = 1;
            this.gbForecaseOptions.TabStop = false;
            // 
            // cbOverages
            // 
            this.cbOverages.Checked = true;
            this.cbOverages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOverages.Location = new System.Drawing.Point(9, 54);
            this.cbOverages.Name = "cbOverages";
            this.cbOverages.Size = new System.Drawing.Size(134, 48);
            this.cbOverages.TabIndex = 4;
            this.cbOverages.Text = "Add the following % to Anytime Spending (for overages)";
            this.cbOverages.UseVisualStyleBackColor = true;
            this.cbOverages.CheckedChanged += new System.EventHandler(this.cbOverages_CheckedChanged);
            // 
            // nudOverages
            // 
            this.nudOverages.Location = new System.Drawing.Point(159, 72);
            this.nudOverages.Name = "nudOverages";
            this.nudOverages.Size = new System.Drawing.Size(63, 20);
            this.nudOverages.TabIndex = 3;
            this.nudOverages.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // tbOverrideBalance
            // 
            this.tbOverrideBalance.Enabled = false;
            this.tbOverrideBalance.Location = new System.Drawing.Point(159, 28);
            this.tbOverrideBalance.Name = "tbOverrideBalance";
            this.tbOverrideBalance.Size = new System.Drawing.Size(63, 20);
            this.tbOverrideBalance.TabIndex = 1;
            // 
            // cbOverrideBalance
            // 
            this.cbOverrideBalance.AutoSize = true;
            this.cbOverrideBalance.Location = new System.Drawing.Point(9, 31);
            this.cbOverrideBalance.Name = "cbOverrideBalance";
            this.cbOverrideBalance.Size = new System.Drawing.Size(110, 17);
            this.cbOverrideBalance.TabIndex = 0;
            this.cbOverrideBalance.Text = "Override balance:";
            this.cbOverrideBalance.UseVisualStyleBackColor = true;
            this.cbOverrideBalance.CheckedChanged += new System.EventHandler(this.cbOverrideBalance_CheckedChanged);
            // 
            // gbOptions
            // 
            this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOptions.Controls.Add(this.btnLoadReport);
            this.gbOptions.Controls.Add(this.cbCategoryName);
            this.gbOptions.Controls.Add(this.label3);
            this.gbOptions.Controls.Add(this.cbDateRange);
            this.gbOptions.Controls.Add(this.cbReportType);
            this.gbOptions.Controls.Add(this.label2);
            this.gbOptions.Controls.Add(this.label1);
            this.gbOptions.Location = new System.Drawing.Point(3, 0);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(353, 109);
            this.gbOptions.TabIndex = 0;
            this.gbOptions.TabStop = false;
            // 
            // btnLoadReport
            // 
            this.btnLoadReport.Enabled = false;
            this.btnLoadReport.Location = new System.Drawing.Point(268, 31);
            this.btnLoadReport.Name = "btnLoadReport";
            this.btnLoadReport.Size = new System.Drawing.Size(75, 23);
            this.btnLoadReport.TabIndex = 6;
            this.btnLoadReport.Text = "Load Report";
            this.btnLoadReport.UseVisualStyleBackColor = true;
            this.btnLoadReport.Click += new System.EventHandler(this.btnLoadReport_Click);
            // 
            // cbCategoryName
            // 
            this.cbCategoryName.Enabled = false;
            this.cbCategoryName.FormattingEnabled = true;
            this.cbCategoryName.Location = new System.Drawing.Point(9, 78);
            this.cbCategoryName.Name = "cbCategoryName";
            this.cbCategoryName.Size = new System.Drawing.Size(334, 21);
            this.cbCategoryName.TabIndex = 5;
            this.cbCategoryName.SelectedIndexChanged += new System.EventHandler(this.cbCategoryName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Category name:";
            // 
            // cbDateRange
            // 
            this.cbDateRange.Enabled = false;
            this.cbDateRange.FormattingEnabled = true;
            this.cbDateRange.Items.AddRange(new object[] {
            "Last month",
            "Last 3 months",
            "Last 6 months",
            "Month to date",
            "Year to date"});
            this.cbDateRange.Location = new System.Drawing.Point(141, 33);
            this.cbDateRange.Name = "cbDateRange";
            this.cbDateRange.Size = new System.Drawing.Size(121, 21);
            this.cbDateRange.TabIndex = 3;
            this.cbDateRange.SelectedIndexChanged += new System.EventHandler(this.cbDateRange_SelectedIndexChanged);
            // 
            // cbReportType
            // 
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "All Categories",
            "By Category",
            "Cash Forecast",
            "Items Due"});
            this.cbReportType.Location = new System.Drawing.Point(9, 33);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(121, 21);
            this.cbReportType.TabIndex = 2;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Report type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date Range:";
            // 
            // dgvReports
            // 
            this.dgvReports.AllowUserToAddRows = false;
            this.dgvReports.AllowUserToDeleteRows = false;
            this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReports.Location = new System.Drawing.Point(0, 0);
            this.dgvReports.Name = "dgvReports";
            this.dgvReports.ReadOnly = true;
            this.dgvReports.Size = new System.Drawing.Size(686, 305);
            this.dgvReports.TabIndex = 0;
            // 
            // bwBuildCashForecast
            // 
            this.bwBuildCashForecast.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwBuildCashForecast_DoWork);
            this.bwBuildCashForecast.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwBuildCashForecast_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslRows,
            this.tsslTotal,
            this.tsslAverage,
            this.tsslMonthly});
            this.statusStrip1.Location = new System.Drawing.Point(12, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(698, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslRows
            // 
            this.tsslRows.Name = "tsslRows";
            this.tsslRows.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslTotal
            // 
            this.tsslTotal.Name = "tsslTotal";
            this.tsslTotal.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslAverage
            // 
            this.tsslAverage.Name = "tsslAverage";
            this.tsslAverage.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslMonthly
            // 
            this.tsslMonthly.Name = "tsslMonthly";
            this.tsslMonthly.Size = new System.Drawing.Size(0, 17);
            // 
            // FrmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 492);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbReports);
            this.Name = "FrmReports";
            this.Text = "FrmReports";
            this.gbReports.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.gbForecaseOptions.ResumeLayout(false);
            this.gbForecaseOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverages)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbReports;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbForecaseOptions;
        private System.Windows.Forms.NumericUpDown nudOverages;
        private System.Windows.Forms.TextBox tbOverrideBalance;
        private System.Windows.Forms.CheckBox cbOverrideBalance;
        private System.Windows.Forms.Button btnLoadReport;
        private System.Windows.Forms.ComboBox cbCategoryName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDateRange;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvReports;
        private System.ComponentModel.BackgroundWorker bwBuildCashForecast;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslRows;
        private System.Windows.Forms.ToolStripStatusLabel tsslTotal;
        private System.Windows.Forms.ToolStripStatusLabel tsslAverage;
        private System.Windows.Forms.ToolStripStatusLabel tsslMonthly;
        private System.Windows.Forms.CheckBox cbOverages;
    }
}