using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampGroundSqlDAL
    {
        private string connectionString;
        private string SQL_GetCampGrounds = @"SELECT * FROM campground;";

        public CampGroundSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Campground> GetParkCampGround()
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetCampGrounds, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground();
                        c.Campground_id = Convert.ToInt32(reader["campground_id"]);
                        c.Park_id = Convert.ToInt32(reader["park_id"]);
                        c.Name = Convert.ToString(reader["name"]);
                        c.Open_from_mm = Convert.ToInt32(reader["open_from_mm"]);
                        c.Open_to_mm = Convert.ToInt32(reader["open_to_mm"]);
                        c.Daily_fee = Convert.ToDecimal(reader["daily_fee"]);

                        output.Add(c);
                    }


                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return output;
        }

    }
}
