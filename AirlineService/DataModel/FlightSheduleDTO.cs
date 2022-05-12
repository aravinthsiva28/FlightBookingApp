using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel
{
    public class FlightSheduleDTO
    {
        public int Id { get; set; }
        public string AirlineName { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string SheduledDay { get; set; }
        public string InstrumentUsed { get; set; }
        public int TotalNBCSeats { get; set; }
        public double BcTicketCost { get; set; }
        public double NBcTicketCost { get; set; }
        public string MealType { get; set; }
    }
}
