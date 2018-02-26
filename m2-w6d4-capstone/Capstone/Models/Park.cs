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
            return Name + "\nLocation:" + Location.ToString().PadLeft(21) + "\nEstablished:\t" + Establish_date.ToString("d").PadLeft(18)
                   + "\nArea:" + Area.ToString("N0").PadLeft(26) + "\nAnnual Visitors:" + Visitors.ToString("N0").PadLeft(18); 
        }

        public static void WrapText(Park parkToDetail)
        {
            // source utilized: https://stackoverflow.com/questions/10541124/wrap-text-to-the-next-line-when-it-exceeds-a-certain-length
            int myLimit = 65;
            string sentence = parkToDetail.Description;
            string[] words = sentence.Split(' ');

            StringBuilder newSentence = new StringBuilder();

            string line = "";
            foreach (string word in words)
            {
                if ((line + word).Length > myLimit)
                {
                    newSentence.AppendLine(line);
                    line = "";
                }

                line += string.Format("{0} ", word);
            }
            if (line.Length > 0)
            {
                newSentence.AppendLine(line);
            }
            Console.WriteLine(newSentence.ToString());
        }
    }
}


