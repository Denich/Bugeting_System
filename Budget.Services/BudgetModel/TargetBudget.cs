using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class TargetBudget
    {
        public TargetBudgetInfo Info { get; set; }

        public double Value { get; set; }

        public IEnumerable<BudgetItem> BudgetItems { get; set; }
    }
}
