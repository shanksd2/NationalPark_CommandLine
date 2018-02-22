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
    public class ParkSqlDAL_test
    {
        private TransactionScope tran;
        private string dbconnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private int park_id;
        private int park_num;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(dbconnectionString))
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand("INSERT INTO park VALUES ('test_park', 'test_location', '2018-2-2', '300', '1', 'This is a Test Description This is a Test Description This is a Test Description This is a Test Description This is a Test Description'); SELECT CAST(SCOPE_IDENTITY() as INT);", conn);
                park_id = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(name) FROM park;", conn);
                park_num = (int)cmd.ExecuteScalar();
            }
        }
        
        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void ListAllParks()
        {
            ParkSqlDAL parkSqlDAL = new ParkSqlDAL(dbconnectionString);

            List<Park> testPark = new List<Park>();
            testPark = parkSqlDAL.ListAllParkNames();

            Assert.IsNotNull(testPark);
            Assert.AreEqual(park_num, testPark.Count);

        }

        [TestMethod]
        public void GetParkDetails()
        {
            ParkSqlDAL parkSqlDAL = new ParkSqlDAL(dbconnectionString);

            Park testPark = new Park();
            testPark = parkSqlDAL.GetParkDetails("test_park");

            Assert.IsNotNull(testPark);
            Assert.AreEqual("test_location", testPark.Location);

        }
    }
}
