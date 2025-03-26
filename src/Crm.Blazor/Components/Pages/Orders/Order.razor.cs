using Crm.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Blazor.Components.Dialogs.Orders;

namespace Crm.Blazor.Components.Pages.Orders
{
    public partial class Order
    {
        private OrderDto OrderDto { get; set; }
        private List<OrderDto> orderList;
        private OrderDto selectedOrder;

        protected override async Task OnInitializedAsync()
        {
            orderList = await OrderAppService.GetListAllAsync();
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
