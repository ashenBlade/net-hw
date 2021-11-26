using HW10.Models;
using Microsoft.EntityFrameworkCore;

namespace HW10.Infrastructure
{
    public class CalculatorDbContext : DbContext
    {
        public DbSet<CalculatorCache> CalculatorCache { get; set; }

        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options) { }
    }
}