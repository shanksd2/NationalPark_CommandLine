using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class ReservationSqlDAL
    {
        private string connectionString;
        private string SQL_GetReservations = @"SELECT * FROM reservations";
        private string SQL_ShowSiteReservations = @"SELECT * FROM reservation WHERE @site_id = site_id";
        private string SQL_ShowCampReservations = @"SELECT * FROM reservation JOIN site on reservation.site_id = site.site_id Join campground on site.campground_id = campground.campground_id WHERE @campgroud_id = campground_id";

        public ReservationSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        //public List<Reservation> GetReservationsFromSite(int input)
        //{
        //    List<Reservation> existingReservations = new List<Reservation>();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            SqlCommand cmd = new SqlCommand(SQL_ShowSiteReservations, connection);
        //            cmd.Parameters.AddWithValue("@site_id", input);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Reservation r = new Reservation();
        //                r.Reservation_id = Convert.ToInt32(reader["reservation_id"]);
        //                r.Site_id = Convert.ToInt32(reader["site_id"]);
        //                r.Name = Convert.ToString(reader["name"]);
        //                r.From_date = Convert.ToDateTime(reader["from_date"]);
        //                r.To_date = Convert.ToDateTime(reader["to_date"]);
        //                r.Create_date = Convert.ToDateTime(reader["creat_date"]);

        //                existingReservations.Add(r);
        //            }
        //        }
        //        return existingReservations;
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }
        //}

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
    }
}

