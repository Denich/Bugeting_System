namespace Budget.Services.BudgetModel
{
    public class UnapproveMonthBudget : BaseUnapproveBudget
    {
        public int Year { get; set; }

        public int Month { get; set; }
    }
}