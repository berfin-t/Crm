using Crm.Orders;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Crm.Blazor.Hubs
{
    public class CrmHub : AbpHub
    {
        public async Task SendOrderCreated(OrderDto order)
        {
            await Clients.All.SendAsync("OrderCreated", order);
        }
    }
}
