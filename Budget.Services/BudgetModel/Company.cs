using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;


namespace Budget.Services.BudgetModel
{
    public class Company : AdministrativeUnit, IDataRetriever<Company>
    {
        private IEnumerable<FinancialCenter> _financialCenters;

        [Dependency]
        public IFinancialCenterDataProvider FinancialCenterDataProvider { get; set; }

        public int AccountNumber { get; set; }

        public int Edrpou { get; set; }

        public IEnumerable<FinancialCenter> FinancialCenters
        {
            get
            {
                if (_financialCenters != null)
                {
                    return _financialCenters;
                }

                var targetBudgets = FinancialCenterDataProvider.GetAll();

                return targetBudgets == null ? null : targetBudgets.Where(t => t.CompanyId == Id);
            }
            set { _financialCenters = value; }
        }



        public ICollection<SqlParameter> InsertSqlParameters
        {
            get
            {
                return new[]
                    {
                        new SqlParameter("Name", SqlHelper.GetSqlValue(Name)),
                        new SqlParameter("Adress", SqlHelper.GetSqlValue(Adress)),
                        new SqlParameter("Phone", SqlHelper.GetSqlValue(Phone)),
                        new SqlParameter("Description", SqlHelper.GetSqlValue(Description)),
                        new SqlParameter("DirectorId", SqlHelper.GetSqlValue(DirectorId)),
                        new SqlParameter("Edrpou", SqlHelper.GetSqlValue(Edrpou)),
                        new SqlParameter("AccountNumber", SqlHelper.GetSqlValue(AccountNumber)),
                    };
            }
        }

        public ICollection<SqlParameter> UpdateSqlParameters
        {
            get
            {
                var sqlParams = InsertSqlParameters;
                InsertSqlParameters.Add(new SqlParameter("Id", Id));
                return sqlParams;
            }
        }

        public Company Setup(IDataRecord record)
        {
            Id = Convert.ToInt32(record["Id"]);
            Name = Convert.ToString(record["Name"]);
            AccountNumber = Convert.ToInt32(record["AccountNumber"]);
            Edrpou = Convert.ToInt32(record["EDRPOU"]);
            Description = Convert.ToString(record["Description"]);
            Adress = Convert.ToString(record["Adress"]);
            Phone = Convert.ToString(record["Phone"]);
            DirectorId = Convert.ToInt32(record["DirectorId"]);
            return this;
        }
    }
}