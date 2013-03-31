using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IYearComplexBudgetDataProvider
    {
        IEnumerable<YearComplexBudget> GetAll();

        YearComplexBudget Get(int yearComplexBudgetId);

        int Insert(YearComplexBudget yearComplexBudget);

        int Update(YearComplexBudget yearComplexBudget);

        int Delete(int yearComplexBudgetId);
    }
}