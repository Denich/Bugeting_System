using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IMonthComplexBudgetDataProvider
    {
        IEnumerable<MonthComplexBudget> GetAll();

        MonthComplexBudget Get(int monthComplexBudgetId);

        int Insert(MonthComplexBudget monthComplexBudget);

        int Update(MonthComplexBudget monthComplexBudget);

        int Delete(int monthComplexBudgetId);

        MonthComplexBudget GetFor(int year, int month, int adminUnitId);
        
        void StartBudgetResultForProject(MonthComplexBudgetProject budgetProject);
    }
}