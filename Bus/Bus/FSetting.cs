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
    public partial class FSetting : Form
    {

        FMain fm = new FMain();
        Cloaddb loaddb = new Cloaddb();
        CRound loadcr = new CRound();

        public FSetting()
        {
            InitializeComponent();
        }

        public void loadsetting()
        {

            try
            {
                loaddb.checkdb();
                string strs = "Select * from DBSetting";
                OleDbCommand cmds = new OleDbCommand(strs, loaddb.cnn);
                OleDbDataReader drs = cmds.ExecuteReader();

                while (drs.Read())
                {
                    txtSround.Text = drs["SRound"].ToString();
                    txtSMTic.Text = drs["STMoney"].ToString();
                    cbbMTic.Text  = drs["STicket"].ToString() + " %";
                    txtStime.Text = drs["STdeley"].ToString();
                }
                drs.Close();
                loaddb.cnn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        public void reSet()
        {
            txtSMTic.Clear();
            txtSround.Clear();
            txtStime.Clear();

            loadsetting();

        }

        public bool checktxt(TextBox txtc)
        {
            if (txtc.Text.Equals("") || txtc.Text.Equals("0"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSMTic.Clear();
            txtSround.Clear();
            txtStime.Clear();
        }

        //check number

        public bool checknum(TextBox txtc,char currentc)
        {
            if (((int)currentc >= 48 && (int)currentc <= 57) || (int)currentc == 8 || (int)currentc == 13)
            {
                return false;
            }

            else
            {
                MessageBox.Show("สามารถใส่ได้แค่ตัวเลข !!!", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
        }
        private void txtStic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknum(txtSMTic, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtSround_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknum(txtSround, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtStime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknum(txtStime, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        //check number

        private void FSetting_Load(object sender, EventArgs e)
        {

            try
            {
                this.Icon = Properties.Resources.icon_Bus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
            
            for (int i = 1; i <= 100; i++)
            {
                cbbMTic.Items.Add(i + " %");
            }
            loadsetting();
        }

        private void txtStic_Click(object sender, EventArgs e)
        {
            txtSMTic.SelectAll();
        }

        private void txtSround_Click(object sender, EventArgs e)
        {
            txtSround.SelectAll();
        }

        private void txtStime_Click(object sender, EventArgs e)
        {
            txtStime.SelectAll();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checktxt(txtSMTic) || checktxt(txtSround) || checktxt(txtStime))
            {
                MessageBox.Show("กรุณาใส่ข้อมูลให้ครบ", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("คุณแน่ใจใช้ไหมที่จะแก้ไข่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        loaddb.checkdb();

                        string upset = "Update DBSetting Set SRound='" + txtSround.Text + "',STMoney = '" + txtSMTic.Text + "',STicket='" + cbbMTic.Text.Replace(" %", "") + "',STdeley='" + txtStime.Text + "'";
                        OleDbCommand cmd = new OleDbCommand(upset, loaddb.cnn);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("แก้ไข้สำเร็จ", "แก้ไข้", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddb.cnn.Close();
                        reSet();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
                    }
                }
                else
                {
                    MessageBox.Show("Cancel", "คำเตือน");
                    return;
                }
            }
        }

    }
}
