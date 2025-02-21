using System.Data.SqlClient;

namespace Application.SqlQueries.Database;

public class EnsurePhoneNumberTableCreated
{
    #region Fields

    private readonly string connectionString;

    #endregion Fields
    
    #region Constructor

    public EnsurePhoneNumberTableCreated(string connectionString)
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
                var cratePhoneNumberTableQuery =
                    @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PhoneNumber')
BEGIN
    CREATE TABLE PhoneNumber
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        Number NVARCHAR(50),
        ClientId UNIQUEIDENTIFIER,
        CONSTRAINT FK_PhoneNumber_Client FOREIGN KEY (ClientId) REFERENCES Client(Id)
        ON DELETE NO ACTION
    );
END";
                var createPhoneNumberTableCommand = new SqlCommand(cratePhoneNumberTableQuery, connection);
                createPhoneNumberTableCommand.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    #endregion Method
}