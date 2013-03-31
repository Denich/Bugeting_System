using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class QuarterComplexBudgetDataProvider : IQuarterComplexBudgetDataProvider
    {
        private readonly CustomDataProvider<QuarterComplexBudget> _provider;

        [InjectionConstructor]
        public QuarterComplexBudgetDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("QuarterComplexBudgetProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<QuarterComplexBudget>(connectionString, procedureSet);
        }

        public IEnumerable<QuarterComplexBudget> GetAll()
        {
            return _provider.GetItems();
        }

        public QuarterComplexBudget Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(QuarterComplexBudget quarterComplexBudget)
        {
            return _provider.AddItem(quarterComplexBudget);
        }

        public int Update(QuarterComplexBudget quarterComplexBudget)
        {
            return _provider.UpdateItem(quarterComplexBudget);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
