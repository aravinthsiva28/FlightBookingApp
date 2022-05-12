using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel
{
    [Table("Passengertbl")]
    public class Passenger
    {
        [Key]
        public int Id { get; set; }
        public int BookingId { get; set; }
        [Required]
        public string PassengerName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int  Age { get; set; }
        [Required]
        public int SeatNo { get; set; }
        [ForeignKey("BookingId")]
        public virtual UserBookingModel bookingmodel { get; set; }
    }
}
