﻿namespace Backoffice.Application.Queries.Clients.Models;

public class GetClientByNameInputModel
{
    public string Name { get; set; }
    public string ConnectionString { get; set; }
}