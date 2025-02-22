using System.Data.SqlClient;
using Application.SqlQueries.Clients.Models;

namespace Application.SqlQueries.Clients;

public class GetClientByNameFromSqlQuery
{
    #region Methods

    public async Task<List<ClientModel>> Execute(ClientByNameFromSqlInputModel fromSqlInputModel)
    {
        var clients = new List<ClientModel>();
        try
        {
            using (var connection = new SqlConnection(fromSqlInputModel.ConnectionString))
            {
                await connection.OpenAsync();
                var query = @"SELECT [Id]
      ,[Firstname]
      ,[Lastname]
  FROM [AdoNetData].[dbo].[Client]
  WHERE [Firstname] LIKE (@Name) OR [Lastname] LIKE (@Name);";
                var command = new SqlCommand(query, connection);
                fromSqlInputModel.Name = $"%{fromSqlInputModel.Name}%";
                command.Parameters.AddWithValue("@Name", "%" + fromSqlInputModel.Name + "%");
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
        }
        return clients;
    }
    
    #endregion Methods
}