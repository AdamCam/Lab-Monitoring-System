///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql HTML
///Spring Term Lane Community College, 2015
///
///File:Form1.cs--Form1
///Authors: Adam M. Cameron
///Creation Date: 6/10/15
///Update History: 6/12/15
///******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReportData
{
    public partial class ExportData : Form
    {
        public ExportData()
        {
            InitializeComponent();
        }
        private string connectionString = null;
        private string tableName = null;
        Export export = null;

        private void button1_Click(object sender, EventArgs e)
        {
            export = new Export();
            Connection.connectionString = connectionString;
            Connection.tableName = tableName;
            dataGridView1.DataSource = export.GetData();
            export.To_HTML();
        }
        private void ShowData()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connection connection = new Connection();
            MySqlCommand cmdDataBase = new MySqlCommand("select DATE(logontime) as date, Computer, IFNULL(SUM(TIMESTAMPDIFF(MINUTE,logontime, logofftime)), 0) as sessiontime from logs GROUP BY DATE(logontime), Computer ORDER BY COMPUTER,DATE;", connection.connection);
            MySqlCommand cmdDataBase2 = new MySqlCommand("select DATE(starttime), Computer, IFNULL(SUM(TIMESTAMPDIFF(MINUTE,starttime, shutdowntime)), 0) as sessiontime2 from logs GROUP BY DATE(logontime), Computer;", connection.connection); 

            MySqlDataReader myReader;
            try
            {
                //open connection
                connection.OpenConnection();
                myReader = cmdDataBase.ExecuteReader();

                DateTime chartdate = DateTime.Now;
                string computername = string.Empty;
                while (myReader.Read())
                {
                    string currentcomputer = myReader.GetString("computer");
                    if (computername==string.Empty || computername!=currentcomputer) 
                    {

                        computername = currentcomputer;
                        this.chart1.Series.Add(computername);
                    }
                    this.chart1.Series[computername].Points.AddXY(myReader.GetDateTime("date"),myReader.GetInt32("sessiontime").ToString());

                }           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportData_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'labdataDataSet.logs' table. You can move, or remove it, as needed.
            this.logsTableAdapter.Fill(this.labdataDataSet.logs);
            dateTimePicker1.Value = DateTime.Now.AddDays(-7);
            dateTimePicker2.Value = DateTime.Now;
            Connection con = new Connection();
            Connection.connectionString = connectionString;
            Connection.tableName = "logs";
            dataGridView1.DataSource = con.LoadDataWithAllColumns();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

    }
}