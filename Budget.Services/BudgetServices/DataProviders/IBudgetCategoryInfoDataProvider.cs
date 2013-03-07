using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IBudgetCategoryInfoDataProvider
    {
        IEnumerable<BudgetCategoryInfo> GetBudgetCategorieInfos();

        BudgetCategoryInfo GetBudgetCategoryInfoById(int budgetCategoryInfoId);

        int AddBudgetCategoryInfo(BudgetCategoryInfo budgetCategoryInfo);

        int UpdateBudgetCategoryInfo(BudgetCategoryInfo budgetCategoryInfo);

        int DeleteBudgetCategoryInfo(int budgetCategoryInfoId);
    }
}