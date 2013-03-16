using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IBudgetCategoryDataProvider
    {
        IEnumerable<BudgetCategory> GetBudgetCategorieInfos();
        BudgetCategory GetBudgetCategoryById(int budgetCategoryId);
        int AddBudgetCategory(BudgetCategory budgetCategory);
        int UpdateBudgetCategory(BudgetCategory budgetCategory);
        int DeleteBudgetCategory(int budgetCategoryId);
    }
}