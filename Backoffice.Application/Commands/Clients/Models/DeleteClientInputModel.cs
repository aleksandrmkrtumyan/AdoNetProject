namespace Backoffice.Application.Commands.Clients.Models;

public class DeleteClientInputModel
{
    public Guid Id { get; set; }
    public string ConnectionString { get; set; }
}