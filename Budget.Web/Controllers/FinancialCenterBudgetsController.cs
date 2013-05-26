using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetModel;
using Budget.Web.Controllers.Common;
using Budget.Web.Helpers.Converters;
using Budget.Web.Models.BudgetModels;

namespace Budget.Web.Controllers
{
    public class FinancialCenterBudgetsController : BaseController
    {
        #region #Region: View Budgets
        
        public ActionResult YearBudgetProjectByAdminUnits(int budgetId)
        {
            var yearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.Get(budgetId);
            var model = yearBudget.ToAdminParentViewModel();

            model.CanFinalize = GetCurrentUser().EmployeInfo.CanFinallize(yearBudget.AdministrativeUnitId);
            model.CanReview = DateTime.Now.Year <= yearBudget.Year;

            model.FinilizeController = "FinancialCenterBudgets";
            model.FinilizeAction = "FinilizeYearBudget";
            model.ReviewController = "FinancialCenterBudgets";
            model.ReviewAction = "ReviewYearBudget";

            if (yearBudget.ChildBudgets.Any())
            {
                model.EditChildBudgetsController = "FinancialCenterBudgets";
                model.EditChildBudgetsAction = "YearBudgetProjectByPeriods";

                if (yearBudget.HasQuarterBudgets)
                {
                    model.SwitchToOtherTypeController = "FinancialCenterBudgets";
                    model.SwitchToOtherTypeAction = "YearBudgetProjectByPeriods";
                    model.SwitchtoOtherTypeName = "По кварталам";
                }

                return View("ComplexBudgetProject", model);
            }

            if (!yearBudget.HasQuarterBudgets && !yearBudget.ChildBudgets.Any())
            {
                model.CanEdit = true;
                model.EditAction = "EditYearBudgetProject";
                model.EditController = "FinancialCenterBudgets";
                return View("ComplexBudgetProject", model);
            }

            ViewBag.Error = "Не знайдено дочірніх бюджетів по фінансовим центрам";
            return View("CustomErrorView");
        }

        public ActionResult YearBudgetProjectByPeriods(int budgetId)
        {
            var yearBudget = GetBudgetClient().Data.YearComplexBudgetProjects.Get(budgetId);
            var model = yearBudget.ToPeriodParentViewModel();

            model.CanFinalize = GetCurrentUser().EmployeInfo.CanFinallize(yearBudget.AdministrativeUnitId);
            model.CanReview = DateTime.Now.Year <= yearBudget.Year;

            model.FinilizeController = "FinancialCenterBudgets";
            model.FinilizeAction = "FinilizeYearBudget";
            model.ReviewController = "FinancialCenterBudgets";
            model.ReviewAction = "ReviewYearBudget";

            if (yearBudget.HasQuarterBudgets)
            {
                model.EditChildBudgetsController = "FinancialCenterBudgets";
                model.EditChildBudgetsAction = "QuarterBudgetProjectByPeriods";
                
                if (yearBudget.ChildBudgets.Any())
                {
                    model.SwitchToOtherTypeController = "FinancialCenterBudgets";
                    model.SwitchToOtherTypeAction = "YearBudgetProjectByAdminUnits";
                    model.SwitchtoOtherTypeName = "По фінансовим центрам";
                }

                return View("ComplexBudgetProject", model);
            }

            if (!yearBudget.HasQuarterBudgets && !yearBudget.ChildBudgets.Any())
            {
                model.CanEdit = true;
                model.EditAction = "EditYearBudgetProject";
                model.EditController = "FinancialCenterBudgets";
                return View("ComplexBudgetProject", model);
            }

            ViewBag.Error = "Не знайдено дочірніх бюджетів по періодам";
            return View("CustomErrorView");
        }

        public ActionResult QuarterBudgetProjectByAdminUnits(int budgetId)
        {
            var quarterBudget = GetBudgetClient().Data.QuarterComplexBudgetProjects.Get(budgetId);
            var model = quarterBudget.ToAdminParentViewModel();
            model.CanFinalize = GetCurrentUser().EmployeInfo.CanFinallize(quarterBudget.AdministrativeUnitId);
            model.CanReview = DateTime.Now.Year <= quarterBudget.Year && DateTime.Now.Month % 4 + 1 <= quarterBudget.QuarterNumber;

            model.FinilizeController = "FinancialCenterBudgets";
            model.FinilizeAction = "FinilizeQuarterBudget";
            model.ReviewController = "FinancialCenterBudgets";
            model.ReviewAction = "ReviewQuarterBudget";

            if (quarterBudget.ChildBudgets.Any())
            {
                model.EditChildBudgetsController = "FinancialCenterBudgets";
                model.EditChildBudgetsAction = "QuarterBudgetProjectByPeriods";

                if (quarterBudget.HasMonthBudgets)
                {
                    model.SwitchToOtherTypeController = "FinancialCenterBudgets";
                    model.SwitchToOtherTypeAction = "QuarterBudgetProjectByPeriods";
                    model.SwitchtoOtherTypeName = "По місяцям";
                }

                return View("ComplexBudgetProject", model);
            }

            if (!quarterBudget.HasMonthBudgets && !quarterBudget.ChildBudgets.Any())
            {
                model.CanEdit = true;
                model.EditAction = "EditQuarterBudgetProject";
                model.EditController = "FinancialCenterBudgets";
                return View("ComplexBudgetProject", model);
            }

            ViewBag.Error = "Не знайдено дочірніх бюджетів по фінансовим центрам";
            return View("CustomErrorView");
        }

        public ActionResult QuarterBudgetProjectByPeriods(int budgetId)
        {
            var quarterBudget = GetBudgetClient().Data.QuarterComplexBudgetProjects.Get(budgetId);
            var model = quarterBudget.ToPeriodParentViewModel();

            model.CanFinalize = GetCurrentUser().EmployeInfo.CanFinallize(quarterBudget.AdministrativeUnitId);
            model.CanReview = DateTime.Now.Year <= quarterBudget.Year && DateTime.Now.Month % 4 + 1 <= quarterBudget.QuarterNumber;

            model.FinilizeController = "FinancialCenterBudgets";
            model.FinilizeAction = "FinilizeQuarterBudget";
            model.ReviewController = "FinancialCenterBudgets";
            model.ReviewAction = "ReviewQuarterBudget";

            if (quarterBudget.HasMonthBudgets)
            {
                model.EditChildBudgetsController = "FinancialCenterBudgets";
                model.EditChildBudgetsAction = "MonthBudgetProject";

                if (quarterBudget.ChildBudgets.Any())
                {
                    model.SwitchToOtherTypeController = "FinancialCenterBudgets";
                    model.SwitchToOtherTypeAction = "QuarterBudgetProjectByAdminUnits";
                    model.SwitchtoOtherTypeName = "По фінансовим центрам";
                }

                return View("ComplexBudgetProject", model);
            }

            if (!quarterBudget.HasMonthBudgets && !quarterBudget.ChildBudgets.Any())
            {
                model.CanEdit = true;
                model.EditAction = "EditQuarterBudgetProject";
                model.EditController = "FinancialCenterBudgets";
                return View("ComplexBudgetProject", model);
            }

            ViewBag.Error = "Не знайдено дочірніх бюджетів по періодам";
            return View("CustomErrorView");
        }

        public ActionResult MonthBudgetProject(int budgetId)
        {
            var monthBudget = GetBudgetClient().Data.MonthComplexBudgetProjects.Get(budgetId);
            var model = monthBudget.ToAdminParentViewModel();
            model.CanFinalize = GetCurrentUser().EmployeInfo.CanFinallize(monthBudget.AdministrativeUnitId);
            model.CanReview = DateTime.Now.Year <= monthBudget.Year && DateTime.Now.Month <= monthBudget.Month && GetCurrentUser().EmployeInfo.CanFinallize(monthBudget.AdministrativeUnitId);
            
            var monthResultBudget = GetBudgetClient().Data.MonthComplexBudgets.GetFor(monthBudget.Year, monthBudget.Month, monthBudget.AdministrativeUnitId);
            model.ResultsBudgetId = monthResultBudget == null ? -1 : monthResultBudget.Id;
            model.CanStartResults = DateTime.Now.Year >= monthBudget.Year && 
                                    DateTime.Now.Month > monthBudget.Month &&
                                    GetCurrentUser().EmployeInfo.CanFinallize(monthBudget.AdministrativeUnitId) &&
                                    model.ResultsBudgetId == -1 &&
                                    monthBudget.AdministrativeUnitId == CompanyId;

            model.ViewBudgetResultController = "FinancialCenterBudgets";
            model.ViewBudgetResultAction = "MonthBudgetResult";
            model.StartResultController = "BudgetResults";
            model.StartResultAction = "StartMonthBudgetResults";
            model.FinilizeController = "FinancialCenterBudgets";
            model.FinilizeAction = "FinilizeMonthBudget";
            model.ReviewController = "FinancialCenterBudgets";
            model.ReviewAction = "ReviewMonthBudget";

            if (monthBudget.ChildBudgets.Any())
            {
                model.EditChildBudgetsController = "FinancialCenterBudgets";
                model.EditChildBudgetsAction = "MonthBudgetProject";
                return View("ComplexBudgetProject", model);
            }

            model.CanEdit = true;
            model.EditAction = "EditMonthBudgetProject";
            model.EditController = "FinancialCenterBudgets";
            return View("ComplexBudgetProject", model);
        }
        
        #endregion

        #region #Region: Finilize budgets

        public ActionResult FinilizeMonthBudget(int budgetId)
        {
            GetBudgetClient().Data.MonthComplexBudgetProjects.FinilizeBudget(budgetId);

            return RedirectToAction("MonthBudgetProject", new { budgetId });
        }

        public ActionResult ReviewMonthBudget(int budgetId)
        {
            GetBudgetClient().Data.MonthComplexBudgetProjects.ReviewBudget(budgetId);

            GetBudgetClient().Data.QuarterComplexBudgetProjects.ReviewByMonthBudget(budgetId);

            GetBudgetClient().Data.YearComplexBudgetProjects.ReviewByMonthBudget(budgetId);

            return RedirectToAction("MonthBudgetProject", new { budgetId });
        }

        public ActionResult FinilizeQuarterBudget(int budgetId)
        {
            GetBudgetClient().Data.QuarterComplexBudgetProjects.FinilizeBudget(budgetId);

            return RedirectToAction("QuarterBudgetProjectByPeriods", new { budgetId });
        }

        public ActionResult ReviewQuarterBudget(int budgetId)
        {
            GetBudgetClient().Data.QuarterComplexBudgetProjects.ReviewBudget(budgetId);

            GetBudgetClient().Data.YearComplexBudgetProjects.ReviewByQuarterBudget(budgetId);

            return RedirectToAction("QuarterBudgetProjectByPeriods", new { budgetId });
        }

        public ActionResult FinilizeYearBudget(int budgetId)
        {
            GetBudgetClient().Data.YearComplexBudgetProjects.FinilizeBudget(budgetId);

            return RedirectToAction("YearBudgetProjectByPeriods", new { budgetId });
        }

        public ActionResult ReviewYearBudget(int budgetId)
        {
            GetBudgetClient().Data.YearComplexBudgetProjects.ReviewBudget(budgetId);

            return RedirectToAction("YearBudgetProjectByPeriods", new { budgetId });
        }

        #endregion

        public ActionResult EditMonthBudgetProject(int budgetId)
        {
            var budget = GetBudgetClient().Data.MonthComplexBudgetProjects.Get(budgetId);

            var model = budget.ToEditModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditMonthBudgetProject(ComplexBudgetProjectEditModel budgetProjectEdit)
        {
            var model = new ComplexCompareBudgetEditModel();
            
            var baseBudget = GetBudgetClient().Data.MonthComplexBudgetProjects.Get(budgetProjectEdit.BaseBudgetId);
            model.OldBudgetProject = baseBudget.ToEditModel();

            var modelBudget = baseBudget;
            modelBudget.BudgetCategories = budgetProjectEdit.Categories.Select(c => c.ToObj(GetBudgetClient().Data));
            modelBudget.CalculateValues();

            model.NewBudgetProject = modelBudget.ToEditModel();
            model.IsApprove = GetCurrentUser().EmployeInfo.GetAllowedApproveStatus(baseBudget.AdministrativeUnitId) == BudgetProjectStatus.Accepted;

            return View("ViewComplareBudgetProject", model);
        }

        [HttpPost]
        public ActionResult ApproveMonthBudget(ComplexCompareBudgetEditModel budgetProjectEdit)
        {
            var monthBudget = GetBudgetClient().Data.MonthComplexBudgetProjects.Get(budgetProjectEdit.NewBudgetProject.BaseBudgetId);
            monthBudget.BudgetCategories = budgetProjectEdit.NewBudgetProject.Categories.Select(c => c.ToObj(GetBudgetClient().Data));

            monthBudget.UpdatedPersonId = GetCurrentUser().EmployeInfo.Id;
            monthBudget.Status = GetCurrentUser().EmployeInfo.GetAllowedApproveStatus(CompanyId);

            if (!budgetProjectEdit.IsApprove)
            {
                GetBudgetClient().Data.MonthComplexBudgetProjects.Insert(monthBudget);
                return RedirectToAction("Index", "ActiveBudgets");
            }

            var baseCompanyProject = GetBudgetClient().Data.YearComplexBudgetProjects.GetLatestAcceptedBudgetProject(monthBudget.Year, CompanyId);

            if (monthBudget.AdministrativeUnitId == CompanyId)
            {
                baseCompanyProject.UpdateMonthBudget(monthBudget);
            }
            else
            {
                baseCompanyProject.ChildBudgets = baseCompanyProject.ChildBudgets.Select(b =>
                    {
                        if (b.AdministrativeUnitId == monthBudget.AdministrativeUnitId)
                        {
                            b.UpdateMonthBudget(monthBudget);
                        }
                        return b;
                    }).ToList();
            }
            baseCompanyProject.PopulateQuarterBudgetsFromChilds();
            baseCompanyProject.CalculateValues();
            GetBudgetClient().Data.YearComplexBudgetProjects.Insert(baseCompanyProject);

            return RedirectToAction("Index", "ActiveBudgets");
        }

        public ActionResult ViewComplareBudgetProject()
        {
            return View();
        }
    }
}
