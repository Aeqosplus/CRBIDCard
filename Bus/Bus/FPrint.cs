using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.OleDb;
using System.IO;

namespace Bus
{
    public partial class FPrint : Form
    {


        Cloaddb loaddb = new Cloaddb();

        public string rpDate { set; get; }
        public string rpIDRR { set; get; }
        public string rpIDCard { set; get; }
        public string rpName { set; get; }
        public string rpLName { set; get; }
        public string rpPST { set; get; }
        public string rpNCar { set; get; }
        public string rpTime { set; get; }
        public string rpRound { set; get; }
        public string rpSRound { set; get; }
        public string rpRMoney { set; get; }
        public string rpTicket { set; get; }
        public string rpPTicket { set; get; }
        public string rpTMoney { set; get; }
        public string rpMoney { set; get; }

        //Calculator Round Ticket
        public double cr, ct;

        string sRound, sTMoney, sTicket;

        public FPrint()
        {
            InitializeComponent();
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
                System.Windows.Forms.MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }

        private void calculator()
        {
            loadsetting();

            cr = Convert.ToDouble(rpRound) * Convert.ToDouble(sRound);
            ct = ((Convert.ToDouble(sTicket) * Convert.ToDouble(sTMoney) / 100) * Convert.ToDouble(rpTicket));

            rpRMoney = cr.ToString("00.00");
            rpTMoney = ct.ToString("00.00");

            rpSRound = sRound;
            rpPTicket = sTicket + " %";
        }

        private void FPrint_Load(object sender, EventArgs e)
        {

            try
            {
                this.Icon = Properties.Resources.icon_Bus;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }

            this.Text = "Report วันที่ : " + rpDate + " ของ : " + rpName;

            calculator();

            this.reportViewer1.Refresh();

            ReportParameter rp1 = new ReportParameter("rpDate", rpDate);
            ReportParameter rp2 = new ReportParameter("rpIDRR", rpIDRR);
            ReportParameter rp3 = new ReportParameter("rpIDCard", rpIDCard);
            ReportParameter rp4 = new ReportParameter("rpName", rpName);
            ReportParameter rp5 = new ReportParameter("rpLName", rpLName);
            ReportParameter rp6 = new ReportParameter("rpPST", rpPST);
            ReportParameter rp7 = new ReportParameter("rpNCar", rpNCar);
            ReportParameter rp8 = new ReportParameter("rpTime", rpTime);
            ReportParameter rp9 = new ReportParameter("rpRound", rpRound);
            ReportParameter rp10 = new ReportParameter("rpSRound", rpSRound);
            ReportParameter rp11 = new ReportParameter("rpRMoney", rpRMoney);
            ReportParameter rp12 = new ReportParameter("rpTicket", rpTicket);
            ReportParameter rp13 = new ReportParameter("rpPTicket", rpPTicket);
            ReportParameter rp14 = new ReportParameter("rpTMoney", rpTMoney);
            ReportParameter rp15 = new ReportParameter("rpMoney", rpMoney);

            //this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] {rp1,rp2,rp3,rp4,rp5,rp6,rp7,rp8,rp9,rp10,rp11,rp12,rp13,rp14,rp15});
            this.reportViewer1.RefreshReport();
        }

        private void FPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }

        

    }
}
