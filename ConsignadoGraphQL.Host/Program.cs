var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent) //Recomendável utilizar uma vida útil persistente para evitar reinicializações desnecessárias.
                 .WithDataVolume();
// usado para armazenar permanentemente os dados de SQL Server
// fora do ciclo de vida de seu contêiner.

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
 