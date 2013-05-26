namespace Budget.Services.BudgetModel
{
    public class UnapproveMonthBudget : BaseUnapproveBudget
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string Period { get; set; }
    }
}