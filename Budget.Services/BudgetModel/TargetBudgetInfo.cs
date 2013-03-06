using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class TargetBudgetInfo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<BudgetItemInfo> TargetBudgetInfos { get; set; }

        public bool IsActive { get; set; }
    }
}