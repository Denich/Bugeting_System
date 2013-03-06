using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IEmployeDataProvider
    {
        Employe GetEmploye(int employeId);
    }
}