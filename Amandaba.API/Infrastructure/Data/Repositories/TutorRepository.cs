using Amandaba.API.Domain.Interfaces;
using Amandaba.Domain.Entities;

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
			var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
			if (usuario is null)
				return null;

			return ObterOuCriarTutor(usuario);
		}

		public TutorEntity? ObterPorIdUsuario(decimal idUsuario)
		{
			var usuario = _context.Usuarios.Find(idUsuario);
			if (usuario is null)
				return null;

			return ObterOuCriarTutor(usuario);
		}

		private TutorEntity ObterOuCriarTutor(UsuarioEntity usuario)
		{
			var tutor = _context.Tutores.FirstOrDefault(t => t.IdUsuario == usuario.IdUsuario);

			if (tutor is null)
			{
				tutor = new TutorEntity { IdUsuario = usuario.IdUsuario };
				_context.Tutores.Add(tutor);
				_context.SaveChanges();
			}

			tutor.Usuario = usuario;
			return tutor;
		}
	}
}