using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class BudgetCategoryInfoDataProvider : IBudgetCategoryInfoDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<BudgetCategoryInfo> GetBudgetCategorieInfos()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetCategoryInfoSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<BudgetCategoryInfo>();

                    while (reader.Read())
                    {
                        categories.Add(GetBudgetCategoryInfoFromReader(reader));
                    }

                    return categories;
                }
            }
        }

        public BudgetCategoryInfo GetBudgetCategoryInfoById(int budgetCategoryInfoId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetCategoryInfoSelect", CommandType.StoredProcedure, new SqlParameter("id", budgetCategoryInfoId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return GetBudgetCategoryInfoFromReader(reader);
                }
            }
        }

        public int AddBudgetCategoryInfo(BudgetCategoryInfo budgetCategoryInfo)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetCategoryInfoInsert", CommandType.StoredProcedure, GetSqlParametersFromBudgetCategoryInfo(budgetCategoryInfo));
            }
        }

        public int UpdateBudgetCategoryInfo(BudgetCategoryInfo budgetCategoryInfo)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetCategoryInfoUpdate", CommandType.StoredProcedure, GetSqlParametersFromBudgetCategoryInfo(budgetCategoryInfo));
            }
        }

        public int DeleteBudgetCategoryInfo(int budgetCategoryInfoId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetCategoryInfoDelete", CommandType.StoredProcedure, new SqlParameter("id", budgetCategoryInfoId));
            }
        }

        private BudgetCategoryInfo GetBudgetCategoryInfoFromReader(SqlDataReader reader)
        {
            return new BudgetCategoryInfo()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                Description = Convert.ToString(reader["Description"]),
                
            };
        }

        private SqlParameter[] GetSqlParametersFromBudgetCategoryInfo(BudgetCategoryInfo budgetCategoryInfo)
        {
            var sqlParams = new List<SqlParameter>
                {
                    new SqlParameter("Name", SqlHelper.GetSqlValue(budgetCategoryInfo.Name)),
                    new SqlParameter("Description", SqlHelper.GetSqlValue(budgetCategoryInfo.Description)),
                    new SqlParameter("IsDeleted", SqlHelper.GetSqlValue(budgetCategoryInfo.IsDeleted)),
                };

            //note: it budget category info center is new, its id = 0
            if (budgetCategoryInfo.Id != 0)
            {
                sqlParams.Add(new SqlParameter("Id", budgetCategoryInfo.Id));
            }

            return sqlParams.ToArray();
        }
    }
}
