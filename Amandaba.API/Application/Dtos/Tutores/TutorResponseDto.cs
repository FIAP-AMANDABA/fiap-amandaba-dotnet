namespace Amandaba.Application.Dtos.Tutores
{
    public class TutorResponseDto
    {
        public decimal IdTutor { get; set; }
        public decimal IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}