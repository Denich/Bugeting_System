using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices.DataProviders;
using Budget.Services.BudgetServices.DataServices;
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
            _company = new Company("TestCompany")
                {
                    AccountNumber = 1111,
                    Adress = "My adress",
                    Director = null,
                    Edrpou = 111,
                    Name = "My company",
                    Phone = "1111"
                };

            _financialCenter = new FinancialCenter("Filial1", FinancialCenterType.FinancialResposibilityCenter)
                {
                    Adress = "My adress",
                    Director = null,
                    Name = "My filial",
                    Phone = "1111"
                };
            
            _changedName = "ChangedName";
        }

        [Test]
        public void CompanyDataTest()
        {
            var adminUnitDataProvider = new AdministrativeUnitDataProvider();

            adminUnitDataProvider.AddCompany(_company);
            
            var company = adminUnitDataProvider.GetCompany();

            Assert.AreNotEqual(_company.Id, company.Id);

            company.Name = _changedName;

            adminUnitDataProvider.UpdateCompany(company);

            Assert.AreEqual(adminUnitDataProvider.GetCompany().Name, _changedName);

            adminUnitDataProvider.DeleteCompany(company.Id);

            Assert.Null(adminUnitDataProvider.GetCompany());
        }

        [Test]
        public void FinancialCenterDataTest()
        {
            var adminUnitDataProvider = new AdministrativeUnitDataProvider();

            adminUnitDataProvider.AddFinancialCenter(_financialCenter);

            var finCenter = adminUnitDataProvider.GetFinancialCenters().Single(c => c.Name == _financialCenter.Name);

            Assert.AreNotEqual(_financialCenter.Id, finCenter.Id);

            finCenter.Name = _changedName;

            adminUnitDataProvider.UpdateFinancialCenter(finCenter);

            Assert.AreEqual(adminUnitDataProvider.GetFinancialCenterById(finCenter.Id).Name, _changedName);

            adminUnitDataProvider.DeleteFinancialCenter(finCenter.Id);

            Assert.Null(adminUnitDataProvider.GetFinancialCenterById(finCenter.Id));
        }
    }
}
