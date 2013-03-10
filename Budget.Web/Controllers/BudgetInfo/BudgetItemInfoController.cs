using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Web.Helpers.Converters;
using Budget.Web.Models;

namespace Budget.Web.Controllers
{
    public class BudgetItemInfoController : Controller
    {
        public ITargetBudgetInfoDataProvider TargetBudgetInfoDataProvider { get; set; }

        public IBudgetItemInfoDataProvider BudgetItemInfoDataProvider { get; set; }

        public BudgetItemInfoController()
        {
            TargetBudgetInfoDataProvider = new TargetBudgetInfoDataProvider();//todo: change for DI

            BudgetItemInfoDataProvider = new BudgetItemInfoDataProvider();//todo: change for DI
        }
        //
        // GET: /BudgetItemInfo/Details/5

        public ActionResult Details(int id)
        {
            var budgetItemInfoModel = BudgetItemInfoDataProvider.GetBudgetItemInfoById(id).ToModel();

            if (budgetItemInfoModel == null)
            {
                return HttpNotFound();
            }

            budgetItemInfoModel.TargetBudgetName =
                TargetBudgetInfoDataProvider.GetTargetBudgetInfoById(budgetItemInfoModel.TargetBudgetId).Name;

            if (Request.IsAjaxRequest())
            {
                return PartialView(budgetItemInfoModel);
            }

            return View(budgetItemInfoModel);
        }

        //
        // GET: /BudgetItemInfo/Create

        public ActionResult Create()
        {
            ViewBag.TargetBudgetId =
                new SelectList(TargetBudgetInfoDataProvider.GetTargetBudgetInfos().Select(o => o.ToModel()), "Id",
                               "Name");
            return View();
        }

        //
        // POST: /BudgetItemInfo/Create

        [HttpPost]
        public ActionResult Create(BudgetItemInfoModel model)
        {
            try
            {
                model.DateAdded = DateTime.Now;
                model.Source = User.Identity.Name;
                BudgetItemInfoDataProvider.AddBudgetItemInfo(model.ToObj());

                return RedirectToAction("Index", "BudgetCategoryInfo");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /BudgetItemInfo/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BudgetItemInfo/Edit/5

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
        // GET: /BudgetItemInfo/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BudgetItemInfo/Delete/5

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
