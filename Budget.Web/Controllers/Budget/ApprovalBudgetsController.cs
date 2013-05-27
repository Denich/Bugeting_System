using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Web.Controllers.Common;
using Budget.Web.Helpers.Converters;
using Budget.Web.Models.BudgetModels;

namespace Budget.Web.Controllers
{
    public class ApprovalBudgetsController : BaseController
    {
        public ActionResult Index()
        {
            var model = new ApprovedBudgetsModel();
            model.MonthApprovedBudgets = GetBudgetClient().Data.MonthComplexBudgetProjects.GetApprovedBudgets(DateTime.Now.Year, CompanyId).Select(b => b.ToListModel());
            model.QuarterApprovedBudgets = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetApprovedBudgets(DateTime.Now.Year, CompanyId).Select(b => b.ToListModel());
            model.YearApprovedBudgets = GetBudgetClient().Data.YearComplexBudgetProjects.GetApprovedBudgets(CompanyId).Select(b => b.ToListModel());
            return View(model);
        }
    }
}
