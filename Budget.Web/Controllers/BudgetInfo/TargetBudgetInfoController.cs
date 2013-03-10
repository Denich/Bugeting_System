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
    public class TargetBudgetInfoController : Controller
    {
        public ITargetBudgetInfoDataProvider TargetBudgetInfoDataProvider { get; set; }

        public IBudgetCategoryInfoDataProvider BudgetCategoryInfoDataProvider { get; set; }

        public TargetBudgetInfoController()
        {
            BudgetCategoryInfoDataProvider = new BudgetCategoryInfoDataProvider();//todo: change for DI

            TargetBudgetInfoDataProvider = new TargetBudgetInfoDataProvider();//todo: change for DI
        }

        //
        // GET: /TargetBudgetInfo/Details/5

        public ActionResult Details(int id)
        {
            var targetBudgetInfoModel = TargetBudgetInfoDataProvider.GetTargetBudgetInfoById(id).ToModel();

            if (targetBudgetInfoModel == null)
            {
                return HttpNotFound();
            }

            targetBudgetInfoModel.BudgetCategoryName =
                BudgetCategoryInfoDataProvider.GetBudgetCategoryInfoById(targetBudgetInfoModel.BudgetCategoryId).Name;

            if (Request.IsAjaxRequest())
            {
                return PartialView(targetBudgetInfoModel);
            }

            return View(targetBudgetInfoModel);
        }

        //
        // GET: /TargetBudgetInfo/Create

        public ActionResult Create()
        {
            ViewBag.BudgetCategoryId =
                new SelectList(BudgetCategoryInfoDataProvider.GetBudgetCategorieInfos().Select(o => o.ToModel()), "Id",
                               "Name");
            return View();
        } 

        //
        // POST: /TargetBudgetInfo/Create

        [HttpPost]
        public ActionResult Create(TargetBudgetInfoModel model)
        {
            try
            {
                model.DateAdded = DateTime.Now;
                model.Source = User.Identity.Name;
                TargetBudgetInfoDataProvider.AddTargetBudgetInfo(model.ToObj());

                return RedirectToAction("Index", "BudgetCategoryInfo");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /TargetBudgetInfo/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TargetBudgetInfo/Edit/5

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
        // GET: /TargetBudgetInfo/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TargetBudgetInfo/Delete/5

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
