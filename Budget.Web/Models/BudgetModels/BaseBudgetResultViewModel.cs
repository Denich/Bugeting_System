namespace Budget.Web.Models
{
    public interface IBaseBudgetResultViewModel
    {
        double TotalIncomeActual { get; set; }

        double TotalCostsActual { get; set; }

        double BalanceActual { get; set; }

        int ProjectItemsCount { get; set; }

        int ProcessedItemsCount { get; set; }
    }
}