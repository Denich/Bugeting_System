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
    public class YearComplexBudgetProjectDataProvider : IYearComplexBudgetProjectDataProvider
    {
        private readonly CustomDataProvider<YearComplexBudgetProject> _provider;

        [InjectionConstructor]
        public YearComplexBudgetProjectDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("YearComplexBudgetProjectProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<YearComplexBudgetProject>(connectionString, procedureSet);
        }

        public IEnumerable<YearComplexBudgetProject> GetAll()
        {
            return _provider.GetItems();
        }

        public YearComplexBudgetProject Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(YearComplexBudgetProject yearComplexBudgetProject)
        {
            return _provider.AddItem(yearComplexBudgetProject);
        }

        public int Update(YearComplexBudgetProject yearComplexBudgetProject)
        {
            return _provider.UpdateItem(yearComplexBudgetProject);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
