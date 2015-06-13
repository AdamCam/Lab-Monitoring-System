///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql HTML
///Spring Term Lane Community College, 2015
///
///File:Export.cs
///Authors: Adam M. Cameron
///Creation Date: 6/10/15
///Update History: 6/11/15
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
    class Export : Connection
    {
        public DataTable Tab = new DataTable();

        //Get ALL Data
        public DataTable GetData()
        {
            Tab = base.LoadDataWithAllColumns();
            return Tab;
        }
        public void To_HTML()
        {
            try
            {
                string filename = null;
                SaveFileDialog SaveHtml = new SaveFileDialog();
                SaveHtml.Filter = "HTML5 files|*.html";//for HTML 5
                SaveHtml.FileName = "HtmlReportData.html";
                if (SaveHtml.ShowDialog() == DialogResult.OK)
                {
                    filename = SaveHtml.FileName;
                }
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    FileInfo fileInfo = new FileInfo(filename);
                    StringBuilder sb = new StringBuilder();
                    
                    //"<html lang = 'en  xmlns='https://www.w3.org/1999/xhtml'>"
                    sb.AppendFormat("<html>");
                    
                    sb.AppendFormat("<head>");
                    sb.AppendFormat("<title>HTML Report File, Lab Monitoring Reports | {0}</title>", DateTime.Now.ToShortDateString() + " . " + DateTime.Now.ToShortTimeString());
                    sb.AppendFormat("</head>");
                    sb.AppendFormat("<body>");
                    sb.AppendFormat("<h1>{0}</h1>", fileInfo.Name.ToUpper() + " Data:");
                    sb.AppendFormat("<table>");
                    sb.AppendFormat("<thead>");
                    sb.AppendFormat("<tr>");
                    foreach (DataColumn column in Tab.Columns)
                    {
                        sb.AppendFormat("<th>{0}</th>", column.ColumnName.ToUpper());
                    }
                    sb.AppendFormat("</tr>");
                    sb.AppendFormat("</thead>");
                    sb.AppendFormat("<tbody>");
                    foreach (DataRow row in Tab.Rows)
                    {
                        sb.AppendFormat("<tr>");
                        for (int i = 0; i < Tab.Columns.Count; i++)
                        {
                            sb.AppendFormat("<td>{0}</td>", row[i].ToString());
                        }
                        sb.AppendFormat("</tr>");
                    }
                    sb.AppendFormat("</tbody>");
                    sb.AppendFormat("</table>");
                    sb.AppendFormat("</body>");
                    sb.AppendFormat("</html>");

                    sw.Write(sb.ToString());//submit your file
                } 
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }
    }
}
