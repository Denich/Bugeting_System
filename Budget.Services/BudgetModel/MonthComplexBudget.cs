namespace Budget.Services.BudgetModel
{
    public class MonthComplexBudget : ComplexBudget
    {
        public int Year { get; set; }

        public int Month { get; set; }
    }
}