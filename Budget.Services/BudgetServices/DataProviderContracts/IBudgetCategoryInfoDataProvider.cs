using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IBudgetCategoryInfoDataProvider
    {
        IEnumerable<BudgetCategoryInfo> GetAll();

        BudgetCategoryInfo Get(int budgetCategoryInfoId);

        int Insert(BudgetCategoryInfo budgetCategoryInfo);

        int Update(BudgetCategoryInfo budgetCategoryInfo);

        int Delete(int budgetCategoryInfoId);
    }
}