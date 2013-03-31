using System.Collections.Generic;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class TargetBudgetInfoDataProvider : ITargetBudgetInfoDataProvider
    {
        private readonly CustomDataProvider<TargetBudgetInfo> _provider;

        [InjectionConstructor]
        public TargetBudgetInfoDataProvider([Dependency("ConnectionString")] string connectionString,
                                            [Dependency("TargetBudgetInfoProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<TargetBudgetInfo>(connectionString, procedureSet);
        }

        public IEnumerable<TargetBudgetInfo> GetAll()
        {
            return _provider.GetItems();
        }

        public TargetBudgetInfo Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(TargetBudgetInfo targetBudgetInfo)
        {
            return _provider.AddItem(targetBudgetInfo);
        }

        public int Update(TargetBudgetInfo targetBudgetInfo)
        {
            return _provider.UpdateItem(targetBudgetInfo);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }
    }
}
