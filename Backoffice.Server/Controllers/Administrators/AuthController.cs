using Backoffice.Application.Commands.Administrators;
using Backoffice.Application.Commands.Administrators.Models;
using Backoffice.Server.Controllers.Administrators.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Server.Controllers.Administrators;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Fields
    
    private readonly AuthenticateAdministratorCommand authenticateAdministratorCommand;
    private readonly string? connectionString;

    #endregion Fields
    
    #region Constructor

    public AuthController(
        AuthenticateAdministratorCommand authenticateAdministratorCommand,
        IConfiguration configuration
        )
    {
        this.authenticateAdministratorCommand = authenticateAdministratorCommand;
        this.connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    #endregion Constuctor
    
    #region Method

    [HttpGet("/test")]
    public async Task<IActionResult> Test()
    {
        return Ok();
    }
    
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody]AuthAdminInputModel authAdminInputModel)
    {
        if (authAdminInputModel == null || string.IsNullOrWhiteSpace(authAdminInputModel.Username) ||
            string.IsNullOrWhiteSpace(authAdminInputModel.Password))
        {
            return BadRequest("Username and/or password are required");
        }

        var authenticatedAdmin = await authenticateAdministratorCommand.Execute(new AuthenticateAdminInputModel
        {
            ConnectionString = connectionString,
            Username = authAdminInputModel.Username,
            Password = authAdminInputModel.Password
        });
        
        return Ok(new { authenticatedAdmin });
    }
    
    #endregion Method
}