using AirlineService.DataModel;
using AirlineService.DataModel.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{

    [Route("api/v1.0/flight")]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        private IAirlineServiceRepository _airline;
        private IBookingRepository _booking;

        public AirlinesController(IAirlineServiceRepository airline, IBookingRepository booking)
        {
            this._airline = airline;
            this._booking = booking;
        }


        // GET api/v1.0/flight/search/
        [HttpGet]
        [Route("Search")]
        public async Task<IEnumerable<AirlineModel>> GetAirline()
        {
            return await _airline.GetAirlinesListAsync();
        }

        // GET api/v1.0/flight/search/{id}
        [HttpGet]
        [Route("search/{id:int}")]
        public async Task<IActionResult> Getairlines(int id)
        {
            var result = await _airline.GetairlinesAsync(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> AddAirline([FromBody] AirlineModel model)
        {
            int id = await _airline.AddInventory(model);

            return CreatedAtAction(nameof(Getairlines), new { id = id, Controller = "Airlines" }, id);

        }

        [HttpPut]
        [Route("UpdateAirline/{id}")]
        public async Task<IActionResult> UpdateAirline(int id, [FromBody] AirlineModel model)
        {
            await _airline.UpdateInventory(id, model);

            return Ok();
        }

        [HttpPut]
        [Route("BlockAirline/{id}")]
        public async Task<IActionResult> BlockAirline(int id)
        {
            await _airline.BlockInventory(id);
            
            return Ok();
        }

        [HttpPut]
        [Route("cancelbooking/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            await _booking.CancelBooking(id);

            return Ok(true);

        }

        [HttpPut]
        [Route("UnblockAirline/{id}")]
        public async Task<IActionResult> UnblockAirline(int id)
        {
            await _airline.UnblockInventory(id);

            return Ok();
        }

        [HttpDelete]
        [Route("RemoveAirline/{id}")]
        public async Task<IActionResult> RemoveAirline(int id)
        {
            await _airline.RemoveInventory(id);

            return Ok();
        }

        [HttpPost]
        [Route("AddBooking")]
        public async Task<IActionResult> AddBooking([FromBody] UserBookingModel model)
        {
            await _booking.AddBooking(model);

            return Ok();

        }

        [HttpGet]
        [Route("Unblocked")]
        public async Task<IEnumerable<AirlineModel>> GetAirlinesUnblockedList()
        {
            return await _airline.GetAirlinesUnblockedListAsync();
        }

        



    }
}
