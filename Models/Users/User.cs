using BarberShop.Models.Appointments;
using BarberShop.Models.Commons;

namespace BarberShop.Models.Users;

public class User : Auditable
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string PhoneNumber {  get; set; }
    
    public string Password { get; set; }

    public IEnumerable<Appointment> Appointments { get; set; }
}