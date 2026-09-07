using Amandaba.Application.Dtos.Especies;
using Amandaba.Application.Interfaces;
using Amandaba.Domain.Interfaces;

namespace Amandaba.Application.UseCases
{
    public class EspecieUseCase : IEspecieUseCase
    {
        private readonly IEspecieRepository _especieRepository;

        public EspecieUseCase(IEspecieRepository especieRepository)
        {
            _especieRepository = especieRepository;
        }

        public IEnumerable<EspecieResponseDto> ObterTodas()
        {
            return _especieRepository
                .ObterTodas()
                .Select(e => new EspecieResponseDto
                {
                    IdEspecie = e.IdEspecie,
                    Nome = e.Nome
                })
                .ToList();
        }
    }
}