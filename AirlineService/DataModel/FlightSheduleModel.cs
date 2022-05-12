using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel
{
    [Table("Flight_shedule_tbl")]
    public class FlightSheduleModel
    {
        [Key]
        public int Id { get; set; }
        
        public int AirlineId { get; set; }
        [Required]
        public string FromPlace { get; set; }
        [Required]
        public string ToPlace { get; set; }
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
        public string SheduledDay { get; set; }
        public string InstrumentUsed { get; set; }
        [Required]
        public int TotalBCSeats { get; set; }
        [Required]
        public int TotalNBCSeats { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public double BcTicketCost { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public double NBcTicketCost { get; set; }
        public string MealType { get; set; }
        [ForeignKey("AirlineId")]
        public virtual AirlineModel airlinemodel { get; set; }


    }
}