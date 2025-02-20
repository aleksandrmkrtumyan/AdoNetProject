namespace Backoffice.Application.Commands.Administrators.Models;

public class AuthenticateAdminInputModel
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string ConnectionString { get; set; }
}