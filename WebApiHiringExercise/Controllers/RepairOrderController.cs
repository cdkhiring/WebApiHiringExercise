using System.Threading.Tasks;
using System.Web.Http;
using WebApiHiringExercise.Services.Contracts;

namespace WebApiHiringExercise.Controllers
{
    public class RepairOrderController : ApiController
    {
        private readonly IRepairOrderService _repairOrderService;

        public RepairOrderController(IRepairOrderService repairOrderService)
        {
            _repairOrderService = repairOrderService;
        }

        [HttpGet, Route("api/repairorders")]
        public async Task<IHttpActionResult> GetRepairOrders()
        {
            var repairOrders = await _repairOrderService.GetRepairOrders();
            return Ok(repairOrders);
        }
    }
}