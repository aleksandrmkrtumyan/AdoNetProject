using System.Data.SqlClient;

namespace Application.SqlQueries.Database;

public class EnsureAdminTableCreated
{
    #region Fields
    
    private readonly string connectionString;
   
    #endregion Fields
    
    #region Constructor

    public EnsureAdminTableCreated(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task Execute()
    {
        try
        {

            using var connection = new SqlConnection(connectionString);
            {
                await connection.OpenAsync();
                var createAdminTableCommand = @"
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Admin')
    BEGIN
        CREATE TABLE Admin (
            Id UNIQUEIDENTIFIER PRIMARY KEY,
            Username NVARCHAR(100) NOT NULL,
            PasswordHash NVARCHAR(100) NOT NULL,
            Name NVARCHAR(100) NOT NULL
        )
    END
";
                var createTableCommand = new SqlCommand(createAdminTableCommand, connection);
                await createTableCommand.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    #endregion Method
}