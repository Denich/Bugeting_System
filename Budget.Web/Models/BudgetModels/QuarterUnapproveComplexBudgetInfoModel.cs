namespace Budget.Web.Models
{
    public class QuarterUnapproveBudgetModel : BaseUnapproveBudgetModel
    {
        public int Quarter { get; set; }

        public int Year { get; set; }
    }
}