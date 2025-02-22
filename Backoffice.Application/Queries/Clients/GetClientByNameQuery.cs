using Application.SqlQueries.Clients;
using Application.SqlQueries.Clients.Models;
using Backoffice.Application.Queries.Clients.Models;

namespace Backoffice.Application.Queries.Clients;

public class GetClientByNameQuery
{
    #region Fields
    
    private readonly GetClientByNameFromSqlQuery getClientByNameFromSqlQuery;

    #endregion Fields
    
    #region Constructor

    public GetClientByNameQuery(GetClientByNameFromSqlQuery getClientByNameFromSqlQuery)
    {
        this.getClientByNameFromSqlQuery = getClientByNameFromSqlQuery;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task<List<GetClientResultModel>> Execute(GetClientByNameInputModel inputModel)
    {
        var clients = await getClientByNameFromSqlQuery.Execute(new ClientByNameFromSqlInputModel
        {
            Name = inputModel.Name,
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
    
    #endregion Method
}