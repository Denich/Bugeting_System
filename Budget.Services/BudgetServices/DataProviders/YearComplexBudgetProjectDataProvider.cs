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
    public class YearComplexBudgetProjectDataProvider : BaseComplexBudgetProjectDataProvider, IYearComplexBudgetProjectDataProvider 
    {
        private readonly CustomDataProvider<YearComplexBudgetProject> _provider;

        [Dependency]
        public IQuarterComplexBudgetProjectDataProvider QuarterComplexBudgetProjectDataProvider { get; set; }

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

            int yearBudgetId = _provider.AddItem(yearComplexBudgetProject);

            InsertCategoriesRecursivly(yearComplexBudgetProject.BudgetCategories, yearBudgetId);

            //Insert quarter budgets
            if (yearComplexBudgetProject.QuarterBudgets != null)
            {
                foreach (var quarterBudget in yearComplexBudgetProject.QuarterBudgets)
                {
                    QuarterComplexBudgetProjectDataProvider.Insert(quarterBudget);
                }
            }

            //Insert child budgets
            if (yearComplexBudgetProject.ChildBudgets != null)
            {
                foreach (var childBudget in yearComplexBudgetProject.ChildBudgets)
                {
                    childBudget.MasterBudgetID = yearBudgetId;
                    Insert(childBudget);
                }
            }

            return yearBudgetId;
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
                       ? budgets.Where(p => p.IsAccepted).OrderByDescending(p => p.Revision).FirstOrDefault()
                       : null;
        }

        public YearComplexBudgetProject GetFinalFor(int adminUnitId, int year)
        {
            var budgets = GetAll();
            return budgets != null
                       ? budgets.FirstOrDefault(b => b.AdministrativeUnitId == adminUnitId && b.Year == year && b.IsFinal)
                       : null;
        }

        public IEnumerable<UnapproveYearBudget> GetUnapprovalBudgets(int adminUnitId)
        {
            var budgets = GetAll();
            return budgets != null
                       ? budgets.Where(b => !b.IsFinal).GroupBy(b => b.Year, (key, group) => new UnapproveYearBudget
                           {
                               Year = key,
                               RevisionCount = group.Count(),
                               WaitingOfferCount = group.Count(x => x.Status == BudgetProjectStatus.Waiting)
                           })
                       : null;
        }

        public IEnumerable<YearComplexBudgetProject> GetByMaster(int masterBudgetId)
        {
            return GetAll().Where(b => b.MasterBudgetID == masterBudgetId);
        }

        public YearComplexBudgetProject GetTemplate()
        {
            return IocContainer.Instance.Resolve<YearComplexBudgetProject>();
        }

        private int GetBudgetNextRevision(int year, int fcenterId)
        {
            var fcenterBudgets = GetBudgetProjects(year, fcenterId);

            return fcenterBudgets.Count() != 0 ? fcenterBudgets.Max(p => p.Revision) : 0;
        }
    }
}
