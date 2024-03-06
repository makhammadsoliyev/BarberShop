using BarberShop.Models.Appointments;
using BarberShop.Models.Barbershops;

namespace BarberShop.Interfaces;

public interface IBarbershopService
{
    Task<Barbershop> CreateAsync(Barbershop barberShop);
    Task<Barbershop> UpdateAsync(long id, Barbershop barbershop);
    Task<bool> DeleteAsync(long id);
    Task<Barbershop> GetByIdAsync(long id);
    Task<IEnumerable<Barbershop>> GetAllAsync();
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
}
