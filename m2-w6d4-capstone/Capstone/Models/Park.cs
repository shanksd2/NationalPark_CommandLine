using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        public int Park_id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Establish_date { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name + "\nLocation:" + Location.ToString().PadLeft(21) + "\nEstablished:\t" + Establish_date.ToString("d").PadLeft(18) + "\nArea:" + Area.ToString("N0").PadLeft(26) + "\nAnnual Visitors:" + Visitors.ToString("N0").PadLeft(18) + "\n\n" + Description.ToString();
      
        }
    }
}

//Park p = new Park();
//p.Park_id = Convert.ToInt32(reader["park_id"]);
//                        p.Name = Convert.ToString(reader["name"]);
//                        p.Location = Convert.ToString(reader["location"]);
//                        p.Establish_date = Convert.ToDateTime(reader["establish_date"]);
//                        p.Area = Convert.ToInt32(reader["area"]);
//                        p.Visitors = Convert.ToInt32(reader["visitors"]);
//                        p.Description = Convert.ToString(reader["description"]);
