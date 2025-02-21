using System.Data.SqlClient;

namespace Application.SqlQueries.Database;

public class EnsureFileDbTableCreated
{
    #region Fields
    
    private readonly string connectionString;

    #endregion Fields
    
    #region Constructor
    
    public EnsureFileDbTableCreated(string connectionString)
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
                var createFileDbQuery = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FileDb')
BEGIN
    CREATE TABLE FileDb
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        FileName NVARCHAR(255),
        ClientId UNIQUEIDENTIFIER
    );
END";
                var command = new SqlCommand(createFileDbQuery, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    #endregion Method
}