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
    public class BudgetItemInfoDataProvider : IBudgetItemInfoDataProvider
    {
        private readonly CustomDataProvider<BudgetItemInfo> _provider;

        [InjectionConstructor]
        public BudgetItemInfoDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("BudgetItemInfoProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<BudgetItemInfo>(connectionString, procedureSet);
        }

        public IEnumerable<BudgetItemInfo> GetAll()
        {
            return _provider.GetItems();
        }

        public BudgetItemInfo Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(BudgetItemInfo budgetItemInfo)
        {
            return _provider.AddItem(budgetItemInfo);
        }

        public int Update(BudgetItemInfo budgetItemInfo)
        {
            return _provider.UpdateItem(budgetItemInfo);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
