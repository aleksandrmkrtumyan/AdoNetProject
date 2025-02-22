using System.Data.SqlClient;
using Application.SqlQueries.Clients.Models;

namespace Application.SqlQueries.Clients;

public class UpdateClientQuery
{
    #region Fields
    #endregion Fields
    
    #region Constructor

    public UpdateClientQuery()
    {
        
    }
    
    #endregion Constructor
    
    #region Methods

    public async Task Execute(UpdateClientInputModel1 input)
    {
        try
        {
            using (var connection = new SqlConnection(input.ConnectionString))
            {
                await connection.OpenAsync();
                var query = @"UPDATE Client
SET FirstName = @FirstName, LastName = @LastName
WHERE Id = @Id;";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", input.Id);
                command.Parameters.AddWithValue("@FirstName", input.FirstName);
                command.Parameters.AddWithValue("@LastName", input.LastName);
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    #endregion Methods
}