using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class BudgetCategoryInfoDataProvider : IBudgetCategoryInfoDataProvider
    {
        private readonly CustomDataProvider<BudgetCategoryInfo> _provider;

        [InjectionConstructor]
        public BudgetCategoryInfoDataProvider([Dependency("ConnectionString")] string connectionString,
                                              [Dependency("BudgetCategoryInfoProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<BudgetCategoryInfo>(connectionString, procedureSet);
        }

        public IEnumerable<BudgetCategoryInfo> GetAll()
        {
            return _provider.GetItems();
        }

        public BudgetCategoryInfo Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(BudgetCategoryInfo budgetCategoryInfo)
        {
            return _provider.AddItem(budgetCategoryInfo);
        }

        public int Update(BudgetCategoryInfo budgetCategoryInfo)
        {
            return _provider.UpdateItem(budgetCategoryInfo);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
