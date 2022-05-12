using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server = JRDOTNETFSECO-2; initial catalog = Airline; User ID=sa;Password=pass@word1");
                //@"Data Source = JRDOTNETFSECO-2; Initial Catalog = Airline;User ID = sa; Password = pass@word1");
            }

        }

        public DbSet<UserModel> User { get; set; }
    }
}
