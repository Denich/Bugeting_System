using Budget.Web.Models.BudgetModels;

namespace Budget.Web.Models
{
    public class MonthComplexBudgetResultListViewModel : MonthComplexBudgetListViewModel, IBaseBudgetResultViewModel
    {
        public double TotalIncomeActual { get; set; }

        public double TotalCostsActual { get; set; }

        public double BalanceActual { get; set; }

        public int ProjectItemsCount { get; set; }

        public int ProcessedItemsCount { get; set; }
    }
}