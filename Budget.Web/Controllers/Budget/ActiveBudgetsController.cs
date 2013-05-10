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
        public ActionResult Index()
        {
            var nowDate = DateTime.Now;

            var model = new ActiveBudgetsListViewModel();

            model.CompanyId = CompanyId;

            model.CurrentYearBudget =
                GetBudgetClient().Data.YearComplexBudgetProjects.GetFinalFor(CompanyId, nowDate.Year).ToListModel();

            model.CurrentQuarterBudget =
                GetBudgetClient()
                    .Data.QuarterComplexBudgetProjects.GetFinalFor(CompanyId, nowDate.Year, (nowDate.Month%4) + 1)
                    .ToListModel();

            model.CurrentMonthBudget =
                GetBudgetClient()
                    .Data.MonthComplexBudgetProjects.GetFinalFor(CompanyId, nowDate.Year, nowDate.Month)
                    .ToListModel();

            model.GetResultsYearBudgets =
                GetBudgetClient().BudgetOperation.GetUnresultedYearBudgets(CompanyId).Select(b => b.ToListModel());

            model.GetResultsQuarterBudgets =
                GetBudgetClient().BudgetOperation.GetUnresultedQuarterBudgets(CompanyId).Select(b => b.ToListModel());

            model.GetResultsMonthBudgets =
                GetBudgetClient().BudgetOperation.GetUnresultedMonthBudgets(CompanyId).Select(b => b.ToListModel());

            model.ApprovalProccesYearBudgets =
                GetBudgetClient().Data.YearComplexBudgetProjects.GetUnapprovalBudgets(CompanyId).Select(b => b.ToListModel());

            model.ApprovalProccesQuarterBudgets =
                GetBudgetClient().Data.QuarterComplexBudgetProjects.GetUnapprovalBudgets(CompanyId).Select(b => b.ToListModel());

            model.ApprovalProccesMonthBudgets =
                GetBudgetClient().Data.MonthComplexBudgetProjects.GetUnapprovalBudgets(CompanyId).Select(b => b.ToListModel());

            return View(model);
        }

        public ActionResult NewBudget()
        {
            var allowedYears = new List<BudgetYearModel>();

            for (int year = DateTime.Now.Year; year < DateTime.Now.Year + 10; year++)
            {
                allowedYears.Add(new BudgetYearModel
                    {
                        Year = year,
                        IsAlreadyExist = GetBudgetClient().Data.YearComplexBudgetProjects.GetBudgetProjects(year, CompanyId).Any(),
                        YearName = year + " р."
                    });
            }

            return View(new NewBudgetModel { AllowedYears = allowedYears });
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
                if (newBudgetModel.SelectedYear < DateTime.MinValue.Year)
                {
                    throw new Exception(String.Format(CultureInfo.InvariantCulture,
                                                      "Бюджетний рік '{0}' є не корректним", newBudgetModel.SelectedYear));
                }

                if (GetBudgetClient()
                    .Data.YearComplexBudgetProjects.GetBudgetProjects(newBudgetModel.SelectedYear, CompanyId)
                    .Any())
                {
                    throw new Exception(String.Format(CultureInfo.InvariantCulture, "Бюджет за {0} рік вже існує",
                                                      newBudgetModel.SelectedYear));
                }

                //Todo: add transaction here
                GetBudgetClient().Data.YearComplexBudgetProjects.Insert(project);

                if (newBudgetModel.GenerateQuarterBudgets)
                {
                    GetBudgetClient()
                        .BudgetOperation.GenerateQuarterBudgetProjects(CompanyId, newBudgetModel.SelectedYear,
                                                                       GetCurrentUser().EmployeInfo.Id,
                                                                       GetCurrentUser()
                                                                           .EmployeInfo.Position.CanApproveBudget);

                    if (newBudgetModel.GenerateMonthBudgets)
                    {
                        GetBudgetClient()
                            .BudgetOperation.GenerateMonthBudgetProjects(CompanyId, newBudgetModel.SelectedYear,
                                                                         GetCurrentUser().EmployeInfo.Id,
                                                                         GetCurrentUser()
                                                                             .EmployeInfo.Position.CanApproveBudget);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                return View("CustomErrorView");
            }

            return RedirectToAction("EditBudgetCenters", new { year = newBudgetModel.SelectedYear, isNewBudgetWizard  = true });
        }

        public ActionResult EditBudgetCenters(int year, bool? isNewBudgetWizard)
        {
            IEnumerable<FinancialCenter> finCenters = GetBudgetClient().Data.FinancialCenters.GetAll();

            if (finCenters == null || !finCenters.Any())
            {
                return RedirectToAction("Index", "FinancialCenters");
            }

            var modelFinCenters = finCenters.Select(m => m.ToSelectModel(year));

            var model = new BudgetProjectFinancialCentersModel(year, modelFinCenters)
                {
                    IsNewBudget = isNewBudgetWizard != null
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBudgetCenters(BudgetProjectFinancialCentersModel model)
        {
            IEnumerable<FinancialCenter> finCenters = GetBudgetClient().Data.FinancialCenters.GetAll();

            foreach (var modelCenter in model.FinancialCenters)
            {
                var foundFinCenter = finCenters.SingleOrDefault(c => c.Id == modelCenter.Id);

                if (foundFinCenter == null || foundFinCenter.IsUsedInYearBudget(model.Year) == modelCenter.IsSeleceted)
                {
                    continue;
                }

                if (modelCenter.IsSeleceted)
                {
                    var project = new YearComplexBudgetProject
                    {
                        Year = model.Year,
                        AdministrativeUnitId = modelCenter.Id,
                        UpdatedPersonId = GetCurrentUser().EmployeInfo.Id,
                        IsAccepted = GetCurrentUser().EmployeInfo.Position.CanApproveBudget,
                    };

                    GetBudgetClient().Data.YearComplexBudgetProjects.Insert(project);
                }
            }

            return model.IsNewBudget ? RedirectToAction("EditBudgetItems", new { year = model.Year }) : RedirectToAction("Index");
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
                return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        public ActionResult YearBudgetProject(int year, int adminUnitId)
        {
            var budgetModel = GetBudgetClient().Data.YearComplexBudgetProjects.GetLatestAcceptedBudgetProject(year, adminUnitId).ToViewModel();

            return View(budgetModel);
        }

        public ActionResult QuarterBudgetProject(int year, int quarter, int adminUnitId)
        {
            var budgetModel = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetLatestAcceptedBudgetProject(year, quarter, adminUnitId).ToViewModel();

            return View(budgetModel);
        }

        public ActionResult MonthBudgetProject(int year, int month, int adminUnitId)
        {
            var budgetModel = GetBudgetClient().Data.MonthComplexBudgetProjects.GetLatestAcceptedBudgetProject(year, month, adminUnitId).ToViewModel();

            return View(budgetModel);
        }
    }
}
