using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel.Interface
{
    public interface IFlightShedule
    {
        Task AddShedule(FlightSheduleModel model);
        Task<IEnumerable<FlightSheduleModel>> GetSheduleListAsync();
        Task<FlightSheduleModel> GetSheduleByIdAsync(int id);
        Task UpdateShedule(int id, FlightSheduleModel model);
        Task RemoveShedule(int id);
        Task<IEnumerable<FlightSheduleModel>> GetAirlinesUnblockedListAsync();

    }
}
