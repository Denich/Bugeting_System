namespace Budget.Web.Models
{
    public class QuarterUnapproveComplexBudgetInfoModel : BaseUnapproveComplexBudgetInfoViewModel
    {
        public int Quarter { get; set; }

        public int Year { get; set; }
    }
}