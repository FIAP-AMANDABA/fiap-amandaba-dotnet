using Amandaba.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Amandaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/especies")]
    public class EspeciesController : ControllerBase
    {
        private readonly IEspecieUseCase _especieUseCase;

        public EspeciesController(IEspecieUseCase especieUseCase)
        {
            _especieUseCase = especieUseCase;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar espécies",
            Description = "Retorna todas as espécies disponíveis para cadastro de pets."
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Espécies retornadas com sucesso."
        )]
        public IActionResult ObterTodas()
        {
            try
            {
                return Ok(_especieUseCase.ObterTodas());
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}