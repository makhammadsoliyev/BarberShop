using BarberShop.Models.Barbershops;
using BarberShop.Models.Commons;
using BarberShop.Models.Users;

namespace BarberShop.Models.Appointments;

public class Appointment : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long BarberShopId { get; set; }
    public Barbershop BarberShop { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}
