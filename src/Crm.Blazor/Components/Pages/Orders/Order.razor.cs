using Crm.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Blazor.Components.Dialogs.Orders;
using Crm.Customers;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using Blazorise;

namespace Crm.Blazor.Components.Pages.Orders
{
    public partial class Order
    {
        [Inject] public NavigationManager? NavigationManager { get; set; }
        private OrderDto? OrderDto { get; set; }
        private List<CustomerDto> CustomerList = new List<CustomerDto>();
        private List<ProjectDto>? ProjectList = new List<ProjectDto>();
        public List<OrderDto>? OrderList { get; set; } = new List<OrderDto>();
        private OrderDto? selectedOrder;
        private OrderCreateModal? orderCreateModal;
        private OrderEditModal? orderEditModal;
        private bool isDeleteModalVisible = false;
        private bool isOrderModalVisible = false;
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        private HubConnection? hubConnection;

        private bool showAlert = false;
        private string latestOrderCode = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            OrderList = await OrderAppService.GetListAllAsync();
            CustomerList = await CustomerAppService.GetListAllAsync();
            ProjectList = await ProjectAppService.GetListAllAsync();
            await base.OnInitializedAsync();            

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"{NavigationManager!.BaseUri}crmhub")
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<OrderDto>("OrderCreated", async (order) =>
            {
                OrderList?.Add(order);
                latestOrderCode = order.OrderCode ?? "Yeni Sipariş";
                showAlert = true;
                await InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();
        }
        private void OnAlertDismissed()
        {
            showAlert = false;
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
                await orderEditModal.ShowModal(order, EventCallback);
            }
        }

        #region Delete 
        private void OnDeleteClicked(OrderDto order)
        {
            selectedOrder = order;
            isDeleteModalVisible = true;
        }
        private async Task ConfirmDelete()
        {
            if (selectedOrder != null && selectedOrder.Id != Guid.Empty)
            {
                await OrderAppService.DeleteAsync(selectedOrder.Id);
                isDeleteModalVisible = false;
                await OnInitializedAsync();
            }
        } 
        #endregion

    }
}
