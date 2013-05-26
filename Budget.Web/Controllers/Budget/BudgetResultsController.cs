using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Web.Controllers.Common;

namespace Budget.Web.Controllers.Budget
{
    public class BudgetResultsController : BaseController
    {
        #region #Region: Budget Results

        public ActionResult StartMonthBudgetResults(int budgetId)
        {
            var budgetProject = GetBudgetClient().Data.MonthComplexBudgetProjects.Get(budgetId);

            GetBudgetClient().Data.MonthComplexBudgets.StartBudgetResultForProject(budgetProject);

            return RedirectToAction("MonthBudgetProject", new { budgetId });
        }

        #endregion

    }
}
