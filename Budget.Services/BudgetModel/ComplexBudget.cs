using System.Collections.Generic;
using Budget.Services.BudgetServices.DataProviders;

namespace Budget.Services.BudgetModel
{
    public class ComplexBudget
    {
        private AdministrativeUnit _administrativeUnit;

        public ComplexBudget()
        {
            AdministrativeUnitDataProvider = new AdministrativeUnitDataProvider();    
        }

        public AdministrativeUnitDataProvider AdministrativeUnitDataProvider { get; set; }

        public int Id { get; set; }

        public int AdministrativeUnitId { get; set; }

        public AdministrativeUnit AdministrativeUnit
        {

            get { return _administrativeUnit ?? AdministrativeUnitDataProvider.GetAdministrativeUnitById(AdministrativeUnitId); }
            set
            {
                _administrativeUnit = value;

                AdministrativeUnitId = value.Id;
            }
        }

        public IEnumerable<BudgetCategory> BudgetCategories { get; set; }
    }
}
