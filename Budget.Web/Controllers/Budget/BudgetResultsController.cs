using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Web.Controllers.Common;
using Budget.Web.Models.BudgetModels;
using Budget.Web.Helpers.Converters;

namespace Budget.Web.Controllers.Budget
{
    public class BudgetResultsController : BaseController
    {
        #region #Region: Budget Results

        public ActionResult StartMonthBudgetResults(int budgetId)
        {
            var budgetProject = GetBudgetClient().Data.MonthComplexBudgetProjects.Get(budgetId);

            GetBudgetClient().Data.MonthComplexBudgets.StartBudgetResultForProject(budgetProject);

            return RedirectToAction("MonthBudgetProject", "FinancialCenterBudgets", new { budgetId });
        }

        public ActionResult StartQuarterBudgetResults(int budgetId)
        {
            var budgetProject = GetBudgetClient().Data.QuarterComplexBudgetProjects.Get(budgetId);

            GetBudgetClient().Data.QuarterComplexBudgets.StartBudgetResultForProject(budgetProject);

            return RedirectToAction("QuarterBudgetProjectByPeriods", "FinancialCenterBudgets", new { budgetId });
        }

        public ActionResult StartYearBudgetResults(int budgetId)
        {
            var budgetProject = GetBudgetClient().Data.YearComplexBudgetProjects.Get(budgetId);

            GetBudgetClient().Data.YearComplexBudgets.StartBudgetResultForProject(budgetProject);

            return RedirectToAction("YearBudgetProjectByPeriods", "FinancialCenterBudgets", new { budgetId });
        }

        #endregion

        public ActionResult YearBudgetResult(int budgetId)
        {
            var model = new BudgetResultModel();
            model.EditAction = "EditYearBudgetResult";
            model.EditController = "BudgetResults";
            model.CalculateChildPeriodAction = "CalculateChildsForYEear";
            model.CalculateChildPeriodController = "BudgetResults";
            model.FinalizeAction = "FinalizeYearBudget";
            model.FinalizeController = "BudgetResults";

            var budgetResult = GetBudgetClient().Data.YearComplexBudgets.Get(budgetId);
            var budgetProject = GetBudgetClient().Data.YearComplexBudgetProjects.GetFinalFor(budgetResult.AdministrativeUnitId, budgetResult.Year);

            model.BudgetResult = budgetResult.ToEditModel();
            model.BudgetProject = budgetProject.ToEditModel();

            model.IsCalculated = !budgetResult.IsFinal && budgetProject.HasQuarterBudgets;
            model.CanEdit = !budgetResult.IsFinal && !budgetResult.ChildBudgets.Any() && !budgetProject.HasQuarterBudgets;
            model.CanFinalize = !budgetResult.IsFinal && GetCurrentUser().EmployeInfo.CanFinallize(budgetResult.AdministrativeUnitId);
            return View("ComplexBudgetResult", model);
        }

        public ActionResult CalculateChildsForYear(int budgetId)
        {
            var budget = GetBudgetClient().Data.YearComplexBudgets.Get(budgetId);

            var quarterBudgets = GetBudgetClient().Data.QuarterComplexBudgets.GetForYear(budget.Year, budget.AdministrativeUnitId);

            budget.BudgetCategories = budget.GetValuesSumFormCategories(budget.BudgetCategories, quarterBudgets.SelectMany(m => m.BudgetCategories));

            GetBudgetClient().Data.YearComplexBudgets.Update(budget);

            return RedirectToAction("QuarterBudgetResult", new { budgetId = budget.Id });
        }

        public ActionResult QuarterBudgetResult(int budgetId)
        {
            var model = new BudgetResultModel();
            model.EditAction = "EditQuarterBudgetResult";
            model.EditController = "BudgetResults";
            model.CalculateChildPeriodAction = "CalculateChildsForQuarter";
            model.CalculateChildPeriodController = "BudgetResults";
            model.FinalizeAction = "FinalizeQuarterBudget";
            model.FinalizeController = "BudgetResults";

            var budgetResult = GetBudgetClient().Data.QuarterComplexBudgets.Get(budgetId);
            var budgetProject = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetFinalFor(budgetResult.AdministrativeUnitId, budgetResult.Year, budgetResult.QuarterNumber);

            model.BudgetResult = budgetResult.ToEditModel();
            model.BudgetProject = budgetProject.ToEditModel();

            model.IsCalculated = !budgetResult.IsFinal && budgetProject.HasMonthBudgets;
            model.CanEdit = !budgetResult.IsFinal && !budgetResult.ChildBudgets.Any() && !budgetProject.HasMonthBudgets;
            model.CanFinalize = !budgetResult.IsFinal && GetCurrentUser().EmployeInfo.CanFinallize(budgetResult.AdministrativeUnitId);
            return View("ComplexBudgetResult", model);
        }

        public ActionResult CalculateChildsForQuarter(int budgetId)
        {
            var budget = GetBudgetClient().Data.QuarterComplexBudgets.Get(budgetId);

            var monthBudgets = GetBudgetClient().Data.MonthComplexBudgets.GetForQuarter(budget.Year, budget.QuarterNumber, budget.AdministrativeUnitId);

            budget.BudgetCategories = budget.GetValuesSumFormCategories(budget.BudgetCategories,
                                                                        monthBudgets.SelectMany(m => m.BudgetCategories));

            GetBudgetClient().Data.QuarterComplexBudgets.Update(budget);

            return RedirectToAction("QuarterBudgetResult", new { budgetId = budget.Id });
        }

        public ActionResult MonthBudgetResult(int budgetId)
        {
            var model = new BudgetResultModel();
            model.EditAction = "EditMonthBudgetResult";
            model.EditController = "BudgetResults";
            model.CalculateChildPeriodAction = "";
            model.CalculateChildPeriodController = "";
            model.FinalizeAction = "FinalizeMonthBudget";
            model.FinalizeController = "BudgetResults";

            var budgetResult = GetBudgetClient().Data.MonthComplexBudgets.Get(budgetId);
            model.BudgetResult = budgetResult.ToEditModel();
            model.BudgetProject = GetBudgetClient().Data.MonthComplexBudgetProjects.GetFinalFor(budgetResult.AdministrativeUnitId, budgetResult.Year, budgetResult.Month).ToEditModel();

            model.IsCalculated = false;
            model.CanEdit = !budgetResult.IsFinal && !budgetResult.ChildBudgets.Any();
            model.CanFinalize = !budgetResult.IsFinal && GetCurrentUser().EmployeInfo.CanFinallize(budgetResult.AdministrativeUnitId);
            return View("ComplexBudgetResult", model);
        }

        #region #Region: Finalize Budgets
        public ActionResult FinalizeMonthBudget(int budgetId)
        {
            GetBudgetClient().Data.MonthComplexBudgets.FinalizeBudget(budgetId);

            return RedirectToAction("MonthBudgetResult", new {budgetId = budgetId});
        }

        public ActionResult FinalizeQuarterBudget(int budgetId)
        {
            GetBudgetClient().Data.QuarterComplexBudgets.FinalizeBudget(budgetId);

            return RedirectToAction("QuarterBudgetResult", new { budgetId = budgetId });
        }

        public ActionResult FinalizeYearBudget(int budgetId)
        {
            GetBudgetClient().Data.YearComplexBudgets.FinalizeBudget(budgetId);

            return RedirectToAction("YearBudgetResult", new { budgetId = budgetId });
        }

        #endregion
        public ActionResult EditMonthBudgetResult(int budgetId)
        {
            var model = new EditComplexBudgetResultModel();
            model.BudgetResult = GetBudgetClient().Data.MonthComplexBudgets.Get(budgetId).ToEditModel();
            model.SubmitAction = "EditMonthBudgetResult";
            model.SubmitController = "BudgetResults";

            return View("EditComplexBudgetResult", model);
        }

        [HttpPost]
        public ActionResult EditMonthBudgetResult(EditComplexBudgetResultModel model)
        {
            var budget = GetBudgetClient().Data.MonthComplexBudgets.Get(model.BudgetResult.Id);

            budget.BudgetCategories = model.BudgetResult.Categories.Select(c => c.ToObj(GetBudgetClient().Data));

            budget.CalculateValues();

            GetBudgetClient().Data.MonthComplexBudgets.Update(budget);

            var parentBudget = GetBudgetClient().Data.MonthComplexBudgets.GetAll().FirstOrDefault(b => b.ChildBudgets.Any(c => c.Id == budget.Id));

            if (parentBudget != null)
            {
                parentBudget.CalculateValues();
                GetBudgetClient().Data.MonthComplexBudgets.Update(parentBudget);
            }

            return RedirectToAction("MonthBudgetResult", new { budgetId = model.BudgetResult.Id });
        }
    }
}
