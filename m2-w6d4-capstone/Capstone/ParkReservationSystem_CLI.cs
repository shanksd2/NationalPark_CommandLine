using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone
{
    class ParkReservationSystem_CLI
    {
        private string databaseconnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
    }
}
