using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface ITargetBudgetInfoDataProvider
    {
        IEnumerable<TargetBudgetInfo> GetAll();

        TargetBudgetInfo Get(int targetBudgetInfoId);

        int Insert(TargetBudgetInfo targetBudgetInfo);

        int Update(TargetBudgetInfo targetBudgetInfo);

        int Delete(int targetBudgetInfoId);
    }
}