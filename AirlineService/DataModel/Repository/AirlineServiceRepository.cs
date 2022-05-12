using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineService.DataModel;
using AirlineService.DataModel.Interface;
using Microsoft.EntityFrameworkCore;

namespace AirlineService.Models
{
    public class AirlineServiceRepository : IAirlineServiceRepository
    {


        private DataContext _airlineContext;

        public AirlineServiceRepository(DataContext airlineContext)
        {
            _airlineContext = airlineContext;
        }
        
        public async Task<IEnumerable<AirlineModel>> GetAirlinesListAsync()
        {
            List<AirlineModel> airlineList = await _airlineContext.Airline.
                ToListAsync();
            return airlineList;
        }

        public async Task<IEnumerable<AirlineModel>> GetAirlinesUnblockedListAsync()
        {
            List<AirlineModel> airlineList = await _airlineContext.Airline.Where(x=>x.Status != "Blocked").
                ToListAsync();
            return airlineList;
        }

        public async Task<AirlineModel> GetairlinesAsync(int id)
        {
            AirlineModel airline = await _airlineContext.Airline.FindAsync(id);
            return airline;
        }

        public async Task UpdateInventory(int id, AirlineModel model)
        {
            try
            {

                AirlineModel airline = await _airlineContext.Airline.FirstOrDefaultAsync(x => x.Id == id);

                if (airline != null)
                {
                    airline.Name = model.Name;
                    airline.Address = model.Address;
                    airline.Contact = model.Contact;
                    airline.Status = model.Status;
                    
                    await _airlineContext.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> AddInventory(AirlineModel model)
        {
            try
            {
                _airlineContext.Airline.Add(model);

                await _airlineContext.SaveChangesAsync();

                return model.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task BlockInventory(int id)
        {
            try
            {

                AirlineModel airline = await _airlineContext.Airline.FirstOrDefaultAsync(x => x.Id == id);

                if (airline != null)
                {

                    airline.Status = "Blocked";

                    await _airlineContext.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UnblockInventory(int id)
        {
            try
            {

                AirlineModel airline = await _airlineContext.Airline.FirstOrDefaultAsync(x => x.Id == id);

                if (airline != null)
                {

                    airline.Status = "Active";

                    await _airlineContext.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task RemoveInventory(int id)
        {
            try
            {

                AirlineModel airline = await _airlineContext.Airline.FirstOrDefaultAsync(x => x.Id == id);

                if (airline != null)
                {
                    _airlineContext.Airline.Remove(airline);
                    await _airlineContext.SaveChangesAsync();
                }
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
