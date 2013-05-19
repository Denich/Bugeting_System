using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.Management;
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

                int budgetId = GetBudgetClient().Data.YearComplexBudgetProjects.Insert(project);

                return RedirectToAction("EditBudgetCenters", new { companyBaseYearBudgetId = budgetId, isNewBudgetWizard = true });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                return View("CustomErrorView");
            }
        }

        public ActionResult EditBudgetCenters(int companyBaseYearBudgetId, bool? isNewBudgetWizard)
        {
            var companyYearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.Get(companyBaseYearBudgetId);

            var finCenters = GetBudgetClient().Data.FinancialCenters.GetAll();

            if (!finCenters.Any())
            {
                return RedirectToAction("Index", "FinancialCenters");
            }

            var modelFinCenters = finCenters.Select(m => m.ToSelectModel(companyYearBudget));

            var model = new BudgetProjectFinancialCentersModel(companyYearBudget.Year, companyBaseYearBudgetId, modelFinCenters)
                {
                    IsNewBudget = isNewBudgetWizard != null
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBudgetCenters(BudgetProjectFinancialCentersModel model)
        {
            var newCompanyBudgetId = model.CompanyBaseYearBudgetId;

            var companyYearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.Get(model.CompanyBaseYearBudgetId);

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

                var childBudgets = companyYearBudget.ChildBudgets.ToList();
                childBudgets.AddRange(finCenterBudgets);
                companyYearBudget.ChildBudgets = childBudgets;

                newCompanyBudgetId = GetBudgetClient().Data.YearComplexBudgetProjects.Insert(companyYearBudget); 
            }

            return model.IsNewBudget
                       ? RedirectToAction("EditBudgetItems",
                                          new { companyBaseYearBudgetId = newCompanyBudgetId })
                       : RedirectToAction("Index");
        }


        public ActionResult EditBudgetItems(int companyBaseYearBudgetId)
        {
            var companyYearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.Get(companyBaseYearBudgetId);

            var model = new EditBudgetItemsModel { Year = companyYearBudget.Year };

            var categoriesInfos = GetBudgetClient().Data.BudgetCategoryInfos.GetAll();

            if (!categoriesInfos.Any())
            {
                return RedirectToAction("Create", "BudgetCategoryInfo");
            }

            model.Categories = categoriesInfos.Select(c => c.ToSelectModel(companyYearBudget)).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBudgetItems(EditBudgetItemsModel model)
        {
            var companyYearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.Get(model.CompanyBaseYearBudgetId);

            var selectedProject = GetBudgetClient().Data.YearComplexBudgetProjects.GetTemplate();

            selectedProject.BudgetCategories = model.Categories.Where(c => c.IsAdded).Select(c => c.ToObj(GetBudgetClient().Data));

            if (selectedProject.BudgetCategories.Any())
            {
                companyYearBudget.Merge(selectedProject);

                GetBudgetClient().Data.YearComplexBudgetProjects.Insert(companyYearBudget);
            }

            return RedirectToAction("Index");
        }
    }
}
