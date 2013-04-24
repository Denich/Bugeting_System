using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.Management;

namespace Budget.Services.BudgetServices
{
    public interface IBudgetClient
    {
        IBudgetDataManagement Data { get; set; }

        IBudgetOperationManagement BudgetOperation { get; set; }
    }
}