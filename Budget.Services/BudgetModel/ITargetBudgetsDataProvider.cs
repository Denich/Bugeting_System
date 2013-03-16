using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public interface ITargetBudgetsDataProvider
    {
        IEnumerable<TargetBudget> GetTargetBudgets();
    }
}