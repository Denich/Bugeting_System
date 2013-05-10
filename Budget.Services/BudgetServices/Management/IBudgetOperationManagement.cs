using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.Management
{
    public interface IBudgetOperationManagement
    {
        void InsertBudgetRecursivly(YearComplexBudgetProject yearComplexBudgetProject);
        
        IEnumerable<YearComplexBudget> GetUnresultedYearBudgets(int adminUnitId);

        IEnumerable<QuarterComplexBudget> GetUnresultedQuarterBudgets(int adminUnitId);

        IEnumerable<MonthComplexBudget> GetUnresultedMonthBudgets(int adminUnitId);

        void GenerateQuarterBudgetProjects(int adminUnitId, int year, int userId, bool accept);

        void GenerateMonthBudgetProjects(int adminUnitId, int year, int userId, bool accept);
    }
}