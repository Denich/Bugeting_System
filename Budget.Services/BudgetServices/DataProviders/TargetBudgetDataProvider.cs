using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class TargetBudgetDataProvider : ITargetBudgetDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<TargetBudget> GetTargetBudgets()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_TargetBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<TargetBudget>();

                    while (reader.Read())
                    {
                        categories.Add(TargetBudget.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public TargetBudget GetTargetBudgetById(int targetBudgetId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_TargetBudgetSelect", CommandType.StoredProcedure, new SqlParameter("id", targetBudgetId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return TargetBudget.Create(reader);
                }
            }
        }

        public int AddTargetBudget(TargetBudget targetBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_TargetBudgetInsert", CommandType.StoredProcedure, targetBudget.SqlParameters);
            }
        }

        public int UpdateTargetBudget(TargetBudget targetBudget)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_TargetBudgetUpdate", CommandType.StoredProcedure, targetBudget.SqlParameters);
            }
        }

        public int DeleteTargetBudget(int targetBudgetId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_TargetBudgetDelete", CommandType.StoredProcedure, new SqlParameter("id", targetBudgetId));
            }
        }
    }
}