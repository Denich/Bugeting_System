namespace Budget.Services.BudgetModel
{
    public class UnapproveQuarterBudget : BaseUnapproveBudget
    {
        public int Year { get; set; }

        public int Quarter { get; set; }
    }
}