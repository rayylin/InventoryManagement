using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace InventoryManagement.Services
{
    public  class DatabaseService
    {
        public  readonly string _connectionString;

        public  DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public  void ExecuteStoredProcedure()
        {
            var connectionString = "Server=RAY\\SQLEXPRESS;Database=InvMgnt;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";
            using (var connection = new SqlConnection(connectionString))
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
