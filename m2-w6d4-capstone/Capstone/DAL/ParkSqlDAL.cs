using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;
        private string SQL_GetParkNames = @"SELECT name FROM park ORDER BY name";
        private string SQL_DetailParks = @"SELECT * FROM park WHERE name = @name";

        public ParkSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public int GetParkID(int input)
        {
            ParkSqlDAL toFindID = new ParkSqlDAL(connectionString);
            List<Park> findID = toFindID.ListAllParkNames();
            Park lookingFor = new Park();
            try
            {
                lookingFor = toFindID.GetParkDetails(findID[input-1].Name);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Invalid input.  Please try again");
         
            }

            return lookingFor.Park_id;
        }


        public List<Park> ListAllParkNames()
        {
            List<Park> output = new List<Park>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetParkNames;
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.Name = Convert.ToString(reader["name"]);
                        output.Add(p);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

        public Park GetParkDetails(string input)
        {
            Park p = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_DetailParks, conn);
                    cmd.Parameters.AddWithValue("@name", input);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        p.Park_id = Convert.ToInt32(reader["park_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.Establish_date = Convert.ToDateTime(reader["establish_date"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.Visitors = Convert.ToInt32(reader["visitors"]);
                        p.Description = Convert.ToString(reader["description"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return p;
        }
    }
}
