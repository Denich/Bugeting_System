using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;
using System.Linq;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class TargetBudgetDataProvider : ITargetBudgetDataProvider
    {
        private readonly CustomDataProvider<TargetBudget> _provider;

        [InjectionConstructor]
        public TargetBudgetDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("TargetBudgetProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<TargetBudget>(connectionString, procedureSet);
        }

        public IEnumerable<TargetBudget> GetAll()
        {
            return _provider.GetItems();
        }

        public TargetBudget Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(TargetBudget targetBudget)
        {
            return _provider.AddItem(targetBudget);
        }

        public int Update(TargetBudget targetBudget)
        {
            return _provider.UpdateItem(targetBudget);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public TargetBudget GetTemplate()
        {
            return IocContainer.Instance.Resolve<TargetBudget>();
        }

        public IEnumerable<TargetBudget> GetForCategory(int categoryId)
        {
            return _provider.GetItems().Where(c => c.BudgetCategoryId == categoryId);
        }
    }
}