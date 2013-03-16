using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IMonthComplexBudgetProjectDataProvider
    {
        IEnumerable<MonthComplexBudgetProject> GetMonthComplexBudgetProjects();
        MonthComplexBudgetProject GetMonthComplexBudgetProjectById(int monthComplexBudgetProjectId);
        int AddMonthComplexBudgetProject(MonthComplexBudgetProject monthComplexBudgetProject);
        int UpdateMonthComplexBudgetProject(MonthComplexBudgetProject monthComplexBudgetProject);
        int DeleteMonthComplexBudgetProject(int monthComplexBudgetProjectId);
    }
}