using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace DapperApp
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration = null;
        private readonly string _connectionString = null;

        public DapperDbContext(IConfiguration configuration) 
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration?.GetConnectionString("sqlserver");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
