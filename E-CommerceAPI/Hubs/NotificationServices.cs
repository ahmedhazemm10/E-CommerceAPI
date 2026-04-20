using E_CommerceAPI.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace E_CommerceAPI.Hubs
{
    public class NotificationServices : INotificationServices
    {
        private readonly IHubContext<NotificationHub> hub;

        public NotificationServices(IHubContext<NotificationHub> hub)
        {
            this.hub = hub;
        }

        public async Task AddProductNotification(AddProductNotificationDTO productNotificationDTO)
        {
            await hub.Clients.All.SendAsync("AddProduct", "New product was Added", productNotificationDTO);
        }
    }

    public interface INotificationServices
    {
        public Task AddProductNotification(AddProductNotificationDTO productNotificationDTO);
    }
}
