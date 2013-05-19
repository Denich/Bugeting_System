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
using MoreLinq;

namespace Budget.Services.BudgetServices.DataProviders
{
    public class MonthComplexBudgetProjectDataProvider : BaseComplexBudgetProjectDataProvider, IMonthComplexBudgetProjectDataProvider
    {
        private readonly CustomDataProvider<MonthComplexBudgetProject> _provider;

        [InjectionConstructor]
        public MonthComplexBudgetProjectDataProvider([Dependency("ConnectionString")] string connectionString,
                                         [Dependency("MonthComplexBudgetProjectProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<MonthComplexBudgetProject>(connectionString, procedureSet);
        }

        public IEnumerable<MonthComplexBudgetProject> GetAll()
        {
            return _provider.GetItems();
        }

        public MonthComplexBudgetProject Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(MonthComplexBudgetProject monthComplexBudgetProject)
        {
            monthComplexBudgetProject.Revision = GetBudgetNextRevision(monthComplexBudgetProject.Year,
                                                             monthComplexBudgetProject.Month,
                                                             monthComplexBudgetProject.AdministrativeUnitId);

            monthComplexBudgetProject.RevisionDate = DateTime.Now;

            int monthBudgetId = _provider.AddItem(monthComplexBudgetProject);

            InsertCategoriesRecursivly(monthComplexBudgetProject.BudgetCategories, monthBudgetId);

            return monthBudgetId;
        }

        public int Update(MonthComplexBudgetProject monthComplexBudgetProject)
        {
            return _provider.UpdateItem(monthComplexBudgetProject);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public IEnumerable<MonthComplexBudgetProject> GetBudgetProjects(int year, int month, int fcenterId)
        {
            var budgets = GetAll();

            return budgets != null
                       ? budgets.Where(
                           b => b.AdministrativeUnitId == fcenterId && b.Year == year && b.Month == month)
                       : null;
        }

        public MonthComplexBudgetProject GetLatestAcceptedBudgetProject(int year, int month, int fcenterId)
        {
            var budgets = GetBudgetProjects(year, month, fcenterId);
            return budgets != null && budgets.Any(p => p.IsAccepted)
                       ? budgets.Where(p => p.IsAccepted).OrderByDescending(p => p.Revision).FirstOrDefault()
                       : null;
        }

        public MonthComplexBudgetProject GetFinalFor(int adminUnitId, int year, int month)
        {
            var budgets = GetAll();

            return budgets != null
                       ? budgets.FirstOrDefault(
                           b => b.AdministrativeUnitId == adminUnitId && b.Year == year && b.Month == month && b.IsFinal)
                       : null;
        }

        public IEnumerable<UnapproveMonthBudget> GetUnapprovalBudgets(int adminUnitId)
        {
            var monthComplexBudgetProjects = _provider.GetItems().Where(b => b.AdministrativeUnitId == adminUnitId && !b.IsFinal).ToList();

            return
                monthComplexBudgetProjects
                         .GroupBy(b => new { b.Year, b.Month }, (key, group) => new UnapproveMonthBudget
                         {
                             LastApprovedBudgetId = group.Where(b => b.IsAccepted).MaxBy(b => b.Revision).Id,
                             Year = key.Year,
                             Month = key.Month,
                             RevisionCount = group.Count(),
                             WaitingOfferCount = group.Count(x => x.Status == BudgetProjectStatus.Waiting)
                         });
        }

        public IEnumerable<MonthComplexBudgetProject> GetChildForQuarterBudget(int quarterBudgetId)
        {
            return GetAll().Where(b => b.QuarterBudgetID == quarterBudgetId);
        }

        public IEnumerable<MonthComplexBudgetProject> GetByMaster(int masterBudgetId)
        {
            return GetAll().Where(b => b.MasterBudgetID == masterBudgetId);
        }

        public MonthComplexBudgetProject GetTemplate()
        {
            return IocContainer.Instance.Resolve<MonthComplexBudgetProject>();
        }

        private int GetBudgetNextRevision(int year, int month, int fcenterId)
        {
            var fcenterBudgets = GetBudgetProjects(year, month, fcenterId);

            return fcenterBudgets.Any() ? fcenterBudgets.Max(p => p.Revision) + 1 : 0;
        }
    }
}
