using System.Data.SqlClient;
using Application.Utilities;

namespace Application.SqlQueries.Administrators;

public class EnsureAdminCreated
{

    #region Fields
    
    private readonly string connectionString;

    #endregion Fields
    
    #region Constructors

    public EnsureAdminCreated(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    #endregion Constructors
    
    #region Methods

    public async Task Execute()
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var checkAdminQuery = @"SELECT COUNT(*) FROM Admin ";
                var checkAdminExistCommand = new SqlCommand(checkAdminQuery, connection);
                var adminExists = (int)await checkAdminExistCommand.ExecuteScalarAsync();

                if (adminExists == 0)
                {
                    var hashString = HashHelper.GetHashString("admin");
                    var insertAdminCommand = new SqlCommand(@"
                        INSERT INTO Admin (Id, Username, PasswordHash, Name)
                        VALUES 
                        (NEWID(), 'admin', @Password, 'Admin')
                        ", connection);
                    insertAdminCommand.Parameters.AddWithValue("@Password", hashString);
                    await insertAdminCommand.ExecuteNonQueryAsync();
                    Console.WriteLine("Default admin added.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}"); 
        }
       
    }
    #endregion Methods
}