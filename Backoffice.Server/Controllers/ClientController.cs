using Backoffice.Application.Commands.Clients;
using Backoffice.Application.Commands.Clients.Models;
using Backoffice.Application.Queries.Clients;
using Backoffice.Application.Queries.Clients.Models;
using Backoffice.Server.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
// using CreateClientInputModel = Backoffice.Application.Commands.Clients.Models.CreateClientInputModel;

namespace Backoffice.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    #region Fields

    private readonly GetClientsQuery getClientsQuery;
    private readonly CreateClientCommand createClientCommand;
    private readonly UpdateClientCommand updateClientCommand;
    private readonly GetClientByNameQuery getClientByNameQuery;
    private readonly DeleteClientCommand deleteClientCommand;
    private readonly string? connectionString;

    #endregion Fields
    
    #region Constructor

    public ClientController(
        GetClientsQuery getClientsQuery,
        CreateClientCommand createClientCommand,
        UpdateClientCommand updateClientCommand,
        GetClientByNameQuery getClientByNameQuery,
        DeleteClientCommand deleteClientCommand,
        IConfiguration configuration)
    {
        this.getClientsQuery = getClientsQuery;
        this.createClientCommand = createClientCommand;
        this.updateClientCommand = updateClientCommand;
        this.getClientByNameQuery = getClientByNameQuery;
        this.deleteClientCommand = deleteClientCommand;
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    #endregion Constructor
    
    #region Methods

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var clients = await getClientsQuery.Execute(new GetClientsInputModel
        {
            ConnectionString = connectionString
        });
        return Ok(clients);
    }
    
    [HttpPost("CreateClient")]
    public async Task<IActionResult> CreateClient([FromBody] Models.CreateClientInputModel inputModel)
    {
        await createClientCommand.Execute(new Backoffice.Application.Commands.Clients.Models.CreateClientInputModel
        {
            FirstName = inputModel.FirstName,
            LastName = inputModel.LastName,
            ConnectionString = connectionString
        });
        return Ok();
    }

    [HttpGet("GetClient/{name}")]
    public async Task<IActionResult> GetClientByName(string name)
    {
        var clients = await getClientByNameQuery.Execute(new GetClientByNameInputModel
        {
            Name = name,
            ConnectionString = connectionString
        });
        return Ok(clients);
    }
    
    [HttpPost("UpdateClient")]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClientInputModel inputModel)
    {
        await updateClientCommand.Execute(new UpdateClientApplicationInputModel
        {
            Id = inputModel.Id,
            FirstName = inputModel.FirstName,
            LastName = inputModel.LastName,
            ConnectionString = connectionString
        });
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        int deletedRowsCount = await deleteClientCommand.Execute(new DeleteClientInputModel
        {
            Id = id,
            ConnectionString = connectionString
        });
        if (deletedRowsCount > 0)
            return Ok("Client deleted successfully");
        else
        {
            return Ok("No rows deleted");
        }
    }

    #endregion Methods
}