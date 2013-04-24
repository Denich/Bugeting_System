using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Budget.Services.BudgetModel;
using Budget.Services.BudgetServices;
using Budget.Web.Controllers.Common;
using Moq;
using NUnit.Framework;

namespace Budget.Web.Tests.Controllers
{
    [TestFixture]
    public class BaseControllerTest
    {
        private Mock<IBudgetServiceFactory> _factoryMock;
        private Mock<IBudgetClient> _clientMock;
        //private string _connString = "TestStr";

        [SetUp]
        public void SetUp()
        {
            _factoryMock = new Mock<IBudgetServiceFactory>();
            _clientMock = new Mock<IBudgetClient>();
        }

        [Test]
        public void GetUserIdResturnsZeroIfNoEmployes()
        {
            _clientMock.Setup(c => c.Data.Employes.GetAll()).Returns((IEnumerable<Employe>) null);
            
            _factoryMock.Setup(m => m.GetBudgetClient(ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString)).Returns(_clientMock.Object);

            var controller = new BaseController(_factoryMock.Object);

            int userId = controller.GetCurrentUser().EmployeInfo.Id;

            Assert.That(userId == 0);
        }

        [Test]
        public void GetUserIdResturnsZeroIfEmployeNotFound()
        {
            _clientMock.Setup(c => c.Data.Employes.GetAll()).Returns(new List<Employe>());

            _factoryMock.Setup(m => m.GetBudgetClient(ConfigurationManager.ConnectionStrings["CompanyDatabase"].ConnectionString)).Returns(_clientMock.Object);

            var controller = new BaseController(_factoryMock.Object);

            int userId = controller.GetCurrentUser().EmployeInfo.Id;

            Assert.That(userId == 0);
        }
    }
}
