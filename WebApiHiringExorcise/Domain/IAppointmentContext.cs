using System.Data.Entity;
using WebApiHiringExorcise.Domain.Entities;

namespace WebApiHiringExorcise.Domain
{
    public interface IAppointmentContext
    {
        IDbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        IDbSet<Vehicle> Vehicles { get; set; }
        IDbSet<Appointment> Appointments { get; set; }

        int SaveChanges();
    }
}
