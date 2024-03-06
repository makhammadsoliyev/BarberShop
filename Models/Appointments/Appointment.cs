using BarberShop.Models.Commons;

namespace BarberShop.Models.Appointments;

public class Appointment : Auditable
{
    public long UserId { get; set; }
    public long BarberShopId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}
