using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class EmployeContactDataProvider : IEmployeContactDataProvider
    {
        private readonly CustomDataProvider<EmployeContact> _provider;

        [InjectionConstructor]
        public EmployeContactDataProvider([Dependency("ConnectionString")] string connectionString,
                                          [Dependency("EmployeContactProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<EmployeContact>(connectionString, procedureSet);
        }

        public IEnumerable<EmployeContact> GetAll()
        {
            return _provider.GetItems();
        }

        public EmployeContact Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(EmployeContact employeContact)
        {
            return _provider.AddItem(employeContact);
        }

        public int Update(EmployeContact employeContact)
        {
            return _provider.UpdateItem(employeContact);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
