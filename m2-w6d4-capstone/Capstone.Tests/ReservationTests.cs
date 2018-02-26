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
    public class ReservationTests
    {
        private TransactionScope tran;
        private string dbconnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private int reservation_idx;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(dbconnectionString))
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand("INSERT INTO reservation VALUES ('1', 'TestReservation', '1/1/2018', '1/2/2018', '1/1/2018'); SELECT CAST(SCOPE_IDENTITY() as INT);", conn);
                reservation_idx = (int)cmd.ExecuteScalar();
                
                //  join site on reservation.site_id = site.site_id and join campground on site.campground_id = campground.campground_id WHERE campground.campground_id = '1'

            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetReservationIdTest()
        {
            ReservationSqlDAL reservationtests = new ReservationSqlDAL(dbconnectionString);
            string testResult = reservationtests.GetReservationId("TestReservation");
            Assert.AreEqual($"TestReservation_{reservation_idx}", testResult);
        }

        
    }
}
