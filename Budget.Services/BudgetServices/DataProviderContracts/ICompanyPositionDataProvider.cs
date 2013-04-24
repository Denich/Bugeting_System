using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface ICompanyPositionDataProvider
    {
        IEnumerable<CompanyPosition> GetAll();

        CompanyPosition Get(int companyPositionId);

        int Insert(CompanyPosition companyPosition);

        int Update(CompanyPosition companyPosition);

        int Delete(int companyPositionId);
    }
}
