using BarberShop.DbContexts;
using BarberShop.Interfaces;
using BarberShop.Models.Appointments;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IUserService userService;
    private readonly BarbershopDbContext dbContext;
    private readonly DbSet<Appointment> appointments;
    private readonly IBarbershopService barbershopService;

    public AppointmentService(BarbershopDbContext dbContext, IUserService userService, IBarbershopService barbershopService)
    {
        this.dbContext = dbContext;
        this.userService = userService;
        this.appointments = dbContext.Appointments;
        this.barbershopService = barbershopService;
    }

    public async Task<Appointment> BookAsync(Appointment appointment)
    {
        await userService.GetByIdAsync(appointment.UserId);
        await barbershopService.GetByIdAsync(appointment.BarberShopId);
        var entityAppointment = await appointments.AddAsync(appointment);

        await dbContext.SaveChangesAsync();

        return entityAppointment.Entity;
    }

    public async Task<bool> CancelAsync(long id)
    {
        var appointment = await appointments.FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new Exception();

        appointments.Remove(appointment);

        await dbContext.SaveChangesAsync();

        return true;
    }
}
