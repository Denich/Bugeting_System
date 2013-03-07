using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models
{
    public class BudgetCategoryInfoModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<TargetBudgetInfoModel> TargetBudgetInfoModels { get; set; }
    }

    public class TargetBudgetInfoModel
    {
        public int Id { get; set; }

        public int BudgetCategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<BudgetItemInfoModel> BudgetItemInfoModels { get; set; }
    }

    public class BudgetItemInfoModel
    {
        public int Id { get; set; }

        public int TargetBudgetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}