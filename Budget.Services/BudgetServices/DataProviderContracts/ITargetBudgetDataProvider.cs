using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface ITargetBudgetDataProvider
    {
        IEnumerable<TargetBudget> GetAll();

        TargetBudget Get(int targetBudgetId);

        int Insert(TargetBudget targetBudget);

        int Update(TargetBudget targetBudget);
        
        int Delete(int targetBudgetId);
    }
}