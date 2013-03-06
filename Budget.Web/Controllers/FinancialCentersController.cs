using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Web.Models;
using EmitMapper;
using Budget.Web.Helpers.Converters;

namespace Budget.Web.Controllers
{
    public class FinancialCentersController : Controller
    {
        public IAdministrativeUnitDataProvider AdministrativeUnitDataProvider { get; set; }
        //
        // GET: /FinancialCenters/

        public FinancialCentersController()
        {
            AdministrativeUnitDataProvider = new AdministrativeUnitDataProvider(); 
        }

        public ActionResult Index()
        {
            IEnumerable<FinancialCenter> financialCenters = AdministrativeUnitDataProvider.GetFinancialCenters();

            if (financialCenters == null || !financialCenters.Any())
            {
                return View("FinancialCentersNotFound");
            }

            var model = financialCenters.Select(m => m.ToModel());

            return View(model);
        }

        //
        // GET: /FinancialCenters/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FinancialCenters/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FinancialCenters/Create

        [HttpPost]
        public ActionResult Create(FinancialCenterModel model)
        {
            try
            {
                AdministrativeUnitDataProvider.AddFinancialCenter(model.ToObj());

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //ViewBag.Error = "Create faild: " + ex. 
                return View();
            }
        }
        
        //
        // GET: /FinancialCenters/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = AdministrativeUnitDataProvider.GetFinancialCenterById(id).ToModel();
            return View(model);
        }

        //
        // POST: /FinancialCenters/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FinancialCenterModel model)
        {
            try
            {
                AdministrativeUnitDataProvider.UpdateFinancialCenter(model.ToObj());
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FinancialCenters/Delete/5
 
        public ActionResult Delete(int id)
        {
            var model = AdministrativeUnitDataProvider.GetFinancialCenterById(id).ToModel();
            return View(model);
        }

        //
        // POST: /FinancialCenters/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FinancialCenterModel model)
        {
            try
            {
                AdministrativeUnitDataProvider.DeleteFinancialCenter(id);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
