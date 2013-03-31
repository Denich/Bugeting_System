using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class MonthComplexBudgetDataProvider : IMonthComplexBudgetDataProvider
    {
        private readonly CustomDataProvider<MonthComplexBudget> _provider;

        [InjectionConstructor]
        public MonthComplexBudgetDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("MonthComplexBudgetProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<MonthComplexBudget>(connectionString, procedureSet);
        }

        public IEnumerable<MonthComplexBudget> GetAll()
        {
            return _provider.GetItems();
        }

        public MonthComplexBudget Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(MonthComplexBudget monthComplexBudget)
        {
            return _provider.AddItem(monthComplexBudget);
        }

        public int Update(MonthComplexBudget monthComplexBudget)
        {
            return _provider.UpdateItem(monthComplexBudget);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
