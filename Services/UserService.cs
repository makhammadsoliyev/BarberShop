using BarberShop.Interfaces;
using BarberShop.Models.Appointments;
using BarberShop.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Services;

public class UserService : IUserService
{
    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<User> LogInAsync(string phoneNumber, string password)
    {
        throw new NotImplementedException();
    }

    public Task<User> RegisterAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(long id, User user)
    {
        throw new NotImplementedException();
    }
}
