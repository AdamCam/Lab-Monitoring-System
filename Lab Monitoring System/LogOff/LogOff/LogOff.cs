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

namespace LogOff
{
    class LogOffTime
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;
        private string port;

        //constructor
        public LogOffTime()
        {
            Initialize();
        }
        //Setup for connection
        public void Initialize()
        {
            server = ConfigurationManager.AppSettings["ServerName"];// "localhost";
            database = ConfigurationManager.AppSettings["Database"];
            username = ConfigurationManager.AppSettings["UserName"]; //root";
            password = ConfigurationManager.AppSettings["Password"]; //12345";
            port = ConfigurationManager.AppSettings["Port"];
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password + ";  port=" + port + ";"; 

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
        public void LogOut()
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                DateTime timeStamp = DateTime.Now;
                MySqlCommand getIdCmd= new MySqlCommand("Select idlogs from labdata.logs where computer='" + Environment.GetEnvironmentVariable("COMPUTERNAME") + "' AND logofftime IS NULL;", connection);

                MySqlDataReader reader = getIdCmd.ExecuteReader();
                reader.Read();
                
                //assign the query and connection from the constructor
                MySqlCommand insertCmd = new MySqlCommand("UPDATE labdata.logs set logofftime=@start WHERE idlogs=@id;", connection);
                insertCmd.Prepare();
                insertCmd.Parameters.AddWithValue("@start", timeStamp);
                insertCmd.Parameters.AddWithValue("@id", reader.GetInt32("idlogs"));
                //insertCmd.Parameters.AddWithValue("@loc", "Lab1");
                //insertCmd.Parameters.AddWithValue("@user", Environment.GetEnvironmentVariable("USERNAME"));
                reader.Close();
                
                insertCmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
