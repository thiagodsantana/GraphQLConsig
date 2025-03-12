namespace Consignado.API.Services
{
    public interface IGraphQLService
    {
        Task<string> GetBeneficiariosAsync();
    }
}
