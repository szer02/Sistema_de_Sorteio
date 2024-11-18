namespace Models.Repositorio.Entidades
{
    public class Sorteio
    {
        public int Id { get; set; }
        public DateTime DataSorteio { get; set; }
        public int IdParticipante { get; set; }
    }
}
