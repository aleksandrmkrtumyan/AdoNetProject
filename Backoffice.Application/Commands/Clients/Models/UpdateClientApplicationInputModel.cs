namespace Backoffice.Application.Commands.Clients.Models;

public class UpdateClientApplicationInputModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ConnectionString { get; set; }
}