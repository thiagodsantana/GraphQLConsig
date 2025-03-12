using ConsignadoGraphQL.GraphQL;
using System.Text.Json;
using System.Text.Json.Serialization;

[JsonSerializable(typeof(GraphQLQuery))]
[JsonSerializable(typeof(JsonElement))]
internal partial class ConsignadoJsonContext : JsonSerializerContext { }
