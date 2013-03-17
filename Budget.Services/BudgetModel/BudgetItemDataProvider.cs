using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetModel
{
    public class BudgetItemDataProvider : IBudgetItemDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<BudgetItem> GetBudgetItems()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetItemSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<BudgetItem>();

                    while (reader.Read())
                    {
                        categories.Add(BudgetItem.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public BudgetItem GetBudgetItemById(int budgetItemId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_BudgetItemSelect", CommandType.StoredProcedure, new SqlParameter("id", budgetItemId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return BudgetItem.Create(reader);
                }
            }
        }

        public int AddBudgetItem(BudgetItem budgetItem)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetItemInsert", CommandType.StoredProcedure, budgetItem.SqlParameters);
            }
        }

        public int UpdateBudgetItem(BudgetItem budgetItem)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetItemUpdate", CommandType.StoredProcedure, budgetItem.SqlParameters);
            }
        }

        public int DeleteBudgetItem(int budgetItemId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_BudgetItemDelete", CommandType.StoredProcedure, new SqlParameter("id", budgetItemId));
            }
        }
    }
}
