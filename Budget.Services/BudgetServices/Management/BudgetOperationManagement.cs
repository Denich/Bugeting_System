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
       

        public Dictionary<YearComplexBudget, YearComplexBudgetProject> GetUnresultedYearBudgets(int adminUnitId)
        {
            var dictinary = new Dictionary<YearComplexBudget, YearComplexBudgetProject>();
            
            var yearBudgets = _data.YearComplexBudgets.GetAll().Where(b => !b.IsFinal && b.AdministrativeUnitId == adminUnitId);

            foreach (var yearBudget in yearBudgets)
            {
                var budgetProject = _data.YearComplexBudgetProjects.GetFinalFor(yearBudget.AdministrativeUnitId, yearBudget.Year);
                dictinary.Add(yearBudget, budgetProject);
            }

            return dictinary;
        }

        public Dictionary<QuarterComplexBudget, QuarterComplexBudgetProject> GetUnresultedQuarterBudgets(int adminUnitId)
        {
            var dictinary = new Dictionary<QuarterComplexBudget, QuarterComplexBudgetProject>();

            var resultBudgets = _data.QuarterComplexBudgets.GetAll().Where(b => !b.IsFinal && b.AdministrativeUnitId == adminUnitId);

            foreach (var resultBudget in resultBudgets)
            {
                var budgetProject = _data.QuarterComplexBudgetProjects.GetFinalFor(resultBudget.AdministrativeUnitId, resultBudget.Year, resultBudget.QuarterNumber);
                dictinary.Add(resultBudget, budgetProject);
            }

            return dictinary;
        }

        public Dictionary<MonthComplexBudget, MonthComplexBudgetProject> GetUnresultedMonthBudgets(int adminUnitId)
        {
            var dictinary = new Dictionary<MonthComplexBudget, MonthComplexBudgetProject>();

            var resultBudgets = _data.MonthComplexBudgets.GetAll().Where(b => !b.IsFinal && b.AdministrativeUnitId == adminUnitId);

            foreach (var resultBudget in resultBudgets)
            {
                var budgetProject = _data.MonthComplexBudgetProjects.GetFinalFor(resultBudget.AdministrativeUnitId, resultBudget.Year, resultBudget.Month);
                dictinary.Add(resultBudget, budgetProject);
            }

            return dictinary;
        }
    }
}