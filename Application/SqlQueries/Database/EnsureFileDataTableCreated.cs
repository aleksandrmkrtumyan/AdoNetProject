using System.Data.SqlClient;

namespace Application.SqlQueries.Database;

public class EnsureFileDataTableCreated
{
    #region Fields
    
    private readonly string connectionString;
    
    #endregion Fields
    
    #region Constructor

    public EnsureFileDataTableCreated(string connectionString)
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
                var query = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FileData')
BEGIN
    CREATE TABLE FileData
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        FileId UNIQUEIDENTIFIER,  -- Внешний ключ, ссылающийся на таблицу FileDb
        FileContent VARBINARY(MAX),  -- Поле для хранения данных файла
        CONSTRAINT FK_FileData_FileDb FOREIGN KEY (FileId) REFERENCES FileDb(Id)
    );
END";
                var command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    #endregion Method
}