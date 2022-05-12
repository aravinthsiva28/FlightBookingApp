using AirlineService.DataModel.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel.Repository
{
    public class BookingRepository : IBookingRepository
    {

        private DataContext _airlineContext;

        public BookingRepository(DataContext airlineContext)
        {
            _airlineContext = airlineContext;
        }
        public async Task<List<UserBookingModel>> GetBookingByName(int id)
        {
            List<UserBookingModel> booking = await _airlineContext.UserBooking.Where(x => x.UserId == id).ToListAsync();


            foreach (var model in booking)
            {
                List<Passenger> pass = await _airlineContext.Passengers.Where(x => x.BookingId == model.Id).ToListAsync();
                //foreach (var usr in pass)
                //{
                //    var user = new Passenger();
                //    usr.PassengerName = user.PassengerName;
                //    usr.SeatNo = user.SeatNo;
                //    usr.Gender = user.Gender;
                //    usr.Age = user.Age;
                //    usr.BookingId = model.Id;
                //    _airlineContext.Passengers.Add(usr);
                //}
                model.PassengerList = pass;
               
            }

            return booking;
        }
        public async Task AddBooking(UserBookingModel model)
        {
            try
            {
                double cost;
                FlightSheduleModel flight = await _airlineContext.Shedule.Where(x => x.Id == model.FlightId).FirstOrDefaultAsync();
                DiscountModel discount = await _airlineContext.Discount.Where(x => x.Id == model.DiscountId).FirstOrDefaultAsync();
                if (model.ClassType == "B-Class")
                {
                    cost = (flight.NBcTicketCost * model.NoOfSeats);
                     if (model.JourneyType == "Round Trip")
                        cost = cost * 2;
                     cost = cost - discount.Amount;
                }
                else
                {
                    cost = (flight.BcTicketCost * model.NoOfSeats);
                     if (model.JourneyType == "Round Trip")
                        cost = cost * 2;
                    cost = cost - discount.Amount;
                }



                UserBookingModel booking = new UserBookingModel()
                {
                    FlightId = model.FlightId,
                    UserId = model.UserId,
                    EmailId = model.EmailId,
                    NoOfSeats = model.NoOfSeats,
                    DiscountId = model.DiscountId,
                    JourneyType = model.JourneyType,
                    ClassType = model.ClassType,
                    BookingStatus = "Active",
                    ReturnDate = model.ReturnDate,
                    TotalCost = cost,
                    PNR = model.FlightId.ToString() + model.UserId.ToString() +"-"+ new Random().Next().ToString()
                };

                _airlineContext.UserBooking.Add(booking);

                await _airlineContext.SaveChangesAsync();

                foreach (var user in model.PassengerList)
                {
                    var usr = new Passenger();
                    usr.PassengerName = user.PassengerName;
                    usr.SeatNo = user.SeatNo;
                    usr.Gender = user.Gender;
                    usr.Age = user.Age;
                    usr.BookingId = booking.Id;
                    _airlineContext.Passengers.Add(usr);
                }

                await _airlineContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<DiscountModel>> GetDiscountList()
        {
            try
            {
                List<DiscountModel> discountList = await _airlineContext.Discount.ToListAsync();

                return discountList;

            }
           

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task AddDiscount(DiscountModel model)
        {
            try
            {
                _airlineContext.Discount.Add(model);
                await _airlineContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task CancelBooking(int id)
        {

            try
            {

                UserBookingModel booking = await _airlineContext.UserBooking.FirstOrDefaultAsync(x => x.Id == id);

                if (booking != null)
                {
                    booking.BookingStatus = "Cancelled";
                    await _airlineContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
