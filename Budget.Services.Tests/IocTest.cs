using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices;
using Budget.Services.Helpers;
using NUnit.Framework;

namespace Budget.Services.Tests
{
    [TestFixture]
    public class IocTest
    {
        [Test]
        public void EmployeIocTest()
        {
            var budgetClient = new BudgetServiceFactory().GetBudgetClient(ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString);
            
            var finCenters = budgetClient.DataManagement.FinancialCenters.GetAll();

            var finCenter = finCenters.First();
            //var finCenter = IocContainer.Instance.Resolve<FinancialCenter>();
            var director = finCenter.Director;
        }
    }
}
