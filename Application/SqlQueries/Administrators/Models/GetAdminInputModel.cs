namespace Application.SqlQueries.Administrators.Models;

public class GetAdminInputModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConnectionString { get; set; }
}