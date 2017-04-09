using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApiHiringExorcise.Controllers;
using WebApiHiringExorcise.Domain;
using WebApiHiringExorcise.Domain.Entities;
using WebApiHiringExorcise.Tests.Extensions;
using WebApiHiringExorcise.ViewModels;

namespace WebApiHiringExorcise.Tests.Controllers
{
    [TestClass]
    public class RepairOrderControllerTests
    {
        private Mock<IRepairOrderContext> _repairOrderDbContext;
        private Mock<DbSet<RepairOrder>> _repairOrderDbSet;
        private Mock<DbSet<RepairOrderStatus>> _repairOrderStatusDbSet;
        private Mock<DbSet<Vehicle>> _vehicles;
        private Mock<DbSet<ServiceLine>> _serviceLines;
        private Mock<DbSet<OpCode>> _opCodes;

        [TestInitialize]
        public void TestInitialize()
        {
            _repairOrderDbContext = new Mock<IRepairOrderContext>();
            _repairOrderDbSet = new Mock<DbSet<RepairOrder>>();
            _repairOrderStatusDbSet = new Mock<DbSet<RepairOrderStatus>>();
            _vehicles = new Mock<DbSet<Vehicle>>();
            _serviceLines = new Mock<DbSet<ServiceLine>>();
            _opCodes = new Mock<DbSet<OpCode>>();

            _opCodes.ProvideList(new List<OpCode>
            {
                new OpCode
                {
                    OpCodeId = 1,
                    Name = "Oil Change (Non-Synthetic)",
                    Category = "Lubrication",
                    PartsCost = 29.99M,
                    LaborCost = 30M,
                    EstimatedHours = 1
                },
                new OpCode
                {
                    OpCodeId = 2,
                    Name = "Oil Change (Partial Synthetic)",
                    Category = "Lubrication",
                    PartsCost = 39.99M,
                    LaborCost = 30M,
                    EstimatedHours = 2
                },
                new OpCode
                {
                    OpCodeId = 3,
                    Name = "Oil Change (Full Synthetic)",
                    Category = "Lubrication",
                    PartsCost = 49.99M,
                    LaborCost = 30M,
                    EstimatedHours = 0.5M
                },
            });

            _repairOrderStatusDbSet.ProvideList(new List<RepairOrderStatus>
            {
                new RepairOrderStatus
                {
                    RepairOrderStatusId = 1,
                    Name = "Scheduled"
                },
                new RepairOrderStatus
                {
                    RepairOrderStatusId = 4,
                    Name = "Completed"
                }
            });

            _vehicles.ProvideList(new List<Vehicle>
            {
                new Vehicle
                {
                    VehicleId = 1,
                    VIN = "1FM5K8D80EGB50022",
                    Year = 2014,
                    Make = "Ford",
                    Model = "Explorer",
                    Color = "Black"
                },
                new Vehicle
                {
                    VehicleId = 2,
                    VIN = "3MEHM0JA0AR612164",
                    Year = 2010,
                    Make = "Mercury",
                    Model = "Milan",
                    Color = "White"
                }
            });

            _repairOrderDbSet.ProvideList(new List<RepairOrder>
            {
                new RepairOrder
                {
                    RepairOrderId = 1,
                    RepairOrderDate = DateTime.Now.AddDays(3),
                    RepairOrderStatusId = 1,
                    VehicleId = 1
                },
                new RepairOrder
                {
                    RepairOrderId = 2,
                    RepairOrderDate = DateTime.Now.AddDays(2),
                    RepairOrderStatusId = 1,
                    VehicleId = 2
                },
                new RepairOrder
                {
                    RepairOrderId = 3,
                    RepairOrderDate = DateTime.Now,
                    RepairOrderStatusId = 4,
                    VehicleId = 2
                }
            });

            _serviceLines.ProvideList(new List<ServiceLine>
            {
                new ServiceLine
                {
                    ServiceLineId = 1,
                    RepairOrderId = 1,
                    OpCodeId = 1,
                    MiscellaneousFee = 10
                },
                new ServiceLine
                {
                    ServiceLineId = 2,
                    RepairOrderId = 1,
                    OpCodeId = 2,
                    MiscellaneousFee = 20
                },
                new ServiceLine
                {
                    ServiceLineId = 3,
                    RepairOrderId = 2,
                    OpCodeId = 3,
                    MiscellaneousFee = 10
                },
                new ServiceLine
                {
                    ServiceLineId = 4,
                    RepairOrderId = 2,
                    OpCodeId = 1,
                    MiscellaneousFee = 20
                },
                new ServiceLine
                {
                    ServiceLineId = 5,
                    RepairOrderId = 3,
                    OpCodeId = 2,
                    MiscellaneousFee = 10
                },
                new ServiceLine
                {
                    ServiceLineId = 6,
                    RepairOrderId = 3,
                    OpCodeId = 3,
                    MiscellaneousFee = 20
                }
            });

            _repairOrderDbContext.Setup(x => x.OpCodes).Returns(_opCodes.Object);
            _repairOrderDbContext.Setup(x => x.RepairOrderStatuses).Returns(_repairOrderStatusDbSet.Object);
            _repairOrderDbContext.Setup(x => x.Vehicles).Returns(_vehicles.Object);
            _repairOrderDbContext.Setup(x => x.RepairOrders).Returns(_repairOrderDbSet.Object);
            _repairOrderDbContext.Setup(x => x.ServiceLines).Returns(_serviceLines.Object);
        }

        [TestMethod]
        public async Task CanGetRepairOrders()
        {
            var controller = new RepairOrderController(_repairOrderDbContext.Object);

            var repairOrders = await controller.GetRepairOrders() as OkNegotiatedContentResult<List<RepairOrderListViewModel>>;

            Assert.IsNotNull(repairOrders);
            Assert.AreEqual(3, repairOrders.Content.Count);
        }

        [TestMethod]
        public async Task GetRepairOrdersCalculatesEstimatedHoursProperly()
        {
            var controller = new RepairOrderController(_repairOrderDbContext.Object);

            var repairOrders = await controller.GetRepairOrders() as OkNegotiatedContentResult<List<RepairOrderListViewModel>>;

            Assert.AreEqual(3, repairOrders.Content.First().EstmatedHours);
        }

        [TestMethod]
        public async Task GetRepairOrdersCalculatesPartsCostProperly()
        {
            var controller = new RepairOrderController(_repairOrderDbContext.Object);

            var repairOrders = await controller.GetRepairOrders() as OkNegotiatedContentResult<List<RepairOrderListViewModel>>;

            Assert.AreEqual(69.98M, repairOrders.Content.First().PartsCost);
        }

        [TestMethod]
        public async Task GetRepairOrdersCalculatesLaborCostProperly()
        {
            var controller = new RepairOrderController(_repairOrderDbContext.Object);

            var repairOrders = await controller.GetRepairOrders() as OkNegotiatedContentResult<List<RepairOrderListViewModel>>;

            Assert.AreEqual(60M, repairOrders.Content.First().LaborCost);
        }

        [TestMethod]
        public async Task GetRepairOrdersCalculatesMiscellaneousCostProperly()
        {
            var controller = new RepairOrderController(_repairOrderDbContext.Object);

            var repairOrders = await controller.GetRepairOrders() as OkNegotiatedContentResult<List<RepairOrderListViewModel>>;

            Assert.AreEqual(30M, repairOrders.Content.First().MiscellaneousFees);
        }

        [TestMethod]
        public async Task GetRepairOrdersCalculatesTotalCostProperly()
        {
            var controller = new RepairOrderController(_repairOrderDbContext.Object);

            var repairOrders = await controller.GetRepairOrders() as OkNegotiatedContentResult<List<RepairOrderListViewModel>>;

            Assert.AreEqual(159.98M, repairOrders.Content.First().TotalCost);
        }
    }
}
