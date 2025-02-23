using System.Data.SqlClient;
using Application.SqlQueries.Clients.Models;

namespace Application.SqlQueries.Clients;

public class DeleteClientQuery
{
    #region Method

    public async Task<int> Execute(DeleteClientInDatabaseInputModel inDatabaseInputModel)
    {
        int deletedRows = 0;
        try
        {
            using (var connection = new SqlConnection(inDatabaseInputModel.ConnectionString))
            {
                await connection.OpenAsync();
                var query = @"DELETE FROM [Client]
WHERE [Id] = @Id;";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", inDatabaseInputModel.Id);
                deletedRows= await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return deletedRows;
    }
    
    #endregion Method
}