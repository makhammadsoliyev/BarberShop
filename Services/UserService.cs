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

    public async Task<User> RegisterAsync(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        var createUser = await dbContext.Users.AddAsync(user);
        dbContext.SaveChanges();
        return createUser.Entity;
    }

    public async Task<User> UpdateAsync(long id, User user)
    {
        var existUser = await users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This user is not found with ID = {id}");

        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.PhoneNumber = user.PhoneNumber;
        existUser.Password = user.Password;
        existUser.UpdatedAt = DateTime.UtcNow;

        dbContext.SaveChanges();
        return existUser;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await users.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"This user is not found with ID = {id}");

        users.Remove(existUser);
        dbContext.SaveChanges();
        return true;
    }

    public async Task<User> GetByIdAsync(long id)
    {
        var existUser = await users.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"This user is not found with ID = {id}");

        return existUser;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await users.ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(long id)
    {
        var existUser = await users.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"This user is not found with ID = {id}");

        var result = existUser.Appointments?.ToList();
        return result ?? new List<Appointment>();
    }



    public async Task<User> LogInAsync(string phoneNumber, string password)
    {
        var existUser = await users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber && x.Password == password)
             ?? throw new Exception($"This user is not found");

        return existUser;
    }

}
