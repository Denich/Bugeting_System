using System;
using System.Collections.Generic;

namespace Budget.Web.Models
{
    public class TargetBudgetInfoModel
    {
        public int Id { get; set; }

        public string BudgetCategoryName { get; set; }

        public int BudgetCategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public string Source { get; set; }

        public IEnumerable<BudgetItemInfoModel> BudgetItemInfoModels { get; set; }
    }
}