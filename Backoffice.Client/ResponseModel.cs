using System.Text.Json.Serialization;

namespace Backoffice.Client;

public class ResponseModel
{
    [JsonPropertyName("authenticatedAdmin")]
    public AuthenticatedAdminModel AuthenticatedAdmin { get; set; }
}