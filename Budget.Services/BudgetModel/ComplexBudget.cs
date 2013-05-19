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

        private IEnumerable<BudgetCategory> _budgetCategories;

        protected ComplexBudget()
        {
            MasterBudgetID = -1;
        }

        [Dependency]
        public IAdministrativeUnitDataProvider AdministrativeUnitDataProvider { get; set; }

        [Dependency]
        public IBudgetCategoryDataProvider BudgetCategoryDataProvider { get; set; }

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

        public IEnumerable<BudgetCategory> BudgetCategories
        {
            get { return _budgetCategories ?? BudgetCategoryDataProvider.GetForBudget(Id); }
            set { _budgetCategories = value; }
        }

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

        public BudgetCategory FindCategoryByInfoId(int infoId)
        {
            return BudgetCategories.FirstOrDefault(b => b.InfoId == infoId);
        }

        public TargetBudget FindTargetBudgetByInfoId(int infoId)
        {
            return BudgetCategories.SelectMany(c => c.TargetBudgets).FirstOrDefault(t => t.InfoId == infoId);
        }

        public BudgetItem FindBudgetItemByInfoId(int infoId)
        {
            return
                BudgetCategories.SelectMany(c => c.TargetBudgets)
                                .SelectMany(c => c.BudgetItems)
                                .FirstOrDefault(t => t.InfoId == infoId);
        }

        //TODO: Chek null reference exceptions
        protected IEnumerable<BudgetCategory> GetValuesSumFormCategories(IEnumerable<BudgetCategory> parentBudgetCategories, IEnumerable<BudgetCategory> childBudgetCategories)
        {
            if (parentBudgetCategories == null)
            {
                return null;
            }

            foreach (var parentCategory in parentBudgetCategories)
            {
                IEnumerable<BudgetCategory> matchCategories = childBudgetCategories.Where(b => b.InfoId == parentCategory.InfoId);

                parentCategory.Value = matchCategories.Sum(c => c.Value);

                parentCategory.TargetBudgets = GetValueFromTargetBudgets(parentCategory.TargetBudgets, matchCategories.SelectMany(c => c.TargetBudgets));
            }

            return parentBudgetCategories;
        }

        private IEnumerable<TargetBudget> GetValueFromTargetBudgets(IEnumerable<TargetBudget> targetBudgets, IEnumerable<TargetBudget> targets)
        {
            if (targetBudgets == null)
            {
                return null;
            }

            foreach (var targetBudget in targetBudgets)
            {
                var matchTargets = targets.Where(b => b.InfoId == targetBudget.InfoId);

                targetBudget.Value = matchTargets.Sum(c => c.Value);

                targetBudget.BudgetItems = GetValueFromBudgetItems(targetBudget.BudgetItems, matchTargets.SelectMany(c => c.BudgetItems));
            }

            return targetBudgets;
        }

        private IEnumerable<BudgetItem> GetValueFromBudgetItems(IEnumerable<BudgetItem> budgetItems, IEnumerable<BudgetItem> items)
        {
            if (budgetItems == null)
            {
                return null;
            }

            foreach (var budgetItem in budgetItems)
            {
                var matchItems = items.Where(b => b.InfoId == budgetItem.InfoId);

                budgetItem.Value = matchItems.Sum(c => c.Value);
            }

            return budgetItems;
        }

        public abstract string GetPeriodName();

        public abstract string GetShortPeriodName();
    }
}
