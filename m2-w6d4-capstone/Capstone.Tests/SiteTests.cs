using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using System.Configuration;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteSqlDAL_tests
    {

        private TransactionScope tran;
        private string dbconnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

        private int park_id;
        private int site_id;
        private int campground_idx;


        //[TestInitialize]
        //public void Initialize()
        //{
        //    tran = new TransactionScope();

        //    using (SqlConnection conn = new SqlConnection(dbconnectionString))
        //    {
        //        SqlCommand cmd;
        //        conn.Open();
        //        cmd = new SqlCommand("INSERT INTO park VALUES ('test_park', 'test_location', '2018-2-2', 300, 1, 'This is a Test Description'); SELECT CAST(SCOPE_IDENTITY() as INT);", conn);
        //        cmd.ExecuteNonQuery();

        //        cmd = new SqlCommand("INSERT INTO campground VALUES (1000, 'CampTest', 1, 5, 20.00); SELECT CAST(SCOPE_IDENTITY() as INT);", conn);
        //        campground_idx = (int)cmd.ExecuteScalar();

        //        cmd = new SqlCommand("INSERT INTO site VALUES (campground_idx, 1000, 1000, 0, 9, 1); SELECT CAST(SCOPE_IDENTITY() as INT);", conn);
        //        site_id = (int)cmd.ExecuteScalar();
        //    }
        //}

        //[TestCleanup]
        //public void CleanUp()
        //{
        //    tran.Dispose();
        //}

        [TestMethod]
        public void GetSiteID()
        {
            SiteSqlDAL siteSqlDAL = new SiteSqlDAL(dbconnectionString);

            int testSite = siteSqlDAL.GetSiteID(4, 1);
            Assert.AreNotEqual(testSite, 0);
            Assert.AreEqual(4, testSite, "for CG 1, SN 4");

            int testSite2 = siteSqlDAL.GetSiteID(12, 2);
            Assert.AreEqual(24, testSite2, "for CG 2, SN 12");

            //int testSite3 = siteSqlDAL.GetSiteID(1000, 1000);
            //Assert.AreEqual(45, testSite3, "for CG 1000, SN 1000");
        }

        [TestMethod]
        public void ListCampGroundSites()
        {
            SiteSqlDAL siteSqlDAL = new SiteSqlDAL(dbconnectionString);

            List<Site> testSite = new List<Site>();
            testSite = siteSqlDAL.ListCampGroundSites(3);

            Assert.IsNotNull(testSite);
            Assert.AreEqual(12, testSite.Count, "For CampGround 3");

            List<Site> testSite2 = new List<Site>();
            testSite2 = siteSqlDAL.ListCampGroundSites(5);
            Assert.AreEqual(1, testSite2.Count, "For CampGround 5");

            List<Site> testSite3 = new List<Site>();
            testSite3 = siteSqlDAL.ListCampGroundSites(7);
            Assert.AreEqual(5, testSite3.Count, "For CampGround 7");
        }

        [TestMethod]
        public void PrintCost()
        {
            SiteSqlDAL siteSqlDAL = new SiteSqlDAL(dbconnectionString);
            List<Site> testSiteList = siteSqlDAL.ListCampGroundSites(3);


            string siteCost = siteSqlDAL.PrintCost(testSiteList[0], "3/3/2018", "3/5/2018");
            Assert.AreEqual("$60.00", siteCost);

        }

        [TestMethod]
        public void AnyAvailable()
        {
            SiteSqlDAL siteSqlDAL = new SiteSqlDAL(dbconnectionString);
            List<Site> testSiteList = siteSqlDAL.ReservationAvailable(4, "3/3/2018", "3/5/2018");
            Assert.AreNotEqual(testSiteList.Count, 0);
        }

        [TestMethod]
        public void ParkwideReservation()
        {
            SiteSqlDAL testSite = new SiteSqlDAL(dbconnectionString);
            List<Site> testListSites = new List<Site>();
            testListSites = testSite.ReservationAvailable("3/3/2018", "3/5/2018", 2);
            Assert.AreNotEqual(0, testListSites.Count);

            testListSites = testSite.ReservationAvailable("3/3/2018", "3/5/2018", 3);
            Assert.AreEqual(0, testListSites.Count);
        }

        [TestMethod]
        public void ReservationPerCampground()
        {
            SiteSqlDAL testSite = new SiteSqlDAL(dbconnectionString);
            List<Site> testListSites = new List<Site>();
            testListSites = testSite.ReservationAvailable("3/3/2018", "3/5/2018", 2);
            Assert.AreNotEqual(0, testListSites.Count);

            testListSites = testSite.ReservationAvailable("3/3/2018", "3/5/2018", 3);
            Assert.AreEqual(0, testListSites.Count);
        }
    }
}
