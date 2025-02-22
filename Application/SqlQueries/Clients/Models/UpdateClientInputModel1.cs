namespace Application.SqlQueries.Clients.Models;

public class UpdateClientInputModel1
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ConnectionString { get; set; }
}