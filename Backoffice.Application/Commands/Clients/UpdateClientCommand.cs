using Application.SqlQueries.Clients;
using Application.SqlQueries.Clients.Models;
using Backoffice.Application.Commands.Clients.Models;

namespace Backoffice.Application.Commands.Clients;

public class UpdateClientCommand
{
    #region Fields
    
    private readonly UpdateClientQuery updateClientQuery;

    #endregion Fields
    
    #region Constructor

    public UpdateClientCommand(UpdateClientQuery updateClientQuery)
    {
        this.updateClientQuery = updateClientQuery;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task Execute(UpdateClientApplicationInputModel applicationInput)
    {
        await updateClientQuery.Execute(new UpdateClientInputModel1
        {
            ConnectionString = applicationInput.ConnectionString,
            FirstName = applicationInput.FirstName,
            LastName = applicationInput.LastName,
            Id = applicationInput.Id,
        });

    }
    
    #endregion Method
}