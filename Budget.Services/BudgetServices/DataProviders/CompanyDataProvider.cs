using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class CompanyDataProvider : ICompanyDataProvider
    {
        private readonly CustomDataProvider<Company> _provider;

        [InjectionConstructor]
        public CompanyDataProvider([Dependency("ConnectionString")] string connectionString,
                                   [Dependency("CompanyProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<Company>(connectionString, procedureSet);
        }

        public Company Get()
        {
            var items = _provider.GetItems();
            return items == null ? null : items.First();
        }

        public int Update(Company company)
        {
            return _provider.UpdateItem(company);
        }
    }
}
