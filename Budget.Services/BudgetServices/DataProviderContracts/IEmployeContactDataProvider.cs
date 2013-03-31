using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IEmployeContactDataProvider
    {
        IEnumerable<EmployeContact> GetAll();

        EmployeContact Get(int employeContactId);

        int Insert(EmployeContact employeContact);

        int Update(EmployeContact employeContact);

        int Delete(int employeContactId);
    }
}