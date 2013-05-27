using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class BaseComplexBudgetProjectDataProvider
    {
        #region TODO: Check for recursive reference

        [Dependency]
        public IBudgetCategoryDataProvider BudgetCategoryDataProvider { get; set; }

        [Dependency]
        public ITargetBudgetDataProvider TargetBudgetDataProvider { get; set; }

        [Dependency]
        public IBudgetItemDataProvider BudgetItemDataProvider { get; set; }

        #endregion

        protected void UpdateCategoriesRecursivly(IEnumerable<BudgetCategory> categories)
        {
            if (categories == null)
            {
                return;
            }

            foreach (var category in categories)
            {
                BudgetCategoryDataProvider.Update(category);
                UpdateTargetsRecursivly(category.TargetBudgets);
            }
        }

        private void UpdateTargetsRecursivly(IEnumerable<TargetBudget> targets)
        {
            if (targets == null)
            {
                return;
            }

            foreach (var target in targets)
            {
                TargetBudgetDataProvider.Update(target);
                UpdateItemsRecursivly(target.BudgetItems);
            }
        }

        private void UpdateItemsRecursivly(IEnumerable<BudgetItem> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                BudgetItemDataProvider.Update(item);
            }
        }

        protected void InsertCategoriesRecursivly(IEnumerable<BudgetCategory> categories, int budgetId)
        {
            if (categories == null)
            {
                return;
            }

            foreach (var category in categories)
            {
                category.ComplexBudgetId = budgetId;
                var categoryId = BudgetCategoryDataProvider.Insert(category);
                InsertTargetsRecursivly(category.TargetBudgets, categoryId);
            }
        }

        private void InsertTargetsRecursivly(IEnumerable<TargetBudget> targets, int categoryId)
        {
            if (targets == null)
            {
                return;
            }

            foreach (var target in targets)
            {
                target.BudgetCategoryId = categoryId;
                int targetId = TargetBudgetDataProvider.Insert(target);
                InsertItemsRecursivly(target.BudgetItems, targetId);
            }
        }

        private void InsertItemsRecursivly(IEnumerable<BudgetItem> items, int targetId)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                item.TargetBudgetId = targetId;
                BudgetItemDataProvider.Insert(item);
            }
        }
    }
}