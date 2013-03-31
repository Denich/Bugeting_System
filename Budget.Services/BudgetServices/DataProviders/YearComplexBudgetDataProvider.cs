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
    public class YearComplexBudgetDataProvider : IYearComplexBudgetDataProvider
    {
        private readonly CustomDataProvider<YearComplexBudget> _provider;

        [InjectionConstructor]
        public YearComplexBudgetDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("YearComplexBudgetProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<YearComplexBudget>(connectionString, procedureSet);
        }

        public IEnumerable<YearComplexBudget> GetAll()
        {
            return _provider.GetItems();
        }

        public YearComplexBudget Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(YearComplexBudget yearComplexBudget)
        {
            return _provider.AddItem(yearComplexBudget);
        }

        public int Update(YearComplexBudget yearComplexBudget)
        {
            return _provider.UpdateItem(yearComplexBudget);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
