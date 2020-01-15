using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Bus
{
    class Cloaddb
    {

        string serverdb = "Provider=Microsoft.ACE.OLEDB.12.0;Data SOURCE=BusDB.accdb;Jet OLEDB:Database Password=JNTT";
        public OleDbConnection cnn = new OleDbConnection();

        string cIDCard, cRTime;

        public string _cIDCard
        {
            get { return cIDCard; }
            set { cIDCard = value; }
        }

        public string _cRTime
        {
            get { return _cRTime; }
            set { cRTime = value; }
        }

        public bool cr = false;

        public bool _cr
        {
            get { return cr; }
            set { cr = value; }
        }

        public void checkdb()
        {
            cnn.Dispose();
            cnn.Close();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.ConnectionString = serverdb;
                    cnn.Open();
                }
                else
                {
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "พบข้อผิดพลาด");
            }
        }
        

    }
}
