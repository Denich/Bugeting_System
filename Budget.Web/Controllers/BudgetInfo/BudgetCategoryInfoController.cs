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
    public class BudgetCategoryInfoController : BaseController
    {
        //
        // GET: /BudgetCategoryInfo/

        public ActionResult Index()
        {
            var budgetCategories = GetBudgetClient().DataManagement.BudgetCategoryInfos.GetAll();

            if (budgetCategories == null || !budgetCategories.Any())
            {
                return View("EmptyBudgetCategoryInfo");
            }

            var model = budgetCategories.Select(b => b.ToEntityModel());

            return View(model);
        }

        //
        // GET: /BudgetCategoryInfo/Details/5

        public ActionResult Details(int id)
        {
            var budgetCategoryInfoModel = GetBudgetClient().DataManagement.BudgetCategoryInfos.Get(id).ToModel();
            
            if (budgetCategoryInfoModel == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(budgetCategoryInfoModel);
            }

            return View(budgetCategoryInfoModel);
        }

        //
        // GET: /BudgetCategoryInfo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BudgetCategoryInfo/Create

        [HttpPost]
        public ActionResult Create(BudgetCategoryInfoModel model)
        {
            try
            {
                model.DateAdded = DateTime.Now;
                model.Source = User.Identity.Name;
                GetBudgetClient().DataManagement.BudgetCategoryInfos.Insert(model.ToObj());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BudgetCategoryInfo/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BudgetCategoryInfo/Edit/5

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
        // GET: /BudgetCategoryInfo/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BudgetCategoryInfo/Delete/5

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
