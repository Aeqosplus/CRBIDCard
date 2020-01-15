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
    public partial class FAddMember : Form
    {
        CRDNID.RDNID CRID = new CRDNID.RDNID();
        Cloaddb loaddb = new Cloaddb();
        string Sidcard;
        int sdelay;
        public string sTdelay;

        //private BindingSource bs = new BindingSource();

        public FAddMember()
        {
            InitializeComponent();
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

        String _yyyymmdd_(String d)
        {

            OperatingSystem os = Environment.OSVersion;
            Version vs = os.Version;

            string s = "";
            string _yyyy = d.Substring(0, 4);
            string _mm = d.Substring(4, 2);
            string _dd = d.Substring(6, 2);


            string[] mm = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            string _tm = "-";
            if (_mm != "00")
                _tm = mm[int.Parse(_mm)];
            //string _ty = (int.Parse( _yyyy) + 543).ToString();

            if (_yyyy == "0000")
                _yyyy = "-";

            if (_dd == "00")
                _dd = "-";

            //s = _dd + "/" + _tm + "/" + _yyyy;

            if (os.Platform == PlatformID.Win32NT)
            {
                if (vs.Major >= 6 || vs.Minor >= 2)
                {
                    s = _tm + "/" + _dd + "/" + _yyyy;
                }
                else
                {
                    s = _dd + "/" + _tm + "/" + _yyyy;
                }
            }
            return s;
        }

        String sex_(String sex)
        {
            string x = "";
            string _se = sex.Substring(0, 1);

            string[] se = { "", "ชาย", "หญิง" };
            string _s = " ";
            if (_se != "0")
            {
                _s = se[int.Parse(_se)];
            }

            x = _s;

            return x;
        }


        enum NID_FIELD
        {
            NID_CODE,//366080001xxxx#

            TITLE_T, //นาย#
            NAME_T, //นิติ#
            MIDNAME_T,//#			//mid name
            SURNAME_T, //xxxx#

            TITLE_E, //Mr.#
            NAME_E, //Niti#
            MIDNAME_E, //#			//mid name
            SURNAME_E, //Wisatxxx#

            //----
            HOME_NO, //362/x#		//บ้านเลขที่
            MOO, //หมู่ที่ 10#		//หมู่ที่
            SOI, //#			//ซอย
            ROAD, //#			//ถนน
            SUBDIRECT, //#			//ตำบล
            DIRECT, //ตำบลบึงxx#		//ตำบล/แขวง
            AMPHAM, //อำเภอเมืองพิษณุโลก#	//อำเภอ/เขต
            PROVICE, //จังหวัดพิษณุโลก#		//จังหวัด

            SEX,    //1#				//เพศ 1=man,2=woman

            BRITH_DATE, //25xx08xx#	//birthdate 25xx08xx YYYYMMDD 

            //ISSUE_PLACE,    // สถานที่/หน่วยงานที่ออกบัตร
            //
            //ISSUE_DATE, //25xx10xx#   //25xx10xx issue date
            //EXPIRE_DATE,//25xx08xx	//25xx08xx expire date	    
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

                mkIDCard.Text = fields[(int)NID_FIELD.NID_CODE];
                txtMName.Text = fields[(int)NID_FIELD.NAME_T];
                txtMLName.Text = fields[(int)NID_FIELD.SURNAME_T];
                dtpM.Text = _yyyymmdd_(fields[(int)NID_FIELD.BRITH_DATE]);

                txtMAdd.Text = fields[(int)NID_FIELD.HOME_NO] + "" +
                                        fields[(int)NID_FIELD.MOO] + " " +
                                        fields[(int)NID_FIELD.SOI] + " " +
                                        fields[(int)NID_FIELD.ROAD] + " " +
                                        fields[(int)NID_FIELD.SUBDIRECT] + " " +
                                        fields[(int)NID_FIELD.DIRECT] + " " +
                                        fields[(int)NID_FIELD.AMPHAM] + " " +
                                        fields[(int)NID_FIELD.PROVICE] + " "
                                        ;
                cbbMSex.Text = sex_(fields[(int)NID_FIELD.SEX]);
                
                //return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
            //CRID.disconnectCard();          
            //RDNID.CloseNIDLib();
            
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
                String m;
                m = String.Format("ไม่ได้เสียบบัตร", nInsertCard);
                MessageBox.Show(m);

                CRID.deselectReader();
                return -1;
            }
            else
            {
                if (nInsertCard == 0)
                {
                    BindDataToScreen();
                }
            }
            //timer1.Enabled = false;
            return 0;
        }
        //RDNID

        public string _sidcard
        {
            get { return Sidcard; }
            set { Sidcard = value; }
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

        public void loadnc()
        {
            try
            {
                loaddb.checkdb();
                string strnc = "SELECT * FROM DBNCar";
                OleDbCommand cmd = new OleDbCommand(strnc, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBNCar");
                loaddb.cnn.Close();
                dt = ds.Tables["DBNCar"];

                cbbMNumCar.DataSource = dt;
                cbbMNumCar.DisplayMember = "NCar";
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }

        public void trueenabled()
        {
            //txtIDMB.Enabled = false;
            txtMName.Enabled = true;
            txtMLName.Enabled = true;
            mkIDCard.Enabled = true;
            txtMIDDL.Enabled = true;
            dtpM.Enabled = true;
            mktMPhone.Enabled = true;
            cbbMSex.Enabled = true;
            txtMAdd.Enabled = true;
            cbbMPo.Enabled = true;
            cbbMNumCar.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            clearToolStripMenuItem.Enabled = true;
            updateToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
        }

        public void offse()
        {
            //false
            //txtIDMB.Enabled = false;
            mkIDCard.Enabled = false;
            cbbMSex.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            clearToolStripMenuItem.Enabled = false;

            //true
            txtMName.Enabled = true;
            txtMLName.Enabled = true;
            txtMIDDL.Enabled = true;
            dtpM.Enabled = true;
            mktMPhone.Enabled = true;
            txtMAdd.Enabled = true;
            cbbMPo.Enabled = true;
            cbbMNumCar.Enabled = true;
            updateToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;
        }

        public void cleartxt()
        {
            //txtIDMB.Clear();
            txtMName.Clear();
            txtMLName.Clear();
            mkIDCard.Clear();
            txtMIDDL.Clear();
            dtpM.Text = "";
            mktMPhone.Clear();
            cbbMSex.Text = "ชาย";
            txtMAdd.Clear();
            cbbMPo.Text = "พนักงานขับรถ";
            txtMID.Clear();
            txtMPass.Clear();
            txtMCP.Clear();
            //txtIDMB.Focus();
        }

        public void falseenable()
        {
            //txtIDMB.Enabled = false;
            txtMName.Enabled = false;
            txtMLName.Enabled = false;
            mkIDCard.Enabled = false;
            txtMIDDL.Enabled = false;
            dtpM.Enabled = false;
            mktMPhone.Enabled = false;
            cbbMSex.Enabled = false;
            txtMAdd.Enabled = false;
            cbbMPo.Enabled = false;
            cbbMNumCar.Enabled = false;
            if (checkcbb(cbbMPo))
            {
                txtMID.Enabled = true;
                txtMPass.Enabled = true;
                txtMCP.Enabled = true;
            }
            else
            {
                txtMID.Enabled = false;
                txtMPass.Enabled = false;
                txtMCP.Enabled = false;
            }
            saveToolStripMenuItem.Enabled = false;
            clearToolStripMenuItem.Enabled = false;
            updateToolStripMenuItem.Enabled = true;
            deleteToolStripMenuItem.Enabled = true;
        }

        public void readlistview()
        {
            ivMData.Clear();
            ivMData.Items.Clear();
            ivMData.Columns.Add("รหัสบัตรประชาชน", 150, HorizontalAlignment.Left);
            ivMData.Columns.Add("ชื่อ", 150, HorizontalAlignment.Left);
            ivMData.Columns.Add("นามสกุล", 150, HorizontalAlignment.Left);
            ivMData.Columns.Add("วันเกิด", 90, HorizontalAlignment.Left);
            ivMData.Columns.Add("เพศ", 90, HorizontalAlignment.Left);
            ivMData.Columns.Add("เบอร์โทรศัพท์", 90, HorizontalAlignment.Left);
            ivMData.Columns.Add("ใบอนุญาตขับรถ", 120, HorizontalAlignment.Left);
            ivMData.Columns.Add("ที่อยู่", 200, HorizontalAlignment.Left);
            ivMData.Columns.Add("ตำแหน่ง", 120, HorizontalAlignment.Left);
            ivMData.Columns.Add("หมายเลขข้างรถ", 120, HorizontalAlignment.Left);
            ivMData.Columns.Add("Username", 90, HorizontalAlignment.Left);
            ivMData.Columns.Add("Password", 90, HorizontalAlignment.Left);
            ivMData.View = View.Details;
        }

        public void readMData()
        {
            ivMData.Items.Clear();
            //(MName,MLname,IDCard,IDCar,Sex,BD,Phone,AddD,PST,IDB,PWB)
            try
            {
                loaddb.checkdb();
                string readstr = "SELECT * FROM DBMember ";
                OleDbCommand readcomd = new OleDbCommand(readstr, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(readcomd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBMember");
                loaddb.cnn.Close();
                dt = ds.Tables["DBMember"];

                for (int i = 0; i < dt.Rows.Count - 0; i++)
                {
                    ivMData.Items.Add(dt.Rows[i]["IDCard"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["MName"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["MLName"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["BD"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["Sex"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["Phone"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["IDCar"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["AddD"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["PST"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["NCar"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["IDB"].ToString());
                    ivMData.Items[i].SubItems.Add(dt.Rows[i]["PWB"].ToString());
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ตรวจพบข้อผิดพลาด");
            }
        }

        //check
        public bool checkID(MaskedTextBox mskcheck)
        {
            if (mskcheck.Text.Replace("-","") == "" || mskcheck.Text.Replace("-","").Length < 13)
                return true;

            if (mskcheck.Text.Replace("-","").ToCharArray().All(c => char.IsNumber(c)) == false)
                return true;

            else
                return false;
        }

        public bool checkPhone(MaskedTextBox mskcphone)
        {
            if (mskcphone.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") == "" || 
                mskcphone.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Length < 10)
                return true;

            else
                return false;
        }

        public bool checktxtbox(TextBox ctxt)
        {
            if (ctxt.Text.Equals(""))
                return true;

            else
                return false;
        }

        public bool checkcbb(ComboBox cbbcheck)
        {
            if (cbbcheck.Text.Equals("พนักงานบัญชี"))
                return true;

            else
                return false;
        }

        public bool checkcbb2(ComboBox cbbcheck2)
        {
            if (cbbcheck2.Text.Equals("พนักงานขับรถ"))
                return true;
            else
                return false;
        }

        public bool checktxtl(TextBox txtl)
        {
            if (txtl.Text.Length < 6)
                return true;

            else
                return false;
        }

        public void insertsave()
        {
            if (checktxtbox(txtMName))
            {
                MessageBox.Show("กรุณาระบุชื่อของท่าน", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checktxtbox(txtMLName))
            {
                MessageBox.Show("กรุณาระบุนามสกุลของท่าน", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkID(mkIDCard))
            {
                MessageBox.Show("กรุณากรอกรหัสบัตรประชาชนให้ครบ", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checktxtbox(txtMIDDL) && (txtMIDDL.Text.Length < 8) && (checkcbb2(cbbMPo)))
            {
                MessageBox.Show("กรุณากรอกรหัสใบอนุญาตขับรถให้ครบ", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkPhone(mktMPhone))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ของท่านให้ครบ", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checktxtbox(txtMAdd))
            {
                MessageBox.Show("กรุณาระบุที่อยู่ของท่าน", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkcbb(cbbMPo) && checktxtbox(txtMID))
            {
                MessageBox.Show("กรุณาระบุ ID ของท่าน", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkcbb(cbbMPo) && checktxtbox(txtMPass))
            {
                MessageBox.Show("กรุณาระบุ Password ของท่าน", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkcbb(cbbMPo) && checktxtbox(txtMCP))
            {
                MessageBox.Show("กรุณายืนยัน Password ของท่านอีกครั้ง", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkcbb(cbbMPo) && (txtMCP.Text != txtMPass.Text))
            {
                MessageBox.Show("Password ของท่านไม่ตรงกัน", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkcbb(cbbMPo) && (checktxtl(txtMID) || checktxtl(txtMPass)))
            {
                MessageBox.Show("กรุณากรอก ID หรือ Password 6 ตัวขึ้นไป", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbbMNumCar.Text == "พนักงานขับรถ" || cbbMNumCar.Text == "พนักงานขายตั๋ว")
            {
                MessageBox.Show("กรุณาระบุหมายเลขข้างรถ", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (MessageBox.Show("คุณแน่ใจแล้วใช้ไหมที่จะเพิ่มข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {        
                        loaddb.checkdb();
                        //(IDCard,MName,MLname,BD,Sex,Phone,IDCar,AddD,MPicture,PST,NCar,IDB,PWB)
                        string insertstr = "INSERT INTO DBMember (IDCard,MName,MLname,BD,Sex,Phone,IDCar,AddD,PST,NCar,IDB,PWB) VALUES ('" + mkIDCard.Text.Replace("-", "") + "','" + txtMName.Text + "','" + txtMLName.Text + "','" + dtpM.Text + "','" + cbbMSex.Text + "','" + mktMPhone.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "") + "','" + txtMIDDL.Text + "','" + txtMAdd.Text + "','" + cbbMPo.Text + "','" + cbbMNumCar.Text + "','" + txtMID.Text + "','" + txtMPass.Text + "')";
                        savedb(insertstr);
                        readMData();
                        //MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "เพิ่มข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cleartxt();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ตรวจพบข้อผิดพลาด");
                    }
                }
                else
                {
                    MessageBox.Show("ยกเลิก", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertsave();
        }

        //check key code

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

        public bool checktoonum(ToolStripTextBox tootxt, char currentc)
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

        private void txtMName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknonum(txtMName, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtMLName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknonum(txtMLName, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }


        private void txtMIDDL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checknum(txtMIDDL, e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
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

        private void mktMPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13)
            {
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
                MessageBox.Show("สามารถใส่ได้แค่ตัวเลข !!!", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbbMPo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkcbb(cbbMPo))
            {
                txtMID.Enabled = true;
                txtMPass.Enabled = true;
                txtMCP.Enabled = true;
            }
            else
            {
                txtMID.Enabled = false;
                txtMPass.Enabled = false;
                txtMCP.Enabled = false;
                txtMID.Text ="";
                txtMPass.Text ="";
                txtMCP.Text ="";
            }

            if ((cbbMPo.Text == "พนักงานขับรถ") || (cbbMPo.Text == "พนักงานขายตั๋ว"))
            {
                cbbMNumCar.Enabled = true;
            }
            else
            {
                cbbMNumCar.Enabled = false;
                cbbMNumCar.Text = "-";
            }
        }

        private void newMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trueenabled();
            cleartxt();

            if (toolStripMenuItem1.Text != "Disconnect")
            {
                ReadCitizenCard();
            }
            
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cleartxt();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจแล้วใช้ไหมที่จะลบข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    loaddb.checkdb();

                    //DELETE FROM DBMember WHERE IDCard ='" + mkIDCard.Text.Replace("-", "") + "' AND IDCar='" + txtMIDDL.Text + "'
                    //string deletestr = "DELETE FROM DBMember where IDMB= " + Convert.ToInt64(txtIDMB.Text.Trim()) + "";
                    string deletestr = "DELETE FROM DBMember where IDCard= '" + mkIDCard.Text.Replace("-", "") + "'";
                    OleDbCommand dcmd = new OleDbCommand(deletestr, loaddb.cnn);
                    dcmd.ExecuteNonQuery();
                    loaddb.cnn.Close();
                    loaddb.cnn.Dispose();
                    readMData();
                    cleartxt();
                    falseenable();
                    MessageBox.Show("ลบข้อมูลสำเร็จ", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ตรวจพบข้อผิดพลาด");
                }
            }
            else
            {
                MessageBox.Show("ยกเลิก", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void FAddMember_Load(object sender, EventArgs e)
        {

            try
            {
                this.Icon = Properties.Resources.icon_Bus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }

            readlistview();
            readMData();
            falseenable();
            loadnc();

            updateToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
            dtpM.Value.ToUniversalTime();
            menuStrip1.Items.Insert(1, new ToolStripSeparator());
            menuStrip1.Items.Insert(4, new ToolStripSeparator());
            menuStrip1.Items.Insert(7, new ToolStripSeparator());

            checksetting();
            Timedeley();

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
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        private void Timedeley()
        {
            sdelay = Convert.ToInt32(sTdelay) * 1000;
            tmrRID.Interval = sdelay;
        }

        private void ivMData_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            for (int i = 0; i < ivMData.Items.Count; i++)
            {
                if (ivMData.Items[i].Selected == true)
                {
                    try
                    {
                        mkIDCard.Text = ivMData.Items[i].SubItems[0].Text;
                        txtMName.Text = ivMData.Items[i].SubItems[1].Text;
                        txtMLName.Text = ivMData.Items[i].SubItems[2].Text;
                        dtpM.Text = ivMData.Items[i].SubItems[3].Text;
                        cbbMSex.Text = ivMData.Items[i].SubItems[4].Text;
                        mktMPhone.Text = ivMData.Items[i].SubItems[5].Text;
                        txtMIDDL.Text = ivMData.Items[i].SubItems[6].Text;
                        txtMAdd.Text = ivMData.Items[i].SubItems[7].Text;
                        cbbMPo.Text = ivMData.Items[i].SubItems[8].Text;
                        cbbMNumCar.Text = ivMData.Items[i].SubItems[9].Text;
                        txtMID.Text = ivMData.Items[i].SubItems[10].Text;
                        txtMPass.Text = ivMData.Items[i].SubItems[11].Text;
                        txtMCP.Text = txtMPass.Text;
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //txtMCP.Clear();
                }
            }        
            ivMData.Focus();
            ivMData.FullRowSelect = true;
            offse();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจแล้วที่จะแก้ไข้ข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    loaddb.checkdb();
                    //(IDCard,MName,MLname,BD,Sex,Phone,IDCar,AddD,MPicture,PST,NCar,IDB,PWB)
                    string updatestr = "UPDATE DBMember SET MName='" + txtMName.Text.Trim() + "',MLname='" + txtMLName.Text.Trim() + "',IDCar='" + txtMIDDL.Text.Trim() + "',BD='" + dtpM.Text.Trim() + "',Phone='" + mktMPhone.Text.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim() + "',AddD='" + txtMAdd.Text.Trim() + "',PST='" + cbbMPo.Text.Trim() + "',IDB='" + txtMID.Text.Trim() + "',PWB='" + txtMPass.Text.Trim() + "' where IDCard='" + mkIDCard.Text.Replace("-", "").Trim() + "'";
                    OleDbCommand dcmd = new OleDbCommand(updatestr, loaddb.cnn);
                    dcmd.ExecuteNonQuery();
                    loaddb.cnn.Close();
                    loaddb.cnn.Dispose();
                    readMData();
                    MessageBox.Show("แก้ไข้ข้อมูลสำเร็จ", "แก้ไข้ข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ตรวจพบข้อผิดพลาด");
                }
            }
            else
            {
                MessageBox.Show("Cancel", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        //select all
        private void mkIDCard_Click(object sender, EventArgs e)
        {
            mkIDCard.SelectAll();
        }

        private void mktMPhone_Click(object sender, EventArgs e)
        {
            mktMPhone.SelectAll();
        }

        private void tmrRID_Tick(object sender, EventArgs e)
        {

            newMemberToolStripMenuItem.Enabled = true;

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
}
