namespace Budget.Web.Models.BudgetModels
{
    public class QuarterComplexBudgetViewModel : CommonComplexBudgetViewModel
    {
        public int Year { get; set; }

        public int QuarterNumber { get; set; }
    }
}