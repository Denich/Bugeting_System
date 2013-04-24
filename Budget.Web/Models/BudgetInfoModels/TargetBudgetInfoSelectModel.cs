using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget.Web.Models
{
    public class TargetBudgetInfoSelectModel : BaseBudgetInfoSelectModel
    {
        public IList<BudgetItemInfoSelectModel> Items { get; set; }
    }
}
