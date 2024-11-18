using System.Collections.Generic;
using Models.Repositorio.Entidades;

namespace Models.Repositorio
{
    public interface IParticipanteRepositorio
    {
        void Adicionar(Participante participante);
        IEnumerable<Participante> ListarTodos();
        Participante ObterPorId(int id);
        void Atualizar(Participante participante);
        void Deletar(int id);
    }
}
