using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class SiteSqlDAL
    {
        private string connectionString;
        private string SQL_GetSites = @"SELECT * FROM site WHERE @campground_id = campground_id";
        private string SQL_AvailibilityEntirePark = @"SELECT DISTINCT (site.site_id), site.campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities, campground.open_from_mm, campground.open_to_mm, campground.daily_fee 
                                            FROM site
                                            INNER JOIN reservation ON site.site_id = reservation.site_id
                                            INNER JOIN campground ON site.campground_id = campground.campground_id
                                            WHERE (campground.park_id = @park_id)
                                            AND (reservation.from_date NOT BETWEEN @startDate AND @endDate)
                                            AND (reservation.to_date NOT BETWEEN @startDate AND @endDate)
                                            AND (@startDate NOT BETWEEN reservation.from_date AND reservation.to_date) AND (@endDate NOT BETWEEN reservation.from_date AND reservation.to_date)";
        private string SQL_PrintPrice = @"Select daily_fee From campground join site on site.campground_id = campground.campground_id where site.campground_id = @campground_id;";
        private string SQL_Availibility = @"SELECT DISTINCT TOP 5(site.site_id), site.campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities, campground.open_from_mm, campground.open_to_mm, campground.daily_fee 
                                            FROM site
                                            INNER JOIN reservation ON site.site_id = reservation.site_id
                                            INNER JOIN campground ON site.campground_id = campground.campground_id
                                            WHERE (site.campground_id = @campground_id)
                                            AND (reservation.from_date NOT BETWEEN @startDate AND @endDate)
                                            AND (reservation.to_date NOT BETWEEN @startDate AND @endDate)
                                            AND (@startDate NOT BETWEEN reservation.from_date AND reservation.to_date) AND (@endDate NOT BETWEEN reservation.from_date AND reservation.to_date)";

        public SiteSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public int GetSiteID(int site_number, int camp_id)
        {
            SiteSqlDAL checkForID = new SiteSqlDAL(connectionString);
            List<Site> findID = checkForID.ListCampGroundSites(camp_id);
            int siteID = 0;
            foreach (Site s in findID)
            {
                if (s.Site_number == site_number)
                {
                    siteID = s.Site_id;
                }
            }
            return siteID;
        }

        public List<Site> ListCampGroundSites(int input)
        {
            List<Site> sitesInCamp = new List<Site>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSites, connection);
                    cmd.Parameters.AddWithValue("@campground_id", input);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.Site_id = Convert.ToInt32(reader["site_id"]);
                        s.Campground_id = Convert.ToInt32(reader["campground_id"]);
                        s.Site_number = Convert.ToInt32(reader["site_number"]);
                        s.Max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToBoolean(reader["accessible"]);
                        s.Max_RV_length = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sitesInCamp.Add(s);
                    }
                }
                return sitesInCamp;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Site> ReservationAvailable(int camp_id, string date1, string date2)
        {
            SiteSqlDAL reservationLookUp = new SiteSqlDAL(connectionString);
            List<Site> sitesAvailable = new List<Site>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_Availibility, connection);
                    cmd.Parameters.AddWithValue("@campground_id", camp_id);
                    cmd.Parameters.AddWithValue("@startDate", date1);
                    cmd.Parameters.AddWithValue("@endDate", date2);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.Site_id = Convert.ToInt32(reader["site_id"]);
                        s.Campground_id = Convert.ToInt32(reader["campground_id"]);
                        s.Site_number = Convert.ToInt32(reader["site_number"]);
                        s.Max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToBoolean(reader["accessible"]);
                        s.Max_RV_length = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sitesAvailable.Add(s);
                    }
                }
                return sitesAvailable;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Site> ReservationAvailable(string date1, string date2, int park_id)
        {
            SiteSqlDAL reservationLookUp = new SiteSqlDAL(connectionString);
            List<Site> sitesAvailable = new List<Site>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AvailibilityEntirePark, connection);
                    cmd.Parameters.AddWithValue("@park_id", park_id);
                    cmd.Parameters.AddWithValue("@startDate", date1);
                    cmd.Parameters.AddWithValue("@endDate", date2);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.Site_id = Convert.ToInt32(reader["site_id"]);
                        s.Campground_id = Convert.ToInt32(reader["campground_id"]);
                        s.Site_number = Convert.ToInt32(reader["site_number"]);
                        s.Max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToBoolean(reader["accessible"]);
                        s.Max_RV_length = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sitesAvailable.Add(s);
                    }
                }
                return sitesAvailable;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public string PrintCost(Site site, string arrival, string departure)
        {
            string output = string.Empty;
            DateTime arrivalDateDT = Convert.ToDateTime(arrival);
            DateTime departureDateDT = Convert.ToDateTime(departure);
            int totalDuration_Days = (departureDateDT.Date - arrivalDateDT.Date).Days;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_PrintPrice, connection);
                    cmd.Parameters.AddWithValue("@campground_id", site.Campground_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        decimal cost = Convert.ToDecimal(reader["daily_fee"]);
                        cost *= totalDuration_Days;
                        output = cost.ToString("C2");
                        
                    }
                }    
               }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

        public bool AnyAvailable(List<Site> siteList)
        {
            if(siteList.Count >= 1)
            {
                return true;
            }
            return false;
        }
    }
}
