using System.Collections.Generic;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public abstract class ComplexBudget
    {
        private AdministrativeUnit _administrativeUnit;

        [Dependency]
        public IAdministrativeUnitDataProvider AdministrativeUnitDataProvider { get; set; }

        public int Id { get; set; }

        public int AdministrativeUnitId { get; set; }

        public bool IsFinal { get; set; }

        public AdministrativeUnit AdministrativeUnit
        {
            get { return _administrativeUnit ?? AdministrativeUnitDataProvider.Get(AdministrativeUnitId); }
            set
            {
                _administrativeUnit = value;

                AdministrativeUnitId = value.Id;
            }
        }

        public IEnumerable<BudgetCategory> BudgetCategories { get; set; }
    }
}
