using System;

namespace WebApiHiringExorcise.ViewModels
{
    public class AppointmentListViewModel
    {
        public int AppointmentId { get; set; }
        public string AppointmentStatus { get; set; }
        public string VIN { get; set; }
        public int? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}