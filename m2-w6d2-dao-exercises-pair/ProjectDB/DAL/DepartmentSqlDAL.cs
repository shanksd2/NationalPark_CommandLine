using ProjectDB.Models;
using System;
using System.Collections.Generic;

namespace ProjectDB.DAL
{
    public class DepartmentSqlDAL
    {
        private string connectionString;

        /// Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Department> GetDepartments()
        {
            throw new NotImplementedException();
        }

        public bool CreateDepartment(Department newDepartment)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDepartment(Department updatedDepartment)
        {
            throw new NotImplementedException();
        }
    }
}
