using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.Management
{
    public interface IBudgetOperationManagement
    {
        Dictionary<YearComplexBudget, YearComplexBudgetProject> GetUnresultedYearBudgets(int adminUnitId);

        Dictionary<QuarterComplexBudget, QuarterComplexBudgetProject> GetUnresultedQuarterBudgets(int adminUnitId);

        Dictionary<MonthComplexBudget, MonthComplexBudgetProject> GetUnresultedMonthBudgets(int adminUnitId);
    }
}