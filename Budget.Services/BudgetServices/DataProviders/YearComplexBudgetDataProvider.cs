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
    public class YearComplexBudgetDataProvider : IYearComplexBudgetDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<YearComplexBudget> GetYearComplexBudgets()
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_YearComplexBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<YearComplexBudget>();

                    while (reader.Read())
                    {
                        categories.Add(YearComplexBudget.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public YearComplexBudget GetYearComplexBudgetById(int yearComplexBudgetId)
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_YearComplexBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", yearComplexBudgetId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return YearComplexBudget.Create(reader);
                }
            }
        }

        public int AddYearComplexBudget(YearComplexBudget yearComplexBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_YearComplexBudgetInsert", CommandType.StoredProcedure, yearComplexBudget.SqlParameters);
            }
        }

        public int UpdateYearComplexBudget(YearComplexBudget yearComplexBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_YearComplexBudgetUpdate", CommandType.StoredProcedure, yearComplexBudget.SqlParameters);
            }
        }

        public int DeleteYearComplexBudget(int yearComplexBudgetId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_YearComplexBudgetDelete", CommandType.StoredProcedure, new SqlParameter("id", yearComplexBudgetId));
            }
        }
    }
}
