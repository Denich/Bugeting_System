using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class EmployeContactDataProvider : IEmployeContactDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<EmployeContact> GetEmployeContacts()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_EmployeContactSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<EmployeContact>();

                    while (reader.Read())
                    {
                        categories.Add(EmployeContact.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public EmployeContact GetEmployeContactById(int employeContactId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_EmployeContactSelect", CommandType.StoredProcedure, new SqlParameter("id", employeContactId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return EmployeContact.Create(reader);
                }
            }
        }

        public int AddEmployeContact(EmployeContact employeContact)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_EmployeContactInsert", CommandType.StoredProcedure, employeContact.SqlParameters);
            }
        }

        public int UpdateEmployeContact(EmployeContact employeContact)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_EmployeContactUpdate", CommandType.StoredProcedure, employeContact.SqlParameters);
            }
        }

        public int DeleteEmployeContact(int employeContactId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_EmployeContactDelete", CommandType.StoredProcedure, new SqlParameter("id", employeContactId));
            }
        }
    }
}
