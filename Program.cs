
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
//var res = await servise.GetAllAsync();

//foreach (var item in res)
//{
//    Console.WriteLine($"{item.Id}   {item.FirstName}    {item.LastName}   {item.PhoneNumber}");
//}
//await servise.DeleteAsync(1);

//var result = await servise.GetByIdAsync(2);
//Console.WriteLine($"{result.FirstName}    {result.LastName}   {result.PhoneNumber}");


//var result = await servise.LogInAsync("a", "a");
//Console.WriteLine($"{result.FirstName}    {result.LastName}   {result.PhoneNumber}");

//var result = await servise.GetAllAppointmentsAsync(2);
//foreach (var item in result)
//{
//    Console.WriteLine($"{item.UserId}");
//}