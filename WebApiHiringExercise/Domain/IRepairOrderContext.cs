using System.Data.Entity;
using WebApiHiringExercise.Domain.Entities;

namespace WebApiHiringExercise.Domain
{
    public interface IRepairOrderContext
    {
        IDbSet<RepairOrderStatus> RepairOrderStatuses { get; set; }
        IDbSet<Vehicle> Vehicles { get; set; }
        IDbSet<RepairOrder> RepairOrders { get; set; }
        IDbSet<OpCode> OpCodes { get; set; }
        IDbSet<ServiceLine> ServiceLines { get; set; }

        int SaveChanges();
    }
}
