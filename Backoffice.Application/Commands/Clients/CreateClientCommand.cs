using Application.SqlQueries.Clients;
using Application.SqlQueries.Clients.Models;
using Backoffice.Application.Commands.Clients.Models;
using CreateClientInputModel = Application.SqlQueries.Clients.Models.CreateClientInputModel;

namespace Backoffice.Application.Commands.Clients;

public class CreateClientCommand
{
    #region Fields
    
    private readonly CreateClientQuery createClientQuery;

    #endregion Fields
    
    #region Constructor

    public CreateClientCommand(CreateClientQuery createClientQuery
        )
    {
        this.createClientQuery = createClientQuery;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task Execute(Models.CreateClientInputModel inputModel)
    {
        await createClientQuery.Execute(new CreateClientInputModel
        {
            ConnectionString = inputModel.ConnectionString,
            FirstName = inputModel.FirstName,
            LastName = inputModel.LastName,
        });
    }
    
    #endregion Method
}