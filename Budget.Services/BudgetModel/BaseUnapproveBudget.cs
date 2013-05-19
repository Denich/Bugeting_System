namespace Budget.Services.BudgetModel
{
    public class BaseUnapproveBudget
    {
        public int LastApprovedBudgetId { get; set; }

        public int RevisionCount { get; set; }

        public int WaitingOfferCount { get; set; }
    }
}