using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices;
using Budget.Services.BudgetServices.DataProviders;
using NUnit.Framework;

namespace Budget.Services.Tests
{
    [TestFixture]
    public class AdministrativeUnitDataTest
    {
        private Company _company;
        private FinancialCenter _financialCenter;
        private string _changedName;

        [SetUp]
        public void SetUp()
        {
            _company = new Company
                {
                    Name = "TestCompany",
                    AccountNumber = 1111,
                    Adress = "My adress",
                    Director = null,
                    Edrpou = 111,
                    Phone = "1111"
                };

            _financialCenter = new FinancialCenter
                {
                    Name = "Filial1",
                    Type = FinancialCenterType.FinancialResposibilityCenter,
                    Adress = "My adress",
                    Phone = "1111"
                };
            
            _changedName = "ChangedName";
        }

        [Test]
        public void CompanyDataTest()
        {
            var companyDataProvider =
                new BudgetServiceFactory().GetBudgetClient(
                    ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString).Data.Company;
            
            var company = companyDataProvider.Get();

            company.Name = _changedName;

            companyDataProvider.Update(company);

            Assert.AreEqual(companyDataProvider.Get().Name, _changedName);
        }

        [Test]
        public void FinancialCenterDataTest()
        {
            var adminUnitDataProvider = new BudgetServiceFactory().GetBudgetClient(
                ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString)
                                                                  .Data.FinancialCenters;

            adminUnitDataProvider.Insert(_financialCenter);

            var finCenter = adminUnitDataProvider.GetAll().Single(c => c.Name == _financialCenter.Name);

            Assert.AreNotEqual(_financialCenter.Id, finCenter.Id);

            finCenter.Name = _changedName;

            adminUnitDataProvider.Update(finCenter);

            Assert.AreEqual(adminUnitDataProvider.Get(finCenter.Id).Name, _changedName);

            adminUnitDataProvider.Delete(finCenter.Id);

            Assert.Null(adminUnitDataProvider.Get(finCenter.Id));
        }
    }
}
