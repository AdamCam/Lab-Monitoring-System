///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql
///LogOn Data Collection
///Spring Term Lane Community College, 2015
///
///File:Connect.cs
///Authors: Adam M. Cameron
///Creation Date: 6/10/15
///Update History: 6/12/15
///******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LogOn
{
    class LogOnTime
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;
        private string port;

        //constructor
        public LogOnTime()
        {
            Initialize();
        }
        //Setup for connection
        public void Initialize()
        {

            server = ConfigurationManager.AppSettings["ServerName"];
            database = ConfigurationManager.AppSettings["Database"];
            username = ConfigurationManager.AppSettings["UserName"]; 
            password = ConfigurationManager.AppSettings["Password"]; 
            port = ConfigurationManager.AppSettings["Port"];
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password + "; port=" + port + ";";

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
        public void LogOn()
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                DateTime timeStamp = DateTime.Now;
                //assign the query and connection from the constructor
                MySqlCommand chckCmd = new MySqlCommand("Delete from labdata.logs where computer='" + Environment.GetEnvironmentVariable("COMPUTERNAME") + "' AND logofftime IS NULL;", connection);
                chckCmd.ExecuteNonQuery();
                MySqlCommand insertCmd = new MySqlCommand("INSERT INTO labdata.logs (user,computer,logontime) VALUES (@user,@name,@start);", connection);
                insertCmd.Prepare();
                insertCmd.Parameters.AddWithValue("@start", timeStamp);
                insertCmd.Parameters.AddWithValue("@name", Environment.GetEnvironmentVariable("COMPUTERNAME"));
                insertCmd.Parameters.AddWithValue("@user", Environment.GetEnvironmentVariable("USERNAME"));

                insertCmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
