using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.BudgetServices.DataServices;
using NUnit.Framework;

namespace Budget.Services.Tests
{
    [TestFixture]
    public class AdministrativeUnitDataTest
    {
        private string _xmlDoc;
        
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void GetCompanyInfo()
        {
            var service = new AdministrativeUnitDatabaseService();

            var company = service.GetCompany();

            company.Name = "My";
        }
    }
}
