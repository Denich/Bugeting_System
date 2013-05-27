using System;
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

        IEnumerable<YearComplexBudgetProject> GetByMaster(int masterBudgetId);
        
        YearComplexBudgetProject GetTemplate();
        
        void FinilizeBudget(int budgetId);
        
        void ReviewBudget(int budgetId);

        void ReviewByQuarterBudget(int budgetId);

        IEnumerable<YearComplexBudgetProject> GetApprovedBudgets(int adminUnitId);
        
        void ReviewByMonthBudget(int monthBudgetId);

        IEnumerable<YearComplexBudgetProject> GetArchiveBudgets(DateTime nowDate, int adminUnitId);
    }
}