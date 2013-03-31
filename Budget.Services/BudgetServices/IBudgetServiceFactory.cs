namespace Budget.Services.BudgetServices
{
    public interface IBudgetServiceFactory
    {
        IBudgetClient GetBudgetClient(string connectionString);
    }
}