using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface ITargetBudgetInfoDataProvider
    {
        IEnumerable<TargetBudgetInfo> GetTargetBudgetInfos();

        TargetBudgetInfo GetTargetBudgetInfoById(int targetBudgetInfoId);

        int AddTargetBudgetInfo(TargetBudgetInfo targetBudgetInfo);

        int UpdateTargetBudgetInfo(TargetBudgetInfo targetBudgetInfo);

        int DeleteTargetBudgetInfo(int targetBudgetInfoId);
    }
}