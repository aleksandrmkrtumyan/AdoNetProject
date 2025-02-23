using Application.SqlQueries.Clients;
using Application.SqlQueries.Clients.Models;
using Backoffice.Application.Commands.Clients.Models;

namespace Backoffice.Application.Commands.Clients;

public class DeleteClientCommand
{
    #region Fields
    
    private readonly DeleteClientQuery deleteClientQuery;

    #endregion Fields
    
    #region Constructor

    public DeleteClientCommand(
        DeleteClientQuery deleteClientQuery 
        )
    {
        this.deleteClientQuery = deleteClientQuery;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task<int> Execute(DeleteClientInputModel inputModel)
    {
        var deletedRowsCount = await deleteClientQuery.Execute(new DeleteClientInDatabaseInputModel
        {
            Id = inputModel.Id,
            ConnectionString = inputModel.ConnectionString
        });
        return deletedRowsCount;
    }
    
    #endregion Method
}