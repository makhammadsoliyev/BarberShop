using BarberShop.DbContexts;
using BarberShop.Interfaces;
using BarberShop.Services;
using Spectre.Console;

namespace BarberShop.Display;

public class MainMenu
{
    private readonly BarbershopDbContext dbContext;

    private readonly LogInMenu logInMenu;
    private readonly RegisterMenu registerMenu;
    private readonly BarbershopMenu barbershopMenu;

    private readonly IUserService userService;
    private readonly IBarbershopService barbershopService;
    private readonly IAppointmentService appointmentService;

    public MainMenu()
    {
        dbContext = new BarbershopDbContext();

        userService = new UserService(dbContext);
        barbershopService = new BarbershopService(dbContext);
        appointmentService = new AppointmentService(dbContext, userService, barbershopService);

        barbershopMenu = new BarbershopMenu(barbershopService);
        logInMenu = new LogInMenu(userService, appointmentService, barbershopService, barbershopMenu);
        registerMenu = new RegisterMenu(userService);
    }

    public async Task Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var options = new string[] { "LogIn", "Register", "[red]Exit[/]" };
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", options);

            switch (selection)
            {
                case "LogIn":
                    await logInMenu.Display();
                    break;
                case "Register":
                    await registerMenu.Display();
                    break;
                case "[red]Exit[/]":
                    circle = false;
                    break;
            }
        }
    }
}
