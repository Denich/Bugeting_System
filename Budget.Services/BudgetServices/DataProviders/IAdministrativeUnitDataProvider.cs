using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IAdministrativeUnitDataProvider
    {
        Company GetCompany();

        int UpdateCompany(Company company);

        int AddCompany(Company company);

        int DeleteCompany(int companyId);

        IEnumerable<FinancialCenter> GetFinancialCenters();

        FinancialCenter GetFinancialCenterById(int centerId);

        int AddFinancialCenter(FinancialCenter financialCenter);

        int UpdateFinancialCenter(FinancialCenter financialCenter);

        int DeleteFinancialCenter(int financialCenterId);
    }
}