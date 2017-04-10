using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHiringExercise.Domain;
using WebApiHiringExercise.ViewModels;
using WebApiHiringExercise.Services.Contracts;

namespace WebApiHiringExercise.Services
{
    public class RepairOrderService : IRepairOrderService
    {
        private readonly IRepairOrderContext _repairOrderContext;

        public RepairOrderService(IRepairOrderContext repairOrderContext)
        {
            _repairOrderContext = repairOrderContext;
        }

        public async Task<IEnumerable<RepairOrderListViewModel>> GetRepairOrders()
        {
            return await Task.Run(() =>
            {
                return (from ro in _repairOrderContext.RepairOrders
                        join stat in _repairOrderContext.RepairOrderStatuses on ro.RepairOrderStatusId equals stat.RepairOrderStatusId
                        join veh in _repairOrderContext.Vehicles on ro.VehicleId equals veh.VehicleId
                        let lines = (from sl in _repairOrderContext.ServiceLines
                                     join oc in _repairOrderContext.OpCodes on sl.OpCodeId equals oc.OpCodeId
                                     where sl.RepairOrderId == ro.RepairOrderId
                                     select new
                                     {
                                         sl.MiscellaneousFee,
                                         oc.PartsCost,
                                         oc.LaborCost,
                                         oc.EstimatedHours
                                     })
                        let estimatedHours = lines.Select(l => l.EstimatedHours).Sum()
                        let laborCost = lines.Select(l => l.LaborCost).Sum()
                        let partsCost = lines.Select(l => l.PartsCost).Sum()
                        let miscCost = lines.Select(l => l.MiscellaneousFee).Sum()
                        select new RepairOrderListViewModel
                        {
                            RepairOrderId = ro.RepairOrderId,
                            RepairOrderStatus = stat.Name,
                            VIN = veh.VIN,
                            Year = veh.Year,
                            Make = veh.Make,
                            Model = veh.Model,
                            RepairOrderDate = ro.RepairOrderDate,
                            EstmatedHours = estimatedHours,
                            LaborCost = laborCost,
                            PartsCost = partsCost,
                            MiscellaneousFees = miscCost,
                            TotalCost = laborCost + partsCost + miscCost
                        }).ToList();
            });
        }
    }
}