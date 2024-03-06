using BarberShop.Configurations;
using BarberShop.Models.Appointments;
using BarberShop.Models.Barbershops;
using BarberShop.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DbContexts;

public class BarbershopDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Barbershop> Barbershops { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = $"Host={Constants.HOST};Port={Constants.PORT};Database={Constants.DATABASE};User Id={Constants.USER};Password={Constants.PASSWORD};";
        optionsBuilder.UseNpgsql(connectionString);
    }
}
