using System.Text.Json.Serialization;

namespace Backoffice.Client.Models.Admin;

public class AuthenticatedAdminModel
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}