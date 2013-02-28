using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class BudgetCategory
    {
        public BudgetCategoryInfo Info { get; set; }

        public double Value { get; set; }

        public ICollection<TargetBudget> Budgets { get; set; }

        public Employe ResponsibleEmploye { get; set; }
    }
}
