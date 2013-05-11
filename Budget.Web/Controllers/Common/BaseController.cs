using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices;
using Budget.Web.Models;
using Microsoft.Practices.Unity;

namespace Budget.Web.Controllers.Common
{
    public class BaseController : Controller
    {
        private readonly IBudgetServiceFactory _budgetServiceFactory;

        protected int CompanyId { get { return GetBudgetClient().Data.Company.Get().Id; } }

        public BaseController()
        {
            _budgetServiceFactory = new BudgetServiceFactory();
        }

        [InjectionConstructor]
        public BaseController(IBudgetServiceFactory budgetServiceFactory)
        {
            _budgetServiceFactory = budgetServiceFactory;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString;

        private IBudgetClient _budgetClient;

        public IBudgetClient GetBudgetClient()
        {
            return _budgetClient ?? (_budgetClient = _budgetServiceFactory.GetBudgetClient(_connectionString));
        }

        public BudgetUser GetCurrentUser()
        {
            var user = new BudgetUser
                {
                    User = User,
                };

            IEnumerable<Employe> employes = GetBudgetClient().Data.Employes.GetAll();

            if (employes == null || !employes.Any())
            {
                user.EmployeInfo = GetBudgetClient().Data.Employes.GetUnknown();
            }
            else
            {
                Employe foundEmploye = employes.SingleOrDefault(e => e.Name == User.Identity.Name);

                user.EmployeInfo = foundEmploye ?? GetBudgetClient().Data.Employes.GetUnknown();
            }

            return user;
        }
    }
}