﻿using Application.SqlQueries.Clients.Models;
using Microsoft.Data.SqlClient;

namespace Application.SqlQueries.Clients;

public class CreateClientQuery
{
    #region Fields
    #endregion Fields
    
    #region Constructor
    #endregion Constructor
    
    #region Method

    public async Task Execute(CreateClientInputModel inputModel)
    {
        try
        {
            using (var connection = new SqlConnection(inputModel.ConnectionString))
            {
                await connection.OpenAsync();
                string query = @"INSERT INTO Client (Id, Firstname, Lastname)
                         VALUES (NEWID(), @Firstname, @Lastname);";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Firstname", inputModel.FirstName);
                command.Parameters.AddWithValue("@Lastname", inputModel.LastName);
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    #endregion Method
}