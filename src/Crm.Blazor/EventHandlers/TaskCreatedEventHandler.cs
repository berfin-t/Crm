using Crm.Blazor.Hubs;
using Crm.Tasks.Events;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Crm.Blazor.EventHandlers
{
    public class TaskCreatedEventHandler: IDistributedEventHandler<TaskCreatedEto>, ITransientDependency
    {
        private readonly IHubContext<CrmHub> _hubContext;

        public TaskCreatedEventHandler(IHubContext<CrmHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleEventAsync(TaskCreatedEto eventData)
        {
            await _hubContext.Clients.All.SendAsync("TaskCreated", eventData);
        }
    }
}
