using Application.SqlQueries.Administrators;
using Application.SqlQueries.Clients;
using Application.SqlQueries.Database;
using Backoffice.Application.Commands.Administrators;
using Backoffice.Application.Commands.Clients;
using Backoffice.Application.Queries.Clients;
using Backoffice.SqlQueries.Administrators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GetAdminQuery>();
builder.Services.AddScoped<AuthenticateAdministratorCommand>();
builder.Services.AddScoped<GetClientsQuery>();
builder.Services.AddScoped<GetClientsFromDatabaseQuery>();
builder.Services.AddScoped<CreateClientCommand>();
builder.Services.AddScoped<CreateClientQuery>();


builder.Services.AddControllers();

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

app.MapControllers();
app.Run();