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
    public class YearComplexBudgetProjectDataProvider : IYearComplexBudgetProjectDataProvider
    {
        private readonly CustomDataProvider<YearComplexBudgetProject> _provider;

        [InjectionConstructor]
        public YearComplexBudgetProjectDataProvider([Dependency("ConnectionString")] string connectionString,
                                                    [Dependency("YearComplexBudgetProjectProcedures")] DbProcedureSet
                                                        procedureSet)
        {
            _provider = new CustomDataProvider<YearComplexBudgetProject>(connectionString, procedureSet);
        }

        public IEnumerable<YearComplexBudgetProject> GetAll()
        {
            return _provider.GetItems();
        }

        public YearComplexBudgetProject Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(YearComplexBudgetProject yearComplexBudgetProject)
        {
            yearComplexBudgetProject.Revision = GetBudgetNextRevision(yearComplexBudgetProject.Year,
                                                                      yearComplexBudgetProject.AdministrativeUnitId);
            yearComplexBudgetProject.RevisionDate = DateTime.Now;
            return _provider.AddItem(yearComplexBudgetProject);
        }

        public int Update(YearComplexBudgetProject yearComplexBudgetProject)
        {
            return _provider.UpdateItem(yearComplexBudgetProject);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public IEnumerable<YearComplexBudgetProject> GetBudgetProjects(int year, int fcenterId)
        {
            var budgets = GetAll();

            return budgets != null ? budgets.Where(b => b.AdministrativeUnitId == fcenterId && b.Year == year) : null;
        }

        public YearComplexBudgetProject GetLatestAcceptedBudgetProject(int year, int fcenterId)
        {
            var budgets = GetBudgetProjects(year, fcenterId);
            return budgets != null && budgets.Any(p => p.IsAccepted)
                       ? budgets.Where(p => p.IsAccepted).OrderByDescending(p => p.Revision).First()
                       : null;
        }

        public YearComplexBudgetProject GetFinalFor(int adminUnitId, int year)
        {
            var budgets = GetAll();
            return budgets != null
                       ? budgets.First(b => b.AdministrativeUnitId == adminUnitId && b.Year == year && b.IsFinal)
                       : null;
        }

        public IEnumerable<UnapproveYearBudget> GetUnapprovalBudgetsInfo(int adminUnitId)
        {
            var budgets = GetAll();
            return budgets != null
                       ? budgets.Where(b => !b.IsFinal).GroupBy(b => b.Year, (key, group) => new UnapproveYearBudget
                           {
                               Yeat = key,
                               RevisionCount = group.Count(),
                               WaitingOfferCount = group.Count(x => !x.IsDenied && !x.IsAccepted)
                           })
                       : null;
        }


        private int GetBudgetNextRevision(int year, int fcenterId)
        {
            var fcenterBudgets = GetBudgetProjects(year, fcenterId);

            return fcenterBudgets.Count() != 0 ? fcenterBudgets.Max(p => p.Revision) : 0;
        }
    }
}
