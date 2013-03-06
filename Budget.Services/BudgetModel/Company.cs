using System.Collections.Generic;
using System.Linq;
using Budget.Services.BudgetServices.DataProviders;


namespace Budget.Services.BudgetModel
{
    public class Company : AdministrativeUnit
    {
        private IEnumerable<FinancialCenter> _financialCenters;

        public IAdministrativeUnitDataProvider AdministrativeUnitDataProvider { get; set; }

        public Company(string name): base(name)
        {
            AdministrativeUnitDataProvider = new AdministrativeUnitDataProvider();//todo: change for DI
        }

        public Company(int id, string name) : base(id, name)
        {
            AdministrativeUnitDataProvider = new AdministrativeUnitDataProvider();//todo: change for DI
        }

        public int AccountNumber { get; set; }

        public int Edrpou { get; set; }

        public IEnumerable<FinancialCenter> FinancialCenters
        {
            get
            {
                return _financialCenters ?? AdministrativeUnitDataProvider.GetFinancialCenters().Where(c => c.CompanyId == Id);
            }
            set { _financialCenters = value; }
        }
    }
}