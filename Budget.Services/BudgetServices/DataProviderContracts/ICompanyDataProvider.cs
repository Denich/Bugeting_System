using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface ICompanyDataProvider
    {
        Company Get();

        int Update(Company company);
    }
}