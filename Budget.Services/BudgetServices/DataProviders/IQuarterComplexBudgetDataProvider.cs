using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IQuarterComplexBudgetDataProvider
    {
        IEnumerable<QuarterComplexBudget> GetQuarterComplexBudgets();
        QuarterComplexBudget GetQuarterComplexBudgetById(int quarterComplexBudgetId);
        int AddQuarterComplexBudget(QuarterComplexBudget quarterComplexBudget);
        int UpdateQuarterComplexBudget(QuarterComplexBudget quarterComplexBudget);
        int DeleteQuarterComplexBudget(int quarterComplexBudgetId);
    }
}