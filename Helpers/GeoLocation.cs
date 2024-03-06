using BarberShop.Models.ApiModels;
using Newtonsoft.Json;

namespace BarberShop.Helpers;

public static class GeoLocation
{
    public static async Task<List<Location>> GetLocations(string address)
    {
        var apiKey = "6df2ee54900741e79ebc8d4b661db667";
        var countryCode = "UZ";
        HttpClient httpClient = new HttpClient();
        var apiUrl = $"https://api.opencagedata.com/geocode/v1/json?q={Uri.EscapeDataString(address)}&key={apiKey}&countrycode={countryCode}";
        var response = await httpClient.GetAsync(apiUrl);
        var content = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
        var locations = apiResponse.Locations;

        return locations;
    }

    public static async Task<List<string>> GetLocationsNames(List<Location> locations)
       => await Task.Run(() => locations.Select(l => l.Formatted).ToList());

    public static async Task<Location> GetLocationByName(List<Location> locations, string name)
       => await Task.Run(() => locations.Find(l => l.Formatted.Equals(name)));
}
