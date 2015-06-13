///******************************************************************
///Adam Cameron - Lab Monitoring System
///C# MySql
///Spring Term Lane Community College, 2015
///
///File:course.cpp
///Authors: Adam M. Cameron
///Creation Date: 6/10/15
///Update History: 6/12/15
///This program still needs work for writing to the datebase correctly
///******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            StartTime startTime = new StartTime();
            startTime.StartUp();
        }
    }
}
