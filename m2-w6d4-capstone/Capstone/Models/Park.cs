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
            return Name.ToString().PadRight(6) + Location.ToString().PadRight(25) + Establish_date.ToString("d").PadRight(25) + Area.ToString("NO").PadRight(20) + Visitors.ToString("NO").PadRight(20) + Description.ToString().PadRight(30);
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
