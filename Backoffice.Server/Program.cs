using Application.AuthOption;
using Application.SqlQueries.Administrators;
using Application.SqlQueries.Database;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backoffice.Application.Queries.Clients;
using Backoffice.SqlQueries.Administrators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = BackofficeAuthOptions.GetSymmetricSecurityKey()
    };
});
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    b.RegisterAssemblyTypes(typeof(GetAdminQuery).Assembly);
    b.RegisterAssemblyTypes(typeof(GetClientsQuery).Assembly);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var app = builder.Build();

var ensureDatabaseCreated = new EnsureDatabaseCreated(connectionString);
await ensureDatabaseCreated.Execute();
var ensureAdminTableCreated = new EnsureAdminTableCreated(connectionString);
await ensureAdminTableCreated.Execute();
var ensureAdminCreated = new EnsureAdminCreated(connectionString);
await ensureAdminCreated.Execute();
var ensureClientTableCreated = new EnsureClientTableCreated(connectionString);
await ensureClientTableCreated.Execute();
var ensurePhoneNumberTableCreated = new EnsurePhoneNumberTableCreated(connectionString);
await ensurePhoneNumberTableCreated.Execute();
var ensureFileDbTableCreated = new EnsureFileDbTableCreated(connectionString);
await ensureFileDbTableCreated.Execute();
var ensureFileDataTableCreated = new EnsureFileDataTableCreated(connectionString);
await ensureFileDataTableCreated.Execute();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();