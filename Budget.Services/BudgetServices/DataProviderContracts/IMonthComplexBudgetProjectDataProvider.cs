using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IMonthComplexBudgetProjectDataProvider
    {
        IEnumerable<MonthComplexBudgetProject> GetAll();

        MonthComplexBudgetProject Get(int monthComplexBudgetProjectId);

        int Insert(MonthComplexBudgetProject monthComplexBudgetProject);

        int Update(MonthComplexBudgetProject monthComplexBudgetProject);

        int Delete(int monthComplexBudgetProjectId);
    }
}