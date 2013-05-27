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
    public class MonthComplexBudgetDataProvider : BaseComplexBudgetProjectDataProvider, IMonthComplexBudgetDataProvider
    {
        private readonly CustomDataProvider<MonthComplexBudget> _provider;

        [InjectionConstructor]
        public MonthComplexBudgetDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("MonthComplexBudgetProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<MonthComplexBudget>(connectionString, procedureSet);
        }

        public IEnumerable<MonthComplexBudget> GetAll()
        {
            return _provider.GetItems();
        }

        public MonthComplexBudget Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(MonthComplexBudget monthComplexBudget)
        {
            var monthBudgetId = _provider.AddItem(monthComplexBudget);

            InsertCategoriesRecursivly(monthComplexBudget.BudgetCategories, monthBudgetId);

            //Insert child budgets
            if (monthComplexBudget.ChildBudgets != null)
            {
                foreach (var childBudget in monthComplexBudget.ChildBudgets)
                {
                    childBudget.MasterBudgetID = monthBudgetId;
                    Insert(childBudget);
                }
            }

            return monthBudgetId;
        }

        public int Update(MonthComplexBudget monthComplexBudget)
        {
            var updateItem = _provider.UpdateItem(monthComplexBudget);

            UpdateCategoriesRecursivly(monthComplexBudget.BudgetCategories);

            return updateItem;
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public MonthComplexBudget GetFor(int year, int month, int adminUnitId)
        {
            return GetAll().FirstOrDefault(b => b.Year == year && b.Month == month && b.AdministrativeUnitId == adminUnitId);
        }

        public void StartBudgetResultForProject(MonthComplexBudgetProject budgetProject)
        {
            var budget = GetFromProject(budgetProject);

            Insert(budget);
        }

        public IEnumerable<MonthComplexBudget> GetByMaster(int masterBudgetId)
        {
            return GetAll().Where(b => b.MasterBudgetID == masterBudgetId);
        }

        public IEnumerable<MonthComplexBudget> GetForQuarter(int year, int quarterNumber, int adminUnitId)
        {
            return GetAll().Where(b => b.Month/4 + 1 == quarterNumber && b.Year == year && b.AdministrativeUnitId == adminUnitId);
        }

        public void FinalizeBudget(int budgetId)
        {
            var monthBudget = Get(budgetId);

            monthBudget.IsFinal = true;

            Update(monthBudget);

            monthBudget.ChildBudgets.ForEach(b => FinalizeBudget(b.Id));
        }

        private MonthComplexBudget GetFromProject(MonthComplexBudgetProject budgetProject)
        {
            var budget = IocContainer.Instance.Resolve<MonthComplexBudget>();

            budget.BudgetCategories = budgetProject.BudgetCategories.Select(c => { c.ClearValues(); return c; });

            budget.AdministrativeUnitId = budgetProject.AdministrativeUnitId;
            budget.Month = budgetProject.Month;
            budget.Year = budgetProject.Year;

            budget.ChildBudgets = budgetProject.ChildBudgets.Select(GetFromProject);

            return budget;
        }
    }
}
