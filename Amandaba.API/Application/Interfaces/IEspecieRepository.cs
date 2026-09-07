using Amandaba.Domain.Entities;

namespace Amandaba.Domain.Interfaces
{
    public interface IEspecieRepository
    {
        IEnumerable<EspecieEntity> ObterTodas();
    }
}