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
    public class QuarterComplexBudgetProjectDataProvider : BaseComplexBudgetProjectDataProvider, IQuarterComplexBudgetProjectDataProvider
    {
        private readonly CustomDataProvider<QuarterComplexBudgetProject> _provider;

        [InjectionConstructor]
        public QuarterComplexBudgetProjectDataProvider([Dependency("ConnectionString")] string connectionString,
                                   [Dependency("QuarterComplexBudgetProjectProcedures")] DbProcedureSet procedureSet)
        {
            _provider = new CustomDataProvider<QuarterComplexBudgetProject>(connectionString, procedureSet);
        }

        [Dependency]
        public IMonthComplexBudgetProjectDataProvider MonthComplexBudgetProjectDataProvider { get; set; }

        public IEnumerable<QuarterComplexBudgetProject> GetAll()
        {
            return _provider.GetItems();
        }

        public QuarterComplexBudgetProject Get(int id)
        {
            return _provider.GetItem(id);
        }

        public int Insert(QuarterComplexBudgetProject quarterComplexBudgetProject)
        {
            quarterComplexBudgetProject.Revision = GetBudgetNextRevision(quarterComplexBudgetProject.Year,
                                                                         quarterComplexBudgetProject.QuarterNumber,
                                                                         quarterComplexBudgetProject.AdministrativeUnitId);
            quarterComplexBudgetProject.RevisionDate = DateTime.Now;
            
            int quarterBudgetId = _provider.AddItem(quarterComplexBudgetProject);

            InsertCategoriesRecursivly(quarterComplexBudgetProject.BudgetCategories, quarterBudgetId);

            //Insert month budgets
            if (quarterComplexBudgetProject.MonthBudgets != null)
            {
                foreach (var monthBudget in quarterComplexBudgetProject.MonthBudgets)
                {
                    monthBudget.QuarterBudgetID = quarterBudgetId;
                    MonthComplexBudgetProjectDataProvider.Insert(monthBudget);
                }
            }

            /*
            //Insert child budgets
            if (quarterComplexBudgetProject.ChildBudgets != null)
            {
                foreach (var childBudget in quarterComplexBudgetProject.ChildBudgets)
                {
                    childBudget.MasterBudgetID = quarterBudgetId;
                    Insert(childBudget);
                }
            }
            */

            return quarterBudgetId;
        }

        public int Update(QuarterComplexBudgetProject quarterComplexBudgetProject)
        {
            return _provider.UpdateItem(quarterComplexBudgetProject);
        }

        public int Delete(int id)
        {
            return _provider.DeleteItem(id);
        }

        public IEnumerable<QuarterComplexBudgetProject> GetBudgetProjects(int year, int quarter, int fcenterId)
        {
            var budgets = GetAll();

            return budgets != null
                       ? budgets.Where(
                           b => b.AdministrativeUnitId == fcenterId && b.Year == year && b.QuarterNumber == quarter)
                       : null;
        }

        public QuarterComplexBudgetProject GetLatestAcceptedBudgetProject(int year, int quarter, int fcenterId)
        {
            var budgets = GetBudgetProjects(year, quarter, fcenterId);
            return budgets != null && budgets.Any(p => p.IsAccepted)
                       ? budgets.Where(p => p.IsAccepted).OrderByDescending(p => p.Revision).FirstOrDefault()
                       : null;
        }

        public QuarterComplexBudgetProject GetFinalFor(int adminUnitId, int year, int quarter)
        {
            var budgets = GetAll();

            return budgets != null
                       ? budgets.FirstOrDefault(
                           b =>
                           b.AdministrativeUnitId == adminUnitId && b.Year == year && b.QuarterNumber == quarter &&
                           b.IsFinal)
                       : null;
        }

        public IEnumerable<UnapproveQuarterBudget> GetUnapprovalBudgets(int adminUnitId)
        {
            var quarterComplexBudgetProjects = _provider.GetItems().Where(b => b.AdministrativeUnitId == adminUnitId && !b.IsFinal).ToList();

            return
                quarterComplexBudgetProjects
                         .GroupBy(b => new { b.Year, b.QuarterNumber }, (key, group) => new UnapproveQuarterBudget
                         {
                             LastApprovedBudgetId = group.Where(b => b.IsAccepted).MaxBy(b => b.Revision).Id,
                             Year = key.Year,
                             Quarter =  key.QuarterNumber,
                             RevisionCount = group.Count(),
                             WaitingOfferCount = group.Count(x => x.Status == BudgetProjectStatus.Waiting)
                         });
        }

        public IEnumerable<QuarterComplexBudgetProject> GetChildForYearBudget(int yearBudgetId)
        {
            return GetAll().Where(b => b.YearBudgetID == yearBudgetId);
        }

        public IEnumerable<QuarterComplexBudgetProject> GetByMaster(int masterBudgetId)
        {
            return GetAll().Where(b => b.MasterBudgetID == masterBudgetId);
        }

        public QuarterComplexBudgetProject GetTemplate()
        {
            return IocContainer.Instance.Resolve<QuarterComplexBudgetProject>();
        }

        private int GetBudgetNextRevision(int year, int quarter, int fcenterId)
        {
            var fcenterBudgets = GetBudgetProjects(year, quarter, fcenterId);

            return fcenterBudgets.Any() ? fcenterBudgets.Max(p => p.Revision) + 1 : 0;
        }
    }
}
