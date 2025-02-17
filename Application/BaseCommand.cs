using System.Text.Json.Serialization;

namespace Application;

public class BaseCommand
{
    [JsonIgnore] 
    public int Id { get; set; }
}