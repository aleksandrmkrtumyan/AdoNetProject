using System.Text.Json.Serialization;

namespace Backoffice.Client.Models.Admin;

public class ResponseModel
{
    [JsonPropertyName("authenticatedAdmin")]
    public AuthenticatedAdminModel AuthenticatedAdmin { get; set; }
}