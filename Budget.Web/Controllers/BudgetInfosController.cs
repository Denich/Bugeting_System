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
    public class BudgetInfosController : Controller
    {
        public IBudgetCategoryInfoDataProvider BudgetCategoryInfoDataProvider { get; set; }

        public BudgetInfosController()
        {
            BudgetCategoryInfoDataProvider = new BudgetCategoryInfoDataProvider();//todo: change for DI
        }

//
        // GET: /BudgetInfos/

        public ActionResult Index()
        {
            var budgetCategories = BudgetCategoryInfoDataProvider.GetBudgetCategorieInfos();
            
            if (budgetCategories == null || !budgetCategories.Any())
            {
                return View("EmptyBudgetInfos");
            }
            var model = budgetCategories.Select(b => b.ToModel());

            return View(model);
        }

        //
        // GET: /BudgetInfos/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BudgetInfos/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /BudgetInfos/Create

        [HttpPost]
        public ActionResult Create(BudgetCategoryInfoModel model)
        {
            try
            {
                BudgetCategoryInfoDataProvider.AddBudgetCategoryInfo(model.ToObj());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /BudgetInfos/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BudgetInfos/Edit/5

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
        // GET: /BudgetInfos/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BudgetInfos/Delete/5

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
