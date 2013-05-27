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
using MoreLinq;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class YearComplexBudgetDataProvider : BaseComplexBudgetProjectDataProvider, IYearComplexBudgetDataProvider
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
            var yearBudgetId = _provider.AddItem(yearComplexBudget);

            InsertCategoriesRecursivly(yearComplexBudget.BudgetCategories, yearBudgetId);

            //Insert child budgets
            if (yearComplexBudget.ChildBudgets != null)
            {
                foreach (var childBudget in yearComplexBudget.ChildBudgets)
                {
                    childBudget.MasterBudgetID = yearBudgetId;
                    Insert(childBudget);
                }
            }

            return yearBudgetId;
        }

        public int Update(YearComplexBudget yearComplexBudget)
        {
            return _provider.UpdateItem(yearComplexBudget);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public IEnumerable<YearComplexBudget> GetByMaster(int masterBudgetId)
        {
            return GetAll().Where(b => b.MasterBudgetID == masterBudgetId);
        }

        public void StartBudgetResultForProject(YearComplexBudgetProject budgetProject)
        {
            var budget = GetFromProject(budgetProject);

            Insert(budget);
        }

        public YearComplexBudget GetFor(int year, int adminUnitId)
        {
            return GetAll().FirstOrDefault(b => b.Year == year && b.AdministrativeUnitId == adminUnitId);
        }

        public void FinalizeBudget(int budgetId)
        {
            var monthBudgetProject = Get(budgetId);

            monthBudgetProject.IsFinal = true;

            Update(monthBudgetProject);

            monthBudgetProject.ChildBudgets.ForEach(c => FinalizeBudget(c.Id));
        }

        private YearComplexBudget GetFromProject(YearComplexBudgetProject budgetProject)
        {
            var budget = IocContainer.Instance.Resolve<YearComplexBudget>();

            budget.BudgetCategories = budgetProject.BudgetCategories.Select(c => { c.ClearValues(); return c; });

            budget.AdministrativeUnitId = budgetProject.AdministrativeUnitId;
            budget.Year = budgetProject.Year;

            budget.ChildBudgets = budgetProject.ChildBudgets.Select(GetFromProject);

            return budget;
        }
    }
}
