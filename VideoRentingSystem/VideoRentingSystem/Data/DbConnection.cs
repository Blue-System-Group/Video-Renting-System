using System.Data.SqlClient;

namespace VideoRentingSystem.Data
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection()
        {
            _connectionString = "Server=NIMESH-LAPTOP\\SQLEXPRESS;Database=VideoRentingDB;Trusted_Connection=True;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
