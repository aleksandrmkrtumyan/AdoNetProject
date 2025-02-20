using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.AuthOption;
using Backoffice.Application.Commands.Administrators.Models;
using Application.SqlQueries.Administrators.Models;
using Backoffice.SqlQueries.Administrators;
using Microsoft.IdentityModel.Tokens;

namespace Backoffice.Application.Commands.Administrators;

public class AuthenticateAdministratorCommand
{
    #region Fields
    
    private readonly GetAdminQuery getAdminQuery;

    #endregion Fields
    
    #region Constructor

    public AuthenticateAdministratorCommand(
        GetAdminQuery getAdminQuery
        )
    {
        this.getAdminQuery = getAdminQuery;
    }
    
    #endregion Constructor
    
    #region Methods

    public async Task<AuthenticatedAdminModel> Execute(AuthenticateAdminInputModel inputModel)
    {
        if(inputModel == null)
            throw new ArgumentNullException(nameof(inputModel));
        if(inputModel.Username == null)
            throw new ArgumentNullException(nameof(inputModel.Username));
        if(inputModel.Password == null)
            throw new ArgumentNullException(nameof(inputModel.Password));

        var adminModel = await getAdminQuery.Execute(new GetAdminInputModel
        {
            ConnectionString = inputModel.ConnectionString,
            Username = inputModel.Username,
            Password = inputModel.Password
        });

        var clims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, adminModel.Username),
            new Claim(ClaimTypes.Name, adminModel.Name),
            new Claim(ClaimTypes.NameIdentifier, adminModel.Id.ToString())
        };

        var jwt = new JwtSecurityToken(
            notBefore: DateTime.Now,
            claims: clims,
            signingCredentials: new SigningCredentials(BackofficeAuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var authenticatedAdmin = new AuthenticatedAdminModel
        {
            AccessToken = encodedJwt,
            Name = adminModel.Name,
            Id = adminModel.Id,
            Username = adminModel.Username,
        };
        
        return authenticatedAdmin;

    }
    
    #endregion Methods
}