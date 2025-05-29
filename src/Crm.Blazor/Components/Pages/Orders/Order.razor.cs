using Crm.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Blazor.Components.Dialogs.Orders;
using Crm.Customers;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;

namespace Crm.Blazor.Components.Pages.Orders
{
    public partial class Order
    {
        [Parameter]  public EventCallback OnOrderCreated { get; set; }
        private OrderDto OrderDto { get; set; } 
        private List<CustomerDto> CustomerList { get; set; } = new();
        private List<ProjectDto> ProjectList { get; set; } = new();
        public List<OrderDto> orderList;
        private OrderDto selectedOrder;
        public List<OrderDto> OrderList { get; set; } = new();
        private OrderCreateModal orderCreateModal;
        private OrderEditModal orderEditModal;

        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);

        //private async Task SaveOrder()
        //{
        //    await OrderAppService.CreateAsync(newOrder);
        //    await OnOrderCreated.InvokeAsync();
        //}

        protected override async Task OnInitializedAsync()
        {
            orderList = await OrderAppService.GetListAllAsync();
            CustomerList = await CustomerAppService.GetListAllAsync();
            ProjectList = await ProjectAppService.GetListAllAsync();
            await base.OnInitializedAsync();
        }       

        private async Task ShowCreateModal()
        {
            if (orderCreateModal != null)
            {
                await orderCreateModal.ShowModal(EventCallback);
            }
        }

        private async Task ShowEditModal(OrderDto order)
        {
            if (orderEditModal != null)
            {
                await orderEditModal.ShowModal(order);
            }
        }
        //private async Task DeleteOrder(Guid id)
        //{
        //    await OrderAppService.DeleteAsync(id);
        //    await ReloadOrders();
        //}
        private async Task ReloadOrders()
        {
            orderList = await OrderAppService.GetListAllAsync();
            StateHasChanged();
        }
    }
}
