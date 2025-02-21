namespace Backoffice.Application.Commands.Administrators.Models;

public class AuthenticatedAdminModel
{
    public string AccessToken { get; set; }
 
    public string Name { get; set; }
    
    public string Username { get; set; }
    
    public Guid Id { get; set; }
}