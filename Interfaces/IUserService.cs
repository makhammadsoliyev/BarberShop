using BarberShop.Models.Appointments;
using BarberShop.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync (User user);

    Task<User> LogInAsync(string phoneNumber, string password);

    Task<IEnumerable<User>> GetAllAsync();

    Task<User> GetByIdAsync(long id);

    Task<bool> DeleteAsync(long id);

    Task<User> UpdateAsync(long id, User user);

    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
}
