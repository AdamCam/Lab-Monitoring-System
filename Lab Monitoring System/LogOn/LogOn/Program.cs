///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql
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
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace LogOn
{
    class Program
    {
        static void Main(string[] args)
        {
            LogOnTime LogOnTime = new LogOnTime();
            LogOnTime.LogOn();
        }
    }
}
