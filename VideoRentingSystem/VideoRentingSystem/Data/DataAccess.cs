using System;
using System.Data.SqlClient;
using System.Data;

namespace VideoRentingSystem.Data
{
    public class DataAccess
    {
        private readonly DbConnection _db;

        public DataAccess()
        {
            _db = new DbConnection();
        }

        public void ExecuteQuery(string query, Action<SqlCommand> parameterAction = null)
        {
            using (var connection = _db.GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    parameterAction?.Invoke(command);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetData(string query)
        {
            using (var connection = _db.GetConnection())
            {
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        internal DataTable GetData(string query, Action<object> value)
        {
            throw new NotImplementedException();
        }
    }
}
