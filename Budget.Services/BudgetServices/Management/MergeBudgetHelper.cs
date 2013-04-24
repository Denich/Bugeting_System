using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.Management
{
    public static class MergeBudgetHelper
    {
        public static YearComplexBudgetProject Merge(this YearComplexBudgetProject baseProject, YearComplexBudgetProject secondaryProject)
        {
            if (secondaryProject == null || secondaryProject.BudgetCategories == null || baseProject == null ||
                baseProject.BudgetCategories == null)
            {
                return baseProject;
            }

            List<BudgetCategory> mergeCategories = baseProject.BudgetCategories.ToList();

            foreach (var secondaryCategory in secondaryProject.BudgetCategories)
            {
                BudgetCategory category = baseProject.BudgetCategories.SingleOrDefault(c => c.InfoId == secondaryCategory.InfoId);

                if (category == null)
                {
                    mergeCategories.Add(secondaryCategory);
                }
                else
                {
                    mergeCategories.Remove(category);
                    mergeCategories.Add(category.Merge(secondaryCategory));
                }
            }

            baseProject.BudgetCategories = mergeCategories;

            return baseProject;
        }

        public static BudgetCategory Merge(this BudgetCategory baseCategory, BudgetCategory secondaryCategory)
        {
            if (baseCategory == null || baseCategory.TargetBudgets == null || secondaryCategory == null ||
                secondaryCategory.TargetBudgets == null)
            {
                return baseCategory;
            }

            var mergeTargets = new List<TargetBudget>();

            foreach (var secondaryTarget in secondaryCategory.TargetBudgets)
            {
                TargetBudget target = baseCategory.TargetBudgets.SingleOrDefault(t => t.InfoId == secondaryTarget.InfoId);

                if (target == null)
                {
                    mergeTargets.Add(secondaryTarget);
                }
                else
                {
                    mergeTargets.Remove(target);
                    mergeTargets.Add(target.Merge(secondaryTarget));
                }
            }

            baseCategory.TargetBudgets = mergeTargets;

            return baseCategory;
        }

        public static TargetBudget Merge(this TargetBudget baseTarget, TargetBudget secondaryTarget)
        {
            if (baseTarget == null || baseTarget.BudgetItems == null || secondaryTarget == null ||
                secondaryTarget.BudgetItems == null)
            {
                return baseTarget;
            }

            List<BudgetItem> mergeItems = baseTarget.BudgetItems.ToList();

            foreach (var secondaryItem in secondaryTarget.BudgetItems)
            {
                BudgetItem item = baseTarget.BudgetItems.SingleOrDefault(i => i.InfoId == secondaryItem.InfoId);

                if (item == null)
                {
                    mergeItems.Add(secondaryItem);
                }
            }

            baseTarget.BudgetItems = mergeItems;

            return baseTarget;
        }
    }
}
