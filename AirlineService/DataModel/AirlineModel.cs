using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.DataModel
{
    [Table("Airlinetbl")]
    public class AirlineModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Mininum 4 characters required")]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
