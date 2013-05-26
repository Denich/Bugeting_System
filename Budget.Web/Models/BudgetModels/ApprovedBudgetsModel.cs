using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Web.Models.BudgetModels
{
    public class ApprovedBudgetsModel
    {
        public IEnumerable<YearComplexBudgetListViewModel> YearApprovedBudgets { get; set; }

        public IEnumerable<QuarterComplexBudgetListViewModel> QuarterApprovedBudgets { get; set; }

        public IEnumerable<MonthComplexBudgetListViewModel> MonthApprovedBudgets { get; set; }
    }
}