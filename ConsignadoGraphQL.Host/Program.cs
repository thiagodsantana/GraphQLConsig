var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent) //Recomend�vel utilizar uma vida �til persistente para evitar reinicializa��es desnecess�rias.
                 .WithDataVolume();
// usado para armazenar permanentemente os dados de SQL Server
// fora do ciclo de vida de seu cont�iner.

var db = sql.AddDatabase("database");


// Registra a API GraphQL no Aspire
var consignadographqlapi = builder.AddProject<Projects.ConsignadoGraphQL>("consignadographqlapi")
                                  .WithReference(db)
                                  .WaitFor(db);


builder.AddProject<Projects.Consignado_API>("ConsignadoApi")
       .WithExternalHttpEndpoints()
       .WithReference(consignadographqlapi)
       .WaitFor(consignadographqlapi);

builder.Build().Run();
 