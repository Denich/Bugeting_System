﻿using System;
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
    public class TargetBudgetInfoDataProvider : ITargetBudgetInfoDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<TargetBudgetInfo> GetTargetBudgetInfos()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_TargetBudgetInfoSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<TargetBudgetInfo>();

                    while (reader.Read())
                    {
                        categories.Add(GetTargetBudgetInfoFromReader(reader));
                    }

                    return categories;
                }
            }
        }

        public TargetBudgetInfo GetTargetBudgetInfoById(int targetBudgetInfoId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_TargetBudgetInfoSelect", CommandType.StoredProcedure, new SqlParameter("id", targetBudgetInfoId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return GetTargetBudgetInfoFromReader(reader);
                }
            }
        }

        public int AddTargetBudgetInfo(TargetBudgetInfo targetBudgetInfo)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_TargetBudgetInfoInsert", CommandType.StoredProcedure, GetSqlParametersFromTargetBudgetInfo(targetBudgetInfo));
            }
        }

        public int UpdateTargetBudgetInfo(TargetBudgetInfo targetBudgetInfo)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_TargetBudgetInfoUpdate", CommandType.StoredProcedure, GetSqlParametersFromTargetBudgetInfo(targetBudgetInfo));
            }
        }

        public int DeleteTargetBudgetInfo(int targetBudgetInfoId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_TargetBudgetInfoDelete", CommandType.StoredProcedure, new SqlParameter("id", targetBudgetInfoId));
            }
        }

        private TargetBudgetInfo GetTargetBudgetInfoFromReader(SqlDataReader reader)
        {
            return new TargetBudgetInfo()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                BudgetCategoryId = Convert.ToInt32(reader["BudgetCategoryId"]),
                IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                Description = Convert.ToString(reader["Description"]),
                DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                Source = Convert.ToString(reader["Source"])
            };
        }

        private SqlParameter[] GetSqlParametersFromTargetBudgetInfo(TargetBudgetInfo targetBudgetInfo)
        {
            var sqlParams = new List<SqlParameter>
                {
                    new SqlParameter("Name", SqlHelper.GetSqlValue(targetBudgetInfo.Name)),
                    new SqlParameter("Description", SqlHelper.GetSqlValue(targetBudgetInfo.Description)),
                    new SqlParameter("IsDeleted", SqlHelper.GetSqlValue(targetBudgetInfo.IsDeleted)),
                    new SqlParameter("BudgetCategoryId", SqlHelper.GetSqlValue(targetBudgetInfo.BudgetCategoryId)),
                    new SqlParameter("DateAdded", SqlHelper.GetSqlValue(targetBudgetInfo.DateAdded)),
                    new SqlParameter("Source", SqlHelper.GetSqlValue(targetBudgetInfo.Source))
                };

            //note: it target budget info center is new, its id = 0
            if (targetBudgetInfo.Id != 0)
            {
                sqlParams.Add(new SqlParameter("Id", targetBudgetInfo.Id));
            }

            return sqlParams.ToArray();
        }
    }
}
