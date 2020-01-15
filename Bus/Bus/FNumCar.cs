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
    public partial class FNumCar : Form
    {
        Cloaddb loaddb = new Cloaddb();
        FAddMember Fam = new FAddMember();

        public FNumCar()
        {
            InitializeComponent();
        }

        public bool checkmk(MaskedTextBox checkmk)
        {
            if (checkmk.Text.Replace("-","") == "" || checkmk.Text.Replace("-","").Length < 6)
                return true;

            if (checkmk.Text.Replace("-","").ToCharArray().All(c => char.IsNumber(c)) == false)
                return true;

            else
                return false;
        }

        public bool checktxt(TextBox checktxt)
        {
            if (checktxt.Text.Equals(""))
                return true;

            else
                return false;
        }

        public bool checknumc(MaskedTextBox mkcheck, char charcheck)
        {
            if (((int)charcheck >= 48 && (int)charcheck <= 57) || (int)charcheck == 8 || (int)charcheck == 13)
            {
                return false;
            }
            else
            {
                MessageBox.Show("สามารถใส่ได้แค่ตัวเลข !!!", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
        }

        public void offset()
        {
            mkNumCar.Enabled = true;
            txtNCCom.Enabled = true;
            updateToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = false;
        }

        public void cleartxt()
        {
            mkNumCar.Text = "";
            txtNCCom.Text = "";
        }

        public void savedb(string insertstr)
        {
            OleDbCommand cmd = new OleDbCommand();

            try
            {


                cmd.CommandText = insertstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = loaddb.cnn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            loaddb.cnn.Close();
            loaddb.cnn.Dispose();
        }

        public void readNCdata()
        {
            ivNCData.Clear();
            ivNCData.Items.Clear();
            ivNCData.Columns.Add("หมายเลขข้างรถ", 150, HorizontalAlignment.Left);
            ivNCData.Columns.Add("บริษัท", 150, HorizontalAlignment.Left);
            ivNCData.View = View.Details;

            try
            {
                loaddb.checkdb();
                string readNCar = "SELECT * FROM DBNCar";
                OleDbCommand rcmd = new OleDbCommand(readNCar, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(rcmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBNCar");
                loaddb.cnn.Close();
                dt = ds.Tables["DBNCar"];

                for (int i = 0; i < dt.Rows.Count - 0; i++)
                {
                    ivNCData.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    ivNCData.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkmk(mkNumCar))
            {
                MessageBox.Show("กรุณากรอกหมายเลขข้างรถให้ครบ", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checktxt(txtNCCom))
            {
                MessageBox.Show("กรุณากรอกชื่อบริษัท", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("คุณแน่ใจแล้วใช้ไหมที่จะเพิ่มข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        loaddb.checkdb();
                        string insertstr = "INSERT INTO DBNCar (NCar,NCom) VALUES ('" + mkNumCar.Text + "','" + txtNCCom.Text + "')";
                        savedb(insertstr);
                        readNCdata();
                        Fam.loadnc();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }

                }
            }

            cleartxt();
        }

        private void newNumberCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mkNumCar.Enabled = true;
            txtNCCom.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            clearToolStripMenuItem.Enabled = true;
            updateToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจแล้วที่จะแก้ไข้ข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    loaddb.checkdb();
                    string strup = "UPDATE DBNCar SET NCar='" + mkNumCar.Text + "',NCom='" + txtNCCom.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(strup, loaddb.cnn);
                    cmd.ExecuteNonQuery();
                    loaddb.cnn.Close();
                    loaddb.cnn.Dispose();
                    readNCdata();
                    MessageBox.Show("แก้ไข้ข้อมูลสำเร็จ", "แก้ไข้ข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            else
            {
            }

            cleartxt();
            updateToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจแล้วใช้ไหมที่จะลบข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    loaddb.checkdb();
                    string strdel = "DELETE FROM DBNCar where NCar= '" + mkNumCar.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(strdel, loaddb.cnn);
                    cmd.ExecuteNonQuery();
                    loaddb.cnn.Close();
                    loaddb.cnn.Dispose();
                    readNCdata();
                    Fam.loadnc();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("ยกเลิก", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cleartxt();
            updateToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
        }

        private void mkNumCar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknumc(mkNumCar, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void mkNumCar_Click(object sender, EventArgs e)
        {
            mkNumCar.SelectAll();
        }

        private void ivNCData_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ivNCData.Items.Count;i++)
            {
                if (ivNCData.Items[i].Selected == true)
                {
                    try
                    {
                        mkNumCar.Text = ivNCData.Items[i].SubItems[0].Text;
                        txtNCCom.Text = ivNCData.Items[i].SubItems[1].Text;
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }
            }
            ivNCData.Focus();
            ivNCData.FullRowSelect = true;
            offset();
        }

        private void FNumCar_Load(object sender, EventArgs e)
        {

            try
            {
                this.Icon = Properties.Resources.icon_Bus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
            
            readNCdata();
            saveToolStripMenuItem.Enabled = false;
            clearToolStripMenuItem.Enabled = false;
            updateToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
            menuStrip1.Items.Insert(1, new ToolStripSeparator());
            menuStrip1.Items.Insert(4, new ToolStripSeparator());
            mkNumCar.Enabled = false;
            txtNCCom.Enabled = false;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mkNumCar.Text = "";
            txtNCCom.Text = "";
        }


    }
}
