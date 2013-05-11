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

        IEnumerable<MonthComplexBudgetProject> GetBudgetProjects(int year, int month, int fcenterId);

        MonthComplexBudgetProject GetLatestAcceptedBudgetProject(int year, int month, int fcenterId);

        MonthComplexBudgetProject GetFinalFor(int adminUnitId, int year, int month);
        
        IEnumerable<UnapproveMonthBudget> GetUnapprovalBudgets(int adminUnitId);
        
        IEnumerable<MonthComplexBudgetProject> GetChildForQuarterBudget(int quarterBudgetId);
        
        IEnumerable<MonthComplexBudgetProject> GetByMaster(int masterBudgetId);
        
        MonthComplexBudgetProject GetTemplate();
    }
}