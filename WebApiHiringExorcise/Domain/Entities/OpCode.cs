using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiHiringExorcise.Domain.Entities
{
    public class OpCode
    {
        [Key]
        public int OpCodeId { get; set; }
        [Required, StringLength(10), Column("OpCode")]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        public decimal PartsCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal EstimatedHours { get; set; }
    }
}