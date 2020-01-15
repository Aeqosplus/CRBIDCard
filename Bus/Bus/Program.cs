using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bus
{
    static class Program
    {
        public static string PID = null;
        public static Boolean flag = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FLogin());

            if (flag == true)
            {
                Application.Run(new FMain());
            }
        }
    }
}
