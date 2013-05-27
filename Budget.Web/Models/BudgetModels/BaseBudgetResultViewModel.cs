namespace Budget.Web.Models
{
    public interface IBaseBudgetResultViewModel
    {
        int ProjectBudgetId { get; set; }

        int ResultBudgetId { get; set; }

        double TotalIncomePlan { get; set; }

        double TotalCostsPlan { get; set; }

        double BalancePlan { get; set; }

        int ProjectItemsCount { get; set; }

        int ProcessedItemsCount { get; set; }
    }
}