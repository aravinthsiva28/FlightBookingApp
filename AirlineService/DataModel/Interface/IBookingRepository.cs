using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel.Interface
{
    public interface IBookingRepository
    {
        Task AddBooking(UserBookingModel model);
        Task CancelBooking(int id);
        Task<List<UserBookingModel>> GetBookingByName(int id);
        Task<List<DiscountModel>> GetDiscountList();
        Task AddDiscount(DiscountModel model);


    }
}
