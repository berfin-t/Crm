using Crm.Customers.Events;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using System.Threading.Tasks;
using Crm.Blazor.Hubs;

namespace Crm.Blazor.EventHandlers
{
    public class CustomerCreatedEventHandler : IDistributedEventHandler<CustomerCreatedEto>, ITransientDependency
    {
        private readonly IHubContext<CrmHub> _hubContext;

        public CustomerCreatedEventHandler(IHubContext<CrmHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleEventAsync(CustomerCreatedEto eventData)
        {
            await _hubContext.Clients.All.SendAsync("CustomerCreated", eventData);
        }
    }
}
