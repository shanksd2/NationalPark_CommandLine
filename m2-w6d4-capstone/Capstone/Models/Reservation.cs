using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class Reservation
    {
        private int Reservation_id { get; set; }
        private int Site_id { get; set; }
        private string Name { get; set; }
        private DateTime From_date { get; set; }
        private DateTime To_date { get; set; }
        private DateTime Create_date { get; set; }
    }
}
