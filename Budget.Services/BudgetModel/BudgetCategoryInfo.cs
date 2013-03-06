using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class BudgetCategoryInfo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<TargetBudgetInfo> TargetBudgetInfos { get; set; }

        public bool IsDeleted { get; set; }
    }
}
