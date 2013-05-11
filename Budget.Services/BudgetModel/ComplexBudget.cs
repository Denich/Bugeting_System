using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetModel
{
    public abstract class ComplexBudget
    {
        private AdministrativeUnit _administrativeUnit;

        protected ComplexBudget()
        {
            MasterBudgetID = -1;
        }

        [Dependency]
        public IAdministrativeUnitDataProvider AdministrativeUnitDataProvider { get; set; }

        public int Id { get; set; }

        public int MasterBudgetID { get; set; }

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

        
        public double TotalIncome
        {
            get { return BudgetCategories != null ? BudgetCategories.Where(c => c.Value > 0).Sum(c => c.Value) : 0; }
        }

        public double TotalCosts
        {
            get
            {
                return BudgetCategories != null
                           ? Math.Abs(BudgetCategories.Where(c => c.Value < 0).Sum(c => c.Value))
                           : 0;
            }
        }

        public double Balance
        {
            get { return TotalIncome - TotalCosts; }
        }

        public virtual void CalculateValues()
        {
            BudgetCategories.ForEach(b => b.Calulate());
        }

        //TODO: Chek null reference exceptions
        protected IEnumerable<BudgetCategory> GetValuesSumFormCategories(IEnumerable<BudgetCategory> budgetCategories, IEnumerable<IEnumerable<BudgetCategory>> categories)
        {
            if (budgetCategories == null)
            {
                return null;
            }

            foreach (var budgetCategory in budgetCategories)
            {
                IEnumerable<BudgetCategory> matchCategories = categories.Select(c => c.SingleOrDefault(b => b.InfoId == budgetCategory.InfoId));

                budgetCategory.Value = matchCategories.Sum(c => c.Value);

                budgetCategory.TargetBudgets = GetValueFromTargetBudgets(budgetCategory.TargetBudgets, matchCategories.Select(c => c.TargetBudgets));
            }

            return budgetCategories;
        }

        private IEnumerable<TargetBudget> GetValueFromTargetBudgets(IEnumerable<TargetBudget> targetBudgets, IEnumerable<IEnumerable<TargetBudget>> targets)
        {
            if (targetBudgets == null)
            {
                return null;
            }

            foreach (var targetBudget in targetBudgets)
            {
                var matchTargets = targets.Select(c => c.SingleOrDefault(b => b.InfoId == targetBudget.InfoId));

                targetBudget.Value = matchTargets.Sum(c => c.Value);

                targetBudget.BudgetItems = GetValueFromBudgetItems(targetBudget.BudgetItems, matchTargets.Select(c => c.BudgetItems));
            }

            return targetBudgets;
        }

        private IEnumerable<BudgetItem> GetValueFromBudgetItems(IEnumerable<BudgetItem> budgetItems, IEnumerable<IEnumerable<BudgetItem>> items)
        {
            if (budgetItems == null)
            {
                return null;
            }

            foreach (var budgetItem in budgetItems)
            {
                var matchItems = items.Select(c => c.SingleOrDefault(b => b.InfoId == budgetItem.InfoId));

                budgetItem.Value = matchItems.Sum(c => c.Value);
            }

            return budgetItems;
        }
    }
}
