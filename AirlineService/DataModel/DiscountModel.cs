using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel
{
    [Table("Discounttbl")]
    public class DiscountModel
    {
        [Key]
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
