using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IBudgetItemInfoDataProvider
    {
        IEnumerable<BudgetItemInfo> GetBudgetItemInfos();

        BudgetItemInfo GetBudgetItemInfoById(int budgetItemInfoId);

        int AddBudgetItemInfo(BudgetItemInfo budgetItemInfo);

        int UpdateBudgetItemInfo(BudgetItemInfo budgetItemInfo);

        int DeleteBudgetItemInfo(int budgetItemInfoId);
    }
}