using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class EmployeDataProvider : IEmployeDataProvider
    {
        private readonly CustomDataProvider<Employe> _provider;

        [InjectionConstructor]
        public EmployeDataProvider([Dependency("ConnectionString")] string connectionString,
                                   [Dependency("EmployeProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<Employe>(connectionString, procedureSet);
        }

        public IEnumerable<Employe> GetAll()
        {
            return _provider.GetItems();
        }

        public Employe Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(Employe employe)
        {
            return _provider.AddItem(employe);
        }

        public int Update(Employe employe)
        {
            return _provider.UpdateItem(employe);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public Employe GetUnknown()
        {
            var unknownEmploye = IocContainer.Instance.Resolve<Employe>();

            unknownEmploye.Id = 0;
            
            return unknownEmploye;
        }
    }
}
