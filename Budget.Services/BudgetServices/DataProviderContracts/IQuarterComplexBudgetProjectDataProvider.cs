using System;
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

        IEnumerable<QuarterComplexBudgetProject> GetBudgetProjects(int year, int quarter, int fcenterId);

        QuarterComplexBudgetProject GetLatestAcceptedBudgetProject(int year, int quarter, int fcenterId);
        
        QuarterComplexBudgetProject GetFinalFor(int adminUnitId, int year, int quarter);
        
        IEnumerable<UnapproveQuarterBudget> GetUnapprovalBudgets(int adminUnitId);
        
        IEnumerable<QuarterComplexBudgetProject> GetChildForYearBudget(int yearBudgetId);
        
        IEnumerable<QuarterComplexBudgetProject> GetByMaster(int masterBudgetId);
        
        QuarterComplexBudgetProject GetTemplate();
        
        void FinilizeBudget(int budgetId);
        
        void ReviewBudget(int budgetId);

        IEnumerable<QuarterComplexBudgetProject> GetApprovedBudgets(DateTime nowDate, int adminUnitId);
        
        void ReviewByMonthBudget(int monthBudgetId);
    }
}