using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;
using Microsoft.Practices.Unity;

namespace Budget.Services.Helpers
{
    public class CustomDataProvider<T> where T : IDataRetriever<T>
    {
        private readonly DbProcedureSet _procedureSet;

        private readonly string _connectionString;
        
        public CustomDataProvider(string connectionString, DbProcedureSet procedureSet)
        {
            _connectionString = connectionString;
            _procedureSet = procedureSet;
        }

        public IEnumerable<T> GetItems()
        {
            return IocContainer.Instance.Resolve<T>().GetItems(_connectionString, _procedureSet.SelectProcedureName);
        }

        public T GetItem(int id)
        {
            return IocContainer.Instance.Resolve<T>().GetItemById(id, _connectionString, _procedureSet.SelectByIdProcedureName);
        }

        public int AddItem(T item)
        {
            return item.AddItem(_connectionString, _procedureSet.InsertProcedureName);
        }

        public int UpdateItem(T item)
        {
            return item.UpdateItem(_connectionString, _procedureSet.UpdateProcedureName);
        }

        public int DeleteItem(int id)
        {
            return IocContainer.Instance.Resolve<T>().DeleteItem(id, _connectionString, _procedureSet.DeleteProcedureName);
        }
    }
}
