namespace Budget.Web.Models
{
    public class QuarterComplexBudgetListViewModel : BaseBudgetListViewModel
    {
        public int Year { get; set; }

        public int QuarterNumber { get; set; }
    }
}