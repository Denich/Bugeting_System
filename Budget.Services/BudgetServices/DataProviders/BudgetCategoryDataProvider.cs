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
    public class BudgetCategoryDataProvider : IBudgetCategoryDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<BudgetCategory> GetBudgetCategorieInfos()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetCategorySelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<BudgetCategory>();

                    while (reader.Read())
                    {
                        categories.Add(GetBudgetCategoryFromReader(reader));
                    }

                    return categories;
                }
            }
        }

        public BudgetCategory GetBudgetCategoryById(int budgetCategoryId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetCategorySelect", CommandType.StoredProcedure, new SqlParameter("id", budgetCategoryId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return GetBudgetCategoryFromReader(reader);
                }
            }
        }

        public int AddBudgetCategory(BudgetCategory budgetCategory)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetCategoryInsert", CommandType.StoredProcedure, GetSqlParametersFromBudgetCategory(budgetCategory));
            }
        }

        public int UpdateBudgetCategory(BudgetCategory budgetCategory)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetCategoryUpdate", CommandType.StoredProcedure, GetSqlParametersFromBudgetCategory(budgetCategory));
            }
        }

        public int DeleteBudgetCategory(int budgetCategoryId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetCategoryDelete", CommandType.StoredProcedure, new SqlParameter("id", budgetCategoryId));
            }
        }

        private BudgetCategory GetBudgetCategoryFromReader(SqlDataReader reader)
        {
            return new BudgetCategory
            {
                Id = Convert.ToInt32(reader["Id"]),
                InfoId = Convert.ToInt32(reader["InfoId"]),
                Value = Convert.ToDouble(reader["Value"]),
                ResponsibleEmployeId = Convert.ToInt32(reader["ResponsibleEmployeId"]),
                ComplexBudgetId = Convert.ToInt32(reader["ComplexBudgetId"])
            };
        }

        private SqlParameter[] GetSqlParametersFromBudgetCategory(BudgetCategory budgetCategory)
        {
            var sqlParams = new List<SqlParameter>
                {
                    new SqlParameter("InfoId", SqlHelper.GetSqlValue(budgetCategory.InfoId)),
                    new SqlParameter("Value", SqlHelper.GetSqlValue(budgetCategory.Value)),
                    new SqlParameter("ResponsibleEmployeId", SqlHelper.GetSqlValue(budgetCategory.ResponsibleEmployeId)),
                    new SqlParameter("ComplexBudgetId", SqlHelper.GetSqlValue(budgetCategory.ComplexBudgetId)),
                };

            //note: it budget category info center is new, its id = 0
            if (budgetCategory.Id != 0)
            {
                sqlParams.Add(new SqlParameter("Id", budgetCategory.Id));
            }

            return sqlParams.ToArray();
        }
    }
}
