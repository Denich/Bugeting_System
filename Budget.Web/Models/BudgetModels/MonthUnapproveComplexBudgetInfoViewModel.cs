namespace Budget.Web.Models
{
    public class MonthUnapproveBudgetModel : BaseUnapproveBudgetModel
    {
        public int Month { get; set; }

        public int Year { get; set; }
    }
}