using Application.SqlQueries.Administrators;
using Application.SqlQueries.Database;
using Backoffice.Application.Commands.Administrators;
using Backoffice.SqlQueries.Administrators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<GetAdminQuery>();
builder.Services.AddScoped<AuthenticateAdministratorCommand>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

var ensureDatabaseCreated = new EnsureDatabaseCreated(connectionString);
await ensureDatabaseCreated.Execute();
var ensureAdminTableCreated = new EnsureAdminTableCreated(connectionString);
await ensureAdminTableCreated.Execute();
var ensureAdminCreated = new EnsureAdminCreated(connectionString);
await ensureAdminCreated.Execute();


app.MapControllers();
app.Run();