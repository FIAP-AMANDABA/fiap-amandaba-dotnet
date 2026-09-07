using Amandaba.Application.Dtos.Pets;
using Amandaba.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Amandaba.Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class PetsController : ControllerBase
    {
        private readonly IPetUseCase _petUseCase;

        public PetsController(IPetUseCase petUseCase)
        {
            _petUseCase = petUseCase;
        }

        [HttpGet("tutores/{idTutor}/pets")]
        [SwaggerOperation(
            Summary = "Listar pets de um tutor",
            Description = "Retorna todos os pets, ativos e inativos, associados ao tutor."
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Pets retornados com sucesso."
        )]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            "Tutor não encontrado."
        )]
        public IActionResult ObterPorTutor(decimal idTutor)
        {
            try
            {
                var pets = _petUseCase.ObterPorTutor(idTutor);

                return Ok(pets);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("tutores/{idTutor}/pets")]
        [SwaggerOperation(
            Summary = "Cadastrar pet",
            Description = "Cadastra um novo pet associado ao tutor informado."
        )]
        [SwaggerResponse(
            StatusCodes.Status201Created,
            "Pet cadastrado com sucesso."
        )]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Dados inválidos."
        )]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            "Tutor não encontrado."
        )]
        public IActionResult Cadastrar(
            decimal idTutor,
            [FromBody] PetRequestDto dto)
        {
            try
            {
                var pet = _petUseCase.Cadastrar(idTutor, dto);

                return CreatedAtAction(
                    nameof(ObterPorId),
                    new { petId = pet.IdPet },
                    pet
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("pets/{petId}")]
        [SwaggerOperation(
            Summary = "Consultar pet",
            Description = "Retorna os dados completos de um pet."
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Pet retornado com sucesso."
        )]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            "Pet não encontrado."
        )]
        public IActionResult ObterPorId(decimal petId)
        {
            try
            {
                var pet = _petUseCase.ObterPorId(petId);

                if (pet is null)
                    return NotFound(new { mensagem = "Pet não encontrado." });

                return Ok(pet);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("pets/{petId}")]
        [SwaggerOperation(
            Summary = "Editar pet",
            Description = "Atualiza os dados cadastrais de um pet."
        )]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            "Pet atualizado com sucesso."
        )]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Dados inválidos."
        )]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            "Pet não encontrado."
        )]
        public IActionResult Atualizar(
            decimal petId,
            [FromBody] PetRequestDto dto)
        {
            try
            {
                var atualizado = _petUseCase.Atualizar(petId, dto);

                if (!atualizado)
                    return NotFound(new { mensagem = "Pet não encontrado." });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPatch("pets/{petId}/status")]
        [SwaggerOperation(
            Summary = "Alterar status do pet",
            Description = "Ativa ou desativa um pet sem removê-lo fisicamente."
        )]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            "Status atualizado com sucesso."
        )]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Status inválido."
        )]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            "Pet não encontrado."
        )]
        public IActionResult AtualizarStatus(
            decimal petId,
            [FromBody] PetStatusRequestDto dto)
        {
            try
            {
                var atualizado = _petUseCase.AtualizarStatus(petId, dto);

                if (!atualizado)
                    return NotFound(new { mensagem = "Pet não encontrado." });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}