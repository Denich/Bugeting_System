using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Web.Controllers.Common;
using Budget.Web.Helpers.Converters;
using Budget.Web.Models;

namespace Budget.Web.Controllers
{
    public class TargetBudgetInfoController : BaseController
    {
        //
        // GET: /TargetBudgetInfo/Details/5

        public ActionResult Details(int id)
        {
            var targetBudgetInfoModel = GetBudgetClient().Data.TargetBudgetInfos.Get(id).ToModel();

            if (targetBudgetInfoModel == null)
            {
                return HttpNotFound();
            }

            targetBudgetInfoModel.BudgetCategoryName =
                GetBudgetClient().Data.BudgetCategoryInfos.Get(targetBudgetInfoModel.BudgetCategoryId).Name;

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
                new SelectList(
                    GetBudgetClient()
                        .Data.BudgetCategoryInfos.GetAll()
                        .Select(o => o.ToModel()), "Id",
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
                GetBudgetClient().Data.TargetBudgetInfos.Insert(model.ToObj());

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
