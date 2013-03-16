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
    public class QuarterComplexBudgetProjectDataProvider : IQuarterComplexBudgetProjectDataProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public IEnumerable<QuarterComplexBudgetProject> GetQuarterComplexBudgetProjects()
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_QuarterComplexBudgetProjectSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var categories = new List<QuarterComplexBudgetProject>();

                    while (reader.Read())
                    {
                        categories.Add(QuarterComplexBudgetProject.Create(reader));
                    }

                    return categories;
                }
            }
        }

        public QuarterComplexBudgetProject GetQuarterComplexBudgetProjectById(int quarterComplexBudgetProjectId)
        {
            using (var connection = SqlHelper.GetConnection(_connectionString))
            {
                using (var reader = connection.ExecuteReader("usp_QuarterComplexBudgetProjectSelect", CommandType.StoredProcedure, new SqlParameter("id", quarterComplexBudgetProjectId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return QuarterComplexBudgetProject.Create(reader);
                }
            }
        }

        public int AddQuarterComplexBudgetProject(QuarterComplexBudgetProject quarterComplexBudgetProject)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_QuarterComplexBudgetProjectInsert", CommandType.StoredProcedure, quarterComplexBudgetProject.SqlParameters);
            }
        }

        public int UpdateQuarterComplexBudgetProject(QuarterComplexBudgetProject quarterComplexBudgetProject)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_QuarterComplexBudgetProjectUpdate", CommandType.StoredProcedure, quarterComplexBudgetProject.SqlParameters);
            }
        }

        public int DeleteQuarterComplexBudgetProject(int quarterComplexBudgetProjectId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_QuarterComplexBudgetProjectDelete", CommandType.StoredProcedure, new SqlParameter("id", quarterComplexBudgetProjectId));
            }
        }
    }
}
