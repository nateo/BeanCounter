using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BeanCounter
{
    public partial class FrmSetPassword : Form
    {
        public FrmSetPassword()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void FrmSetPassword_Load(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BeanCounterDB"].ToString();
            connectionString +=  @";Database Password = 'test'";
            //connectionString += @";Mode=Share Exclusive";
            using (OleDbConnection myConnection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                myConnection.Open();
            }
        }


    }
}
