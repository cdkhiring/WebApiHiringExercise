using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApiHiringExercise.Controllers;
using WebApiHiringExercise.ViewModels;
using WebApiHiringExercise.Services.Contracts;

namespace WebApiHiringExercise.Tests.Controllers
{
    [TestClass]
    public class RepairOrderControllerTests
    {
        private Mock<IRepairOrderService> _repairOrderService;
        private IEnumerable<RepairOrderListViewModel> _testingRepairOrders;

        [TestInitialize]
        public void TestInitialize()
        {
            _repairOrderService = new Mock<IRepairOrderService>();
            _testingRepairOrders = new List<RepairOrderListViewModel>
            {
                new RepairOrderListViewModel
                {
                    RepairOrderId = 1
                },
                new RepairOrderListViewModel
                {
                    RepairOrderId = 2
                },
                new RepairOrderListViewModel
                {
                    RepairOrderId = 3
                }
            };
        }

        [TestMethod]
        public async Task CanGetRepairOrders()
        {
            _repairOrderService.Setup(x => x.GetRepairOrders()).ReturnsAsync(_testingRepairOrders);
            var controller = new RepairOrderController(_repairOrderService.Object);

            var repairOrders = await controller.GetRepairOrders() as OkNegotiatedContentResult<IEnumerable<RepairOrderListViewModel>>;

            Assert.IsNotNull(repairOrders);
            Assert.AreEqual(3, repairOrders.Content.Count());
        }
    }
}
