///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql
///Spring Term Lane Community College, 2015
///
///File:course.cpp
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

namespace Shutdown
{
    class ShutdownTime
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;
        private string port;

        //constructor
        public ShutdownTime()
        {
            Initialize();
        }
        //Setup for connection
        public void Initialize()
        {
            //this goes in the app.config file for the lab. Changed it to test charts at home.
            /*<add key="ServerName" value="10.1.222.1"/>
            <add key="Database" value="labdata"/>
            <add key="UserName" value="labuser"/>
            <add key="Password" value="lab222"/>
            <add key ="Port" value="3306" />*/
            server = ConfigurationManager.AppSettings["ServerName"];// "localhost";
            database = ConfigurationManager.AppSettings["Database"];
            username = ConfigurationManager.AppSettings["UserName"]; //root";
            password = ConfigurationManager.AppSettings["Password"]; //12345";
            port = ConfigurationManager.AppSettings["Port"];
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password + "; port=" + port +";";

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
        public void Shutdown()
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                DateTime timeStamp = DateTime.Now;

                //assign the query and connection from the constructor
                MySqlCommand insertCmd = new MySqlCommand("INSERT INTO labdata.logs (user,computer,shutdowntime) VALUES (@user,@name,@start);", connection);
                insertCmd.Prepare();
                insertCmd.Parameters.AddWithValue("@start", timeStamp);
                insertCmd.Parameters.AddWithValue("@name", Environment.GetEnvironmentVariable("COMPUTERNAME"));
                //insertCmd.Parameters.AddWithValue("@loc", "Lab1");
                insertCmd.Parameters.AddWithValue("@user", Environment.GetEnvironmentVariable("USERNAME"));

                insertCmd.ExecuteNonQuery();
                this.CloseConnection();
            }
            /*if (this.OpenConnection() == true)
            {
                DateTime timeStamp = DateTime.Now;
                //assign the query and connection from the constructor
                MySqlCommand chckCmd = new MySqlCommand("Delete from labdata.logs where computer='" + Environment.GetEnvironmentVariable("COMPUTERNAME") + "' AND starttime IS NULL;", connection);
                chckCmd.ExecuteNonQuery();
                MySqlCommand insertCmd = new MySqlCommand("INSERT INTO labdata.logs (user,computer,starttime) VALUES (@user,@name,@start);", connection);
                insertCmd.Prepare();
                insertCmd.Parameters.AddWithValue("@start", timeStamp);
                insertCmd.Parameters.AddWithValue("@name", Environment.GetEnvironmentVariable("COMPUTERNAME"));
                //insertCmd.Parameters.AddWithValue("@loc", "Lab1");
                insertCmd.Parameters.AddWithValue("@user", Environment.GetEnvironmentVariable("USERNAME"));

                insertCmd.ExecuteNonQuery();
                this.CloseConnection();
            }*/
        }
    }
}
