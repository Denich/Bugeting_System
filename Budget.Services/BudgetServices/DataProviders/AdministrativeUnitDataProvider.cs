using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.Helpers;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class AdministrativeUnitDataProvider : IAdministrativeUnitDataProvider
    {
       private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public Company GetCompany()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_CompanySelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();
                    return GetCompanyFromReader(reader);
                }
            }
        }

        public int UpdateCompany(Company company)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_CompanyUpdate", CommandType.StoredProcedure, GetSqlParametersFromCompany(company));
            }
        }

        public int AddCompany(Company company)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_CompanyInsert", CommandType.StoredProcedure, GetSqlParametersFromCompany(company));
            }
        }

        public int DeleteCompany(int companyId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_CompanyDelete", CommandType.StoredProcedure, new SqlParameter("id", companyId));
            }
        }

        public IEnumerable<FinancialCenter> GetFinancialCenters()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_FinancialCenterSelect", CommandType.StoredProcedure, new SqlParameter("id", DBNull.Value)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var fcenters = new List<FinancialCenter>();

                    while (reader.Read())
                    {
                        fcenters.Add(GetFinancialCenterFromReader(reader));
                    }

                    return fcenters;
                }
            }
        }

        public FinancialCenter GetFinancialCenterById(int centerId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlDataReader reader = connection.ExecuteReader("usp_FinancialCenterSelect", CommandType.StoredProcedure, new SqlParameter("id", centerId)))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();

                    return GetFinancialCenterFromReader(reader);
                }
            }
        }

        public int AddFinancialCenter(FinancialCenter financialCenter)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_FinancialCenterInsert", CommandType.StoredProcedure, GetSqlParametersFromFinancialCenter(financialCenter));
            }
        }

        public int UpdateFinancialCenter(FinancialCenter financialCenter)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_FinancialCenterUpdate", CommandType.StoredProcedure, GetSqlParametersFromFinancialCenter(financialCenter));
            }
        }

        public int DeleteFinancialCenter(int financialCenterId)
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                return connection.ExecuteNonQuery("usp_FinancialCenterDelete", CommandType.StoredProcedure, new SqlParameter("id", financialCenterId));
            }
        }

        private SqlParameter[] GetSqlParametersFromCompany(Company company)
        {
            var sqlParams = new List<SqlParameter>
                {
                    new SqlParameter("Edrpou", SqlHelper.GetSqlValue(company.Edrpou)),
                    new SqlParameter("AccountNumber", SqlHelper.GetSqlValue(company.AccountNumber)),
                };

            sqlParams.AddRange(GetSqlParametersFromAdministrativeUnit(company));

            return sqlParams.ToArray();
        }

        private static SqlParameter[] GetSqlParametersFromFinancialCenter(FinancialCenter financialCenter)
        {
            var sqlParams = new List<SqlParameter>
                {
                    new SqlParameter("Type", SqlHelper.GetSqlValue(financialCenter.Type)),
                    new SqlParameter("CompanyId", SqlHelper.GetSqlValue(financialCenter.CompanyId)),
                };

            sqlParams.AddRange(GetSqlParametersFromAdministrativeUnit(financialCenter));

            return sqlParams.ToArray();
        }

        private static SqlParameter[] GetSqlParametersFromAdministrativeUnit(AdministrativeUnit administrativeUnit)
        {
            var sqlParams = new List<SqlParameter>
                {
                    new SqlParameter("Name", SqlHelper.GetSqlValue(administrativeUnit.Name)),
                    new SqlParameter("Adress", SqlHelper.GetSqlValue(administrativeUnit.Adress)),
                    new SqlParameter("Phone", SqlHelper.GetSqlValue(administrativeUnit.Phone)),
                    new SqlParameter("Description", SqlHelper.GetSqlValue(administrativeUnit.Description)),
                    new SqlParameter("DirectorId", SqlHelper.GetSqlValue(administrativeUnit.DirectorId))
                };

            //note: it financial center is new, its id = 0
            if (administrativeUnit.Id != 0)
            {
                sqlParams.Add(new SqlParameter("Id", administrativeUnit.Id));
            }

            return sqlParams.ToArray();
        }

        private Company GetCompanyFromReader(SqlDataReader reader)
        {
            return new Company(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]))
            {
                AccountNumber = Convert.ToInt32(reader["AccountNumber"]),
                Edrpou = Convert.ToInt32(reader["EDRPOU"]),
                Description = Convert.ToString(reader["Description"]),
                Adress = Convert.ToString(reader["Adress"]),
                Phone = Convert.ToString(reader["Phone"]),
                DirectorId = Convert.ToInt32(reader["DirectorId"]),
            };
        }

        private FinancialCenter GetFinancialCenterFromReader(SqlDataReader reader)
        {
            return new FinancialCenter(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]), (FinancialCenterType)Convert.ToInt32(reader["Type"]))
            {
                CompanyId = Convert.ToInt32(reader["CompanyId"]),
                Adress = Convert.ToString(reader["Adress"]),
                Phone = Convert.ToString(reader["Phone"]),
                Description = Convert.ToString(reader["Description"]),
                DirectorId = Convert.ToInt32(reader["DirectorId"])
            };
        }

        /// <summary>
        /// test method
        /// </summary>
        public AdministrativeUnit GetAdministrativeUnitById(int administrativeUnitId)//todo: chage it to real impl
        {
            var adminUnit = GetFinancialCenterById(administrativeUnitId);
            
            if (adminUnit != null)
            {
                return adminUnit;
            }

            return GetCompany();
        }

        /// <summary>
        /// test method
        /// </summary>
        public IEnumerable<AdministrativeUnit> GetAdministrativeUnits()//todo: chage it to real impl
        {
            var adminUnits = new List<AdministrativeUnit>();

            adminUnits.AddRange(GetFinancialCenters());

            //adminUnits.Add(GetCompany());

            return adminUnits.Count > 0 ? adminUnits : null;
        }
    }
}
