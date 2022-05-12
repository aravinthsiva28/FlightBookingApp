using AirlineService.DataModel.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel.Repository
{
    public class FlightShedule : IFlightShedule
    {
        private DataContext _airlineContext;

        public FlightShedule(DataContext airlineContext)
        {
            _airlineContext = airlineContext;
        }

        public async Task<IEnumerable<FlightSheduleModel>> GetSheduleListAsync()
        {
            List<FlightSheduleModel> sheduleList = await _airlineContext.Shedule.
                ToListAsync();
            return sheduleList;
        }
        public async Task<FlightSheduleModel> GetSheduleByIdAsync(int id)
        {
            FlightSheduleModel sheduleList = await _airlineContext.Shedule.FirstOrDefaultAsync(x => x.Id == id);
            return sheduleList;
        }

        public async Task AddShedule(FlightSheduleModel model)
        {
            try
            {
                FlightSheduleModel shedule = new FlightSheduleModel()
                {
                    AirlineId = model.AirlineId,
                    InstrumentUsed = model.InstrumentUsed,
                    ToPlace = model.ToPlace,
                    FromPlace = model.FromPlace,
                    BcTicketCost = model.BcTicketCost,
                    NBcTicketCost = model.NBcTicketCost,
                    SheduledDay = model.SheduledDay,
                    StartDateTime = model.StartDateTime,
                    MealType = model.MealType,
                    TotalBCSeats = model.TotalBCSeats,
                    TotalNBCSeats = model.TotalNBCSeats

                };
                _airlineContext.Shedule.Add(model);

                await _airlineContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task UpdateShedule(int id, FlightSheduleModel model)
        {
            try
            {

                FlightSheduleModel shedule = await _airlineContext.Shedule.FirstOrDefaultAsync(x => x.Id == id);

                shedule.AirlineId = model.AirlineId;
                shedule.InstrumentUsed = model.InstrumentUsed;
                shedule.ToPlace = model.ToPlace;
                shedule.FromPlace = model.FromPlace;
                shedule.BcTicketCost = model.BcTicketCost;
                shedule.NBcTicketCost = model.NBcTicketCost;
                shedule.SheduledDay = model.SheduledDay;
                shedule.StartDateTime = model.StartDateTime;
                shedule.MealType = model.MealType;
                shedule.TotalBCSeats = model.TotalBCSeats;
                shedule.TotalNBCSeats = model.TotalNBCSeats;

               
                //_airlineContext.Shedule.Update(shedule);

                await _airlineContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task RemoveShedule(int id)
        {
            try
            {

                FlightSheduleModel shedule = await _airlineContext.Shedule.FirstOrDefaultAsync(x => x.Id == id);

                if (shedule != null)
                {
                    _airlineContext.Shedule.Remove(shedule);
                    await _airlineContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<FlightSheduleModel>> GetAirlinesUnblockedListAsync()
        {
            List<int> AirlineId = await _airlineContext.Airline.Where(x => x.Status == "Active").Select(x=>x.Id).ToListAsync();

            List <FlightSheduleModel> sheduleList = await _airlineContext.Shedule.Where(x => AirlineId.Contains(x.AirlineId)).
                ToListAsync();
            return sheduleList;
        }



    }
}
