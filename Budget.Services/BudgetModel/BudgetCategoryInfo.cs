﻿using System.Collections.Generic;

namespace Budget.Services.BudgetModel
{
    public class BudgetCategoryInfo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<TargetBudgetInfo> TargetBudgetInfos { get; set; }

        public bool IsActive { get; set; }
    }
}