using System.Data.SqlClient;

namespace Application.SqlQueries.Database;

public class EnsureClientTableCreated
{
    #region Fields
    
    private readonly string connectionString;

    #endregion Fields
    
    #region Constructor

    public EnsureClientTableCreated(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task Execute()
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var createCustomerTableQuery = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Client')
BEGIN
    CREATE TABLE Client
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        Firstname NVARCHAR(100),
        Lastname NVARCHAR(100)
    );
END
";
                var createUserTableCommand = new SqlCommand(createCustomerTableQuery, connection);
                await createUserTableCommand.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    #endregion Method
}