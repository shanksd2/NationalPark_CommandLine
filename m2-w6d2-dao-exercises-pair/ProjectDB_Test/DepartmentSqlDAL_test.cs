using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.DAL;
using ProjectDB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;



namespace ProjectDB.DAL.Test
{
    [TestClass]
    public class DepartmentSqlDAL_test
    {
        private TransactionScope tran;
        // string DatabaseConnection = ConfigurationManager.ConnectionStrings["ProjectDatabaseConnection"].ConnectionString;
        private string connectionString = @"Data Source=localhost\SQLexpress;Initial Catalog=project;Integrated Security=True";
        private int numOfDept = 0;
        private int deptId = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM Department;", connection);
                numOfDept = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO department VALUES ('Fake Department'); SELECT CAST(SCOPE_IDENTITY() as INT);", connection);
                deptId = (int) cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }



        [TestMethod]
        public void GetDept()
        {
            DepartmentSqlDAL deptSqlDal = new DepartmentSqlDAL(connectionString);
            List<Department> deptNames = deptSqlDal.GetDepartments();
            Assert.IsNotNull(deptNames);
            Assert.AreEqual(numOfDept, deptNames.Count - 1);
        }


        [TestMethod]
        public void CreateDept()
        {
            DepartmentSqlDAL deptSqlDal = new DepartmentSqlDAL(connectionString);
            Department dept = new Department
            {
                Name = "Test Dept"
            };
            bool didWork = deptSqlDal.CreateDepartment(dept);     
            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void UpdateDept()
        {
            DepartmentSqlDAL deptSqlDal = new DepartmentSqlDAL(connectionString);
            Department dept = new Department
            {
                Name = "Test Dept",
                Id = deptId
            };
            bool didWork = deptSqlDal.UpdateDepartment(dept);
            Assert.AreEqual(true, didWork);
        }


    }
}
