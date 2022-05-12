using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class UserLogin
    {   [Key]
        public int Id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    } 
}
