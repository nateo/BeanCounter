namespace BeanCounter
{
    partial class FrmMerchants
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
            this.gbMerchants = new System.Windows.Forms.GroupBox();
            this.lblMerchantType = new System.Windows.Forms.Label();
            this.cbMerchantType = new System.Windows.Forms.ComboBox();
            this.dgvMerchants = new System.Windows.Forms.DataGridView();
            this.cmsDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.gbMerchants.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMerchants)).BeginInit();
            this.cmsDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMerchants
            // 
            this.gbMerchants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMerchants.Controls.Add(this.lblMerchantType);
            this.gbMerchants.Controls.Add(this.cbMerchantType);
            this.gbMerchants.Controls.Add(this.dgvMerchants);
            this.gbMerchants.Location = new System.Drawing.Point(12, 12);
            this.gbMerchants.Name = "gbMerchants";
            this.gbMerchants.Size = new System.Drawing.Size(611, 448);
            this.gbMerchants.TabIndex = 0;
            this.gbMerchants.TabStop = false;
            // 
            // lblMerchantType
            // 
            this.lblMerchantType.AutoSize = true;
            this.lblMerchantType.Location = new System.Drawing.Point(194, 22);
            this.lblMerchantType.Name = "lblMerchantType";
            this.lblMerchantType.Size = new System.Drawing.Size(308, 13);
            this.lblMerchantType.TabIndex = 4;
            this.lblMerchantType.Text = " * Needs to be a exact match in order to automatically categoize";
            // 
            // cbMerchantType
            // 
            this.cbMerchantType.FormattingEnabled = true;
            this.cbMerchantType.Items.AddRange(new object[] {
            "Local Merchants *",
            "National Merchants *"});
            this.cbMerchantType.Location = new System.Drawing.Point(9, 19);
            this.cbMerchantType.Name = "cbMerchantType";
            this.cbMerchantType.Size = new System.Drawing.Size(179, 21);
            this.cbMerchantType.TabIndex = 3;
            this.cbMerchantType.Text = "National Merchants *";
            this.cbMerchantType.SelectedIndexChanged += new System.EventHandler(this.cbMerchantType_SelectedIndexChanged);
            // 
            // dgvMerchants
            // 
            this.dgvMerchants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMerchants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMerchants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMerchants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvMerchants.Location = new System.Drawing.Point(9, 55);
            this.dgvMerchants.Name = "dgvMerchants";
            this.dgvMerchants.Size = new System.Drawing.Size(596, 387);
            this.dgvMerchants.TabIndex = 2;
            this.dgvMerchants.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMerchants_CellContentClick);
            this.dgvMerchants.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMerchants_CellMouseClick);
            this.dgvMerchants.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvMerchants_EditingControlShowing);
            this.dgvMerchants.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMerchants_RowEnter);
            this.dgvMerchants.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMerchants_RowHeaderMouseClick);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDelete});
            this.cmsDelete.Name = "contextMenuStrip1";
            this.cmsDelete.Size = new System.Drawing.Size(108, 26);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(152, 22);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // FrmMerchants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 471);
            this.Controls.Add(this.gbMerchants);
            this.Name = "FrmMerchants";
            this.Text = "Merchants";
            this.Load += new System.EventHandler(this.Merchants_Load);
            this.gbMerchants.ResumeLayout(false);
            this.gbMerchants.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMerchants)).EndInit();
            this.cmsDelete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMerchants;
        private System.Windows.Forms.DataGridView dgvMerchants;
        private System.Windows.Forms.ContextMenuStrip cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.Label lblMerchantType;
        private System.Windows.Forms.ComboBox cbMerchantType;
    }
}