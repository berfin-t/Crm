using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Crm.Blazor.Hubs;
using Crm.Orders.Events;

namespace Crm.Blazor.Handlers
{
    public class OrderCreatedEventHandler : IDistributedEventHandler<OrderCreatedEto>, ITransientDependency
    {
        private readonly IHubContext<CrmHub> _hubContext;

        public OrderCreatedEventHandler(IHubContext<CrmHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleEventAsync(OrderCreatedEto eventData)
        {
            await _hubContext.Clients.All.SendAsync("OrderCreated", eventData);
        }
    }
}
