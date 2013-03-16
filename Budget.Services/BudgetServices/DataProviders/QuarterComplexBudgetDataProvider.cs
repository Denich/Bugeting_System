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
    public class QuarterComplexBudgetDataProvider : IQuarterComplexBudgetDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<QuarterComplexBudget> GetQuarterComplexBudgets()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_QuarterComplexBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<QuarterComplexBudget>();

                    while (reader.Read())
                    {
                        categories.Add(QuarterComplexBudget.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public QuarterComplexBudget GetQuarterComplexBudgetById(int quarterComplexBudgetId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_QuarterComplexBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", quarterComplexBudgetId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return QuarterComplexBudget.Create(reader);
                }
            }
        }

        public int AddQuarterComplexBudget(QuarterComplexBudget quarterComplexBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_QuarterComplexBudgetInsert", CommandType.StoredProcedure, quarterComplexBudget.SqlParameters);
            }
        }

        public int UpdateQuarterComplexBudget(QuarterComplexBudget quarterComplexBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_QuarterComplexBudgetUpdate", CommandType.StoredProcedure, quarterComplexBudget.SqlParameters);
            }
        }

        public int DeleteQuarterComplexBudget(int quarterComplexBudgetId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_QuarterComplexBudgetDelete", CommandType.StoredProcedure, new SqlParameter("id", quarterComplexBudgetId));
            }
        }
    }
}
