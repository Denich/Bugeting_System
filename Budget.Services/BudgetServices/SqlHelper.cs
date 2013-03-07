using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Budget.Services.BudgetServices
{
    public static class SqlHelper
    {
        /// <summary>
        /// creates and open a sqlconnection
        /// </summary>
        /// <param name="connectionString">
        /// A <see cref="System.String"/> that contains the sql connectin parameters
        /// </param>
        /// <returns>
        /// A <see cref="SqlConnection"/> 
        /// </returns>
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception)
            {
                //ex should be written into a error log

                // dispose of the connection to avoid connections leak
                if (connection != null)
                {
                    connection.Dispose();
                }

                throw;
            }
            return connection;
        }

        /// <summary>
        /// Creates a sqlcommand
        /// </summary>
        /// <param name="connection">
        /// A <see cref="SqlConnection"/>
        /// </param>
        /// <param name="commandText">
        /// A <see cref="System.String"/> of the sql query.
        /// </param>
        /// <param name="commandType">
        /// A <see cref="CommandType"/> of the query type.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCommand"/>
        /// </returns>
        public static SqlCommand GetCommand(this SqlConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = connection.ConnectionTimeout;
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }

        public static SqlDataReader ExecuteReader(this SqlConnection connection, string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCommand = connection.GetCommand(commandText, commandType);

            if (sqlParameters != null && sqlParameters.Length != 0)
            {
                sqlCommand.Parameters.AddRange(sqlParameters);
            }

            return sqlCommand.ExecuteReader();
        }

        public static int ExecuteNonQuery(this SqlConnection connection, string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCommand = connection.GetCommand(commandText, commandType);

            if (sqlParameters != null && sqlParameters.Length != 0)
            {
                sqlCommand.Parameters.AddRange(sqlParameters);
            }

            return sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Adds a parameter to the command parameter array.
        /// </summary>
        /// <param name="command">
        /// A <see cref="SqlCommand"/> 
        /// </param>
        /// <param name="parameterName">
        /// A <see cref="System.String"/> of the named parameter in the sql query.
        /// </param>
        /// <param name="parameterValue">
        /// A <see cref="System.Object"/> of the parameter value.
        /// </param>
        /// <param name="parameterSqlType">
        /// A <see cref="SqlDbType"/>
        /// </param>
        public static void AddParameter(this SqlCommand command, string parameterName, object parameterValue, SqlDbType parameterSqlType)
        {
            if (!parameterName.StartsWith("@"))
            {
                parameterName = "@" + parameterName;
            }
            command.Parameters.Add(parameterName, parameterSqlType);
            command.Parameters[parameterName].Value = parameterValue;
        }

        public static object GetSqlValue(object value)
        {
            return value ?? DBNull.Value;
        }
    }
}
