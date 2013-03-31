using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviderContracts;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Web.Controllers.Common;
using Budget.Web.Models;
using EmitMapper;
using Budget.Web.Helpers.Converters;

namespace Budget.Web.Controllers
{
    public class FinancialCentersController : BaseController
    {
        public ActionResult Index()
        {
            var financialCenters = GetBudgetClient().DataManagement.FinancialCenters.GetAll();

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
                GetBudgetClient().DataManagement.FinancialCenters.Insert(model.ToObj());

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                //ViewBag.Error = "Create faild: " + ex. 
                return View();
            }
        }
        
        //
        // GET: /FinancialCenters/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = GetBudgetClient().DataManagement.FinancialCenters.Get(id).ToModel();
            return View(model);
        }

        //
        // POST: /FinancialCenters/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FinancialCenterModel model)
        {
            try
            {
                GetBudgetClient().DataManagement.FinancialCenters.Update(model.ToObj());
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
            var model = GetBudgetClient().DataManagement.FinancialCenters.Get(id).ToModel();
            return View(model);
        }

        //
        // POST: /FinancialCenters/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FinancialCenterModel model)
        {
            try
            {
                GetBudgetClient().DataManagement.FinancialCenters.Delete(id);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
