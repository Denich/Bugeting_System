using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IAdministrativeUnitDataProvider
    {
        AdministrativeUnit Get(int id);
    }
}