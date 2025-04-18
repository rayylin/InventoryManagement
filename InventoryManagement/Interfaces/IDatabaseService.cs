using System.Data;

namespace InventoryManagement.Interfaces
{
    public interface IDatabaseService
    {
        public void ExecuteStoredProcedure(string query = "");
        public DataTable connectDb(string query);
    }
}
