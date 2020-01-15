using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CRDNID;

namespace Bus
{
    class CRound
    {

        Cloaddb loaddb = new Cloaddb();
        CRDNID.RDNID RID = new CRDNID.RDNID();

        //cRound
        public double cr, ct, cm;

        //Check
        public string cTime, cIDCard;

        //DBSetting
        public string sRound, sTicket, sTMoney;

        //DBMember
        public string mIDCard, mName, mLName, mPhone, mPST, mNCar;

        //DBRunRound
        public string rIDCard, rTime, rName, rLName, rPhone, rPST, rNCar, rRound, rTicket, rMoney, rCheck;

        //bool
        public bool read,rmess,checkdata;

        public bool _read
        {
            get { return read; }
            set { read = value; }
        }

        public bool _rmess
        {
            get { return rmess; }
            set { rmess = value; }
        }

        public bool _checkdata
        {
            get { return checkdata; }
            set { checkdata = value; }
        }

        public string _cTime
        {
            get { return cTime; }
            set { cTime = value; }
        }

        public string _cIDCard
        {
            get { return cIDCard; }
            set { cIDCard = value; }
        }

        public void loadmMember()
        {
            try
            {
                loaddb.checkdb();

                string mMember = "SELECT * FROM DBMember WHERE IDCard = '" + cIDCard + "'";
                OleDbCommand cmdm = new OleDbCommand(mMember, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmdm);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBRunRound");
                loaddb.cnn.Close();
                dt = ds.Tables["DBRunRound"];

                for (int i = 0; i < dt.Rows.Count - 0; i++)
                {
                    mIDCard = dt.Rows[i]["IDCard"].ToString();
                    mName = dt.Rows[i]["MName"].ToString();
                    mLName = dt.Rows[i]["MLName"].ToString();
                    mPhone = dt.Rows[i]["Phone"].ToString();
                    mPST = dt.Rows[i]["PST"].ToString();
                    mNCar = dt.Rows[i]["NCar"].ToString();
                }
                //loaddb.cnn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        public void loadrRound()
        {
            try
            {
                loaddb.checkdb();

                string sRound = "SELECT * FROM DBRunRound WHERE IDCard= '" + cIDCard + "' AND [RTime]= '" + cTime + "'";
                OleDbCommand cmds = new OleDbCommand(sRound, loaddb.cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmds);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds, "DBRunRound");
                dt = ds.Tables["DBRunRound"];

                for (int i = 0; i < dt.Rows.Count - 0; i++)
                {
                    rIDCard = dt.Rows[i]["IDCard"].ToString();
                    rTime = dt.Rows[i]["RTime"].ToString();
                    rName = dt.Rows[i]["RName"].ToString();
                    rLName = dt.Rows[i]["RLName"].ToString();
                    rPhone = dt.Rows[i]["RPhone"].ToString();
                    rPST = dt.Rows[i]["RPST"].ToString();
                    rNCar = dt.Rows[i]["RNCar"].ToString();
                    rRound = dt.Rows[i]["Round"].ToString();
                    rTicket = dt.Rows[i]["RTicket"].ToString();
                    rMoney = dt.Rows[i]["RMoney"].ToString();
                    rCheck = dt.Rows[i]["RCheck"].ToString();
                }
                //loaddb.cnn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
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

        public void loaddsRound()
        {
            FMain fm = new FMain();
            rmess = false;
            loadmMember();
            loadrRound();
            loadsetting();

            try
            {
                loaddb.checkdb();
                int incm = 0;
                string strmc = "SELECT COUNT(*) FROM DBMember WHERE IDCard= '" + cIDCard + "'";
                OleDbCommand cmdmc = new OleDbCommand(strmc, loaddb.cnn);
                incm = Convert.ToInt32(cmdmc.ExecuteScalar());
                if (incm > 0)
                {
                    loaddb.checkdb();
                    int incr = 0;
                    string strrc = "SELECT COUNT(*) FROM DBRunRound WHERE IDCard= '" + cIDCard + "' AND [RTime]= '" + cTime + "'";
                    OleDbCommand cmdrc = new OleDbCommand(strrc, loaddb.cnn);
                    incr = Convert.ToInt32(cmdrc.ExecuteScalar());
                    if (incr > 0)
                    {
                        cr = Convert.ToDouble(rRound) + 1;
                        rRound = cr.ToString();

                        cr = Convert.ToDouble(rRound) * Convert.ToDouble(sRound);
                        ct = ((Convert.ToDouble(sTMoney) * Convert.ToDouble(sTicket)) / 100) * Convert.ToDouble(rTicket);
                        cm = cr + ct;
                        rMoney = cm.ToString("00.00");

                        string strup = "UPDATE DBRunRound SET Round='" + rRound + "',RMoney='" + rMoney + "' WHERE IDCard='" + rIDCard + "' AND [RTime]='" + rTime + "' ";
                        OleDbCommand cmdu = new OleDbCommand(strup, loaddb.cnn);
                        cmdu.ExecuteNonQuery();
                        loaddb.cnn.Dispose();
                        loaddb.cnn.Close();

                        rmess = true;
                        checkdata = false;

                        loadrRound();
                    }
                    else
                    {
                        rRound = "1";
                        rTicket = "0";
                        rCheck = "ยังไม่ได้ออก";

                        cr = Convert.ToInt32(rRound) * Convert.ToInt32(sRound);
                        ct = ((Convert.ToInt32(sTMoney) * Convert.ToInt32(sTicket)) / 100) * Convert.ToInt32(rTicket);
                        cm = cr + ct;
                        rMoney = cm.ToString("0.00");

                        //mIDCard mName mLName mPhone mPST mNCar
                        //rIDCard rTime rName rLName rPhone rPST rNCar rRound rMoney rCheck
                        //cTime,cIDCard
                        loaddb.checkdb();
                        string stris = "INSERT INTO DBRunRound (IDCard,RTime,RName,RLName,RPhone,RPST,RNCar,Round,Rticket,RMoney,RCheck) VALUES ('" + mIDCard + "','" + cTime + "','" + mName + "','" + mLName + "','" + mPhone + "','" + mPST + "','" + mNCar + "','" + rRound + "','" + rTicket + "','" + rMoney + "','" + rCheck + "')";
                        OleDbCommand cmdis = new OleDbCommand();

                        cmdis.CommandText = stris;
                        cmdis.CommandType = CommandType.Text;
                        cmdis.Connection = loaddb.cnn;
                        cmdis.ExecuteNonQuery();

                        loaddb.cnn.Dispose();
                        loaddb.cnn.Close();

                        rmess = true;
                        checkdata = false;

                        loadrRound();
                    }
                }
                else
                {
                    read = false;
                    if (MessageBox.Show("ข้อมูลนี้ไม่มีในระบบ!! คุณต้องการเพิ่มข้อมูลหรือไม่", "คำเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        loadmMember();
                        loadrRound();
                        loadsetting();
                        FAddMember fam = new FAddMember();
                        fam.ShowDialog();
                        read = true;
                    }
                    else
                    read = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
            loaddb.cnn.Dispose();
            loaddb.cnn.Close();
        }
        
    }
}
