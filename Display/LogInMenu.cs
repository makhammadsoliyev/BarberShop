using BarberShop.Configurations;
using BarberShop.Interfaces;
using BarberShop.Models.Appointments;
using BarberShop.Models.Users;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace BarberShop.Display;

public class LogInMenu
{
    private User user;
    private readonly IUserService userService;
    private readonly IBarbershopService barbershopService;
    private readonly IAppointmentService appointmentService;
    private readonly BarbershopMenu barbershopMenu;

    public LogInMenu(IUserService userService, IAppointmentService appointmentService, IBarbershopService barbershopService, BarbershopMenu barbershopMenu)
    {
        this.userService = userService;
        this.appointmentService = appointmentService;
        this.barbershopService = barbershopService;
        this.barbershopMenu = barbershopMenu;
    }

    private async Task GetAllUsers()
    {
        var users = await userService.GetAllAsync();
        var table = new SelectionMenu().DataTable("Users", users.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private async Task UpdateUser()
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
            var updatedUser = await userService.UpdateAsync(this.user.Id, user);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task DeleteUser()
    {
        try
        {
            bool isDeleted = await userService.DeleteAsync(user.Id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task GetUser()
    {
        try
        {
            var table = new SelectionMenu().DataTable("User", user);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            await Task.Delay(1500);
        }
    }

    private async Task GetAppointments()
    {
        try
        {
            var appointments = await userService.GetAllAppointmentsAsync(user.Id);
            var table = new SelectionMenu().DataTable("Appointments", appointments.ToArray());
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task MakeAppointment()
    {
        try
        {
            var barbershops = await barbershopService.GetAllAsync();
            var options = barbershops.Select(b => $"{b.Id} {b.Name} {b.Address}");
            var selection = new SelectionMenu().ShowSelectionMenu("Barbershops", options.ToArray());
            var date = AnsiConsole.Ask<DateOnly>("[cyan1]Date: [/]");
            var timeOptions = Enumerable.Range(8, 14).Select(t => $"{t}:00");
            var time = TimeOnly.Parse(new SelectionMenu().ShowSelectionMenu("Time", timeOptions.ToArray()));
            var barbershopId = Convert.ToInt64(selection.Split()[0]);
            var appointment = new Appointment()
            {
                Time = time,
                Date = date,
                UserId = user.Id,
                BarberShopId = barbershopId,
            };
            await appointmentService.BookAsync(appointment);
            AnsiConsole.MarkupLine("[blue]Successfully booked...[/]");
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task CancelAppointment()
    {
        try
        {
            var appointments = await userService.GetAllAppointmentsAsync(user.Id);
            var options = appointments.Select(a => $"{a.Id} {a.BarberShop.Name} {a.Date} {a.Time}").Append("Back");
            var selection = new SelectionMenu().ShowSelectionMenu("Appointments", options.ToArray());
            if (selection != "Back")
            {
                var id = Convert.ToInt64(selection.Split()[0]);
                await appointmentService.CancelAsync(id);
                AnsiConsole.MarkupLine("[blue]Successfully cancelled...[/]");
                AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    public async Task Display()
    {
        string phone = AnsiConsole.Ask<string>("[blue]Phone: [/]").Trim();
        string password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());

        if (phone == Constants.ADMIN_PHONE && password == Constants.ADMIN_PASSWORD)
        {
            var circle = true;
            var selectionDisplay = new SelectionMenu();

            while (circle)
            {
                AnsiConsole.Clear();
                var options = new string[] { "Barbershop", "GetAllUsers", "[red]Back[/]" };
                var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", options);

                switch (selection)
                {
                    case "Barbershop":
                        await barbershopMenu.Display();
                        break;
                    case "GetAllUsers":
                        await GetAllUsers();
                        break;
                    case "[red]Back[/]":
                        circle = false;
                        break;
                }
            }
        }
        else
        {
            try
            {
                user = await userService.LogInAsync(phone, password);
                var circle = true;
                var selectionDisplay = new SelectionMenu();

                while (circle)
                {
                    AnsiConsole.Clear();
                    var options = new string[] { "UpdateProfile", "DeleteProfile", "ShowProfile", "Appointments", "MakeAppointment", "CancelAppointment", "[red]Back[/]" };
                    var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", options);

                    switch (selection)
                    {
                        case "UpdateProfile":
                            await UpdateUser();
                            break;
                        case "DeleteProfile":
                            await DeleteUser();
                            circle = false;
                            break;
                        case "ShowProfile":
                            await GetUser();
                            break;
                        case "Appointments":
                            await GetAppointments();
                            break;
                        case "MakeAppointment":
                            await MakeAppointment();
                            break;
                        case "CancelAppointment":
                            await CancelAppointment();
                            break;
                        case "[red]Back[/]":
                            circle = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                Thread.Sleep(1500);
            }
        }
    }
}
