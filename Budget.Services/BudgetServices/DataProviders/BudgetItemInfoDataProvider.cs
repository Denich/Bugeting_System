using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class BudgetItemInfoDataProvider : IBudgetItemInfoDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<BudgetItemInfo> GetBudgetItemInfos()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetItemInfoSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<BudgetItemInfo>();

                    while (reader.Read())
                    {
                        categories.Add(GetBudgetItemInfoFromReader(reader));
                    }

                    return categories;
                }
            }
        }

        public BudgetItemInfo GetBudgetItemInfoById(int budgetItemInfoId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetItemInfoSelect", CommandType.StoredProcedure, new SqlParameter("id", budgetItemInfoId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return GetBudgetItemInfoFromReader(reader);
                }
            }
        }

        public int AddBudgetItemInfo(BudgetItemInfo budgetItemInfo)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetItemInfoInsert", CommandType.StoredProcedure, GetSqlParametersFromBudgetItemInfo(budgetItemInfo));
            }
        }

        public int UpdateBudgetItemInfo(BudgetItemInfo budgetItemInfo)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetItemInfoUpdate", CommandType.StoredProcedure, GetSqlParametersFromBudgetItemInfo(budgetItemInfo));
            }
        }

        public int DeleteBudgetItemInfo(int budgetItemInfoId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetItemInfoDelete", CommandType.StoredProcedure, new SqlParameter("id", budgetItemInfoId));
            }
        }

        private BudgetItemInfo GetBudgetItemInfoFromReader(SqlDataReader reader)
        {
            return new BudgetItemInfo()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                TargetBudgetId = Convert.ToInt32(reader["TargetBudgetId"]),
                IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                Description = Convert.ToString(reader["Description"]),

            };
        }

        private SqlParameter[] GetSqlParametersFromBudgetItemInfo(BudgetItemInfo budgetItemInfo)
        {
            var sqlParams = new List<SqlParameter>
                {
                    new SqlParameter("Name", SqlHelper.GetSqlValue(budgetItemInfo.Name)),
                    new SqlParameter("Description", SqlHelper.GetSqlValue(budgetItemInfo.Description)),
                    new SqlParameter("IsDeleted", SqlHelper.GetSqlValue(budgetItemInfo.IsDeleted)),
                    new SqlParameter("TargetBudgetId", SqlHelper.GetSqlValue(budgetItemInfo.TargetBudgetId)),
                };

            //note: it target budget info center is new, its id = 0
            if (budgetItemInfo.Id != 0)
            {
                sqlParams.Add(new SqlParameter("Id", budgetItemInfo.Id));
            }

            return sqlParams.ToArray();
        }
    }
}
