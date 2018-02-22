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

namespace ProjectDB_Test
{
    [TestClass]
    public class EmployeeSqlDAL_Test
    {

        private TransactionScope tran;
        // string DatabaseConnection = ConfigurationManager.ConnectionStrings["ProjectDatabaseConnection"].ConnectionString;
        private string connectionString = @"Data Source=localhost\SQLexpress;Initial Catalog=project;Integrated Security=True";
        private int numOfEmployee = 0;
        private int employeeId = 0;
        private string firstName = "Joe";
        private string lastName = "Testman";



        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM employee;", connection);
                numOfEmployee = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO employee VALUES ('3', 'Joe', 'Testman', 'Director of Testcology', '1955-5-5', 'M', '2000-6-7'); SELECT CAST(SCOPE_IDENTITY() as INT);", connection);
                employeeId = (int)cmd.ExecuteScalar();
                
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }



        [TestMethod]
        public void ShowEmployees()
        {
            EmployeeSqlDAL employeeSqlDal = new EmployeeSqlDAL(connectionString);
            List<Employee> employees = employeeSqlDal.GetAllEmployees();
            Assert.IsNotNull(employees);
            Assert.AreEqual(numOfEmployee, employees.Count-1);

        }

        [TestMethod]
        public void SearchEmployee()
        {
            EmployeeSqlDAL employeeSqlDal = new EmployeeSqlDAL(connectionString);
            List<Employee> employees = employeeSqlDal.GetAllEmployees();
            employees = employeeSqlDal.Search("Joe", "Testman");

            Assert.AreEqual(employeeId, employees[0].EmployeeId);
            Assert.AreEqual("Joe", employees[0].FirstName);
        }

        [TestMethod]
        public void GetEmployeesWOProjects()
        {
            EmployeeSqlDAL employeeSqlDal = new EmployeeSqlDAL(connectionString);
            List<Employee> employees = employeeSqlDal.GetEmployeesWithoutProjects();
           
            bool testbool = false;
          for ( int i = 0; i< employees.Count; i++)
            {
                if (employees[i].EmployeeId == employeeId)
                { testbool = true; }
            }
            Assert.AreEqual(true, testbool);
               
        }

    
    }
}
