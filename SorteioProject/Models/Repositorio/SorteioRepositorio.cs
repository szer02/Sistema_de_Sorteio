using System.Collections.Generic;
using System.Data;
using Dapper;
using Models.Infraestrutura;
using Models.Repositorio.Entidades;

namespace Models.Repositorio
{
    public class SorteioRepositorio : ISorteioRepositorio
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SorteioRepositorio(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Adicionar(Sorteio sorteio)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                string sql = "INSERT INTO Sorteio (DataSorteio, IdParticipante) VALUES (@DataSorteio, @IdParticipante)";
                db.Execute(sql, sorteio);
            }
        }
        public void DeletarPorParticipanteId(int idParticipante)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM Sorteio WHERE IdParticipante = @IdParticipante";
                db.Execute(sql, new { IdParticipante = idParticipante });
            }
        }



        public IEnumerable<Sorteio> ListarTodos()
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                return db.Query<Sorteio>("SELECT * FROM Sorteio");
            }
        }
    }
}
