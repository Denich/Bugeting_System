using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Budget.Services.Helpers
{
    public static class DataRetrieverExtenssions
    {
        public static IEnumerable<T> GetItems<T>(this IDataRetriever<T> source, string connectionString, string selectProcedureName)
        {
            if (String.IsNullOrEmpty(selectProcedureName))
            {
                throw new ArgumentException("Select procedure name does not specify");//todo: add message from resources
            }

            using (SqlConnection connection = SqlHelper.GetConnection(connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader(selectProcedureName, CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var items = new List<T>();

                    while (reader.Read())
                    {
                        items.Add(source.Setup(reader));
                    }

                    return items;
                }
            }
        }

        public static T GetItemById<T>(this IDataRetriever<T> source, int id, string connectionString, string selectByIdProcedureName)
        {
            if (String.IsNullOrEmpty(selectByIdProcedureName))
            {
                throw new ArgumentException("Select by id procedure name does not specify");//todo: add message from resources
            }

            using (SqlConnection connection = SqlHelper.GetConnection(connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader(selectByIdProcedureName, CommandType.StoredProcedure, new SqlParameter("id", id)))
                {
                    if (!reader.HasRows)
                    {
                        return default(T);
                    }

                    reader.Read();

                    return source.Setup(reader);
                }
            }
        }

        public static int AddItem<T>(this IDataRetriever<T> source, string connectionString, string insertProcedureName)
        {
            if (String.IsNullOrEmpty(insertProcedureName))
            {
                throw new ArgumentException("Insert procedure name does not specify");//todo: add message from resources
            }

            using (SqlConnection connection = SqlHelper.GetConnection(connectionString))
            {
                return connection.ExecuteNonQuery(insertProcedureName, CommandType.StoredProcedure, source.InsertSqlParameters.ToArray());
            }
        }

        public static int UpdateItem<T>(this IDataRetriever<T> source, string connectionString, string updateProcedureName)
        {
            if (String.IsNullOrEmpty(updateProcedureName))
            {
                throw new ArgumentException("Update procedure name does not specify");//todo: add message from resources
            }

            using (SqlConnection connection = SqlHelper.GetConnection(connectionString))
            {
                return connection.ExecuteNonQuery(updateProcedureName, CommandType.StoredProcedure, source.UpdateSqlParameters.ToArray());
            }
        }

        public static int DeleteItem<T>(this IDataRetriever<T> source, int id, string connectionString, string deleteProcedureName)
        {
            if (String.IsNullOrEmpty(deleteProcedureName))
            {
                throw new ArgumentException("Delete procedure name does not specify");//todo: add message from resources
            }

            using (SqlConnection connection = SqlHelper.GetConnection(connectionString))
            {
                return connection.ExecuteNonQuery(deleteProcedureName, CommandType.StoredProcedure, new SqlParameter("id", id));
            }
        }
    }
}