using System.ComponentModel.DataAnnotations;

namespace WebApiHiringExercise.Domain.Entities
{
    public class RepairOrderStatus
    {
        [Key]
        public int RepairOrderStatusId { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }
    }
}