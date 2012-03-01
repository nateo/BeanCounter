using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeanCounter.BusinessLogic;

namespace BeanCounter
{
    public partial class frmEnterPassword : Form
    {

        bool cancelClose = false;
        public frmEnterPassword()
        {
            InitializeComponent();
        }

        private void frmEnterPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!DatabaseProperties.PasswordIsCorrect(tbPassword.Text))
                cancelClose = true;
        }

        private void frmEnterPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cancelClose)
                e.Cancel = true;
        }
    }
}
