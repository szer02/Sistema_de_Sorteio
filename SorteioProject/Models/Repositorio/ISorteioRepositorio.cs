using System.Collections.Generic;
using Models.Repositorio.Entidades;

namespace Models.Repositorio
{
    public interface ISorteioRepositorio
    {
        void Adicionar(Sorteio sorteio);
        IEnumerable<Sorteio> ListarTodos();
        void DeletarPorParticipanteId(int idParticipante);

    }
}
