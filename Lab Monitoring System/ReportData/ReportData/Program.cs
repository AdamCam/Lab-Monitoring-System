///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql HTML
///Spring Term Lane Community College, 2015
///
///File:Program.cs--Main
///Authors: Adam M. Cameron
///Creation Date: 6/10/15
///Update History: 6/12/15
///******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ReportData
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ExportData());
        }
    }
}
