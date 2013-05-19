namespace Budget.Web.Models.BudgetModels
{
    public class QuarterParentComplexBudgetViewModel : ParentComplexBudgetViewModel
    {
        public int Year { get; set; }

        public int QuarterNumber { get; set; }
    }
}