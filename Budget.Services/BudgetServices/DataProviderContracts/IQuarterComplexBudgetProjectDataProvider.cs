using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IQuarterComplexBudgetProjectDataProvider
    {
        IEnumerable<QuarterComplexBudgetProject> GetAll();

        QuarterComplexBudgetProject Get(int quarterComplexBudgetProjectId);

        int Insert(QuarterComplexBudgetProject quarterComplexBudgetProject);

        int Update(QuarterComplexBudgetProject quarterComplexBudgetProject);

        int Delete(int quarterComplexBudgetProjectId);
        
        QuarterComplexBudget GetFinalFor(int adminUnitId, int year, int quarter);
    }
}