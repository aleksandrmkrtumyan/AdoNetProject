using System.Data.SqlClient;
using Application.SqlQueries.FileDbs.Models;

namespace Application.SqlQueries.FileDbs;

public class AddFileInfoSqlQuery
{
    #region Method

    public async Task Execute(FileInfoInputModel inputModel)
    {
        try
        {
            using (var connection = new SqlConnection(inputModel.ConnectionString))
            {
                connection.Open();
                var query = @"INSERT INTO FileDb (Id, FileName, ClientId)
VALUES (@Id, @FileName, @ClientId);";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", inputModel.FileId);
                command.Parameters.AddWithValue("@FileName", inputModel.FileName);
                command.Parameters.AddWithValue("@ClientId", inputModel.ClientId);
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    #endregion Method
}