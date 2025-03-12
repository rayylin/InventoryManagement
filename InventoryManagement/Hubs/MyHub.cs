using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InventoryManagement.Hubs
{
    public class MyHub: Hub
    {
        public async Task RefreshPage()
        {
            await Clients.All.SendAsync("ReceiveRefresh");
        }
    }
}
