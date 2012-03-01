namespace BeanCounter
{
    partial class FrmCategories
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
            this.components = new System.ComponentModel.Container();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.chkByDate = new System.Windows.Forms.CheckBox();
            this.gbProperties = new System.Windows.Forms.GroupBox();
            this.rbSpending = new System.Windows.Forms.RadioButton();
            this.rbIncome = new System.Windows.Forms.RadioButton();
            this.rbWeekly = new System.Windows.Forms.RadioButton();
            this.rbOneTime = new System.Windows.Forms.RadioButton();
            this.rbMonthly = new System.Windows.Forms.RadioButton();
            this.rbBiWeekly = new System.Windows.Forms.RadioButton();
            this.rbAnytime = new System.Windows.Forms.RadioButton();
            this.rbAnnually = new System.Windows.Forms.RadioButton();
            this.gbMonthlyAmounts = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFill = new System.Windows.Forms.Button();
            this.tbFill = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbDec = new System.Windows.Forms.TextBox();
            this.tbNov = new System.Windows.Forms.TextBox();
            this.tbOct = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSep = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbAug = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbJul = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbJun = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbApr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFeb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbJan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmsDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeleteCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.gbProperties.SuspendLayout();
            this.gbMonthlyAmounts.SuspendLayout();
            this.cmsDelete.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.dgvCategories);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.gbProperties);
            this.scMain.Panel2.Controls.Add(this.chkByDate);
            this.scMain.Panel2.Controls.Add(this.gbMonthlyAmounts);
            this.scMain.Size = new System.Drawing.Size(846, 624);
            this.scMain.SplitterDistance = 651;
            this.scMain.TabIndex = 0;
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToOrderColumns = true;
            this.dgvCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvCategories.Location = new System.Drawing.Point(13, 12);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.Size = new System.Drawing.Size(635, 599);
            this.dgvCategories.TabIndex = 0;
            this.dgvCategories.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategories_CellContentClick);
            this.dgvCategories.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCategories_CellMouseClick);
            this.dgvCategories.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCategories_EditingControlShowing);
            this.dgvCategories.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategories_RowEnter);
            this.dgvCategories.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCategories_RowHeaderMouseClick);
            this.dgvCategories.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvCategories_RowsAdded);
            // 
            // chkByDate
            // 
            this.chkByDate.AutoSize = true;
            this.chkByDate.Location = new System.Drawing.Point(14, 551);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(90, 17);
            this.chkByDate.TabIndex = 2;
            this.chkByDate.Text = "Order by date";
            this.chkByDate.UseVisualStyleBackColor = true;
            this.chkByDate.Visible = false;
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // gbProperties
            // 
            this.gbProperties.Controls.Add(this.groupBox1);
            this.gbProperties.Controls.Add(this.groupBox2);
            this.gbProperties.Location = new System.Drawing.Point(7, 6);
            this.gbProperties.Name = "gbProperties";
            this.gbProperties.Size = new System.Drawing.Size(172, 165);
            this.gbProperties.TabIndex = 0;
            this.gbProperties.TabStop = false;
            // 
            // rbSpending
            // 
            this.rbSpending.AutoSize = true;
            this.rbSpending.Location = new System.Drawing.Point(84, 18);
            this.rbSpending.Name = "rbSpending";
            this.rbSpending.Size = new System.Drawing.Size(70, 17);
            this.rbSpending.TabIndex = 7;
            this.rbSpending.TabStop = true;
            this.rbSpending.Text = "Spending";
            this.rbSpending.UseVisualStyleBackColor = true;
            this.rbSpending.CheckedChanged += new System.EventHandler(this.rbSpending_CheckedChanged);
            // 
            // rbIncome
            // 
            this.rbIncome.AutoSize = true;
            this.rbIncome.Location = new System.Drawing.Point(6, 18);
            this.rbIncome.Name = "rbIncome";
            this.rbIncome.Size = new System.Drawing.Size(60, 17);
            this.rbIncome.TabIndex = 6;
            this.rbIncome.TabStop = true;
            this.rbIncome.Text = "Income";
            this.rbIncome.UseVisualStyleBackColor = true;
            this.rbIncome.CheckedChanged += new System.EventHandler(this.rbIncome_CheckedChanged);
            // 
            // rbWeekly
            // 
            this.rbWeekly.AutoSize = true;
            this.rbWeekly.Location = new System.Drawing.Point(84, 58);
            this.rbWeekly.Name = "rbWeekly";
            this.rbWeekly.Size = new System.Drawing.Size(61, 17);
            this.rbWeekly.TabIndex = 5;
            this.rbWeekly.TabStop = true;
            this.rbWeekly.Text = "Weekly";
            this.rbWeekly.UseVisualStyleBackColor = true;
            this.rbWeekly.CheckedChanged += new System.EventHandler(this.rbWeekly_CheckedChanged);
            // 
            // rbOneTime
            // 
            this.rbOneTime.AutoSize = true;
            this.rbOneTime.Location = new System.Drawing.Point(84, 35);
            this.rbOneTime.Name = "rbOneTime";
            this.rbOneTime.Size = new System.Drawing.Size(71, 17);
            this.rbOneTime.TabIndex = 4;
            this.rbOneTime.TabStop = true;
            this.rbOneTime.Text = "One Time";
            this.rbOneTime.UseVisualStyleBackColor = true;
            this.rbOneTime.CheckedChanged += new System.EventHandler(this.rbOneTime_CheckedChanged);
            // 
            // rbMonthly
            // 
            this.rbMonthly.AutoSize = true;
            this.rbMonthly.Location = new System.Drawing.Point(84, 10);
            this.rbMonthly.Name = "rbMonthly";
            this.rbMonthly.Size = new System.Drawing.Size(62, 17);
            this.rbMonthly.TabIndex = 3;
            this.rbMonthly.TabStop = true;
            this.rbMonthly.Text = "Monthly";
            this.rbMonthly.UseVisualStyleBackColor = true;
            this.rbMonthly.CheckedChanged += new System.EventHandler(this.rbMonthly_CheckedChanged);
            // 
            // rbBiWeekly
            // 
            this.rbBiWeekly.AutoSize = true;
            this.rbBiWeekly.Location = new System.Drawing.Point(6, 58);
            this.rbBiWeekly.Name = "rbBiWeekly";
            this.rbBiWeekly.Size = new System.Drawing.Size(73, 17);
            this.rbBiWeekly.TabIndex = 2;
            this.rbBiWeekly.TabStop = true;
            this.rbBiWeekly.Text = "Bi-Weekly";
            this.rbBiWeekly.UseVisualStyleBackColor = true;
            this.rbBiWeekly.CheckedChanged += new System.EventHandler(this.rbBiWeekly_CheckedChanged);
            // 
            // rbAnytime
            // 
            this.rbAnytime.AutoSize = true;
            this.rbAnytime.Location = new System.Drawing.Point(6, 34);
            this.rbAnytime.Name = "rbAnytime";
            this.rbAnytime.Size = new System.Drawing.Size(62, 17);
            this.rbAnytime.TabIndex = 1;
            this.rbAnytime.TabStop = true;
            this.rbAnytime.Text = "Anytime";
            this.rbAnytime.UseVisualStyleBackColor = true;
            this.rbAnytime.CheckedChanged += new System.EventHandler(this.rbAnytime_CheckedChanged);
            // 
            // rbAnnually
            // 
            this.rbAnnually.AutoSize = true;
            this.rbAnnually.Location = new System.Drawing.Point(6, 10);
            this.rbAnnually.Name = "rbAnnually";
            this.rbAnnually.Size = new System.Drawing.Size(65, 17);
            this.rbAnnually.TabIndex = 0;
            this.rbAnnually.TabStop = true;
            this.rbAnnually.Text = "Annually";
            this.rbAnnually.UseVisualStyleBackColor = true;
            this.rbAnnually.CheckedChanged += new System.EventHandler(this.rbAnnually_CheckedChanged);
            // 
            // gbMonthlyAmounts
            // 
            this.gbMonthlyAmounts.Controls.Add(this.btnClear);
            this.gbMonthlyAmounts.Controls.Add(this.btnSave);
            this.gbMonthlyAmounts.Controls.Add(this.btnFill);
            this.gbMonthlyAmounts.Controls.Add(this.tbFill);
            this.gbMonthlyAmounts.Controls.Add(this.label19);
            this.gbMonthlyAmounts.Controls.Add(this.label12);
            this.gbMonthlyAmounts.Controls.Add(this.label11);
            this.gbMonthlyAmounts.Controls.Add(this.tbDec);
            this.gbMonthlyAmounts.Controls.Add(this.tbNov);
            this.gbMonthlyAmounts.Controls.Add(this.tbOct);
            this.gbMonthlyAmounts.Controls.Add(this.label10);
            this.gbMonthlyAmounts.Controls.Add(this.tbSep);
            this.gbMonthlyAmounts.Controls.Add(this.label9);
            this.gbMonthlyAmounts.Controls.Add(this.tbAug);
            this.gbMonthlyAmounts.Controls.Add(this.label8);
            this.gbMonthlyAmounts.Controls.Add(this.tbJul);
            this.gbMonthlyAmounts.Controls.Add(this.label7);
            this.gbMonthlyAmounts.Controls.Add(this.tbJun);
            this.gbMonthlyAmounts.Controls.Add(this.label6);
            this.gbMonthlyAmounts.Controls.Add(this.tbMay);
            this.gbMonthlyAmounts.Controls.Add(this.label5);
            this.gbMonthlyAmounts.Controls.Add(this.tbApr);
            this.gbMonthlyAmounts.Controls.Add(this.label4);
            this.gbMonthlyAmounts.Controls.Add(this.tbMar);
            this.gbMonthlyAmounts.Controls.Add(this.label3);
            this.gbMonthlyAmounts.Controls.Add(this.tbFeb);
            this.gbMonthlyAmounts.Controls.Add(this.label2);
            this.gbMonthlyAmounts.Controls.Add(this.tbJan);
            this.gbMonthlyAmounts.Controls.Add(this.label1);
            this.gbMonthlyAmounts.Enabled = false;
            this.gbMonthlyAmounts.Location = new System.Drawing.Point(7, 177);
            this.gbMonthlyAmounts.Name = "gbMonthlyAmounts";
            this.gbMonthlyAmounts.Size = new System.Drawing.Size(172, 367);
            this.gbMonthlyAmounts.TabIndex = 1;
            this.gbMonthlyAmounts.TabStop = false;
            this.gbMonthlyAmounts.Text = "Monthly TransactionAmounts";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(93, 290);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 23);
            this.btnClear.TabIndex = 28;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(54, 23);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(94, 338);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(55, 23);
            this.btnFill.TabIndex = 26;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // tbFill
            // 
            this.tbFill.Location = new System.Drawing.Point(6, 341);
            this.tbFill.Name = "tbFill";
            this.tbFill.Size = new System.Drawing.Size(55, 20);
            this.tbFill.TabIndex = 25;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 325);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(22, 13);
            this.label19.TabIndex = 24;
            this.label19.Text = "Fill:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(96, 236);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Dec:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(96, 193);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Nov:";
            // 
            // tbDec
            // 
            this.tbDec.Location = new System.Drawing.Point(94, 255);
            this.tbDec.Name = "tbDec";
            this.tbDec.Size = new System.Drawing.Size(55, 20);
            this.tbDec.TabIndex = 23;
            // 
            // tbNov
            // 
            this.tbNov.Location = new System.Drawing.Point(94, 211);
            this.tbNov.Name = "tbNov";
            this.tbNov.Size = new System.Drawing.Size(55, 20);
            this.tbNov.TabIndex = 21;
            // 
            // tbOct
            // 
            this.tbOct.Location = new System.Drawing.Point(94, 167);
            this.tbOct.Name = "tbOct";
            this.tbOct.Size = new System.Drawing.Size(55, 20);
            this.tbOct.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(96, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Oct:";
            // 
            // tbSep
            // 
            this.tbSep.Location = new System.Drawing.Point(94, 123);
            this.tbSep.Name = "tbSep";
            this.tbSep.Size = new System.Drawing.Size(55, 20);
            this.tbSep.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(96, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Sep:";
            // 
            // tbAug
            // 
            this.tbAug.Location = new System.Drawing.Point(94, 79);
            this.tbAug.Name = "tbAug";
            this.tbAug.Size = new System.Drawing.Size(55, 20);
            this.tbAug.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(96, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Aug:";
            // 
            // tbJul
            // 
            this.tbJul.Location = new System.Drawing.Point(94, 36);
            this.tbJul.Name = "tbJul";
            this.tbJul.Size = new System.Drawing.Size(55, 20);
            this.tbJul.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(96, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Jul:";
            // 
            // tbJun
            // 
            this.tbJun.Location = new System.Drawing.Point(6, 255);
            this.tbJun.Name = "tbJun";
            this.tbJun.Size = new System.Drawing.Size(55, 20);
            this.tbJun.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Jun:";
            // 
            // tbMay
            // 
            this.tbMay.Location = new System.Drawing.Point(6, 211);
            this.tbMay.Name = "tbMay";
            this.tbMay.Size = new System.Drawing.Size(55, 20);
            this.tbMay.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "May:";
            // 
            // tbApr
            // 
            this.tbApr.Location = new System.Drawing.Point(6, 167);
            this.tbApr.Name = "tbApr";
            this.tbApr.Size = new System.Drawing.Size(55, 20);
            this.tbApr.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Arp:";
            // 
            // tbMar
            // 
            this.tbMar.Location = new System.Drawing.Point(6, 123);
            this.tbMar.Name = "tbMar";
            this.tbMar.Size = new System.Drawing.Size(55, 20);
            this.tbMar.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mar:";
            // 
            // tbFeb
            // 
            this.tbFeb.Location = new System.Drawing.Point(6, 79);
            this.tbFeb.Name = "tbFeb";
            this.tbFeb.Size = new System.Drawing.Size(55, 20);
            this.tbFeb.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Feb";
            // 
            // tbJan
            // 
            this.tbJan.Location = new System.Drawing.Point(6, 36);
            this.tbJan.Name = "tbJan";
            this.tbJan.Size = new System.Drawing.Size(55, 20);
            this.tbJan.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Jan:";
            // 
            // cmsDelete
            // 
            this.cmsDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteCategory});
            this.cmsDelete.Name = "contextMenuStrip1";
            this.cmsDelete.Size = new System.Drawing.Size(159, 48);
            // 
            // tsmiDeleteCategory
            // 
            this.tsmiDeleteCategory.Enabled = false;
            this.tsmiDeleteCategory.Name = "tsmiDeleteCategory";
            this.tsmiDeleteCategory.Size = new System.Drawing.Size(158, 22);
            this.tsmiDeleteCategory.Text = "Delete Category";
            this.tsmiDeleteCategory.Click += new System.EventHandler(this.tsmiDeleteCategory_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWeekly);
            this.groupBox1.Controls.Add(this.rbOneTime);
            this.groupBox1.Controls.Add(this.rbAnnually);
            this.groupBox1.Controls.Add(this.rbMonthly);
            this.groupBox1.Controls.Add(this.rbAnytime);
            this.groupBox1.Controls.Add(this.rbBiWeekly);
            this.groupBox1.Location = new System.Drawing.Point(5, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbSpending);
            this.groupBox2.Controls.Add(this.rbIncome);
            this.groupBox2.Location = new System.Drawing.Point(5, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 47);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // FrmCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 624);
            this.Controls.Add(this.scMain);
            this.Name = "FrmCategories";
            this.Text = "frmCategories";
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.gbProperties.ResumeLayout(false);
            this.gbMonthlyAmounts.ResumeLayout(false);
            this.gbMonthlyAmounts.PerformLayout();
            this.cmsDelete.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.GroupBox gbMonthlyAmounts;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbDec;
        private System.Windows.Forms.TextBox tbNov;
        private System.Windows.Forms.TextBox tbOct;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbSep;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbAug;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbJul;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbJun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbMay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbApr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFeb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbJan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.TextBox tbFill;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox gbProperties;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ContextMenuStrip cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCategory;
        private System.Windows.Forms.RadioButton rbSpending;
        private System.Windows.Forms.RadioButton rbIncome;
        private System.Windows.Forms.RadioButton rbWeekly;
        private System.Windows.Forms.RadioButton rbOneTime;
        private System.Windows.Forms.RadioButton rbMonthly;
        private System.Windows.Forms.RadioButton rbBiWeekly;
        private System.Windows.Forms.RadioButton rbAnytime;
        private System.Windows.Forms.RadioButton rbAnnually;
        private System.Windows.Forms.CheckBox chkByDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}