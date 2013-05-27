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
    public class BudgetArchiveController : BaseController
    {
        public ActionResult Index()
        {
            var model = new ArchiveBudgetsModel();
            model.MonthArchiveBudgets = GetBudgetClient().Data.MonthComplexBudgetProjects.GetArchiveBudgets(DateTime.Now.Date, CompanyId).Select(b => b.ToListModel());
            model.QuarterArchiveBudgets = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetArchiveBudgets(DateTime.Now.Date, CompanyId).Select(b => b.ToListModel());
            model.YearArchiveBudgets = GetBudgetClient().Data.YearComplexBudgetProjects.GetArchiveBudgets(DateTime.Now.Date, CompanyId).Select(b => b.ToListModel());
            return View(model);
        }
    }
}
