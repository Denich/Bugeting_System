using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;
using System.Linq;
using MoreLinq;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class QuarterComplexBudgetDataProvider : BaseComplexBudgetProjectDataProvider, IQuarterComplexBudgetDataProvider
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
            var quarterBudgetId = _provider.AddItem(quarterComplexBudget);

            InsertCategoriesRecursivly(quarterComplexBudget.BudgetCategories, quarterBudgetId);

            //Insert child budgets
            if (quarterComplexBudget.ChildBudgets != null)
            {
                foreach (var childBudget in quarterComplexBudget.ChildBudgets)
                {
                    childBudget.MasterBudgetID = quarterBudgetId;
                    Insert(childBudget);
                }
            }

            return quarterBudgetId;
        }

        public int Update(QuarterComplexBudget quarterComplexBudget)
        {
            var updateItem = _provider.UpdateItem(quarterComplexBudget);

            UpdateCategoriesRecursivly(quarterComplexBudget.BudgetCategories);

            return updateItem;
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public IEnumerable<QuarterComplexBudget> GetByMaster(int masterBudgetId)
        {
            return GetAll().Where(b => b.MasterBudgetID == masterBudgetId);
        }

        public void StartBudgetResultForProject(QuarterComplexBudgetProject budgetProject)
        {
            var budget = GetFromProject(budgetProject);

            Insert(budget);
        }

        public QuarterComplexBudget GetFor(int year, int quarterNumber, int adminUnitId)
        {
            return GetAll().FirstOrDefault(b => b.Year == year && b.QuarterNumber == quarterNumber && b.AdministrativeUnitId == adminUnitId);
        }

        public IEnumerable<QuarterComplexBudget> GetForYear(int year, int adminUnitId)
        {
            return GetAll().Where(b => b.Year == year && b.AdministrativeUnitId == adminUnitId);
        }

        public void FinalizeBudget(int budgetId)
        {
            var quarterBudgetProject = Get(budgetId);

            quarterBudgetProject.IsFinal = true;

            Update(quarterBudgetProject);

            quarterBudgetProject.ChildBudgets.ForEach(c => FinalizeBudget(c.Id));
        }

        private QuarterComplexBudget GetFromProject(QuarterComplexBudgetProject budgetProject)
        {
            var budget = IocContainer.Instance.Resolve<QuarterComplexBudget>();

            budget.BudgetCategories = budgetProject.BudgetCategories.Select(c => { c.ClearValues(); return c; });

            budget.AdministrativeUnitId = budgetProject.AdministrativeUnitId;
            budget.QuarterNumber = budgetProject.QuarterNumber;
            budget.Year = budgetProject.Year;

            budget.ChildBudgets = budgetProject.ChildBudgets.Select(GetFromProject);

            return budget;
        }
    }
}
