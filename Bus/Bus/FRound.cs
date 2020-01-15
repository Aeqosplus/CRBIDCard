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
    public partial class FRound : Form
    {

        Cloaddb loaddb = new Cloaddb();

        //Setting
        public string sRound { set; get; }
        public string sTicket { set; get; }
        public string sTMoney { set; get; }
        //Round
        public string rMoney { set; get; }
        double cr, ct, cm;

        string cIDCard, cRTime;

        bool cRound;

        public bool _cRound
        {
            get { return cRound; }
            set { cRound = value; }
        }

        public string _cIDCard
        {
            get { return cIDCard; }
            set { cIDCard = value; }
        }

        public string _cRTime
        {
            get { return cRTime; }
            set { cRTime = value; }
        }

        string rRIDRR, rRDate, rRIDCard, rRName, rRLName, rRPhone, rRPo, rRNCar, rRound, rRTicket, rRMoney, rRCheck;

        public FRound()
        {
            InitializeComponent();
        }

        private void FRound_Load(object sender, EventArgs e)
        {

            try
            {
                this.Icon = Properties.Resources.icon_Bus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
            
            loadiview();

            if (cRound == true)
            {
                CPrint();
                cRound = false;
            }
        }

        private void CPrint()
        {
            try
            {
                loaddb.checkdb();
                string sRound = "SELECT * FROM DBRunRound WHERE IDCard= '" + cIDCard + "' AND [RTime]= '" + cRTime + "'";
                OleDbCommand cmd = new OleDbCommand(sRound, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBRunRound");
                loaddb.cnn.Close();
                dt = ds.Tables["DBRunRound"];

                for (int i = 0; i < dt.Rows.Count - 0; i++)
                {

                    rRIDRR = dt.Rows[i]["IDRR"].ToString();
                    rRDate = dt.Rows[i]["RTime"].ToString();
                    rRIDCard = dt.Rows[i]["IDCard"].ToString();
                    rRName = dt.Rows[i]["RName"].ToString();
                    rRLName = dt.Rows[i]["RLName"].ToString();
                    rRPhone = dt.Rows[i]["RPhone"].ToString();
                    rRPo = dt.Rows[i]["RPST"].ToString();
                    rRNCar = dt.Rows[i]["RNCar"].ToString();
                    rRound = dt.Rows[i]["Round"].ToString();
                    rRTicket = dt.Rows[i]["RTicket"].ToString();
                    rRMoney = dt.Rows[i]["RMoney"].ToString();
                    rRCheck = dt.Rows[i]["RCheck"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }

            lblIDRR.Text = rRIDRR;
            lblDate.Text = rRDate;
            lblIDCard.Text = rRIDCard;
            lblName.Text = rRName;
            lblLName.Text = rRLName;
            lblPhone.Text = rRPhone;
            lblPo.Text = rRPo;
            lblNCar.Text = rRNCar;
            txtRound.Text = rRound;
            txtTicket.Text = rRTicket;
            lblMoney.Text = rRMoney;
            cbbCheckP.Text = rRCheck;

            trueenable();
        }

        private void loadiview()
        {
            ivRound.Clear();
            ivRound.Items.Clear();
            ivRound.Columns.Add("รายการที่", 90, HorizontalAlignment.Left);
            ivRound.Columns.Add("วันที่", 90, HorizontalAlignment.Left);
            ivRound.Columns.Add("รหัสบัตรประชาชน", 120, HorizontalAlignment.Left);
            ivRound.Columns.Add("ชื่อ", 150, HorizontalAlignment.Left);
            ivRound.Columns.Add("นามสกุล", 150, HorizontalAlignment.Left);
            ivRound.Columns.Add("เบอร์โทรศัพท์", 90, HorizontalAlignment.Left);
            ivRound.Columns.Add("ตำแหน่ง", 90, HorizontalAlignment.Left);
            ivRound.Columns.Add("หมายเลขข้างรถ", 90, HorizontalAlignment.Left);
            ivRound.Columns.Add("จำนวนรอบวิ่ง", 90, HorizontalAlignment.Left);
            ivRound.Columns.Add("จำนวนตั๋ว", 150, HorizontalAlignment.Left);
            ivRound.Columns.Add("จำนวนเงิน", 90, HorizontalAlignment.Left);
            ivRound.Columns.Add("ใบรับเงิน", 150, HorizontalAlignment.Left);

            loadround();

        }

        public void loadround()
        {
            ivRound.Items.Clear();
            try
            {
                loaddb.checkdb();
                string loadr = "SELECT * FROM DBRunRound ";
                OleDbCommand cmd = new OleDbCommand(loadr, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBRunRound");
                loaddb.cnn.Close();
                dt = ds.Tables["DBRunRound"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ivRound.Items.Add(dt.Rows[i]["IDRR"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RTime"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["IDCard"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RName"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RLName"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RPhone"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RPST"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RNCar"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["Round"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RTicket"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RMoney"].ToString());
                    ivRound.Items[i].SubItems.Add(dt.Rows[i]["RCheck"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ตรวจพบข้อผิดพลาด");
            }
        }

        private void loadsetting()
        {
            try
            {
                loaddb.checkdb();
                string strs = "Select * from DBSetting";
                OleDbCommand cmds = new OleDbCommand(strs, loaddb.cnn);
                OleDbDataReader drs = cmds.ExecuteReader();

                while (drs.Read())
                {
                    sRound = drs["SRound"].ToString();
                    sTicket = drs["STicket"].ToString();
                    sTMoney = drs["STMoney"].ToString();
                }
                drs.Close();
                loaddb.cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        private void updatedata()
        {
    
            if (MessageBox.Show("คุณแน่ใจแล้วที่จะแก้ไข้ข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    loadsetting();
                    loaddb.checkdb();


                    cr = Convert.ToDouble(txtRound.Text) * Convert.ToDouble(sRound);
                    ct = ((Convert.ToDouble(sTMoney) * Convert.ToDouble(sTicket)) / 100) * Convert.ToDouble(txtTicket.Text);
                    cm = cr + ct;

                    rMoney = cm.ToString("00.00");

                    string upstr = "UPDATE DBRunRound SET Round='" + txtRound.Text + "',RTicket='" + txtTicket.Text + "',RMoney='" + rMoney + "',RCheck='" + cbbCheckP.Text + "' Where IDCard='" + lblIDCard.Text + "' AND [RTime] ='" + lblDate.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(upstr, loaddb.cnn);
                    cmd.ExecuteNonQuery();
                    loaddb.cnn.Dispose();
                    loaddb.cnn.Close();
                    loadround();
                    MessageBox.Show("แก้ไข้ข้อมูลสำเร็จ", "แก้ไข้ข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
                }
            }
            else
            {
                MessageBox.Show("Cancel", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void deletedata()
        {
            if (MessageBox.Show("คุณแน่ใจแล้วใช้ไหมที่จะลบข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    loaddb.checkdb();

                    string delstr = "DELETE FROM DBRunRound Where IDCard='" + lblIDCard.Text + "' AND [RTime] ='" + lblDate.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(delstr , loaddb.cnn);
                    cmd.ExecuteNonQuery();
                    loaddb.cnn.Close();
                    loaddb.cnn.Dispose();
                    loadround();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
                }
            }
            else
            {
                MessageBox.Show("Cancel", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void trueenable()
        {

            updateToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;

            txtRound.Enabled = true;
            txtTicket.Enabled = true;
            cbbCheckP.Enabled = true;
        }

        private void falseenable()
        {
            updateToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;

            txtRound.Enabled = false;
            txtTicket.Enabled = false;
            cbbCheckP.Enabled = false;
        }

        private void cleartxt()
        {
            txtRound.Text = "";
            txtTicket.Text = "";
            cbbCheckP.Text = "";
        }

        private void ivRound_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ivRound.Items.Count; i++)
            {
                if (ivRound.Items[i].Selected == true)
                {
                    try
                    {
                        //cIRRD, cIDCard, cRTime, cRName, cRLName, cRPhone, cRPST, cRNCar, cRound, cRTicket, RMoney, RCheck
                        lblIDRR.Text = ivRound.Items[i].SubItems[0].Text;
                        lblDate.Text = ivRound.Items[i].SubItems[1].Text;
                        lblIDCard.Text = ivRound.Items[i].SubItems[2].Text;
                        lblName.Text = ivRound.Items[i].SubItems[3].Text;
                        lblLName.Text = ivRound.Items[i].SubItems[4].Text;
                        lblPhone.Text = ivRound.Items[i].SubItems[5].Text;
                        lblPo.Text = ivRound.Items[i].SubItems[6].Text;
                        lblNCar.Text = ivRound.Items[i].SubItems[7].Text;
                        txtRound.Text = ivRound.Items[i].SubItems[8].Text;
                        txtTicket.Text = ivRound.Items[i].SubItems[9].Text;
                        lblMoney.Text = ivRound.Items[i].SubItems[10].Text;
                        cbbCheckP.Text = ivRound.Items[i].SubItems[11].Text;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //txtMCP.Clear();
                }
            }
            ivRound.Focus();
            ivRound.FullRowSelect = true;

            trueenable();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatedata();
            cleartxt();
            falseenable();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deletedata();
            cleartxt();
            falseenable();
        }

        public bool checknum(TextBox txtcheck, char currentc)
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

        private void txtRound_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknum(txtRound, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknum(txtTicket, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
