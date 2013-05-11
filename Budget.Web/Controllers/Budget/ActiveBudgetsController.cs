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


                var project = GetBudgetClient().Data.YearComplexBudgetProjects.GetTemplate();
                project.Year = newBudgetModel.SelectedYear;
                project.AdministrativeUnitId = CompanyId;
                project.UpdatedPersonId = GetCurrentUser().EmployeInfo.Id;
                project.Status = GetCurrentUser().EmployeInfo.GetAllowedApproveStatus(CompanyId);

                if (newBudgetModel.GenerateMonthBudgets)
                {
                    project.GenerateQuarterMonthBudgets();
                }
                else
                {
                   if (newBudgetModel.GenerateQuarterBudgets)
                   {
                       project.GenerateQuarterBudgets();
                   }
                }

                GetBudgetClient().Data.YearComplexBudgetProjects.Insert(project);
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
            var yearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.GetLatestAcceptedBudgetProject(year, CompanyId);

            IEnumerable<FinancialCenter> finCenters = GetBudgetClient().Data.FinancialCenters.GetAll();

            if (finCenters == null || !finCenters.Any())
            {
                return RedirectToAction("Index", "FinancialCenters");
            }

            var modelFinCenters = finCenters.Select(m => m.ToModel(year));

            modelFinCenters.ForEach(
                c =>
                c.IsSeleceted = yearBudget.ChildBudgets.Any(b => b.AdministrativeUnitId == c.Id));

            var model = new BudgetProjectFinancialCentersModel(year, modelFinCenters)
                {
                    IsNewBudget = isNewBudgetWizard != null
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBudgetCenters(BudgetProjectFinancialCentersModel model)
        {
            var companyYearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.GetLatestAcceptedBudgetProject(model.Year, CompanyId);

            var finCenterBudgets = new List<YearComplexBudgetProject>();

            foreach (var modelCenter in model.FinancialCenters)
            {
                if (!modelCenter.IsSeleceted /*Cann't delete exist financial center (may corrupt data)*/ ||
                    companyYearBudget.ChildBudgets.Any(b => b.AdministrativeUnitId == modelCenter.Id) ==
                    modelCenter.IsSeleceted)
                {
                    continue;
                }

                var project = GetBudgetClient().Data.YearComplexBudgetProjects.GetTemplate();

                project.Year = model.Year;
                project.AdministrativeUnitId = modelCenter.Id;
                project.UpdatedPersonId = GetCurrentUser().EmployeInfo.Id;
                project.Status = GetCurrentUser().EmployeInfo.GetAllowedApproveStatus(CompanyId);

                finCenterBudgets.Add(project);
            }

            if (finCenterBudgets.Any())
            {
                if (companyYearBudget.HasQuarterMonthBudgets)
                {
                    finCenterBudgets.ForEach(b => b.GenerateQuarterMonthBudgets());
                }
                else
                {
                    if (companyYearBudget.HasQuarterBudgets)
                    {
                        finCenterBudgets.ForEach(b => b.GenerateQuarterBudgets());        
                    }
                }

                companyYearBudget.ChildBudgets = finCenterBudgets;

                GetBudgetClient().Data.YearComplexBudgetProjects.Insert(companyYearBudget); 
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
                        Status = GetCurrentUser().EmployeInfo.GetAllowedApproveStatus(CompanyId),
                        BudgetCategories = model.Categories.Where(c => c.IsSelected).Select(c => c.ToObj())
                    };

            GetBudgetClient().BudgetOperation.InsertBudgetRecursivly(project);

            foreach (var fcenter in GetBudgetClient().Data.FinancialCenters.GetAll()/*.Where(c => c.IsUsedInYearBudget(model.Year))*/)
            {
                var fcenterProject = new YearComplexBudgetProject
                {
                    Year = model.Year,
                    AdministrativeUnitId = fcenter.Id,
                    Status = GetCurrentUser().EmployeInfo.GetAllowedApproveStatus(CompanyId),
                    BudgetCategories = model.Categories.Where(c => c.IsSelected).Select(c => c.ToObj())
                };

                GetBudgetClient().BudgetOperation.InsertBudgetRecursivly(fcenterProject);
            }

            return RedirectToAction("Index");
        }

        public ActionResult YearBudgetProject(int year, int adminUnitId)
        {
            var budgetModel = GetBudgetClient().Data.YearComplexBudgetProjects.GetLatestAcceptedBudgetProject(year, adminUnitId).ToViewModel();

            var finCenters = GetBudgetClient().BudgetOperation.GetYearBudgetInvolvedFinancialCenters(year);


            budgetModel.FinancialCenterBudgets = finCenters.Select(c =>
                                                                   GetBudgetClient()
                                                                       .Data.YearComplexBudgetProjects
                                                                       .GetLatestAcceptedBudgetProject(year, c.Id)
                                                                       .ToViewModel());
            

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
