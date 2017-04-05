using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiHiringExorcise.Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int AppointmentStatusId { get; set; }
        public int VehicleId { get; set; }
        public DateTime AppointmentDate { get; set; }

        [ForeignKey("AppointmentStatusId")]
        public virtual AppointmentStatus AppointmentStatus { get; set; }
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }
    }
}