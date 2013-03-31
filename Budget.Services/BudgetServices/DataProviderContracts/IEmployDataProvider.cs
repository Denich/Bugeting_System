using System.Collections.Generic;
using Budget.Services.BudgetModel;

namespace Budget.Services.BudgetServices.DataProviderContracts
{
    public interface IEmployeDataProvider
    {
        IEnumerable<Employe> GetAll();

        Employe Get(int id);

        int Insert(Employe employe);

        int Update(Employe employe);

        int Delete(int id);
    }
}