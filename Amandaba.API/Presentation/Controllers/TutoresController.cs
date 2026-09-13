using Amandaba.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Amandaba.Presentation.Controllers
{
	[ApiController]
	[Route("api/tutores")]
	public class TutoresController : ControllerBase
	{
		private readonly ITutorUseCase _tutorUseCase;

		public TutoresController(ITutorUseCase tutorUseCase)
		{
			_tutorUseCase = tutorUseCase;
		}

		[HttpGet("by-email/{email}")]
		public IActionResult ObterPorEmail(string email)
		{
			var tutor = _tutorUseCase.ObterPorEmail(email);

			if (tutor is null)
				return NotFound(new { mensagem = "Tutor não encontrado." });

			return Ok(tutor);
		}
	}
}