using BarberShop.DbContexts;
using BarberShop.Interfaces;
using BarberShop.Models.Appointments;
using BarberShop.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BarberShop.Services;

public class UserService : IUserService
{
    private readonly BarbershopDbContext dbContext;
    private readonly DbSet<User> users;

    public UserService(BarbershopDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.users = dbContext.Users;        
    }

    public async Task<bool> DeleteAsync(long id)
    {

        var exisUser = await users.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"This user is not found with ID = {id}");

        users.Remove(exisUser);
        dbContext.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return users;
    }

    public async Task<User> GetByIdAsync(long id)
    {
        var exisUser = await users.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"This user is not found with ID = {id}");

        return exisUser;
    }

    public Task<User> LogInAsync(string phoneNumber, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<User> RegisterAsync(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return user;
    }

    public async Task<User> UpdateAsync(long id, User user)
    {
        var exisUser = await users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This user is not found with ID = {id}");

        exisUser.FirstName = user.FirstName;
        exisUser.LastName = user.LastName;
        exisUser.PhoneNumber = user.PhoneNumber;
        exisUser.Password = user.Password;
        exisUser.UpdatedAt =DateTime.UtcNow;

        dbContext.SaveChanges();
        return exisUser;
    }
}
