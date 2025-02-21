using System.Data.SqlClient;
using Application.SqlQueries.Clients.Models;
// using Microsoft.Data.SqlClient;

namespace Application.SqlQueries.Clients;

public class GetClientsFromDatabaseQuery
{
   
    
    #region Method

    public async Task<List<ClientModel>> Execute(GetClientInputModel inputModel)
    {
        var clients = new List<ClientModel>();
        try
        {
            using (var connection = new SqlConnection(inputModel.ConnectionString))
            {
                await connection.OpenAsync();
                var query = @"SELECT [Id]
      ,[Firstname]
      ,[Lastname]
  FROM [AdoNetData].[dbo].[Client]";
                var command = new SqlCommand(query, connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var client = new ClientModel
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                        Lastname = reader.GetString(reader.GetOrdinal("Lastname"))
                    };
                    clients.Add(client);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return clients;
    }
    
    #endregion Method
}