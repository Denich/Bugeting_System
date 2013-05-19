using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviders;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IBudgetCategoryDataProvider
    {
        IEnumerable<BudgetCategory> GetAll();

        BudgetCategory Get(int budgetCategoryId);

        int Insert(BudgetCategory budgetCategory);

        int Update(BudgetCategory budgetCategory);

        int Delete(int budgetCategoryId);

        IEnumerable<BudgetCategory> GetForBudget(int budgetId);
        
        BudgetCategory GetTemplate();
    }
}