using BarberShop.Models.Appointments;
using BarberShop.Models.Commons;

namespace BarberShop.Models.Barbershops;

public class Barbershop : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
}
