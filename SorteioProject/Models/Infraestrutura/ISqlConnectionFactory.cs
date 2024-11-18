using System.Data;

namespace Models.Infraestrutura
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
