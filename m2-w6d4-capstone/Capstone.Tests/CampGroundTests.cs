﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class CampGroundTests
    {
        private TransactionScope tran;
        private string dbconnectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private int park_id;
        private int campground_count;
        private int campground_idx;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(dbconnectionString))
            {
                SqlCommand cmd;
                conn.Open();
                //cmd = new SqlCommand("INSERT INTO campground VALUES ('100', '100', 'CampTest', '1', '5', '20.00'); SELECT CAST(SCOPE_IDENTITY() as INT);", conn);
                //campground_idx = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(name) FROM campground;", conn);
                campground_count = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetParkCampGroundTest()
        {
            CampGroundSqlDAL camptest = new CampGroundSqlDAL(dbconnectionString);
            List<Campground> camp = camptest.GetParkCampGround();
            Assert.IsNotNull(camp);
            Assert.AreEqual(campground_count, camp.Count);

        }
    }
}