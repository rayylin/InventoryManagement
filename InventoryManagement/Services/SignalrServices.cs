using InventoryManagement.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace InventoryManagement.Services
{
    public class SignalrServices
    {
        private readonly IHubContext<MyHub> _hubContext;

        // Use constructor injection to get IHubContext
        public SignalrServices(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void RefreshPage()
        {
            _hubContext.Clients.All.SendAsync("ReceiveRefresh").Wait();
        }

    }
}
