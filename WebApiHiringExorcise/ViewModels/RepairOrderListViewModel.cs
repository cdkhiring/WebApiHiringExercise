using System;

namespace WebApiHiringExorcise.ViewModels
{
    public class RepairOrderListViewModel
    {
        public int RepairOrderId { get; set; }
        public string RepairOrderStatus { get; set; }
        public string VIN { get; set; }
        public int? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime RepairOrderDate { get; set; }
        public decimal PartsCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal MiscellaneousFees { get; set; }
        public decimal TotalCost { get; set; }
    }
}