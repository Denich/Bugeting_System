using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IYearComplexBudgetDataProvider
    {
        IEnumerable<YearComplexBudget> GetYearComplexBudgets();

        YearComplexBudget GetYearComplexBudgetById(int yearComplexBudgetId);

        int AddYearComplexBudget(YearComplexBudget yearComplexBudget);

        int UpdateYearComplexBudget(YearComplexBudget yearComplexBudget);

        int DeleteYearComplexBudget(int yearComplexBudgetId);
    }
}