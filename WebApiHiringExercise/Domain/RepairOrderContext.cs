using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApiHiringExercise.Domain.Entities;

namespace WebApiHiringExercise.Domain
{
    public class RepairOrderContext : DbContext,  IRepairOrderContext
    {
        public IDbSet<RepairOrderStatus> RepairOrderStatuses { get; set; }
        public IDbSet<Vehicle> Vehicles { get; set; }
        public IDbSet<RepairOrder> RepairOrders { get; set; }
        public IDbSet<OpCode> OpCodes { get; set; }
        public IDbSet<ServiceLine> ServiceLines { get; set; }

        public RepairOrderContext()
            :base("defaultConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}