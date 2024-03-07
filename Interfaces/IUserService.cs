using BarberShop.Models.Appointments;
using BarberShop.Models.Users;

namespace BarberShop.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(User user);

    Task<User> LogInAsync(string phoneNumber, string password);

    Task<IEnumerable<User>> GetAllAsync();

    Task<User> GetByIdAsync(long id);

    Task<bool> DeleteAsync(long id);

    Task<User> UpdateAsync(long id, User user);

    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(long id);
}
