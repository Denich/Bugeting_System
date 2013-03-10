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

        public DateTime DateAdded { get; set; }

        public string Source { get; set; }

        public IEnumerable<TargetBudgetInfoModel> TargetBudgetInfoModels { get; set; }
    }
}