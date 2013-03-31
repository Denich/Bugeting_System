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
    public class BudgetItemDataProvider : IBudgetItemDataProvider
    {
        private readonly CustomDataProvider<BudgetItem> _provider;

        [InjectionConstructor]
        public BudgetItemDataProvider([Dependency("ConnectionString")] string connectionString,
                                      [Dependency("BudgetItemProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<BudgetItem>(connectionString, procedureSet);
        }

        public IEnumerable<BudgetItem> GetAll()
        {
            return _provider.GetItems();
        }

        public BudgetItem Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(BudgetItem budgetItem)
        {
            return _provider.AddItem(budgetItem);
        }

        public int Update(BudgetItem budgetItem)
        {
            return _provider.UpdateItem(budgetItem);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
