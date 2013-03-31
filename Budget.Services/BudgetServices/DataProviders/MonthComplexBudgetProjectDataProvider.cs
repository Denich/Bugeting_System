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
    public class MonthComplexBudgetProjectDataProvider : IMonthComplexBudgetProjectDataProvider
    {
        private readonly CustomDataProvider<MonthComplexBudgetProject> _provider;

        [InjectionConstructor]
        public MonthComplexBudgetProjectDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("MonthComplexBudgetProjectProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<MonthComplexBudgetProject>(connectionString, procedureSet);
        }

        public IEnumerable<MonthComplexBudgetProject> GetAll()
        {
            return _provider.GetItems();
        }

        public MonthComplexBudgetProject Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(MonthComplexBudgetProject MonthComplexBudgetProject)
        {
            return _provider.AddItem(MonthComplexBudgetProject);
        }

        public int Update(MonthComplexBudgetProject MonthComplexBudgetProject)
        {
            return _provider.UpdateItem(MonthComplexBudgetProject);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
