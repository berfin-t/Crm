using Crm.Customers;
using Crm.Orders;
using Crm.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Crm.Blazor.Hubs
{
    public class CrmHub : AbpHub
    {
        public async System.Threading.Tasks.Task SendOrderCreated(OrderDto order)
        {
            await Clients.All.SendAsync("OrderCreated", order);
        }

        public async System.Threading.Tasks.Task SendCustomerCreated(CustomerDto customer)
        {
            await Clients.All.SendAsync("CustomerCreated", customer);
        }

        public async System.Threading.Tasks.Task SendTaskCreated(TaskDto task)
        {
            await Clients.All.SendAsync("TaskCreated", task);
        }
    }
}
