using System.Collections.Generic;
using System.Linq;
using Budget.Services.BudgetModel;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.Management
{
    public class BudgetOperationManagement : IBudgetOperationManagement
    {
        private readonly IBudgetDataManagement _data;

        [InjectionConstructor]
        public BudgetOperationManagement(IBudgetDataManagement budgetDataManagement)
        {
            _data = budgetDataManagement;
        }

        public void InsertBudgetRecursivly(YearComplexBudgetProject project)
        {
            var acceptedBudget = _data.YearComplexBudgetProjects.GetLatestAcceptedBudgetProject(project.Year, project.AdministrativeUnitId);

            if (project.IsAccepted)
            {
                project = project.Merge(acceptedBudget);
            }

            //todo: add transaction to this operation
            int budgetId = _data.YearComplexBudgetProjects.Insert(project);

            InsertCategoriesRecursivly(project.BudgetCategories, budgetId, acceptedBudget != null ? acceptedBudget.BudgetCategories : null);
        }

        public IEnumerable<YearComplexBudget> GetUnresultedYearBudgets(int adminUnitId)
        {
            var budgets = _data.YearComplexBudgets.GetAll();
            return budgets != null ? budgets.Where(b => !b.IsFinal && b.AdministrativeUnitId == adminUnitId) : null;
        }

        public IEnumerable<QuarterComplexBudget> GetUnresultedQuarterBudgets(int adminUnitId)
        {
            var budgets = _data.QuarterComplexBudgets.GetAll();
            return budgets != null ? budgets.Where(b => b.AdministrativeUnitId == adminUnitId && !b.IsFinal) : null;
        }

        public IEnumerable<MonthComplexBudget> GetUnresultedMonthBudgets(int adminUnitId)
        {
            var budgets = _data.MonthComplexBudgets.GetAll();
            return budgets != null ? budgets.Where(b => b.AdministrativeUnitId == adminUnitId && !b.IsFinal) : null;
        }


        public IEnumerable<FinancialCenter> GetYearBudgetInvolvedFinancialCenters(int year)
        {
            return _data.FinancialCenters.GetAll()/*.Where(b => b.IsUsedInYearBudget(year))*/;
        }

        private void InsertCategoriesRecursivly(IEnumerable<BudgetCategory> categories, int budgetId, IEnumerable<BudgetCategory> checkCategories)
        {
            if (categories == null)
            {
                return;    
            }

            if (checkCategories == null)
            {
                foreach (var category in categories)
                {
                    category.ComplexBudgetId = budgetId;
                    
                    var categoryId = _data.BudgetCategories.Insert(category);

                    InsertTargetsRecursivly(category.TargetBudgets, categoryId, null);
                }
            }
            else
            {
                foreach (var category in categories)
                {
                    var acceptedCategory = checkCategories.SingleOrDefault(c => c.InfoId == category.Id);

                    if (acceptedCategory != null)
                    {
                        InsertTargetsRecursivly(category.TargetBudgets, acceptedCategory.Id, acceptedCategory.TargetBudgets);
                    }
                    else
                    {
                        category.ComplexBudgetId = budgetId;

                        var categoryId = _data.BudgetCategories.Insert(category);

                        InsertTargetsRecursivly(category.TargetBudgets, categoryId, null);
                    }
                }
            }
        }

        private void InsertTargetsRecursivly(IEnumerable<TargetBudget> targets, int categoryId, IEnumerable<TargetBudget> checkTargets)
        {
            if (targets == null)
            {
                return;    
            }

            if (checkTargets == null)
            {
                foreach (var target in targets)
                {
                    target.BudgetCategoryId = categoryId;
                    int targetId = _data.TargetBudgets.Insert(target);
                    InsertItemsRecursivly(target.BudgetItems, targetId, null);
                }
            }
            else
            {
                foreach (var target in targets)
                {
                    var acceptedTarget = checkTargets.SingleOrDefault(t => t.InfoId == target.Id);

                    if (acceptedTarget != null)
                    {
                        InsertItemsRecursivly(target.BudgetItems, acceptedTarget.Id, acceptedTarget.BudgetItems);
                    }
                    else
                    {
                        target.BudgetCategoryId = categoryId;
                        int targetId = _data.TargetBudgets.Insert(target);
                        InsertItemsRecursivly(target.BudgetItems, targetId, null);
                    }
                }
            }
        }

        private void InsertItemsRecursivly(IEnumerable<BudgetItem> items, int targetId, IEnumerable<BudgetItem> checkItems)
        {
            if (items == null)
            {
                return;
            }

            if (checkItems == null)
            {
                foreach (var item in items)
                {
                    item.TargetBudgetId = targetId;
                    _data.BudgetItems.Insert(item);
                }
            }
            else
            {
                foreach (var item in items)
                {
                    var acceptedItem = checkItems.SingleOrDefault(i => i.InfoId == item.Id);

                    if (acceptedItem == null)
                    {
                        item.TargetBudgetId = targetId;
                        _data.BudgetItems.Insert(item);
                    }
                }
            }
        }
    }
}