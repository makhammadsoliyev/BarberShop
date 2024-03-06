using Newtonsoft.Json;

namespace BarberShop.Models.ApiModels;

public class Location
{
    [JsonProperty("components")]
    public Component Components { get; set; }

    [JsonProperty("confidence")]
    public int Confidence { get; set; }

    [JsonProperty("formatted")]
    public string Formatted { get; set; }

    [JsonProperty("geometry")]
    public Geometry Geometry { get; set; }
}