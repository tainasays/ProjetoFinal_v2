using Microsoft.AspNetCore.Mvc.Rendering;

namespace PFinal_v2.Models.ViewModels
{
    public class DepartamentoFormViewModel
    {
        public List<Departamento>? Departamentos { get; set; }
        public SelectList? Nome { get; set; }
        public string? TipoDepartamentos { get; set; }
        public string? SearchString { get; set; }
    }
}
