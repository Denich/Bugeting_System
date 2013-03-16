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
    class MonthComplexBudgetDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<MonthComplexBudget> GetMonthComplexBudgets()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_MonthComplexBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<MonthComplexBudget>();

                    while (reader.Read())
                    {
                        categories.Add(MonthComplexBudget.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public MonthComplexBudget GetMonthComplexBudgetById(int monthComplexBudgetId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_MonthComplexBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", monthComplexBudgetId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return MonthComplexBudget.Create(reader);
                }
            }
        }

        public int AddMonthComplexBudget(MonthComplexBudget monthComplexBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_MonthComplexBudgetInsert", CommandType.StoredProcedure, monthComplexBudget.SqlParameters);
            }
        }

        public int UpdateMonthComplexBudget(MonthComplexBudget monthComplexBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_MonthComplexBudgetUpdate", CommandType.StoredProcedure, monthComplexBudget.SqlParameters);
            }
        }

        public int DeleteMonthComplexBudget(int monthComplexBudgetId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_MonthComplexBudgetDelete", CommandType.StoredProcedure, new SqlParameter("id", monthComplexBudgetId));
            }
        }
    }
}
