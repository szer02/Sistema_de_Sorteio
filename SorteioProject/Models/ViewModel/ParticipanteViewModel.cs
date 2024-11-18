namespace Models.ViewModel
{
    public class ParticipanteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool? StatusPresenca { get; set; } // Adiciona esta linha para o status de presença
    }
}
