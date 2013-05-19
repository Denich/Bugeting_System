using System.Collections.Generic;
using System.Linq;
using Budget.Services.BudgetModel;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.Management
{
    public class BudgetOperationManagement : IBudgetOperationManagement
    {
        private readonly IBudgetDataManagement _data;

        [InjectionConstructor]
        public BudgetOperationManagement(IBudgetDataManagement budgetDataManagement)
        {
            _data = budgetDataManagement;
        }
       
        public IEnumerable<YearComplexBudget> GetUnresultedYearBudgets(int adminUnitId)
        {
            var budgets = _data.YearComplexBudgets.GetAll();
            return budgets != null ? budgets.Where(b => !b.IsFinal && b.AdministrativeUnitId == adminUnitId) : null;
        }

        public IEnumerable<QuarterComplexBudget> GetUnresultedQuarterBudgets(int adminUnitId)
        {
            var budgets = _data.QuarterComplexBudgets.GetAll();
            return budgets != null ? budgets.Where(b => b.AdministrativeUnitId == adminUnitId && !b.IsFinal) : null;
        }

        public IEnumerable<MonthComplexBudget> GetUnresultedMonthBudgets(int adminUnitId)
        {
            var budgets = _data.MonthComplexBudgets.GetAll();
            return budgets != null ? budgets.Where(b => b.AdministrativeUnitId == adminUnitId && !b.IsFinal) : null;
        }
    }
}