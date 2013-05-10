using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IYearComplexBudgetProjectDataProvider /*: IBaseComplexBudgetProjectDataProvider*/
    {
        IEnumerable<YearComplexBudgetProject> GetAll();

        YearComplexBudgetProject Get(int yearComplexBudgetProjectId);

        int Insert(YearComplexBudgetProject yearComplexBudgetProject);

        int Update(YearComplexBudgetProject yearComplexBudgetProject);

        int Delete(int yearComplexBudgetProjectId);

        IEnumerable<YearComplexBudgetProject> GetBudgetProjects(int year, int fcenterId);

        YearComplexBudgetProject GetLatestAcceptedBudgetProject(int year, int fcenterId);
        
        YearComplexBudgetProject GetFinalFor(int adminUnitId, int year);
        
        IEnumerable<UnapproveYearBudget> GetUnapprovalBudgets(int adminUnitId);
    }
}