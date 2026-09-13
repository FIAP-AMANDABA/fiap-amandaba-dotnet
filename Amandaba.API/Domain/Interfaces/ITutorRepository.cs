using Amandaba.Domain.Entities;

namespace Amandaba.API.Domain.Interfaces
{
    public interface ITutorRepository
    {
        TutorEntity? ObterPorEmail(string email);
    }
}