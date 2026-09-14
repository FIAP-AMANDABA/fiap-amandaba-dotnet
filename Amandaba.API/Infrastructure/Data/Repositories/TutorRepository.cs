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
			var tutor = _context.Tutores
				.Include(t => t.Usuario)
				.FirstOrDefault(t => t.Usuario.Email == email);

			if (tutor is not null)
				return tutor;

			var usuario = _context.Usuarios
				.FirstOrDefault(u => u.Email == email);

			if (usuario is null)
				return null;

			var novoTutor = new TutorEntity { IdUsuario = usuario.IdUsuario };
			_context.Tutores.Add(novoTutor);
			_context.SaveChanges();

			novoTutor.Usuario = usuario;
			return novoTutor;
		}
	}
}