using AirlineService.DataModel;
using AirlineService.DataModel.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/v1.0/shedule")]
    public class FlightController : Controller
    {
        private IFlightShedule _shedule;

        public FlightController(IFlightShedule shedule)
        {

            _shedule = shedule;
        }
        [HttpPost]
        [Route("AddShedule")]
        public async Task<IActionResult> AddFlightShedule([FromBody] FlightSheduleModel model)
        {
            await _shedule.AddShedule(model);

            return Ok();

        }

        [HttpGet]
        [Route("Search")]
        public async Task<IEnumerable<FlightSheduleModel>> GetAirline()
        {
            return await _shedule.GetSheduleListAsync();
        }

        [HttpGet]
        [Route("Search/{id}")]
        public async Task<FlightSheduleModel> GetAirline(int id)
        {
            return await _shedule.GetSheduleByIdAsync(id);
        }

        [HttpPut]
        [Route("UpdateShedule/{id}")]
        public async Task<IActionResult> UpdateAirline(int id, [FromBody]FlightSheduleModel model)
        {
            await _shedule.UpdateShedule(id, model);

            return Ok();
        }

        [HttpDelete]
        [Route("RemoveShedule/{id}")]
        public async Task<IActionResult> RemoveAirline(int id)
        {
            await _shedule.RemoveShedule(id);

            return Ok();
        }
        [HttpGet]
        [Route("Unblocked")]
        public async Task<IEnumerable<FlightSheduleModel>> GetAirlinesUnblockedList()
        {
            return await _shedule.GetAirlinesUnblockedListAsync();
        }
    }
}
