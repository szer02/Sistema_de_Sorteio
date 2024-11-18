
namespace Models.ViewModel
{
    public class SorteioViewModel
    {
        public int IdParticipante { get; set; }
        public string NomeParticipante { get; set; }
        public DateTime DataSorteio { get; set; }
        public bool? StatusPresenca { get; set; } // Adicione esta linha
    }

}
