namespace Budget.Web.Models
{
    public class MonthComplexBudgetListViewModel : BaseBudgetListViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }
    }
}