using System.Collections.Generic;
using Budget.Web.Helpers.Converters;

namespace Budget.Web.Models.BudgetModels
{
    public class ParentComplexBudgetViewModel : CommonComplexBudgetViewModel
    {
        public IEnumerable<BudgetProjectItemViewModel> BudgetItems { get; set; }

        public IEnumerable<CommonComplexBudgetViewModel> DerivedBudgets { get; set; }

        public bool CanEdit { get; set; }

        public bool CanFinalize { get; set; }

        public bool CanReview { get; set; }

        public string EditChildBudgetsController { get; set; }

        public string EditChildBudgetsAction { get; set; }

        public string SwitchToOtherTypeController { get; set; }

        public string SwitchToOtherTypeAction { get; set; }

        public string SwitchtoOtherTypeName { get; set; }
        
        public string EditAction { get; set; }

        public string EditController { get; set; }

        public string FinilizeAction { get; set; }

        public string FinilizeController { get; set; }

        public string ReviewController { get; set; }

        public string ReviewAction { get; set; }

        public int ResultsBudgetId { get; set; }

        public bool CanStartResults { get; set; }

        public string StartResultAction { get; set; }

        public string StartResultController { get; set; }

        public string ViewBudgetResultAction { get; set; }

        public string ViewBudgetResultController { get; set; }
    }
}