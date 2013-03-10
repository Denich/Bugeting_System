using System;

namespace Budget.Web.Models
{
    public class BudgetItemInfoModel
    {
        public int Id { get; set; }

        public int TargetBudgetId { get; set; }

        public string TargetBudgetName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateAdded { get; set; }

        public string Source { get; set; }
    }
}