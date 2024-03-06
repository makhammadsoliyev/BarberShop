
using BarberShop.DbContexts;
using BarberShop.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


var contex = new BarbershopDbContext();

var servise = new UserService(contex);

//await servise.RegisterAsync(
//    new BarberShop.Models.Users.User
//    {
//        FirstName = "aqq",
//        LastName = "adcw",
//        PhoneNumber = "acqwsd",
//        Password = "acqw",
//    });
var res = await servise.GetAllAsync();

foreach (var item in res)
{
    Console.WriteLine($"{item.FirstName}    {item.LastName}   {item.PhoneNumber}");
}
//await servise.DeleteAsync(1);