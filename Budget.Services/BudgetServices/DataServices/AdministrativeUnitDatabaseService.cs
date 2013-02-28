using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataServices
{
    public class AdministrativeUnitDatabaseService
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        public CompanyInfo GetCompany()
        {
            using (SqlConnection connection = SqlHelper.GetConnection(_connectionString))
            {
                using (SqlCommand command = connection.GetCommand("SELECT * FROM Company comp, AdministrativeUnit aunit Where comp.AdministrativeUnitID = aunit.ID", CommandType.Text)) 
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            throw new DataException("Company information does not found");
                        }

                        reader.Read();
                        return new CompanyInfo
                            {
                                AccountNumber = Convert.ToInt32(reader["AccountNumber"]),
                                Edrpou = Convert.ToInt32(reader["EDRPOU"]),
                                Name = Convert.ToString(reader["Name"]),
                                Description = Convert.ToString(reader["Description"]),
                                Adress = Convert.ToString(reader["Adress"]),
                                Phone = Convert.ToString(reader["Phone"]),
                            };
                    }
                }
            }
        }

        public void UpdateCompany()
        {
            throw new NotImplementedException();
        }
    }
}
