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
using CRDNID;

namespace Bus
{
    public partial class FMain : Form
    {

        //string MIDCard, MName, MLName, Mnc, MPhone;
        //string RIDCard, Rround, RMoney;
        int s;

        string RID = "-";
        string CID = null;

        CRound loadcr = new CRound();
        Cloaddb loaddb = new Cloaddb();
        CRDNID.RDNID CRID = new CRDNID.RDNID();
        string strname;
        string sMName;
        int sdelay;

        //Checkdata
        public string cIRRD, cIDCard, cRTime, cRName, cRLName, cRPhone, cRPST, cRNCar, cRound, cRTicket, RMoney, RCheck;

        //Setting
        public string sTdelay;

        public string _sTdelay
        {
            get { return sTdelay; }
            set { value = sTdelay; }
        }

        //loaddata
        string loadr = "SELECT * FROM DBRunRound ";

        public FMain()
        {
            InitializeComponent();

            this.Activate();
        }

        //RDNID

        private void ListCardReader()
        {
            String[] readlist = RDNID.getReaderListRD();
            if (readlist != null)
            {
                for (int i = 0; i < readlist.Length; i++)
                    toolStripMenuItem1.Text = (readlist[i]);
            }
        }

        enum NID_FIELD
        {
            NID_CODE,//366080001xxxx#  
            END


        };

        void BindDataToScreen()
        {
            try
            {
                String ID = CRID.getNIDNumber();
                String Data = CRID.getNIDData();
                String version = CRID.getSoftwareInfoRD();

                string[] fields = Data.Split('#');

                
                RID = fields[(int)NID_FIELD.NID_CODE];
                CID = RID;

                loadcr._cIDCard = RID;
                loadcr._cTime = lblDate.Text.Replace("วันที่ : ", "");
                loadcr.loaddsRound();
                loadcr._cIDCard = null;
                loadcr._cTime = null;
                loaddata();
                if (loadcr._rmess == true)
                {
                    MessageBox.Show("รอบวิ่งของคุณ " + loadcr.rName + " วันที่ " + loadcr.rTime + " ได้เพิ่มขึ้นเป็น " + loadcr.rRound + " รอบ", "ข้อความ");
                    //loadcr._rmess = false;
                }
                //return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        //-1 : cannot read citizen card
        // 1 : read sucessfull 
        protected int ReadCitizenCard()
        {
            String strTerminal = toolStripMenuItem1.Text;

            IntPtr obj = CRID.selectReader(strTerminal);

            Int32 nInsertCard = CRID.isCardInsert();
            if (nInsertCard != 0)
            {
                //String m;
                //m = String.Format("ไม่ได้เสียบบัตร", nInsertCard);
                //MessageBox.Show(m);

                CRID.deselectReader();
                CID = null;
                loadcr._checkdata = true;
                return -1;
            }
            else
            {
                if (RID != CID)
                {
                    if (loadcr._checkdata == true)
                    {
                        BindDataToScreen();
                    }
                    return 0;
                }
            }
            return 0;
        }

        //RDNID

        public string _strname
        {
            get { return strname; }
            set { strname = value; }
        }

        public void loadname()
        {
            try
            {
                loaddb.checkdb();
                string loadn = "SELECT * FROM DBMember WHERE IDB = '" + strname + "'";
                OleDbCommand cmd = new OleDbCommand(loadn ,loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBMember");
                loaddb.cnn.Close();
                dt = ds.Tables["DBMember"];
                //da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count - 0; i++)
                {
                    sMName = dt.Rows[i]["MName"].ToString();
                }

                //if (dt.Rows.Count > 0)
                //{
                //    this.lblMName.Text = dt.Rows[0]["MName"].ToString();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            string lname = "ผู้ใช้งาน : " + sMName;
            lblMName.Text = lname;
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadcr._read = false;
            FSetting fset = new FSetting();
            fset.MdiParent = this.MdiParent;
            fset.ShowDialog();
            checksetting();
            Timedeley();
            loadcr._read = true;
        }

        public void loadmember()
        {
            try
            {
                loaddb.checkdb();
                string DBMember = "SELECT IDCard,MName,MLname,Phone,NCar FROM DBMember";
                OleDbCommand cmd = new OleDbCommand(DBMember ,loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBMember");
                loaddb.cnn.Close();
                dt = ds.Tables["DBMember"];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public void loaddata()
        {
            ivMain.Clear();
            ivMain.Items.Clear();
            ivMain.Columns.Add("รายการที่", 90, HorizontalAlignment.Left);
            ivMain.Columns.Add("วันที่", 90, HorizontalAlignment.Left);
            ivMain.Columns.Add("รหัสบัตรประชาชน", 120, HorizontalAlignment.Left);
            ivMain.Columns.Add("ชื่อ", 150, HorizontalAlignment.Left);
            ivMain.Columns.Add("นามสกุล", 150, HorizontalAlignment.Left);
            ivMain.Columns.Add("เบอร์โทรศัพท์", 90, HorizontalAlignment.Left);
            ivMain.Columns.Add("ตำแหน่ง", 90, HorizontalAlignment.Left);
            ivMain.Columns.Add("หมายเลขข้างรถ", 90, HorizontalAlignment.Left);
            ivMain.Columns.Add("จำนวนรอบวิ่ง", 90, HorizontalAlignment.Left);
            ivMain.Columns.Add("จำนวนตั๋ว", 150, HorizontalAlignment.Left);
            ivMain.Columns.Add("จำนวนเงิน", 90, HorizontalAlignment.Left);
            ivMain.Columns.Add("ใบรับเงิน", 150, HorizontalAlignment.Left);
            ivMain.View = View.Details;

            try
            {
                loaddb.checkdb();
                OleDbCommand cmd = new OleDbCommand(loadr, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBRunRound");
                loaddb.cnn.Close();
                dt = ds.Tables["DBRunRound"];
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ivMain.Items.Add(dt.Rows[i]["IDRR"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RTime"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["IDCard"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RName"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RLName"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RPhone"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RPST"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RNCar"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["Round"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RTicket"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RMoney"].ToString());
                    ivMain.Items[i].SubItems.Add(dt.Rows[i]["RCheck"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        private void cbbSear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSear.Text == "Date")
            {
                dtpMD.Visible = true;
                txtName.Visible = false;
                CbbCheck.Visible = false;
                mkIDCard.Visible = false;
                btSearch.Visible = true;
            }
            else if (cbbSear.Text == "ใบรับเงิน")
            {
                CbbCheck.Visible = true;
                dtpMD.Visible = false;
                txtName.Visible = false;
                mkIDCard.Visible = false;
                btSearch.Visible = true;
            }
            else if (cbbSear.Text == "IDCard")
            {
                mkIDCard.Visible = true;
                txtName.Visible = false;
                dtpMD.Visible = false;
                CbbCheck.Visible = false;
                btSearch.Visible = true;
            }
            else if (cbbSear.Text == "Name")
            {
                txtName.Visible = true;
                dtpMD.Visible = false;
                CbbCheck.Visible = false;
                mkIDCard.Visible = false;
                btSearch.Visible = true;
            }

            forselect();
        }

        public void forselect()
        {
            if (cbbSear.Text == "Date")
                s = 1;
            else if (cbbSear.Text == "IDCard")
                s = 2;
            else if (cbbSear.Text == "Name")
                s = 3;
            else if (cbbSear.Text == "ใบรับเงิน")
                s = 4;
        }

        private void FMain_Load(object sender, EventArgs e)
        {

            loadcr._checkdata = true;
            loadcr._read = true;

            this.StartPosition = FormStartPosition.CenterScreen;

            menuStrip1.Items.Insert(2, new ToolStripSeparator());
            try
            {
                this.Icon = Properties.Resources.icon_Bus;
                this.Size = Properties.Settings.Default.MSize;
                this.Location = Properties.Settings.Default.MLCT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }

            checksetting();
            Timedeley();

            s = 5;
            switchch();
            _strname = Program.PID;
            loadname();


        }

        private void checksetting()
        {
            try
            {
                loaddb.checkdb();

                loaddb.checkdb();
                string strs = "Select * from DBSetting";
                OleDbCommand cmds = new OleDbCommand(strs, loaddb.cnn);
                OleDbDataReader drs = cmds.ExecuteReader();

                while (drs.Read())
                {
                    sTdelay = drs["STdeley"].ToString();
                }
                drs.Close();
                loaddb.cnn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"พบข้อผิดพลาด");
            }
        }

        private void Timedeley()
        {
            sdelay = Convert.ToInt32(sTdelay) * 1000;
            tmrRID.Interval = sdelay;
            tmrReadID.Interval = sdelay;
        }

        public void switchch()
        {
            switch (s)
            {
                case 1:
                    loadr = "SELECT * FROM DBRunRound WHERE RTime='" + dtpMD.Text + "'";
                    loaddata();
                    break;

                case 2:
                    if (checkID(mkIDCard))
                    {
                        MessageBox.Show("กรุณากรอกรหัสบัตรประชาชนให้ครบ", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        loadr = "SELECT * FROM DBRunRound WHERE IDCard='" + mkIDCard.Text.Replace("-", "") + "'";
                        loaddata();
                    }
                    break; 

                case 3:
                    loadr = "SELECT * FROM DBRunRound WHERE RName='" + txtName.Text + "'";
                    loaddata();
                    break;

                case 4:
                    loadr = "SELECT * FROM DBRunRound WHERE RCheck='" + CbbCheck.Text + "'";
                    loaddata();
                    break;

                case 5:
                    loadr = "SELECT * FROM DBRunRound ";
                    loaddata();
                    break;

            }
        }

        private void tmrDate_Tick(object sender, EventArgs e)
        {
            DateTime sDate = DateTime.Now;
            lblDate.Text = "วันที่ : " + sDate.ToShortDateString();
        }

        private void FMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            tmrDate.Enabled = false;
            tmrReadID.Enabled = false;
            tmrRID.Enabled = false;

            Properties.Settings.Default.MSize = this.Size;
            Properties.Settings.Default.MLCT = this.Location;
            Properties.Settings.Default.Save();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            switchch();
        }

        private void ivMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ivMain.Items.Count; i++)
            {
                if (ivMain.Items[i].Selected == true)
                {
                    try
                    {
                        //cIRRD, cIDCard, cRTime, cRName, cRLName, cRPhone, cRPST, cRNCar, cRound, cRTicket, RMoney, RCheck
                        cIRRD = ivMain.Items[i].SubItems[0].Text;
                        cRTime = ivMain.Items[i].SubItems[1].Text;
                        cIDCard = ivMain.Items[i].SubItems[2].Text;
                        cRName = ivMain.Items[i].SubItems[3].Text;
                        cRLName = ivMain.Items[i].SubItems[4].Text;
                        cRPhone = ivMain.Items[i].SubItems[5].Text;
                        cRPST = ivMain.Items[i].SubItems[6].Text;
                        cRNCar = ivMain.Items[i].SubItems[7].Text;
                        cRound = ivMain.Items[i].SubItems[8].Text;
                        cRTicket = ivMain.Items[i].SubItems[9].Text;
                        RMoney = ivMain.Items[i].SubItems[10].Text;
                        RCheck = ivMain.Items[i].SubItems[11].Text;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //txtMCP.Clear();
                }
            }
            ivMain.Focus();
            ivMain.FullRowSelect = true;

            if (ivMain.FullRowSelect == true)
            {
                btcRead.Enabled = true;
            }
            else
            {
                btcRead.Enabled = false;
            }
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadcr._read = false;
            FTicket FTC = new FTicket();
            FTC._cIDCard = cIDCard;
            FTC._cRTime = cRTime;
            FTC.ShowDialog(this);
            loaddata();
            loadcr._read = true;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadr = "SELECT * FROM DBRunRound ";
            loaddata();
        }

        private void tmrReadID_Tick(object sender, EventArgs e)
        {
            if (loadcr._read == true)
            {
                if (toolStripMenuItem1.Text != "Disconnect")
                {
                    ReadCitizenCard();
                }
            }
        }

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadcr._read = false;
            FAddMember fadd = new FAddMember();
            fadd.ShowDialog();
            loadcr._read = true;
        }

        private void numberCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadcr._read = false;
            FNumCar fnc = new FNumCar();
            fnc.ShowDialog();
            loadcr._read = true;
        }

        private void editRoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadcr._read = false;
            FRound fr = new FRound();
            fr.ShowDialog(this);
            loaddata();
            loadcr._read = true;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void Print()
        {
            if (RCheck == "ออกแล้ว")
            {
                if (MessageBox.Show("ข้อมูลนี้ได้ปริ้นออกไปแล้ว คุณต้องการแก้ไข้เพื่อให้สามารถปริ้นได้อีกครั้งหรือไม่", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    loadcr._read = false;
                    FRound fr = new FRound();
                    fr._cRound = true;
                    fr._cIDCard = cIDCard;
                    fr._cRTime = cRTime;
                    fr.ShowDialog();
                    loaddata();
                    loadcr._read = true;
                }
                return;
            }
            else
            {
                if (MessageBox.Show("คุณแน่ใจแล้วที่ปริ้นข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        loadcr._read = false;
                        FPrint fp = new FPrint();
                        fp.rpDate = lblDate.Text.Replace("วันที่ : ", "");
                        fp.rpIDRR = cIRRD;
                        fp.rpIDCard = cIDCard;
                        fp.rpName = cRName;
                        fp.rpLName = cRLName;
                        fp.rpPST = cRPST;
                        fp.rpNCar = cRNCar;
                        fp.rpTime = cRTime;
                        fp.rpRound = cRound;
                        fp.rpTicket = cRTicket;
                        fp.rpMoney = RMoney;
                        fp.ShowDialog(this);

                        string print = "ออกแล้ว";

                        loaddb.checkdb();
                        string upp = "UPDATE DBRunRound SET RCheck='" + print + "' WHERE IDCard='" + cIDCard + "' AND [RTime] ='" + cRTime + "'";
                        OleDbCommand cmd = new OleDbCommand(upp, loaddb.cnn);
                        cmd.ExecuteNonQuery();
                        loaddb.cnn.Dispose();
                        loaddb.cnn.Close();
                        loaddata();
                        loadcr._read = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ตรวจพบข้อผิดพลาด");
                    }
                }
            }
        }

        public bool checkID(MaskedTextBox mskcheck)
        {
            if (mskcheck.Text.Replace("-", "") == "" || mskcheck.Text.Replace("-", "").Length < 13)
                return true;

            if (mskcheck.Text.Replace("-", "").ToCharArray().All(c => char.IsNumber(c)) == false)
                return true;

            else
                return false;
        }

        public bool checknummk(MaskedTextBox txtcheck, char currentc)
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

        public bool checknonum(TextBox txtcheck, char currentc)
        {
            //thank BlacklistModz-SK thaicreate.com
            //thank tee thaicreate.com
            //char currentc = new char();

            if ((int)currentc >= 44 && (int)currentc <= 57)
            {
                MessageBox.Show("ไม่สามารถใส่ตัวเลขได้ !!!", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (((int)currentc >= 48 && (int)currentc <= 122) || (int)currentc >= 161 || (int)currentc == 8 || (int)currentc == 13 || (int)currentc == 46 || (int)currentc == 32)
            {
                return false;
            }

            return true;
        }

        private void mkIDCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknummk(mkIDCard, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void mkIDCard_Click(object sender, EventArgs e)
        {
            mkIDCard.SelectAll();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknonum(txtName, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            txtName.SelectAll();
        }

        private void tmrRID_Tick(object sender, EventArgs e)
        {
            if (loadcr._read == true)
            {
                string fileName = System.Reflection.Assembly.GetEntryAssembly().Location.ToLower();
                fileName = fileName.Replace("\\bus.exe", "") + "\\RDNIDLib.dlx";

                int nres = RDNID.OpenNIDLib(fileName);
                if (nres != 0)
                {
                    toolStripMenuItem1.Text = "Disconnect";
                    return;
                }
                ListCardReader();
            }
        }

        private void mkcIDCard_Click(object sender, EventArgs e)
        {
            mkcIDCard.SelectAll();
        }

        private void btcRead_Click(object sender, EventArgs e)
        {
            loadcr._cIDCard = mkcIDCard.Text.Replace("-", "");
            loadcr._cTime = lblDate.Text.Replace("วันที่ : ", "");

            loadcr.loaddsRound();
            loadcr._cIDCard = null;
            loadcr._cTime = null;
            loaddata();
            if (loadcr._rmess == true)
            {
                MessageBox.Show("รอบวิ่งของคุณ " + loadcr.rName + " วันที่ " + loadcr.rTime + " ได้เพิ่มขึ้นเป็น " + loadcr.rRound + " รอบ", "ข้อความ");
                //loadcr._rmess = false;
            }

            mkcIDCard.Text = "";
        }

        private void mkcIDCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknummk(mkcIDCard, e.KeyChar))
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
