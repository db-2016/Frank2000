using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrankCodingTest.Data;
using System.Configuration;
using System.Diagnostics;

namespace FrankCodingTest
{
    class Program
    {       

        public static string dbConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionString"]))
                    throw new ConfigurationErrorsException("Configuration value missing!");

                return ConfigurationManager.AppSettings["ConnectionString"];
            }
        }

        public static string startupPath
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WorkingDir"]))
                    throw new ConfigurationErrorsException("Configuration value missing!");

                return ConfigurationManager.AppSettings["WorkingDir"];
            }
        }
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            //Data Access
            using (var dataTransferObject = new DTO(dbConnectionString))
            {
                //dataTransferObject.Select("select query");
            }           

            //Call another console application accepting arguments as input
            Process _p1 = new Process();
            _p1.StartInfo = new ProcessStartInfo("TestConsoleProgram.exe");
            _p1.StartInfo.WorkingDirectory = startupPath;
            _p1.StartInfo.CreateNoWindow = false;
            _p1.StartInfo.Arguments = string.Concat("Arg1", "Arg2 ");
            _p1.StartInfo.UseShellExecute = false;
            _p1.Start();

            Console.WriteLine(string.Format("Press [Enter] to exit."));
            Console.ReadLine();
            
        }
    }
}
