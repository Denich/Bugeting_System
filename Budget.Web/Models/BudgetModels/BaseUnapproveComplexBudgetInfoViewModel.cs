namespace Budget.Web.Models
{
    public class BaseUnapproveBudgetModel
    {
        public int LastApprovedBudgetId { get; set; }

        public int RevisionCount { get; set; }

        public int WaitingOfferCount { get; set; }
    }
}