using Consignado.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consignado.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariosController(IGraphQLService graphQLService) : ControllerBase
    {
        private readonly IGraphQLService _graphQLService = graphQLService;

        [HttpGet]
        public async Task<IActionResult> GetBeneficiarios()
        {
            try
            {
                var result = await _graphQLService.GetBeneficiariosAsync();
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "Erro ao acessar a API GraphQL", statusCode: 500);
            }
        }
    }
}