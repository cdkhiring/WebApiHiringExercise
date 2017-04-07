using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiHiringExorcise.Domain.Entities
{
    public class RepairOrder
    {
        [Key]
        public int RepairOrderId { get; set; }
        public int RepairOrderStatusId { get; set; }
        public int VehicleId { get; set; }
        public DateTime RepairOrderDate { get; set; }

        [ForeignKey("RepairOrderStatusId")]
        public virtual RepairOrderStatus RepairOrderStatus { get; set; }
        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }
    }
}