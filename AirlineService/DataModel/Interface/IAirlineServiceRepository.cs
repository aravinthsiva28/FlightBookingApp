using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel.Interface
{
    public interface IAirlineServiceRepository
    {
        Task<IEnumerable<AirlineModel>> GetAirlinesListAsync();
        Task<AirlineModel> GetairlinesAsync(int id);
        Task<int> AddInventory(AirlineModel model);
        Task UpdateInventory(int id, AirlineModel model);
        Task BlockInventory(int id);
        Task UnblockInventory(int id);
        Task RemoveInventory(int id);
        Task<IEnumerable<AirlineModel>> GetAirlinesUnblockedListAsync();
        Task CancelBooking(int id);
    }
}
