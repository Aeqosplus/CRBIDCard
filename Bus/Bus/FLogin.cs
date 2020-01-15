using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace Bus
{
    public partial class FLogin : Form
    {

        Cloaddb loaddb = new Cloaddb();
        public string lname;
        OleDbConnection con = new OleDbConnection();

        string strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data SOURCE= BusDB.accdb";

        public FLogin()
        {
            InitializeComponent();
        }

        public void checkcon()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = strcon;
                    con.Open();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Connected database fail!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            checkid();
        }

        private void checkid()
        {
            if (txtUser.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Username or Password Incorrect", "พบข้อผิดพลาด");
                return;
            }

            try
            {
                //checkcon();
                loaddb.checkdb();
                int inr = 0;
                string strse = "SELECT COUNT(*) FROM DBMember WHERE IDB= '" + txtUser.Text + "' AND [PWB]= '" + txtPass.Text + "'";
                OleDbCommand com = new OleDbCommand(strse, loaddb.cnn);
                inr = Convert.ToInt32(com.ExecuteScalar());
                if (inr > 0)
                {
                    Program.flag = true;
                    Program.PID = this.txtUser.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password Incorrect", "พบข้อผิดพลาด");
                    txtUser.Text = "";
                    txtPass.Text = "";
                    return;
                }
                loaddb.cnn.Dispose();
                loaddb.cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        private void FLogin_Load(object sender, EventArgs e)
        {     
            try
            {
                this.Icon = Properties.Resources.icon_Bus;
                pictureBox1.Image = Properties.Resources.logo_color;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkid();
            }
        }

    }
}
