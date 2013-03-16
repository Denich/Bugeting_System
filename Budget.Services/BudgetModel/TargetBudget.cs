using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class TargetBudget
    {
        public int Id { get; set; }

        public TargetBudgetInfo Info { get; set; }

        public double Value { get; set; }

        public int BudgetCategoryId { get; set; }

        public IEnumerable<BudgetItem> BudgetItems { get; set; }
    }
}
