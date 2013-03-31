using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IQuarterComplexBudgetDataProvider
    {
        IEnumerable<QuarterComplexBudget> GetAll();

        QuarterComplexBudget Get(int quarterComplexBudgetId);

        int Insert(QuarterComplexBudget quarterComplexBudget);

        int Update(QuarterComplexBudget quarterComplexBudget);

        int Delete(int quarterComplexBudgetId);
    }
}