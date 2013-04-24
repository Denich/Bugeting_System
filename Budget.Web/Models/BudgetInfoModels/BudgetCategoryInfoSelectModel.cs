using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models
{
    public class BudgetCategoryInfoSelectModel : BaseBudgetInfoSelectModel
    {
        public IList<TargetBudgetInfoSelectModel> Targets { get; set; }
    }
}