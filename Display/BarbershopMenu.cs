using BarberShop.Helpers;
using BarberShop.Interfaces;
using BarberShop.Models.ApiModels;
using BarberShop.Models.Barbershops;
using Spectre.Console;

namespace BarberShop.Display;

public class BarbershopMenu
{
    private readonly IBarbershopService barbershopService;

    public BarbershopMenu(IBarbershopService barbershopService)
    {
        this.barbershopService = barbershopService;
    }

    private async Task Add()
    {
        var name = AnsiConsole.Ask<string>("[cyan1]Name: [/]").Trim();
    key:
        var address = AnsiConsole.Ask<string>("[cyan3]Address: [/]").Trim();
        var locations = await GeoLocation.GetLocations(address);
        var names = await GeoLocation.GetLocationsNames(locations);
        names.Add("Change address");

        var selectionDisplay = new SelectionMenu();
        var selection = selectionDisplay.ShowSelectionMenu("Choose one of locations", names.ToArray());
        Location location;

        if (selection.Equals("Change address"))
            goto key;
        else
            location = await GeoLocation.GetLocationByName(locations, selection);

        var barbershop = new Barbershop()
        {
            Name = name,
            Address = location.Formatted,
            Longitude = location.Geometry.Longitude,
            Latitude = location.Geometry.Latitude,
        };

        var addedShop = await barbershopService.CreateAsync(barbershop);
        AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        await Task.Delay(1500);
    }

    private async Task Get()
    {
        var barbershops = await barbershopService.GetAllAsync();
        var options = barbershops.Select(b => $"{b.Id} {b.Name}");
        var selection = new SelectionMenu().ShowSelectionMenu("BarberShops", options.ToArray());
        var id = Convert.ToInt64(selection.Split()[0]);
        var barbershop = await barbershopService.GetByIdAsync(id);
        var table = new SelectionMenu().DataTable("Barbershop", barbershop);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private async Task Update()
    {
        var barbershops = await barbershopService.GetAllAsync();
        var options = barbershops.Select(b => $"{b.Id} {b.Name}");
        var selection = new SelectionMenu().ShowSelectionMenu("BarberShops", options.ToArray());
        var id = Convert.ToInt64(selection.Split()[0]);
        var name = AnsiConsole.Ask<string>("[cyan1]Name: [/]").Trim();
    key:
        var address = AnsiConsole.Ask<string>("[cyan3]Address: [/]").Trim();
        var locations = await GeoLocation.GetLocations(address);
        var names = await GeoLocation.GetLocationsNames(locations);
        names.Add("Change address");

        var selectionDisplay = new SelectionMenu();
        var selectionLocation = selectionDisplay.ShowSelectionMenu("Choose one of locations", names.ToArray());
        Location location;

        if (selectionLocation.Equals("Change address"))
            goto key;
        else
            location = await GeoLocation.GetLocationByName(locations, selectionLocation);

        var barbershop = new Barbershop()
        {
            Name = name,
            Address = location.Formatted,
            Longitude = location.Geometry.Longitude,
            Latitude = location.Geometry.Latitude,
        };

        try
        {
            var updatedSHop = await barbershopService.UpdateAsync(id, barbershop);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        await Task.Delay(1500);
    }

    private async Task Delete()
    {
        var barbershops = await barbershopService.GetAllAsync();
        var options = barbershops.Select(b => $"{b.Id} {b.Name}");
        var selection = new SelectionMenu().ShowSelectionMenu("BarberShops", options.ToArray());
        var id = Convert.ToInt64(selection.Split()[0]);

        try
        {
            bool isDeleted = await barbershopService.DeleteAsync(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        await Task.Delay(1500);
    }

    private async Task GetAll()
    {
        var barbershops = await barbershopService.GetAllAsync();
        var table = new SelectionMenu().DataTable("Barbershops", barbershops.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    public async Task Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "Get", "Update", "Delete", "GetAll", "Back" });

            switch (selection)
            {
                case "Add":
                    await Add();
                    break;
                case "Get":
                    await Get();
                    break;
                case "Update":
                    await Update();
                    break;
                case "Delete":
                    await Delete();
                    break;
                case "GetAll":
                    await GetAll();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
