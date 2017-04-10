using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiHiringExercise.ViewModels;

namespace WebApiHiringExercise.Services.Contracts
{
    public interface IRepairOrderService
    {
        Task<IEnumerable<RepairOrderListViewModel>> GetRepairOrders();
    }
}
