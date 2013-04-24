using System;
using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class CompanyPositionDataProvider : ICompanyPositionDataProvider
    {
        private readonly CustomDataProvider<CompanyPosition> _provider;

        [InjectionConstructor]
        public CompanyPositionDataProvider([Dependency("ConnectionString")] string connectionString,
                                          [Dependency("CompanyPositionProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<CompanyPosition>(connectionString, procedureSet);
        }

        public IEnumerable<CompanyPosition> GetAll()
        {
            return _provider.GetItems();
        }

        public CompanyPosition Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(CompanyPosition companyPosition)
        {
            return _provider.AddItem(companyPosition);
        }

        public int Update(CompanyPosition companyPosition)
        {
            return _provider.UpdateItem(companyPosition);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}