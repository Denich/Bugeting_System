using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetServices;
using Microsoft.Practices.Unity;

namespace Budget.Web.Controllers.Common
{
    public class BaseController : Controller
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        private IBudgetClient _budgetClient;

        public IBudgetClient GetBudgetClient()
        {
            return _budgetClient ?? (_budgetClient = new BudgetServiceFactory().GetBudgetClient(_connectionString));
        }
    }
}