using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetModel;
using Budget.Web.Controllers.Common;
using Budget.Web.Helpers.Converters;
using Budget.Web.Models;
using Budget.Web.Models.BudgetModels;
using Budget.Web.Helpers;
using Microsoft.Practices.ObjectBuilder2;

namespace Budget.Web.Controllers
{
    public class ActiveBudgetsController : BaseController
    {
        //
        // GET: /ActiveBudgets/

        public ActionResult Index()
        {
            var nowDate = DateTime.Now;

            var model = new ActiveBudgetsListViewModel();
            model.CurrentYearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.GetFinalFor(CompanyId, nowDate.Year).ToListModel();
            model.CurrentQuarterBudget = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetFinalFor(CompanyId, nowDate.Year, (nowDate.Month % 4) + 1).ToListModel();
            model.CurrentMonthBudget = GetBudgetClient().Data.MonthComplexBudgetProjects.GetFinalFor(CompanyId, nowDate.Year, nowDate.Month).ToListModel();
            model.GetResultsYearBudgets = GetBudgetClient().BudgetOperation.GetUnresultedYearBudgets(CompanyId).ToModel();
            model.GetResultsQuarterBudgets = GetBudgetClient().BudgetOperation.GetUnresultedQuarterBudgets(CompanyId).ToModel();
            model.GetResultsMonthBudgets = GetBudgetClient().BudgetOperation.GetUnresultedMonthBudgets(CompanyId).ToModel();
            model.ApprovalProccesYearBudgets = GetBudgetClient().Data.YearComplexBudgetProjects.GetUnapprovalBudgetsInfo(CompanyId).ToModel();
            model.ApprovalProccesQuarterBudgets = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetUnapprovalBudgetsInfo(CompanyId).ToModel();
            model.ApprovalProccesMonthBudgets = GetBudgetClient().Data.MonthComplexBudgetProjects.GetUnapprovalBudgetsInfo(CompanyId).ToModel();

            return View(model);
        }

        public ActionResult NewBudget()
        {
            var allowedYears = new List<int>();

            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 10; i++)
            {
                allowedYears.Add(i);
            }

            var model = new NewBudgetModel
                {
                    AllowedYears = allowedYears
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult NewBudget(NewBudgetModel newBudgetModel)
        {
            var project = new YearComplexBudgetProject
                {
                    Year = newBudgetModel.SelectedYear,
                    AdministrativeUnitId = CompanyId,
                    UpdatedPersonId = GetCurrentUser().EmployeInfo.Id,
                    IsAccepted = GetCurrentUser().EmployeInfo.Position.CanApproveBudget,
                };

            try
            {
                GetBudgetClient().Data.YearComplexBudgetProjects.Insert(project);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                throw;
            }

            return RedirectToAction("EditBudgetCenters", new {year = newBudgetModel.SelectedYear});
        }

        public ActionResult EditBudgetCenters(int year)
        {
            IEnumerable<FinancialCenter> finCenters = GetBudgetClient().Data.FinancialCenters.GetAll();

            if (finCenters == null || !finCenters.Any())
            {
                return RedirectToAction("Index", "FinancialCenters");
            }

            var modelFinCenters = finCenters.Select(m => new FinancialCenterSelectModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Type = m.Type,
                    IsSeleceted = m.IsUsedInYearBudget(year)
                });

            var model = new BudgetProjectFinancialCentersModel(year, modelFinCenters);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBudgetCenters(BudgetProjectFinancialCentersModel model)
        {
            IEnumerable<FinancialCenter> finCenters = GetBudgetClient().Data.FinancialCenters.GetAll();

            foreach (var fcenter in model.FinancialCenters)
            {
                var foundFinCenter = finCenters.SingleOrDefault(c => c.Id == fcenter.Id);

                if (foundFinCenter == null || foundFinCenter.IsUsedInYearBudget(model.Year) == fcenter.IsSeleceted)
                {
                    continue;
                }

                if (fcenter.IsSeleceted)
                {
                    var project = new YearComplexBudgetProject
                    {
                        Year = model.Year,
                        AdministrativeUnitId = fcenter.Id,
                        UpdatedPersonId = GetCurrentUser().EmployeInfo.Id,
                        IsAccepted = GetCurrentUser().EmployeInfo.Position.CanApproveBudget,
                    };

                    GetBudgetClient().Data.YearComplexBudgetProjects.Insert(project);
                }
            }

            return RedirectToAction("EditBudgetItems", new {year = model.Year});
        }


        public ActionResult EditBudgetItems(int year)
        {
            var model = new EditBudgetItemsModel {Year = year};

            IEnumerable<BudgetCategoryInfo> categories = GetBudgetClient().Data.BudgetCategoryInfos.GetAll();

            if (categories == null)
            {
                return RedirectToAction("Create", "BudgetCategoryInfo");
            }

            model.Categories = categories.Select(c => c.ToSelectModel(year, CompanyId)).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBudgetItems(EditBudgetItemsModel model)
        {
            if (model.Categories == null || !model.Categories.Any(c => c.IsSelected))
            {
                return View("Index");
            }

            var project = new YearComplexBudgetProject
                    {
                        Year = model.Year,
                        AdministrativeUnitId = CompanyId,
                        IsAccepted = GetCurrentUser().EmployeInfo.Position.CanApproveBudget,
                        BudgetCategories = model.Categories.Where(c => c.IsSelected).Select(c => c.ToObj())
                    };

            GetBudgetClient().BudgetOperation.InsertBudgetRecursivly(project);

            foreach (var fcenter in GetBudgetClient().Data.FinancialCenters.GetAll().Where(c => c.IsUsedInYearBudget(model.Year)))
            {
                var fcenterProject = new YearComplexBudgetProject
                {
                    Year = model.Year,
                    AdministrativeUnitId = fcenter.Id,
                    IsAccepted = GetCurrentUser().EmployeInfo.Position.CanApproveBudget,
                    BudgetCategories = model.Categories.Where(c => c.IsSelected).Select(c => c.ToObj())
                };

                GetBudgetClient().BudgetOperation.InsertBudgetRecursivly(fcenterProject);
            }

            return View("Index");
        }
    }
}
