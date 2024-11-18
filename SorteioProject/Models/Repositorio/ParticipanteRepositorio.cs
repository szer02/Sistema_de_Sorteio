using System.Collections.Generic;
using System.Data;
using Dapper;
using Models.Infraestrutura;
using Models.Repositorio.Entidades;

namespace Models.Repositorio
{
    public class ParticipanteRepositorio : IParticipanteRepositorio
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public ParticipanteRepositorio(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Adicionar(Participante participante)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                string sql = "INSERT INTO Participante (Nome) VALUES (@Nome)";
                db.Execute(sql, new { Nome = participante.Nome });
            }
        }

        public IEnumerable<Participante> ListarTodos()
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                return db.Query<Participante>("SELECT * FROM Participante");
            }
        }

        public Participante ObterPorId(int id)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                return db.QuerySingleOrDefault<Participante>("SELECT * FROM Participante WHERE Id = @Id", new { Id = id });
            }
        }

        public void Atualizar(Participante participante)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                string sql = "UPDATE Participante SET Nome = @Nome, StatusPresenca = @StatusPresenca WHERE Id = @Id";
                db.Execute(sql, participante);
            }
        }

        public void Deletar(int id)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                string sql = "DELETE FROM Participante WHERE Id = @Id";
                db.Execute(sql, new { Id = id });
            }
        }
    }
}
