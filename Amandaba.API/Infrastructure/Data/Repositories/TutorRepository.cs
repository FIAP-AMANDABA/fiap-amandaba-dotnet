using Amandaba.API.Domain.Interfaces;
using Amandaba.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Amandaba.Infrastructure.Data.Repositories
{
	public class TutorRepository : ITutorRepository
	{
		private readonly ApplicationContext _context;

		public TutorRepository(ApplicationContext context)
		{
			_context = context;
		}

		public TutorEntity? ObterPorEmail(string email)
		{
			return _context.Tutores
				.AsNoTracking()
				.Include(t => t.Usuario)
				.FirstOrDefault(t => t.Usuario.Email == email);
		}
	}
}