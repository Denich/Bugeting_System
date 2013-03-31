using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IBudgetItemDataProvider
    {
        IEnumerable<BudgetItem> GetAll();

        BudgetItem Get(int budgetItemId);

        int Insert(BudgetItem budgetItem);

        int Update(BudgetItem budgetItem);

        int Delete(int budgetItemId);
    }
}