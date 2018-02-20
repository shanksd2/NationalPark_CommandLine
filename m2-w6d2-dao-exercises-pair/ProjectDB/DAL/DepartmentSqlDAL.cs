using ProjectDB.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjectDB.DAL
{

    public class DepartmentSqlDAL
    {
        private const string SQL_GetDepartments = @"SELECT name, department_id FROM department";
        private const string SQL_CreateDepartment = @"INSERT INTO department VALUES (@name)";
        private const string SQL_UpdateDepartment = @"UPDATE department SET name = @name WHERE department_id = @department_id";
        private string connectionString;

        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Department> GetDepartments()
        {
            List<Department> output = new List<Department>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetDepartments;
                    cmd.Connection = connection;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Department departmentName = new Department();
                        departmentName.Id = Convert.ToInt32(reader["department_id"]);
                        departmentName.Name = Convert.ToString(reader["name"]);
                        output.Add(departmentName);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new NotImplementedException(); 
            }
            return output;
        }

        public bool CreateDepartment(Department newDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateDepartment, conn);
                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw new NotImplementedException();
            }
        }

        public bool UpdateDepartment(Department updatedDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_UpdateDepartment, conn);
                    cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);
                    cmd.Parameters.AddWithValue("@department_id", updatedDepartment.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
