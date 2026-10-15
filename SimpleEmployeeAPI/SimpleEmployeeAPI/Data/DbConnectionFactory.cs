using Npgsql;

namespace SimpleEmployeeAPI.Data
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public NpgsqlConnection createConnection()
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return new NpgsqlConnection(connectionString);
        } 
    }
}
