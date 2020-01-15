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
    public partial class FTicket : Form
    {
        public FTicket()
        {
            InitializeComponent();
        }

        //check
        string cIDCard, cRTime;

        //RunRound
        string rIDCard, rRTime, rRound, rRTicket, rRMoney;

        //Setting
        string sRound, sTicket, sTMoney;

        double cr, ct, cm, Ticket;

        Cloaddb loaddb = new Cloaddb();

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
                    sRound = drs["SRound"].ToString();
                    sTicket = drs["STicket"].ToString();
                    sTMoney = drs["STMoney"].ToString();
                }
                drs.Close();
                loaddb.cnn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtTicket.Text == "")
            {
                MessageBox.Show("กรูณาใส่ข้อมูล", "ตรวจพบข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("คุณแน่ใจแล้วที่จะแก้ไข้ข้อมูล", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    loaddb.checkdb();

                    Ticket = Convert.ToDouble(txtTicket.Text) + Convert.ToDouble(rRTicket);

                    rRTicket = Ticket.ToString();

                    cr = Convert.ToDouble(rRound) * Convert.ToDouble(sRound);
                    ct = ((Convert.ToDouble(sTMoney) * Convert.ToDouble(sTicket)) / 100) * Convert.ToDouble(rRTicket);
                    cm = cr + ct;
                    rRMoney = cm.ToString("00.00");

                    string uTickket = "UPDATE DBRunRound SET Round='" + rRound + "',RTicket='" + rRTicket + "',RMoney='" + rRMoney + "' WHERE IDCard= '" + rIDCard + "' AND [RTime]= '" + rRTime + "' ";
                    OleDbCommand cmd = new OleDbCommand(uTickket, loaddb.cnn);
                    cmd.ExecuteNonQuery();
                    loaddb.cnn.Dispose();
                    loaddb.cnn.Close();

                    txtTicket.Text = "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
                }

                FMain fm = new FMain();
                
                loadTicket();
                
            }
            else
            {
                MessageBox.Show("Cancel", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtTicket.Text = "0";
        }

        private void FTicket_Load(object sender, EventArgs e)
        {

            try
            {
                this.Icon = Properties.Resources.icon_Bus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
            
            loadTicket();
            loadsetting();
        }

        public void loadTicket()
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
                    rIDCard = dt.Rows[i]["IDCard"].ToString();
                    rRTime = dt.Rows[i]["RTime"].ToString();
                    rRound = dt.Rows[i]["Round"].ToString();
                    rRTicket = dt.Rows[i]["RTicket"].ToString();
                    rRMoney = dt.Rows[i]["RMoney"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }

            lblTIDCard.Text = rIDCard;
            lblTDate.Text = rRTime;
            lblTicket.Text = rRTicket;
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
