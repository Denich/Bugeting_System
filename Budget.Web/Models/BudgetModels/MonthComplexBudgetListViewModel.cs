using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models.BudgetModels
{
    public class MonthComplexBudgetListViewModel : BaseBudgetListViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }
    }
}