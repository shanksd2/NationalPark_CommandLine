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
    public class ProjectSqlDAL_Test
    {
        private TransactionScope tran;
        // string DatabaseConnection = ConfigurationManager.ConnectionStrings["ProjectDatabaseConnection"].ConnectionString;
        private string connectionString = @"Data Source=localhost\SQLexpress;Initial Catalog=project;Integrated Security=True";
        private int numOfProjects = 0;
        private int projectId = 0;
        private int employeeId = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM project;", connection);
                numOfProjects = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO project VALUES ('Test Project', '2017-1-5', '2019-4-5'); SELECT CAST(SCOPE_IDENTITY() as INT);", connection);
                projectId = (int)cmd.ExecuteScalar();

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
        public void GetAllProjects()
        {

            ProjectSqlDAL projectSqlDal = new ProjectSqlDAL(connectionString);
            List<Project> projectNames = projectSqlDal.GetAllProjects();
            Assert.IsNotNull(projectNames);
            Assert.AreEqual(numOfProjects, projectNames.Count - 1);
        }

        [TestMethod]
        public void CreateProject()
        {
            ProjectSqlDAL projectSqlDal = new ProjectSqlDAL(connectionString);
            Project project = new Project
            {
                Name = "Test Dept",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            bool didWork = projectSqlDal.CreateProject(project);
            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void AssignToProject()
        {
            ProjectSqlDAL projectSqlDal = new ProjectSqlDAL(connectionString);
            bool didWork = projectSqlDal.AssignEmployeeToProject(projectId, employeeId);
            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void RemoveFromProject()
        {
            ProjectSqlDAL projectSqlDal = new ProjectSqlDAL(connectionString);
            bool didWork = projectSqlDal.AssignEmployeeToProject(projectId, employeeId);
            bool wasRemoved = false;
            if(didWork)
            {
                wasRemoved = projectSqlDal.RemoveEmployeeFromProject(projectId, employeeId);
            }
            Assert.AreEqual(true, wasRemoved);
        }
    }
}
