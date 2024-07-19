using System.Text.Json.Serialization;

namespace RestApi.Models;

public record Customer
{
    //[JsonPropertyName("CustomerId")] //(regarding exercise) I don't really see a reason to use JSON attributes ... but here is example
    public uint Id { get; set; }
    public string Firstname { get; set; } = default!;
    public string Surname { get; set; } = default!;
}