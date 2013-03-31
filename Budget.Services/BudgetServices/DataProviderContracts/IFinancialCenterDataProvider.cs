using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IFinancialCenterDataProvider
    {
        IEnumerable<FinancialCenter> GetAll();

        FinancialCenter Get(int id);

        int Insert(FinancialCenter financialCenter);

        int Update(FinancialCenter financialCenter);

        int Delete(int id);
    }
}