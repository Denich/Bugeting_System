using Budget.Web.Models.BudgetModels;

namespace Budget.Web.Models
{
    public class MonthComplexBudgetResultListViewModel : MonthComplexBudgetListViewModel, IBaseBudgetResultViewModel
    {
        public int ProjectBudgetId { get; set; }

        public int ResultBudgetId { get; set; }

        public double TotalIncomePlan { get; set; }

        public double TotalCostsPlan { get; set; }

        public double BalancePlan { get; set; }

        public int ProjectItemsCount { get; set; }

        public int ProcessedItemsCount { get; set; }
    }
}