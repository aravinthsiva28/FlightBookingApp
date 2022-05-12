using AirlineService.DataModel;
using AirlineService.DataModel.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Controllers
{
    [Route("api/v1.0/booking")]
    public class BookingController : Controller
    {

        private IBookingRepository _booking;

        public BookingController(IBookingRepository booking)
        {

            _booking = booking;
        }

        [HttpPost]
        [Route("AddBooking")]
        public async Task<IActionResult> AddBooking([FromBody] UserBookingModel model)
        {
            await _booking.AddBooking(model);

            return Ok();

        }

        [HttpGet]
        [Route("BookingByName/{id}")]
        public async Task<IActionResult> GetBookingByName(int id)
        {
           var bookingList = await _booking.GetBookingByName(id);

            return Ok(JsonConvert.SerializeObject(bookingList, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

        }

        [HttpPut]
        [Route("CancelBooking/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            await _booking.CancelBooking(id);

            return Ok();

        }

        [HttpGet]
        [Route("discount")]
        public async Task<IActionResult> GetDiscountList(int id)
        {
            var discountList = await _booking.GetDiscountList();

            return Ok(discountList);

        }

        [HttpPost]
        [Route("AddDiscount")]
        public async Task<IActionResult> AddDiscount(DiscountModel model)
        {
            await _booking.AddDiscount(model);

            return Ok();

        }


    }
}
