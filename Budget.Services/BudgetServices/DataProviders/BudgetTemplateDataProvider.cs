using System;
using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    class BudgetTemplateDataProvider
    {
        public IEnumerable<BudgetCategoryInfo> GetTemplateBudgetCategories()
        {
            throw new NotImplementedException();
        }

        public BudgetCategoryInfo GetTemplateBudgetCategorie(string budgetCategoryName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TargetBudgetInfo> GetTargetBudgetTemplates(string budgetCategoryName)
        {
            throw new NotImplementedException();
        }

        public TargetBudgetInfo GetTemplateTargetBudget(string budgetCategoryName, string targetBudgetName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BudgetItem> GetTemplateBudgetItems(string budgetCategoryName, string targetBudgetName)
        {
            throw new NotImplementedException();
        }

        public BudgetItem GetTemplateBudgetItem(string budgetCategoryName, string targetBudgetName, string budgetItemName)
        {
            throw new NotImplementedException();
        }
    }
}
