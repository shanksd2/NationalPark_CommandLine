using System;
using System.Collections.Generic;
using System.Configuration;


namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
            ParkReservationSystem_CLI program = new ParkReservationSystem_CLI();
            program.RunCLI();
        }
    }
}
