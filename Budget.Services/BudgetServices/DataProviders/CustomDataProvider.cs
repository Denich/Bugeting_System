using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetServices.DataProviders
{
    public static class CustomDataProvider
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public static IEnumerable<T> GetItems<T>(this IDataRetriever<T> source)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader(source.SelectProcedureName, CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var items = new List<T>();

                    while (reader.Read())
                    {
                        items.Add(source.Create(reader));
                    }

                    return items;
                }
            }
        }

        public static T GetItemById<T>(this IDataRetriever<T> source, int id)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader(source.SelectByIdProcedureName, CommandType.StoredProcedure, new SqlParameter("id", id)))
                {
                    if (!reader.HasRows)
                    {
                        return default(T);
                    }

                    reader.Read();

                    return source.Create(reader);
                }
            }
        }

        public static int AddItem<T>(this IDataRetriever<T> source)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery(source.InsertProcedureName, CommandType.StoredProcedure, source.SqlParameters);
            }
        }

        public static int UpdateItem<T>(this IDataRetriever<T> source)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery(source.UpdateProcedureName, CommandType.StoredProcedure, source.SqlParameters);
            }
        }

        public static int DeleteItem<T>(this IDataRetriever<T> source, int id)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery(source.DeleteByIdProcedureName, CommandType.StoredProcedure, new SqlParameter("id", id));
            }
        }
    }
}