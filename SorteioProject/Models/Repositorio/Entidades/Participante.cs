namespace Models.Repositorio.Entidades
{
    public class Participante
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public bool? StatusPresenca { get; set; } // Nullable para registrar após o sorteio
    }
}
