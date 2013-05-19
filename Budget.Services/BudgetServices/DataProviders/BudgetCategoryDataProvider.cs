using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;
using System.Linq;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class BudgetCategoryDataProvider : IBudgetCategoryDataProvider
    {
        private readonly CustomDataProvider<BudgetCategory> _provider;

        [InjectionConstructor]
        public BudgetCategoryDataProvider([Dependency("ConnectionString")] string connectionString,
                                          [Dependency("BudgetCategoryProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<BudgetCategory>(connectionString, procedureSet);
        }

        public IEnumerable<BudgetCategory> GetAll()
        {
            return _provider.GetItems();
        }

        public BudgetCategory Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(BudgetCategory budgetCategory)
        {
            return _provider.AddItem(budgetCategory);
        }

        public int Update(BudgetCategory budgetCategory)
        {
            return _provider.UpdateItem(budgetCategory);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public IEnumerable<BudgetCategory> GetForBudget(int budgetId)
        {
            return _provider.GetItems().Where(c => c.ComplexBudgetId == budgetId);
        }

        public BudgetCategory GetTemplate()
        {
            return IocContainer.Instance.Resolve<BudgetCategory>();
        }
    }
}
