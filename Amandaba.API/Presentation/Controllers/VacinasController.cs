using Amandaba.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Amandaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/vacinas")]
    public class VacinasController : ControllerBase
    {
        private readonly IVacinaUseCase _vacinaUseCase;

        public VacinasController(IVacinaUseCase vacinaUseCase)
        {
            _vacinaUseCase = vacinaUseCase;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar catálogo de vacinas")]
        public IActionResult ObterCatalogo()
        {
            try
            {
                return Ok(_vacinaUseCase.ObterCatalogo());
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{vacinaId}")]
        [SwaggerOperation(Summary = "Consultar vacina do catálogo")]
        public IActionResult ObterPorId(decimal vacinaId)
        {
            try
            {
                var vacina = _vacinaUseCase.ObterVacinaPorId(vacinaId);

                if (vacina is null)
                    return NotFound(new { mensagem = "Vacina não encontrada." });

                return Ok(vacina);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}