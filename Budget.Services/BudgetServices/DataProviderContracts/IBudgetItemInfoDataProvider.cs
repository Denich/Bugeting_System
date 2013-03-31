using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IBudgetItemInfoDataProvider
    {
        IEnumerable<BudgetItemInfo> GetAll();

        BudgetItemInfo Get(int budgetItemInfoId);

        int Insert(BudgetItemInfo budgetItemInfo);

        int Update(BudgetItemInfo budgetItemInfo);

        int Delete(int budgetItemInfoId);
    }
}