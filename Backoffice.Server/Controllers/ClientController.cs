using Backoffice.Application.Commands.Clients;
using Backoffice.Application.Commands.Clients.Models;
using Backoffice.Application.Queries.Clients;
using Backoffice.Application.Queries.Clients.Models;
using Backoffice.Server.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using CreateClientInputModel = Backoffice.Application.Commands.Clients.Models.CreateClientInputModel;

namespace Backoffice.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    #region Fields

    private readonly GetClientsQuery getClientsQuery;
    private readonly CreateClientCommand createClientCommand;
    private readonly UpdateClientCommand updateClientCommand;
    private readonly string? connectionString;

    #endregion Fields
    
    #region Constructor

    public ClientController(
        GetClientsQuery getClientsQuery,
        CreateClientCommand createClientCommand,
        UpdateClientCommand updateClientCommand,
        IConfiguration configuration)
    {
        this.getClientsQuery = getClientsQuery;
        this.createClientCommand = createClientCommand;
        this.updateClientCommand = updateClientCommand;
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
    public async Task<IActionResult> CreateClient([FromBody] CreateClientInputModel inputModel)
    {
        await createClientCommand.Execute(new Application.Commands.Clients.Models.CreateClientInputModel
        {
            FirstName = inputModel.FirstName,
            LastName = inputModel.LastName,
            ConnectionString = connectionString
        });
        return Ok();
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
    #endregion Methods
}