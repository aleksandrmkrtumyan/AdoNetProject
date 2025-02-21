using Application.SqlQueries.Clients;
using Application.SqlQueries.Clients.Models;
using Backoffice.Application.Queries.Clients.Models;

namespace Backoffice.Application.Queries.Clients;

public class GetClientsQuery
{
    private readonly GetClientsFromDatabaseQuery getClientsFromDatabaseQuery;

    #region Constructor
    
    public GetClientsQuery(GetClientsFromDatabaseQuery getClientsFromDatabaseQuery)
    {
        this.getClientsFromDatabaseQuery = getClientsFromDatabaseQuery;
    }

    public async Task<List<GetClientResultModel>> Execute(GetClientsInputModel inputModel)
    {
        var clients = await getClientsFromDatabaseQuery.Execute(new GetClientInputModel
        {
            ConnectionString = inputModel.ConnectionString,
        });
       
        var result = clients.Select(c => new GetClientResultModel
        {
            Id = c.Id,
            Firstname = c.Firstname,
            Lastname = c.Lastname,
        }).ToList();
        
        return result;
    }
    #endregion Constructor
}