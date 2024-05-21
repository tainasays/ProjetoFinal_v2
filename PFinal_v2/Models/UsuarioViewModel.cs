using Microsoft.AspNetCore.Mvc.Rendering;

namespace PFinal_v2.Models
{
    public class UsuarioViewModel
    {
        public List<Usuario>? Usuarios { get; set; }
        public SelectList? DepartamentoId { get; set; }
        public string? Departamento { get; set; }
        public string? SearchString { get; set; }
        
    }
}
