namespace Budget.Web.Models.BudgetModels
{
    public class MonthComplexBudgetViewModel : CommonComplexBudgetViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }
    }
}