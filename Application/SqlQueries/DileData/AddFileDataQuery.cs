using System.Data.SqlClient;
using Application.SqlQueries.DileData.Models;

namespace Application.SqlQueries.DileData;

public class AddFileDataQuery
{
    #region Fields

    public async Task Execute(FileDataInputModel inputModel)
    {
        try
        {
            using (var connection = new SqlConnection(inputModel.ConnectionString))
            {
                await connection.OpenAsync();
                var query = @"INSERT INTO FileData (Id, FileContent, FileId)
values (NEWID(), @FileContent, @FileId);";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FileId", inputModel.FileId);
                command.Parameters.AddWithValue("@FileContent", inputModel.FileData);

                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    #endregion Fields
}