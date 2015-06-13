///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql HTML
///Spring Term Lane Community College, 2015
///
///File:Connection.cs
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
using System.Configuration;

namespace ReportData
{
    class Connection
    {

        public MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;
        private string port;
        public static string connectionString;
        public static string tableName = "labdata";
        protected MySqlCommand command;
        protected MySqlDataReader dataReader;

        //constructor
        public Connection() { Initialize(); }
        //Setup for connection
        public void Initialize()
        {
            server = ConfigurationManager.AppSettings["ServerName"];
            database = ConfigurationManager.AppSettings["Database"];
            username = ConfigurationManager.AppSettings["UserName"];
            password = ConfigurationManager.AppSettings["Password"];
            port = ConfigurationManager.AppSettings["Port"];
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";";

            connection = new MySqlConnection(connectionString);
        }
        //open connection
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Connection Closed");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public DataTable LoadDataWithAllColumns()
        {
            DataTable data = null;
            try
            {
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password + ";";
                using (connection = new MySqlConnection(connectionString))
                {

                    connection.Open();
                    command = connection.CreateCommand();
                    string query = string.Format("select IFNULL (user, \"\") as User, IFNULL (logontime, \"\") as LogOn, IFNULL(logofftime, \"\") as LogOff, IFNULL(starttime,\"\") as Start, IFNULL(shutdowntime, \"\") as Shutdown from labdata.logs;", tableName);

                    using (command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        dataReader = command.ExecuteReader();
                        data = new DataTable();
                        data.Load(dataReader);
                    }
                    return data;
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
                return data;
            }
        }
        public DataTable LoadChartData()
        {
            DataTable data = null;
            try
            {
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password + ";";
                using (connection = new MySqlConnection(connectionString))
                {

                    connection.Open();
                    command = connection.CreateCommand();
                    string query = string.Format("select IFNULL (logontime, \"\") From logs ;", tableName);

                    using (command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        dataReader = command.ExecuteReader();
                        data = new DataTable();
                        data.Load(dataReader);
                    }
                    return data;
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
                return data;
            }
        }
    }
}
