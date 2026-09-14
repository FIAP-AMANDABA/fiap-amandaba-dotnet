using Amandaba.API.Domain.Interfaces;
using Amandaba.Application.Dtos.Tutores;
using Amandaba.Application.Interfaces;
using Amandaba.Domain.Entities;

namespace Amandaba.Application.UseCases
{
    public class TutorUseCase : ITutorUseCase
    {
        private readonly ITutorRepository _tutorRepository;

        public TutorUseCase(ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        public TutorResponseDto? ObterPorEmail(string email)
        {
            return Mapear(_tutorRepository.ObterPorEmail(email));
        }

        public TutorResponseDto? ObterPorIdUsuario(decimal idUsuario)
        {
            return Mapear(_tutorRepository.ObterPorIdUsuario(idUsuario));
        }

        private static TutorResponseDto? Mapear(TutorEntity? tutor)
        {
            if (tutor is null) return null;

            return new TutorResponseDto
            {
                IdTutor = tutor.IdTutor,
                IdUsuario = tutor.IdUsuario,
                Nome = tutor.Usuario.Nome,
                Cpf = tutor.Usuario.Cpf,
                Email = tutor.Usuario.Email,
                Telefone = tutor.Usuario.Telefone,
                DataNascimento = tutor.Usuario.DataNascimento
            };
        }
    }
}