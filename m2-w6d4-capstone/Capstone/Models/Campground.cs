using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int Campground_id { get; set; }
        public int Park_id { get; set; }
        public string Name { get; set; }
        public int Open_from_mm { get; set; }
        public int Open_to_mm { get; set; }
        public decimal Daily_fee { get; set; }

        public override string ToString()
        {
            return Name.PadRight(35) + MonthConversion(Open_from_mm).PadRight(13) + MonthConversion(Open_to_mm).PadRight(13) + Daily_fee.ToString("C2");
        }

        public static string MonthConversion(int month)
        {
            string output = string.Empty;
            switch (month)
            {
                case 1:
                    output = "January";
                    break;
                case 2:
                    output = "February";
                    break;
                case 3:
                    output = "March";
                    break;
                case 4:
                    output = "April";
                    break;
                case 5:
                    output = "May";
                    break;
                case 6:
                    output = "June";
                    break;
                case 7:
                    output = "July";
                    break;
                case 8:
                    output = "August";
                    break;
                case 9:
                    output = "September";
                    break;
                case 10:
                    output = "October";
                    break;
                case 11:
                    output = "November";
                    break;
                case 12:
                    output = "December";
                    break;
            }
            return output;
        }
    }

}
