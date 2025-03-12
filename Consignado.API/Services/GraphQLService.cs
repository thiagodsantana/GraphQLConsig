using ConsignadoGraphQL.GraphQL;
using System.Text;
using System.Text.Json;

namespace Consignado.API.Services
{
    public class GraphQLService : IGraphQLService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GraphQLService(IHttpClientFactory httpClientFactory)
        {
            #region Adicionando configuração ao HTTP Client
            _httpClient = httpClientFactory.CreateClient("consignadographql");
            _httpClient.BaseAddress = new Uri("https+http://consignadographqlapi");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                TypeInfoResolver = ConsignadoJsonContext.Default
            };
            #endregion
        }

        public async Task<string> GetBeneficiariosAsync()
        {
            var query = new GraphQLQuery
            {
                Query = @"
                {
                    beneficiarios {
                        id
                        nome
                        cpf
                        beneficios {
                            id
                            tipo
                            valor
                            contratos {
                                id
                                valorTotal
                                parcelas
                                taxaJuros
                            }
                        }
                    }
                }"
            };

            var content = new StringContent(JsonSerializer.Serialize(query, _jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("graphql", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao acessar a API GraphQL: {errorContent}");
            }

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}