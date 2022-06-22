using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Shift> Shifts { get; set; }
    }
}
