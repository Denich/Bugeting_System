using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class YearComplexBudgetProjectDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<YearComplexBudgetProject> GetYearComplexBudgetProjects()
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_YearComplexBudgetProjectSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<YearComplexBudgetProject>();

                    while (reader.Read())
                    {
                        categories.Add(YearComplexBudgetProject.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public YearComplexBudgetProject GetYearComplexBudgetProjectById(int yearComplexBudgetProjectId)
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_YearComplexBudgetProjectSelect", CommandType.StoredProcedure, new SqlParameter("id", yearComplexBudgetProjectId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return YearComplexBudgetProject.Create(reader);
                }
            }
        }

        public int AddYearComplexBudgetProject(YearComplexBudgetProject yearComplexBudgetProject)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_YearComplexBudgetProjectInsert", CommandType.StoredProcedure, yearComplexBudgetProject.SqlParameters);
            }
        }

        public int UpdateYearComplexBudgetProject(YearComplexBudgetProject yearComplexBudgetProject)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_YearComplexBudgetProjectUpdate", CommandType.StoredProcedure, yearComplexBudgetProject.SqlParameters);
            }
        }

        public int DeleteYearComplexBudgetProject(int yearComplexBudgetProjectId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_YearComplexBudgetProjectDelete", CommandType.StoredProcedure, new SqlParameter("id", yearComplexBudgetProjectId));
            }
        }
    }
}
