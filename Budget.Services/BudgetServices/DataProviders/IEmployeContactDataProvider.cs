using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviders
{
    public interface IEmployeContactDataProvider
    {
        IEnumerable<EmployeContact> GetEmployeContacts();

        EmployeContact GetEmployeContactById(int employeContactId);

        int AddEmployeContact(EmployeContact employeContact);

        int UpdateEmployeContact(EmployeContact employeContact);

        int DeleteEmployeContact(int employeContactId);
    }
}