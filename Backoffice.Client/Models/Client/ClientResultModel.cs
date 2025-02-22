using System.Text.Json.Serialization;

namespace Backoffice.Client.Models.Client;

public class ClientResultModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("firstname")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastname")]
    public string LastName { get; set; }
}