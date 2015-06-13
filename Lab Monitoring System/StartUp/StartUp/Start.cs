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

namespace StartUp
{
    class StartTime
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string username;
        private string password;
        private string port;

        //constructor
        public StartTime()
        {
            Initialize();
        }
        //Setup for connection
        public void Initialize()
        {
            server = ConfigurationManager.AppSettings["ServerName"];// "localhost";
            database = ConfigurationManager.AppSettings["labdata"];
            username = ConfigurationManager.AppSettings["UserName"]; //root";
            password = ConfigurationManager.AppSettings["Password"]; //12345";
            port = ConfigurationManager.AppSettings["Port"];
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password + ";" + "port=" + port + ";";

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
        public void StartUp()
        {

            //open connection
            /*if (this.OpenConnection() == true)
            {
                DateTime timeStamp = DateTime.Now;
                MySqlCommand getIdCmd = new MySqlCommand("Select idlogs from labdata.logs where computer='" + Environment.GetEnvironmentVariable("COMPUTERNAME") + "' AND shutdowntime IS NULL;", connection);

                MySqlDataReader reader = getIdCmd.ExecuteReader();
                reader.Read();

                //assign the query and connection from the constructor
                MySqlCommand insertCmd = new MySqlCommand("UPDATE labdata.logs set shutdowntime=@start WHERE idlogs=@id;", connection);
                insertCmd.Prepare();
                insertCmd.Parameters.AddWithValue("@start", timeStamp);
                insertCmd.Parameters.AddWithValue("@id", reader.GetInt32("idlogs"));
                //insertCmd.Parameters.AddWithValue("@loc", "Lab1");
                //insertCmd.Parameters.AddWithValue("@user", Environment.GetEnvironmentVariable("USERNAME"));
                reader.Close();

                insertCmd.ExecuteNonQuery();
                this.CloseConnection();
            }*/

            if (this.OpenConnection() == true)
            {
                DateTime timeStamp = DateTime.Now;
                //assign the query and connection from the constructor
                MySqlCommand insertCmd = new MySqlCommand("INSERT INTO labdata.logs (user,computer,starttime) VALUES (@user,@name,@start);", connection);
                insertCmd.Prepare();
                insertCmd.Parameters.AddWithValue("@start", timeStamp);
                insertCmd.Parameters.AddWithValue("@name", Environment.GetEnvironmentVariable("COMPUTERNAME"));
                //insertCmd.Parameters.AddWithValue("@loc", "Lab1");
                insertCmd.Parameters.AddWithValue("@user", Environment.GetEnvironmentVariable("USERNAME"));

                insertCmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
    }
}
