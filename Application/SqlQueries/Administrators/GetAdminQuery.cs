using System.Data.SqlClient;
using Application.SqlQueries.Administrators.Models;
using Application.Utilities;

namespace Backoffice.SqlQueries.Administrators;

public class GetAdminQuery
{
    #region Methods

    public async Task<AdminModel?> Execute(GetAdminInputModel inputModel)
    {
        try
        {
            using (var connection = new SqlConnection(inputModel.ConnectionString))
            {
                await connection.OpenAsync();
                var hashedPassword = HashHelper.GetHashString(inputModel.Password);
                var query = @"
                             Select * From Admin 
                             Where Username=@Username
                             And PasswordHash = @HashedPassword"; 
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", inputModel.Username);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new AdminModel
                    {
                        Id = reader.GetGuid(0),
                        Username = reader.GetString(1),
                        Name = reader.GetString(3)
                    };
                }
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }
    #endregion Methods
}