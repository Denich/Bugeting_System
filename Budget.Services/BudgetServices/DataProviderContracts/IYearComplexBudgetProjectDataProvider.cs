using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IYearComplexBudgetProjectDataProvider
    {
        IEnumerable<YearComplexBudgetProject> GetAll();

        YearComplexBudgetProject Get(int yearComplexBudgetProjectId);

        int Insert(YearComplexBudgetProject yearComplexBudgetProject);

        int Update(YearComplexBudgetProject yearComplexBudgetProject);

        int Delete(int yearComplexBudgetProjectId);
    }
}