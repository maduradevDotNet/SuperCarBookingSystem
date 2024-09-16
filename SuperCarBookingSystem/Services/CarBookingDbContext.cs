using Microsoft.EntityFrameworkCore;
using SuperCarBookingSystem.Models;

namespace SuperCarBookingSystem.Services
{
    public class CarBookingDbContext:DbContext
    {
        public CarBookingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> cars {  get; set; }
        public DbSet<Booking> bookings {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Car>();
            modelBuilder.Entity<Booking>();
        }
    }
}
