using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiHiringExercise.Domain.Entities
{
    public class ServiceLine
    {
        [Key]
        public int ServiceLineId { get; set; }
        public int RepairOrderId { get; set; }
        public int OpCodeId { get; set; }
        public decimal MiscellaneousFee { get; set; }

        [ForeignKey("RepairOrderId")]
        public virtual RepairOrder RepairOrder { get; set; }
        [ForeignKey("OpCodeId")]
        public virtual OpCode OpCode { get; set; }
    }
}