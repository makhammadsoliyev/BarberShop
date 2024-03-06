using BarberShop.DbContexts;
using BarberShop.Interfaces;
using BarberShop.Models.Appointments;
using BarberShop.Models.Barbershops;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services;

public class BarbershopService : IBarbershopService
{
    private readonly BarbershopDbContext dbContext;
    private readonly DbSet<Barbershop> barbershops;
    public BarbershopService(BarbershopDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.barbershops = dbContext.Barbershops;
    }
    public async Task<Barbershop> CreateAsync(Barbershop barberShop)
    {
        barberShop.CreatedAt = DateTime.UtcNow;
        var createBarberShop = await barbershops.AddAsync(barberShop);
        await dbContext.SaveChangesAsync();
        return createBarberShop.Entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existBarberShop = await barbershops.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This barbershop is not found with ID = {id}");

        barbershops.Remove(existBarberShop);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(long id)
    {
        var existBarbershop = await barbershops.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted)
            ?? throw new Exception($"This barbershop is not found with ID = {id}");

        var result = existBarbershop.Appointments?.ToList();
        return result ?? new List<Appointment>();
    }

    public async Task<IEnumerable<Barbershop>> GetAllAsync()
    {
        return await barbershops.ToListAsync();
    }

    public async Task<Barbershop> GetByIdAsync(long id)
    {
        var existBarberShop = await barbershops.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This barbershop is not found with ID = {id}");

        return existBarberShop;
    }

    public async Task<Barbershop> UpdateAsync(long id, Barbershop barbershop)
    {
        var existBarberShop = await barbershops.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
           ?? throw new Exception($"This barbershop is not found with ID = {id}");

        existBarberShop.Longitude = barbershop.Longitude;
        existBarberShop.Latitude = barbershop.Latitude;
        existBarberShop.Address = barbershop.Address;
        existBarberShop.Name = barbershop.Name;
        existBarberShop.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return existBarberShop;
    }
}
