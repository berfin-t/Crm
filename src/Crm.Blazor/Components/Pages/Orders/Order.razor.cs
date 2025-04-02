using Crm.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Blazor.Components.Dialogs.Orders;
using Crm.Customers;
using Crm.Projects;

namespace Crm.Blazor.Components.Pages.Orders
{
    public partial class Order
    {
        private OrderDto OrderDto { get; set; } 
        private List<CustomerDto> CustomerList { get; set; } = new();
        private List<ProjectDto> ProjectList { get; set; } = new();
        public List<OrderDto> orderList;
        private OrderDto selectedOrder;
        public List<OrderDto> OrderList { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            orderList = await OrderAppService.GetListAllAsync();
            CustomerList = await CustomerAppService.GetListAllAsync();
            ProjectList = await ProjectAppService.GetListAllAsync();
            await base.OnInitializedAsync();
        }

        private OrderCreateModal orderCreateModal;
        private OrderEditModal orderEditModal;

        private async Task ShowCreateModal()
        {
            if (orderCreateModal != null)
            {
                await orderCreateModal.ShowModal();
            }
        }

        private async Task ShowEditModal(OrderDto order)
        {
            if (orderEditModal != null)
            {
                await orderEditModal.ShowModal(order);
            }
        }
    }
}
