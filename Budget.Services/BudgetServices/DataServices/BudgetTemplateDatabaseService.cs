using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataServices
{
    class BudgetTemplateDatabaseService
    {
        public ICollection<BudgetCategoryInfo> GetTemplateBudgetCategories()
        {
            throw new NotImplementedException();
        }

        public BudgetCategoryInfo GetTemplateBudgetCategorie(string budgetCategoryName)
        {
            throw new NotImplementedException();
        }

        public ICollection<TargetBudgetInfo> GetTargetBudgetTemplates(string budgetCategoryName)
        {
            throw new NotImplementedException();
        }

        public TargetBudgetInfo GetTemplateTargetBudget(string budgetCategoryName, string targetBudgetName)
        {
            throw new NotImplementedException();
        }

        public ICollection<BudgetItem> GetTemplateBudgetItems(string budgetCategoryName, string targetBudgetName)
        {
            throw new NotImplementedException();
        }

        public BudgetItem GetTemplateBudgetItem(string budgetCategoryName, string targetBudgetName, string budgetItemName)
        {
            throw new NotImplementedException();
        }
    }
}
