using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Web.Helpers.Converters;
using Budget.Web.Models.BudgetModels;

namespace Budget.Web.Controllers.Budget
{
    public class BudgetController : Controller
    {
        public IYearComplexBudgetDataProvider ComplexBudgetDataProvider { get; set; }

        public IBudgetCategoryDataProvider BudgetCategoryDataProvider { get; set; }

        public IAdministrativeUnitDataProvider AdministrativeUnitDataProvider { get; set; }
        
        public BudgetController()
        {
            ComplexBudgetDataProvider = new YearComplexBudgetDataProvider();//todo: change for DI

            BudgetCategoryDataProvider = new BudgetCategoryDataProvider();

            AdministrativeUnitDataProvider = new AdministrativeUnitDataProvider();
        }
        
        //
        // GET: /Budget/

        public ActionResult Index()
        {
            var yearComplexBudgets = ComplexBudgetDataProvider.GetYearComplexBudgets();
            
            if (yearComplexBudgets == null)
            {
                return View();
            }

            var model = new List<YearComplexBudgetModel>();//yearComplexBudgets.Select(b => b.ToModel());

            foreach (var yearComplexBudget in yearComplexBudgets)
            {
                var modelItem = yearComplexBudget.ToModel();

                modelItem.AdminUnitName =
                    AdministrativeUnitDataProvider.GetAdministrativeUnitById(yearComplexBudget.AdministrativeUnitId)
                                                  .Name;
                model.Add(modelItem);
            }

            return View(model);
        }

        //
        // GET: /Budget/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Budget/Create

        public ActionResult Create()
        {
            ViewBag.AdministrativeUnitId =
                new SelectList(AdministrativeUnitDataProvider.GetAdministrativeUnits().Select(o => o.ToModel()), "Id",
                               "Name");
            return View();
        }

        //
        // POST: /Budget/Create

        [HttpPost]
        public ActionResult Create(YearComplexBudgetModel model)
        {
            try
            {
                ComplexBudgetDataProvider.AddYearComplexBudget(model.ToObj());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Budget/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Budget/Edit/5

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
        // GET: /Budget/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Budget/Delete/5

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
