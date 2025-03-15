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

        public void ExecuteStoredProcedure(string query = "")
        {
            using (var connection = new SqlConnection(_connectionString)) // Use injected connection string
            {
                using (var command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.ExecuteNonQuery();
                    } 
                    catch (Exception ex) 
                    {                     
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }


}
