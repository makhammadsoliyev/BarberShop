using BarberShop.DbContexts;
using BarberShop.Interfaces;
using BarberShop.Models.Appointments;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services;

public class AppointmentService : IAppointmentService
{
    private readonly BarbershopDbContext dbContext;
    private readonly DbSet<Appointment> appointments;

    public AppointmentService(BarbershopDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.appointments = dbContext.Appointments;
    }

    public async Task<Appointment> BookAsync(Appointment appointment)
    {
        var entityAppointment = await appointments.AddAsync(appointment);

        await dbContext.SaveChangesAsync();

        return entityAppointment.Entity;
    }

    public async Task<bool> CancelAsync(long id)
    {
        var appointment = await appointments.FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new Exception();

        appointments.Remove(appointment);

        return true;
    }
}
