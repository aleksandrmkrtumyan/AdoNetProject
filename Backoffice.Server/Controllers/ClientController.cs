using Backoffice.Application.Commands.Clients;
using Backoffice.Application.Queries.Clients;
using Backoffice.Application.Queries.Clients.Models;
using Backoffice.Server.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    #region Fields

    private readonly GetClientsQuery getClientsQuery;
    private readonly CreateClientCommand createClientCommand;
    private readonly string? connectionString;

    #endregion Fields
    
    #region Constructor

    public ClientController(
        GetClientsQuery getClientsQuery,
        CreateClientCommand createClientCommand,
        IConfiguration configuration)
    {
        this.getClientsQuery = getClientsQuery;
        this.createClientCommand = createClientCommand;
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
    public async Task CreateClient([FromBody] CreateClientInputModel inputModel)
    {
        await createClientCommand.Execute(new Application.Commands.Clients.Models.CreateClientInputModel
        {
            FirstName = inputModel.FirstName,
            LastName = inputModel.LastName,
            ConnectionString = connectionString
        });
    }
    
    #endregion Methods
}