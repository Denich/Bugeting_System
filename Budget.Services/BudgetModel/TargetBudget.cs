using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class TargetBudget
    {
        public TargetBudgetInfo Info { get; set; }

        public double Value { get; set; }

        public ICollection<BudgetItem> BudgetItems { get; set; }
    }
}
