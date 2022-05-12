using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel
{
    [Table("Bookingtbl")]
    public class UserBookingModel
    {   
        [Key]
        public int Id { get; set; }
        public string PNR { get; set; }
        [Required]
        public int FlightId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string EmailId { get; set; }
        public int NoOfSeats { get; set; }
        public string JourneyType { get; set; } = "One way";
        public DateTime? ReturnDate { get; set; }
        public List<Passenger> PassengerList { get; set; }
        public string BookingStatus { get; set; }
        public string ClassType { get; set; }
        public double TotalCost { get; set; }
        public int DiscountId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserModel usermodel { get; set; }

        [ForeignKey("FlightId")]
        public virtual FlightSheduleModel flightmodel { get; set; }
        [ForeignKey("DiscountId")]
        public virtual DiscountModel discountmodel { get; set; }


    }
}
