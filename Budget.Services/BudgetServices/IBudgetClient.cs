using Budget.Services.BudgetServices.DataProviderContracts;

namespace Budget.Services.BudgetServices
{
    public interface IBudgetClient
    {
        IBudgetDataManagement DataManagement { get; set; }
    }
}