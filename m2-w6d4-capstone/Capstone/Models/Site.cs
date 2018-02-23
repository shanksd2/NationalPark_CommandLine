using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
   public class Site
    {
        public int Site_id { get; set; }
        public int Campground_id { get; set; }
        public int Site_number { get; set; }
        public int Max_occupancy { get; set; }
        public bool Accessible { get; set; }
        public int Max_RV_length { get; set; }
        public bool Utilities { get; set; }

        public override string ToString()
        {
            return Site_number.ToString().PadRight(15) + Max_occupancy.ToString().PadRight(15) + Accessible.ToString().PadRight(15) + Max_RV_length.ToString().PadRight(15) + Utilities.ToString().PadRight(15);
    }
    }

}
