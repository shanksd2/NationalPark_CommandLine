using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class EmployeeSqlDAL
    {
        private string connectionString;
        private const string GetAllEmployeesplz = @"SELECT * FROM employee";
        private const string SQL_SearchEmployeeDB = @"SELECT * FROM employee WHERE first_name = @firstname AND last_name = @lastname";
        private const string SQL_EmployeesWithoutProj = @"SELECT * FROM employee LEFT JOIN project_employee on employee.employee_id = project_employee.employee_id WHERE project_employee.project_id IS NULL";
        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> allEmployees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetAllEmployeesplz, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        allEmployees.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new NotImplementedException();
            }
            return allEmployees;
        }

        public List<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employeeSearch = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SearchEmployeeDB, conn);

                    cmd.Parameters.AddWithValue("@firstname", firstname);
                    cmd.Parameters.AddWithValue("@lastname", lastname);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        employeeSearch.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new NotImplementedException();
            }
            return employeeSearch;
        }

        public List<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> WithoutProj = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_EmployeesWithoutProj, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.FirstName = Convert.ToString(reader["first_name"]);
                        employee.LastName = Convert.ToString(reader["last_name"]);
                        employee.Gender = Convert.ToString(reader["gender"]);
                        employee.JobTitle = Convert.ToString(reader["job_title"]);
                        employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        WithoutProj.Add(employee);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new NotImplementedException();
            }
            return WithoutProj;
        }
    }
}
