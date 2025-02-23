namespace Application.SqlQueries.Clients.Models;

public class DeleteClientInDatabaseInputModel
{
    public Guid Id { get; set; }
    public string ConnectionString { get; set; }
}