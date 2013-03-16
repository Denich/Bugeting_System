using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IQuarterComplexBudgetProjectDataProvider
    {
        IEnumerable<QuarterComplexBudgetProject> GetQuarterComplexBudgetProjects();

        QuarterComplexBudgetProject GetQuarterComplexBudgetProjectById(int quarterComplexBudgetProjectId);

        int AddQuarterComplexBudgetProject(QuarterComplexBudgetProject quarterComplexBudgetProject);

        int UpdateQuarterComplexBudgetProject(QuarterComplexBudgetProject quarterComplexBudgetProject);

        int DeleteQuarterComplexBudgetProject(int quarterComplexBudgetProjectId);
    }
}