using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Web.Controllers.Common;
using Budget.Web.Helpers;
using Budget.Web.Models.GraphModels;
using MoreLinq;

namespace Budget.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Вітаємо Вас в системі бюджетування!";

            return RedirectToAction("IncomeGraph");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult BalanceGraph()
        {
            throw new NotImplementedException();
        }

        public ActionResult CostsGraph()
        {
            var model = new IncomeCostsGraphModel();
            model.ByAdminUnitModel = new IncomeCostsGraphByAdminUnitModel();
            model.ByAdminUnitModel.AvailableAdminUnits = GetBudgetClient().Data.FinancialCenters.GetAll().ToDictionary(f => f.Id, f => f.Name);
            var company = GetBudgetClient().Data.Company.Get();
            model.ByAdminUnitModel.AvailableAdminUnits.Add(company.Id, company.Name);

            var availableYears = GetBudgetClient().Data.YearComplexBudgetProjects.GetAll().Select(b => new { year = b.Year, yearName = b.GetPeriodName() }).Distinct().ToDictionary(b => b.year, b => b.yearName);

            model.ByAdminUnitModel.MonthGraphModel = new MonthGraphModel { AvailableYears = availableYears };
            model.ByAdminUnitModel.QuarterGraphModel = new QuarterGraphModel { AvailableYears = availableYears };

            model.ByAdminUnitModel.YearGraphModel = new YearGraphModel();

            //--Period model

            model.ByPeriodModel = new IncomeCostsGrapByPeriodModel();

            model.ByPeriodModel.MonthGraphModel = new MonthSelectGraphModel();
            model.ByPeriodModel.MonthGraphModel.AvailableYears = availableYears;
            model.ByPeriodModel.MonthGraphModel.AvailableMonths = CommonHelper.Months;
            model.ByPeriodModel.QuarterGraphModel = new QuarterSelectGraphModel();
            model.ByPeriodModel.QuarterGraphModel.AvailableQuarters = CommonHelper.Quarters;
            model.ByPeriodModel.QuarterGraphModel.AvailableYears = availableYears;
            model.ByPeriodModel.YearGraphModel = new YearSelectGraphModel();
            model.ByPeriodModel.YearGraphModel.AvailableYears = availableYears;

            return View(model);
        }

        [HttpPost]
        public ActionResult CostsGraph(IncomeCostsGraphModel model)
        {
            var viewModel = new LineralGraphModel();

            if (model.ByAdminUnitModel != null && model.ByAdminUnitModel.IsSelected)
            {
                viewModel.GraphType = "Line";

                int adminUnitId = model.ByAdminUnitModel.SelectedAdminUnitId;
                string adminUnitName = adminUnitId == CompanyId
                                           ? GetBudgetClient().Data.Company.Get().Name
                                           : GetBudgetClient().Data.FinancialCenters.Get(adminUnitId).Name;

                if (model.ByAdminUnitModel.MonthGraphModel.IsSelected)
                {
                    int year = model.ByAdminUnitModel.MonthGraphModel.SelectedYear;

                    var budgets = new Dictionary<string, double>();

                    foreach (var quarter in CommonHelper.Months)
                    {
                        var value = GetBudgetClient().Data.MonthComplexBudgetProjects.GetFinalFor(adminUnitId, year, quarter.Key);
                        budgets.Add(quarter.Value, value != null ? value.TotalCosts : 0);
                    }

                    viewModel.Values = budgets.Select(b => b.Value).ToArray();
                    viewModel.Names = budgets.Select(b => b.Key).ToArray();

                    viewModel.GraphName = "Видатки " + adminUnitName + " на " + year + " р.";

                    return View("DrawLineralGraph", viewModel);
                }
                if (model.ByAdminUnitModel.QuarterGraphModel.IsSelected)
                {
                    int year = model.ByAdminUnitModel.QuarterGraphModel.SelectedYear;

                    var budgets = new Dictionary<string, double>();

                    foreach (var quarter in CommonHelper.Quarters)
                    {
                        var value = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetFinalFor(adminUnitId, year, quarter.Key);
                        budgets.Add(quarter.Value, value != null ? value.TotalCosts : 0);
                    }

                    viewModel.Values = budgets.Select(b => b.Value).ToArray();
                    viewModel.Names = budgets.Select(b => b.Key).ToArray();

                    viewModel.GraphName = "Видатки " + adminUnitName + " на " + year + " р.";

                    return View("DrawLineralGraph", viewModel);
                }
                if (model.ByAdminUnitModel.YearGraphModel.IsSelected)
                {
                    var availableYears = GetBudgetClient().Data.YearComplexBudgetProjects.GetAll().Select(b => new { year = b.Year, yearName = b.GetPeriodName() }).Distinct().ToDictionary(b => b.year, b => b.yearName);
                    var budgets = new Dictionary<string, double>();

                    foreach (var year in availableYears)
                    {
                        var value = GetBudgetClient().Data.YearComplexBudgetProjects.GetFinalFor(adminUnitId, year.Key);
                        budgets.Add(year.Value, value != null ? value.TotalCosts : 0);
                    }

                    viewModel.Values = budgets.Select(b => b.Value).ToArray();
                    viewModel.Names = budgets.Select(b => b.Key).ToArray();

                    viewModel.GraphName = "Видатки " + adminUnitName + " по роках";

                    return View("DrawLineralGraph", viewModel);
                }
            }
            if (model.ByPeriodModel.IsSelected)
            {
                viewModel.GraphType = "Column";
                if (model.ByPeriodModel.MonthGraphModel.IsSelected)
                {
                    int month = model.ByPeriodModel.MonthGraphModel.Month;
                    int year = model.ByPeriodModel.MonthGraphModel.Year;
                    var budgets = GetBudgetClient()
                        .Data.MonthComplexBudgetProjects.GetAll()
                        .Where(b => b.Month == month && b.Year == year && b.IsFinal)
                        .GroupBy(b => b.AdministrativeUnitId)
                        .Select(b => b.MaxBy(c => c.Revision));
                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = budgets.Select(b => b.TotalCosts).ToArray();

                    viewModel.GraphName = "Видатки за " + CommonHelper.Months[month] + " " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }

                if (model.ByPeriodModel.QuarterGraphModel.IsSelected)
                {
                    int quarter = model.ByPeriodModel.QuarterGraphModel.Quarter;
                    int year = model.ByPeriodModel.QuarterGraphModel.Year;
                    var budgets = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetAll()
                                                   .Where(b => b.QuarterNumber == quarter && b.Year == year && b.IsFinal)
                                                   .GroupBy(b => b.AdministrativeUnitId)
                                                   .Select(b => b.MaxBy(c => c.Revision));
                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = budgets.Select(b => b.TotalCosts).ToArray();

                    viewModel.GraphName = "Видатки за " + CommonHelper.Quarters[quarter] + " " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }

                if (model.ByPeriodModel.YearGraphModel.IsSelected)
                {
                    int year = model.ByPeriodModel.YearGraphModel.SelectedYear;
                    var budgets = GetBudgetClient()
                        .Data.YearComplexBudgetProjects.GetAll()
                        .Where(b => b.Year == year && b.IsFinal)
                        .GroupBy(b => b.AdministrativeUnitId)
                        .Select(b => b.MaxBy(c => c.Revision));
                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = budgets.Select(b => b.TotalCosts).ToArray();

                    viewModel.GraphName = "Видатки за " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }
            }

            return View("DrawLineralGraph", viewModel);
        }

        public ActionResult IncomeGraph()
        {
            var model = new IncomeCostsGraphModel();
            model.ByAdminUnitModel = new IncomeCostsGraphByAdminUnitModel();
            model.ByAdminUnitModel.AvailableAdminUnits = GetBudgetClient().Data.FinancialCenters.GetAll().ToDictionary(f => f.Id, f => f.Name);
            var company = GetBudgetClient().Data.Company.Get();
            model.ByAdminUnitModel.AvailableAdminUnits.Add(company.Id, company.Name);

            var availableYears = GetBudgetClient().Data.YearComplexBudgetProjects.GetAll().Select(b => new { year = b.Year, yearName = b.GetPeriodName() }).Distinct().ToDictionary(b => b.year, b => b.yearName);
            
            model.ByAdminUnitModel.MonthGraphModel = new MonthGraphModel {AvailableYears = availableYears};
            model.ByAdminUnitModel.QuarterGraphModel = new QuarterGraphModel {AvailableYears = availableYears};

            model.ByAdminUnitModel.YearGraphModel = new YearGraphModel();

            //--Period model

            model.ByPeriodModel = new IncomeCostsGrapByPeriodModel();

            model.ByPeriodModel.MonthGraphModel = new MonthSelectGraphModel();
            model.ByPeriodModel.MonthGraphModel.AvailableYears = availableYears;
            model.ByPeriodModel.MonthGraphModel.AvailableMonths = CommonHelper.Months;
            model.ByPeriodModel.QuarterGraphModel = new QuarterSelectGraphModel();
            model.ByPeriodModel.QuarterGraphModel.AvailableQuarters = CommonHelper.Quarters;
            model.ByPeriodModel.QuarterGraphModel.AvailableYears = availableYears;
            model.ByPeriodModel.YearGraphModel = new YearSelectGraphModel();
            model.ByPeriodModel.YearGraphModel.AvailableYears = availableYears;
            return View(model);
        }

        [HttpPost]
        public ActionResult IncomeGraph(IncomeCostsGraphModel model)
        {
            var viewModel = new LineralGraphModel();
            
            if (model.ByAdminUnitModel != null && model.ByAdminUnitModel.IsSelected)
            {
                viewModel.GraphType = "Line";

                int adminUnitId = model.ByAdminUnitModel.SelectedAdminUnitId;
                string adminUnitName = adminUnitId == CompanyId
                                           ? GetBudgetClient().Data.Company.Get().Name
                                           : GetBudgetClient().Data.FinancialCenters.Get(adminUnitId).Name;

                if (model.ByAdminUnitModel.MonthGraphModel.IsSelected)
                {
                    int year = model.ByAdminUnitModel.MonthGraphModel.SelectedYear;

                    var budgets = new Dictionary<string, double>();

                    foreach (var month in CommonHelper.Months)
                    {
                        var value = GetBudgetClient().Data.MonthComplexBudgetProjects.GetFinalFor(adminUnitId, year, month.Key);
                        budgets.Add(month.Value, value != null ? value.TotalIncome : 0);
                    }

                    viewModel.Values = budgets.Select(b => b.Value).ToArray();
                    viewModel.Names = budgets.Select(b => b.Key).ToArray();

                    viewModel.GraphName = "Доходи " + adminUnitName + " на " + year + " р.";

                    return View("DrawLineralGraph", viewModel);
                }
                if (model.ByAdminUnitModel.QuarterGraphModel.IsSelected)
                {
                    int year = model.ByAdminUnitModel.QuarterGraphModel.SelectedYear;

                    var budgets = new Dictionary<string, double>();
                    
                    foreach (var quarter in CommonHelper.Quarters)
                    {
                        var value = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetFinalFor(adminUnitId, year, quarter.Key);
                        budgets.Add(quarter.Value, value != null ? value.TotalIncome : 0);
                    }

                    viewModel.Values = budgets.Select(b => b.Value).ToArray();
                    viewModel.Names = budgets.Select(b => b.Key).ToArray();

                    viewModel.GraphName = "Доходи " + adminUnitName + " на " + year + " р.";

                    return View("DrawLineralGraph", viewModel);
                }
                if (model.ByAdminUnitModel.YearGraphModel.IsSelected)
                {
                    var availableYears = GetBudgetClient().Data.YearComplexBudgetProjects.GetAll().Select(b => new { year = b.Year, yearName = b.GetPeriodName() }).Distinct().ToDictionary(b => b.year, b => b.yearName);
                    var budgets = new Dictionary<string, double>();

                    foreach (var year in availableYears)
                    {
                        var value = GetBudgetClient().Data.YearComplexBudgetProjects.GetFinalFor(adminUnitId, year.Key);
                        budgets.Add(year.Value, value != null ? value.TotalIncome : 0);
                    }

                    viewModel.Values = budgets.Select(b => b.Value).ToArray();
                    viewModel.Names = budgets.Select(b => b.Key).ToArray();

                    viewModel.GraphName = "Доходи " + adminUnitName + " по роках";

                    return View("DrawLineralGraph", viewModel);
                }
            }
            if (model.ByPeriodModel.IsSelected)
            {
                viewModel.GraphType = "Column";
                if (model.ByPeriodModel.MonthGraphModel.IsSelected)
                {
                    int month = model.ByPeriodModel.MonthGraphModel.Month;
                    int year = model.ByPeriodModel.MonthGraphModel.Year;
                    
                    var budgets = GetBudgetClient()
                        .Data.MonthComplexBudgetProjects.GetAll()
                        .Where(b => b.Month == month && b.Year == year && b.IsFinal)
                        .GroupBy(b => b.AdministrativeUnitId)
                        .Select(b => b.MaxBy(c => c.Revision));

                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = budgets.Select(b => b.TotalIncome).ToArray();

                    viewModel.GraphName = "Доходи за " + CommonHelper.Months[month] + " " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }

                if (model.ByPeriodModel.QuarterGraphModel.IsSelected)
                {
                    int quarter = model.ByPeriodModel.QuarterGraphModel.Quarter;
                    int year = model.ByPeriodModel.QuarterGraphModel.Year;
                    var budgets = GetBudgetClient().Data.QuarterComplexBudgetProjects.GetAll()
                                                   .Where(b => b.QuarterNumber == quarter && b.Year == year && b.IsFinal)
                                                   .GroupBy(b => b.AdministrativeUnitId)
                                                   .Select(b => b.MaxBy(c => c.Revision));
                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = budgets.Select(b => b.TotalIncome).ToArray();

                    viewModel.GraphName = "Доходи за " + CommonHelper.Quarters[quarter] + " " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }

                if (model.ByPeriodModel.YearGraphModel.IsSelected)
                {
                    int year = model.ByPeriodModel.YearGraphModel.SelectedYear;
                    var budgets = GetBudgetClient()
                        .Data.YearComplexBudgetProjects.GetAll()
                        .Where(b => b.Year == year && b.IsFinal)
                        .GroupBy(b => b.AdministrativeUnitId)
                        .Select(b => b.MaxBy(c => c.Revision));

                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = budgets.Select(b => b.TotalIncome).ToArray();

                    viewModel.GraphName = "Доходи за " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }
            }

            return View("DrawLineralGraph", viewModel);
        }

        public ActionResult BudgetShareGraph()
        {
            var model = new BudgetShareGraphModel();

            var availableYears = GetBudgetClient().Data.YearComplexBudgetProjects.GetAll().Select(b => new { year = b.Year, yearName = b.GetPeriodName() }).Distinct().ToDictionary(b => b.year, b => b.yearName);

            model.MonthGraphModel = new MonthSelectGraphModel();
            model.MonthGraphModel.AvailableYears = availableYears;
            model.MonthGraphModel.AvailableMonths = CommonHelper.Months;
            model.QuarterGraphModel = new QuarterSelectGraphModel();
            model.QuarterGraphModel.AvailableQuarters = CommonHelper.Quarters;
            model.QuarterGraphModel.AvailableYears = availableYears;
            model.YearGraphModel = new YearSelectGraphModel();
            model.YearGraphModel.AvailableYears = availableYears;
            return View(model);
        }

        [HttpPost]
        public ActionResult BudgetShareGraph(BudgetShareGraphModel model)
        {
            var viewModel = new LineralGraphModel();
                viewModel.GraphType = "Pie";
                if (model.MonthGraphModel.IsSelected)
                {
                    int month = model.MonthGraphModel.Month;
                    int year = model.MonthGraphModel.Year;

                    var budgets = GetBudgetClient()
                        .Data.MonthComplexBudgetProjects.GetAll()
                        .Where(b => b.Month == month && b.Year == year && b.IsFinal && b.AdministrativeUnitId != CompanyId)
                        .GroupBy(b => b.AdministrativeUnitId)
                        .Select(b => b.MaxBy(c => c.Revision));

                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = model.ByCosts
                                           ? budgets.Select(b => b.TotalCosts).ToArray()
                                           : budgets.Select(b => b.TotalIncome).ToArray();

                    var name = model.ByCosts ? "видатків" : "доходів";
                    viewModel.GraphName = "Структура " + name + " за " + CommonHelper.Months[month] + " " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }

                if (model.QuarterGraphModel.IsSelected)
                {
                    int quarter = model.QuarterGraphModel.Quarter;
                    int year = model.QuarterGraphModel.Year;
                    var budgets = GetBudgetClient()
                        .Data.QuarterComplexBudgetProjects.GetAll()
                        .Where(b => b.QuarterNumber == quarter && b.Year == year && b.IsFinal && b.AdministrativeUnitId != CompanyId)
                        .GroupBy(b => b.AdministrativeUnitId)
                        .Select(b => b.MaxBy(c => c.Revision));
                    viewModel.Names = budgets.Select(b => b.AdministrativeUnit.Name).ToArray();
                    viewModel.Values = model.ByCosts
                                           ? budgets.Select(b => b.TotalCosts).ToArray()
                                           : budgets.Select(b => b.TotalIncome).ToArray();

                    var name = model.ByCosts ? "видатків" : "доходів";
                    viewModel.GraphName = "Структура " + name + " за " + CommonHelper.Quarters[quarter] + " " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }

                if (model.YearGraphModel.IsSelected)
                {
                    int year = model.YearGraphModel.SelectedYear;
                    var budgets = GetBudgetClient()
                        .Data.YearComplexBudgetProjects.GetAll()
                        .Where(b => b.Year == year && b.IsFinal && b.AdministrativeUnitId != CompanyId)
                        .GroupBy(b => b.AdministrativeUnitId)
                        .Select(b => b.MaxBy(c => c.Revision));

                    viewModel.Values = model.ByCosts
                                           ? budgets.Select(b => b.TotalCosts).ToArray()
                                           : budgets.Select(b => b.TotalIncome).ToArray();

                    var name = model.ByCosts ? "видатків" : "доходів";
                    viewModel.GraphName = "Структура " + name + " за " + year + " рік.";

                    return View("DrawLineralGraph", viewModel);
                }
            
            return View("DrawLineralGraph", viewModel);
        }
    }
}
