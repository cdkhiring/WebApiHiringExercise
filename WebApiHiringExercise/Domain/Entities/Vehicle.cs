using System.ComponentModel.DataAnnotations;

namespace WebApiHiringExercise.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [StringLength(17)]
        public string VIN { get; set; }

        public int? Year { get; set; }

        [StringLength(20)]
        public string Make { get; set; }

        [StringLength(20)]
        public string Model { get; set; }

        [StringLength(20)]
        public string Color { get; set; }
    }
}