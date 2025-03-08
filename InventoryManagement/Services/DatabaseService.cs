using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace InventoryManagement.Services
{
    public  class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteStoredProcedure()
        {
            using (var connection = new SqlConnection(_connectionString)) // Use injected connection string
            {
                using (var command = new SqlCommand("[dbo].[SummarizeCusPurchaseDaily]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }


}
