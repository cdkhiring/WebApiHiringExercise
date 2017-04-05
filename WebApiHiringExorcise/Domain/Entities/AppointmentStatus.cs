using System.ComponentModel.DataAnnotations;

namespace WebApiHiringExorcise.Domain.Entities
{
    public class AppointmentStatus
    {
        [Key]
        public int AppointmentStatusId { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }
    }
}