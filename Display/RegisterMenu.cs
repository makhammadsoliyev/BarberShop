using BarberShop.Interfaces;
using BarberShop.Models.Users;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace BarberShop.Display;

public class RegisterMenu
{
    private readonly IUserService userService;

    public RegisterMenu(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task Display()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone: [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone: [/]");
        }
        string password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        while (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        }

        var user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Password = password,
            PhoneNumber = phone,
        };

        try
        {
            var addedUser = await userService.RegisterAsync(user);
            AnsiConsole.MarkupLine("[green]Successfully registered...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

}
