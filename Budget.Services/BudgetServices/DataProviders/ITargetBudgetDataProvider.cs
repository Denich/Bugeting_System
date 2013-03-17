using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface ITargetBudgetDataProvider
    {
        IEnumerable<TargetBudget> GetTargetBudgets();

        TargetBudget GetTargetBudgetById(int targetBudgetId);

        int AddTargetBudget(TargetBudget targetBudget);

        int UpdateTargetBudget(TargetBudget targetBudget);
        
        int DeleteTargetBudget(int targetBudgetId);
    }
}