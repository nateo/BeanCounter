namespace BeanCounter
{
    partial class FrmWebDownload
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
            this.wbWebDownload = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbWebDownload
            // 
            this.wbWebDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbWebDownload.Location = new System.Drawing.Point(0, 0);
            this.wbWebDownload.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbWebDownload.Name = "wbWebDownload";
            this.wbWebDownload.Size = new System.Drawing.Size(503, 402);
            this.wbWebDownload.TabIndex = 0;
            // 
            // FrmWebDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 402);
            this.Controls.Add(this.wbWebDownload);
            this.Name = "FrmWebDownload";
            this.Text = "FrmWebDownload";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser wbWebDownload;
    }
}