using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.Helpers;
using Microsoft.Practices.Unity;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class QuarterComplexBudgetProjectDataProvider : IQuarterComplexBudgetProjectDataProvider
    {
        private readonly CustomDataProvider<QuarterComplexBudgetProject> _provider;

        [InjectionConstructor]
        public QuarterComplexBudgetProjectDataProvider([Dependency("ConnectionString")] string connectionString,
                                   [Dependency("QuarterComplexBudgetProjectProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<QuarterComplexBudgetProject>(connectionString, procedureSet);
        }

        public IEnumerable<QuarterComplexBudgetProject> GetAll()
        {
            return _provider.GetItems();
        }

        public QuarterComplexBudgetProject Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(QuarterComplexBudgetProject QuarterComplexBudgetProject)
        {
            return _provider.AddItem(QuarterComplexBudgetProject);
        }

        public int Update(QuarterComplexBudgetProject QuarterComplexBudgetProject)
        {
            return _provider.UpdateItem(QuarterComplexBudgetProject);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public QuarterComplexBudget GetFinalFor(int adminUnitId, int year, int quarter)
        {
            var budgets = GetAll();

            return budgets != null
                       ? budgets.First(
                           b =>
                           b.AdministrativeUnitId == adminUnitId && b.Year == year && b.QuarterNumber == quarter &&
                           b.IsFinal)
                       : null;
        }
    }
}
