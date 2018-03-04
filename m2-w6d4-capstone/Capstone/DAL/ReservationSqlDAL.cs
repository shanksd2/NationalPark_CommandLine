using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;
        private string SQL_GetReservation = @"SELECT * FROM reservation where @name = name";
        private string SQL_BookReservation = @"INSERT INTO reservation VALUES (@site_id, @name, @arrivalDate, @departureDate, @createDate);";
        private string SQL_ShowCampReservations = @"SELECT * FROM reservation JOIN site on reservation.site_id = site.site_id Join campground on site.campground_id = campground.campground_id WHERE @campground_id = campground_id";

        public ReservationSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Reservation> GetReservationsFromCampGround(int input)
        {
            List<Reservation> existingReservations = new List<Reservation>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_ShowCampReservations, connection);
                    cmd.Parameters.AddWithValue("@campground_id", input);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation r = new Reservation();
                        r.Reservation_id = Convert.ToInt32(reader["reservation_id"]);
                        r.Site_id = Convert.ToInt32(reader["site_id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.From_date = Convert.ToDateTime(reader["from_date"]);
                        r.To_date = Convert.ToDateTime(reader["to_date"]);
                        r.Create_date = Convert.ToDateTime(reader["creat_date"]);

                        existingReservations.Add(r);
                    }
                }
                return existingReservations;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public void MakeReservation(int park_id, int site_id, string reserveName, string arrivalDate, string departureDate)
        {
            Reservation r = new Reservation();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(SQL_BookReservation, connection);
                    cmd.Parameters.AddWithValue("@site_id", site_id);
                    cmd.Parameters.AddWithValue("@name", reserveName);
                    cmd.Parameters.AddWithValue("@arrivalDate", Convert.ToDateTime(arrivalDate));
                    cmd.Parameters.AddWithValue("departureDate", Convert.ToDateTime(departureDate));
                    cmd.Parameters.AddWithValue("@createDate", DateTime.Today);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        r.Reservation_id = Convert.ToInt32(reader["reservation_id"]);
                        r.Site_id = Convert.ToInt32(reader["site_id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.From_date = Convert.ToDateTime(reader["from_date"]);
                        r.To_date = Convert.ToDateTime(reader["to_date"]);
                        r.Create_date = Convert.ToDateTime(reader["create_date"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public string GetReservationId(string name)
        {
            Reservation r = new Reservation();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(SQL_GetReservation, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        r.Reservation_id = Convert.ToInt32(reader["reservation_id"]);
                        r.Site_id = Convert.ToInt32(reader["site_id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.From_date = Convert.ToDateTime(reader["from_date"]);
                        r.To_date = Convert.ToDateTime(reader["to_date"]);
                        r.Create_date = Convert.ToDateTime(reader["create_date"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return r.Name + "_" + r.Reservation_id;
        }
    }
}

