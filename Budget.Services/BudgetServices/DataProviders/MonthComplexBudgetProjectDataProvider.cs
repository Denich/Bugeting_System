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
    public class MonthComplexBudgetProjectDataProvider : IMonthComplexBudgetProjectDataProvider
    {
         private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<MonthComplexBudgetProject> GetMonthComplexBudgetProjects()
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_MonthComplexBudgetProjectSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<MonthComplexBudgetProject>();

                    while (reader.Read())
                    {
                        categories.Add(MonthComplexBudgetProject.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public MonthComplexBudgetProject GetMonthComplexBudgetProjectById(int monthComplexBudgetProjectId)
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_MonthComplexBudgetProjectSelect", CommandType.StoredProcedure, new SqlParameter("id", monthComplexBudgetProjectId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return MonthComplexBudgetProject.Create(reader);
                }
            }
        }

        public int AddMonthComplexBudgetProject(MonthComplexBudgetProject monthComplexBudgetProject)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_MonthComplexBudgetProjectInsert", CommandType.StoredProcedure, monthComplexBudgetProject.SqlParameters);
            }
        }

        public int UpdateMonthComplexBudgetProject(MonthComplexBudgetProject monthComplexBudgetProject)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_MonthComplexBudgetProjectUpdate", CommandType.StoredProcedure, monthComplexBudgetProject.SqlParameters);
            }
        }

        public int DeleteMonthComplexBudgetProject(int monthComplexBudgetProjectId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_MonthComplexBudgetProjectDelete", CommandType.StoredProcedure, new SqlParameter("id", monthComplexBudgetProjectId));
            }
        }
    }
}
