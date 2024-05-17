namespace PFinal_v2.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int DepartamentoId { get; set; }
        public DateTime DataContratacao { get; set; }
        public bool IsAdmin { get; set; }
        public string? Senha { get; set; }

    }
}
