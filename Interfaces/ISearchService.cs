using BarberShop.Models.Barbershops;

namespace BarberShop.Interfaces;

public interface ISearchService
{
    Task<IEnumerable<Barbershop>> SearchBarbershopByLocation(string lat, string lon);
    Task<IEnumerable<Barbershop>> SearchBarbershopByCity(string city);
}
