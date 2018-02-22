using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    class Site
    {
        private int Site_id { get; set; }
        private int Campground_id { get; set; }
        private int Site_number { get; set; }
        private int Max_occupancy { get; set; }
        private bool Accessible { get; set; }
        private int Max_RV_length { get; set; }
        private bool Utilities { get; set; }
    }
}
