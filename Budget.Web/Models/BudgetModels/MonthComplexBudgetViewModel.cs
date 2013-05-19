namespace Budget.Web.Models.BudgetModels
{
    public class MonthComplexBudgetViewModel : ParentComplexBudgetViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }
    }
}