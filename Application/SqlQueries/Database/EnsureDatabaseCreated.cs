using System.Data.SqlClient;

namespace Application.SqlQueries.Database;

public class EnsureDatabaseCreated
{
    #region Fields

    private readonly string connectionString;

    #endregion Fields
    
    #region Constructor

    public EnsureDatabaseCreated(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task Execute()
    {
        try
        {
            var masterConnectionString = connectionString.Replace("Database=AdoNetData", "Database=master");
            using (var connection = new SqlConnection(masterConnectionString))
            {
                await connection.OpenAsync();

                var command = @"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'AdoNetData')
                BEGIN
                    CREATE DATABASE AdoNetData;
                    PRINT 'Database created successfully.';
                END
                ELSE
                BEGIN
                    PRINT 'Database already exists.';
                END
            ";

                var checkDatabaseCommand = new SqlCommand(command, connection);
            
                await checkDatabaseCommand.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    #endregion Method
}