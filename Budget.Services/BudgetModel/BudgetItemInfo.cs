namespace Budget.Services.BudgetModel
{
    public class BudgetItemInfo
    {
        public int Id { get; set; }

        public int TargetBudgetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
