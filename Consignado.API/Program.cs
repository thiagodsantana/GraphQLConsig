using Consignado.API.Services;
using ConsignadoGraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddProblemDetails();
builder.AddSqlServerDbContext<ConsignadoContext>("database");

builder.Services.AddScoped<IGraphQLService, GraphQLService>();

// Adiciona controllers na API
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
