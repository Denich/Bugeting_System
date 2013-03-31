using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class AdministrativeUnitDataProvider : IAdministrativeUnitDataProvider
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        private readonly IFinancialCenterDataProvider _financialCenterDataProvider;

        [InjectionConstructor]
        public AdministrativeUnitDataProvider(ICompanyDataProvider companyDataProvider,
                                              IFinancialCenterDataProvider financialCenterDataProvider)
        {
            _companyDataProvider = companyDataProvider;
            _financialCenterDataProvider = financialCenterDataProvider;
        }

        public AdministrativeUnit Get(int id)
        {
            return _companyDataProvider.Get().Id == id
                       ? _companyDataProvider.Get()
                       : (AdministrativeUnit) _financialCenterDataProvider.Get(id);
        }
    }
}