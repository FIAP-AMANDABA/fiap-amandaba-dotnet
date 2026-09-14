using Amandaba.Application.Dtos.Tutores;

namespace Amandaba.Application.Interfaces
{
    public interface ITutorUseCase
    {
        TutorResponseDto? ObterPorEmail(string email);
        TutorResponseDto? ObterPorIdUsuario(decimal idUsuario);
    }
}