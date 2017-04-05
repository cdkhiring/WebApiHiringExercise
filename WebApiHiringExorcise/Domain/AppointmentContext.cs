using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApiHiringExorcise.Domain.Entities;

namespace WebApiHiringExorcise.Domain
{
    public class AppointmentContext : DbContext,  IAppointmentContext
    {
        public IDbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public IDbSet<Vehicle> Vehicles { get; set; }
        public IDbSet<Appointment> Appointments { get; set; }

        public AppointmentContext()
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