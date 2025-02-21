namespace Backoffice.Application.Commands.Clients.Models;

public class CreateClientInputModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ConnectionString { get; set; }
}