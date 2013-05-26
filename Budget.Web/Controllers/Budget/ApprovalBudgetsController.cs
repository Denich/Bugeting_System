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
        //
        // GET: /ApprovalBudgets/

        public ActionResult Index()
        {
            var model = new ApprovedBudgetsModel();
            model.MonthApprovedBudgets = GetBudgetClient().Data.MonthComplexBudgetProjects.GetApprovedBudgets(DateTime.Now.Date, CompanyId).Select(b => b.ToListModel());
            model.QuarterApprovedBudgets = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetApprovedBudgets(DateTime.Now.Date, CompanyId).Select(b => b.ToListModel());
            model.YearApprovedBudgets = GetBudgetClient().Data.YearComplexBudgetProjects.GetApprovedBudgets(DateTime.Now.Date, CompanyId).Select(b => b.ToListModel());
            return View(model);
        }

        //
        // GET: /ApprovalBudgets/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ApprovalBudgets/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ApprovalBudgets/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ApprovalBudgets/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ApprovalBudgets/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ApprovalBudgets/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ApprovalBudgets/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
