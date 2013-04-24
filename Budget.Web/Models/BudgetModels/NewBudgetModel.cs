using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models.BudgetModels
{
    public class NewBudgetModel
    {
        public int SelectedYear { get; set; }

        public IEnumerable<int> AllowedYears { get; set; }
    }
}