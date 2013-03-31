using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class FinancialCenterDataProvider : IFinancialCenterDataProvider
    {
        private readonly CustomDataProvider<FinancialCenter> _provider;

        [InjectionConstructor]
        public FinancialCenterDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("FinancialCenterProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<FinancialCenter>(connectionString, procedureSet);
        }

        public IEnumerable<FinancialCenter> GetAll()
        {
            return _provider.GetItems();
        }

        public FinancialCenter Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(FinancialCenter financialCenter)
        {
            return _provider.AddItem(financialCenter);
        }

        public int Update(FinancialCenter financialCenter)
        {
            return _provider.UpdateItem(financialCenter);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}