using BarberShop.Models.Appointments;

namespace BarberShop.Interfaces;

public interface IAppointmentService
{
    Task<Appointment> BookAsync(Appointment appointment);
    Task<bool> CancelAsync(long id);
}