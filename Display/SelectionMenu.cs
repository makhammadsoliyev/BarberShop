using BarberShop.Models.Appointments;
using BarberShop.Models.Barbershops;
using BarberShop.Models.Users;
using Spectre.Console;

namespace BarberShop.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params Barbershop[] barbershops)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Address");
        table.AddColumn("Latitude");
        table.AddColumn("Longitude");

        table.Border = TableBorder.Rounded;
        table.Centered();

        foreach (var donation in barbershops)
            table.AddRow(donation.Id.ToString(), donation.Name, donation.Address, donation.Latitude, donation.Longitude);

        return table;
    }

    public Table DataTable(string title, params Appointment[] appointments)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("User");
        table.AddColumn("BarberShop");
        table.AddColumn("Time");

        foreach (var appointment in appointments)
            table.AddRow(appointment.Id.ToString(), appointment.User.FirstName + " " + appointment.User.LastName, appointment.BarberShop.Name, $"{appointment.Date} {appointment.Time}");

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public Table DataTable(string title, params User[] users)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Phone");

        foreach (var user in users)
            table.AddRow(user.Id.ToString(), user.FirstName, user.LastName, user.PhoneNumber);

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(7) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }
}