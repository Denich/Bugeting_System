using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.Management
{
    public static class MergeBudgetHelper
    {
        public static ComplexBudget Merge(this ComplexBudget baseProject, ComplexBudget secondaryProject)
        {
            if (secondaryProject == null || baseProject == null)
            {
                return baseProject;
            }

            List<BudgetCategory> mergeCategories = baseProject.BudgetCategories.ToList();

            foreach (var secondaryCategory in secondaryProject.BudgetCategories)
            {
                BudgetCategory category = mergeCategories.SingleOrDefault(c => c.InfoId == secondaryCategory.InfoId);

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
            
            baseProject.CalculateValues();

            return baseProject;
        }

        public static BudgetCategory Merge(this BudgetCategory baseCategory, BudgetCategory secondaryCategory)
        {
            if (baseCategory == null || secondaryCategory == null)
            {
                return baseCategory;
            }

            var mergeTargets = baseCategory.TargetBudgets.ToList();

            foreach (var secondaryTarget in secondaryCategory.TargetBudgets)
            {
                TargetBudget target = mergeTargets.SingleOrDefault(t => t.InfoId == secondaryTarget.InfoId);

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
                BudgetItem item = mergeItems.SingleOrDefault(i => i.InfoId == secondaryItem.InfoId);

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
