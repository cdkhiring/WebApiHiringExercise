using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiHiringExorcise.Domain;
using WebApiHiringExorcise.ViewModels;

namespace WebApiHiringExorcise.Controllers
{
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentContext _appointmentContext;

        public AppointmentController(IAppointmentContext appointmentContext)
        {
            _appointmentContext = appointmentContext;
        }

        [HttpGet, Route("api/appointments")]
        public async Task<IHttpActionResult> GetAppointments()
        {
            var appointments = await (from appt in _appointmentContext.Appointments
                                      join stat in _appointmentContext.AppointmentStatuses on appt.AppointmentStatusId equals stat.AppointmentStatusId
                                      join veh in _appointmentContext.Vehicles on appt.VehicleId equals veh.VehicleId
                                      select new AppointmentListViewModel
                                      {
                                          AppointmentId = appt.AppointmentId,
                                          AppointmentStatus = stat.Name,
                                          VIN = veh.VIN,
                                          Year = veh.Year,
                                          Make = veh.Make,
                                          Model = veh.Model,
                                          AppointmentDate = appt.AppointmentDate
                                      }).ToListAsync();

            return Ok(appointments);
        }
    }
}