namespace PFinal_v2.Models.ViewModels
{
    public class DiaFormViewModel
    {
        public List<Dia>? Lancamentos { get; set; }
        public DateTime DataAtual { get; set; }
        public List<Wbs> ListaWbs { get; set; }
    }
}