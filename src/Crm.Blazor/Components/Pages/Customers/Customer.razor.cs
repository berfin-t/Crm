using Crm.Blazor.Components.Dialogs.Customers;
using Crm.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Customers
{
    public partial class Customer
    {
        [Inject] public NavigationManager? NavigationManager { get; set; }

        #region reference to the service
        private CustomerDto? CustomerDto { get; set; }
        private List<CustomerDto>? customerList;
        private CustomerCreateModal? customerCreateModal;
        private CustomerEditModal? customerEditModal;
        private bool isDeleteModalVisible = false;
        private CustomerDto? selectedCustomer;
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        #endregion

        protected override async Task OnInitializedAsync()
        {
            customerList = await CustomerAppService.GetListAllAsync();
            await base.OnInitializedAsync();
        }           

        private async Task ShowCreateModal()
        {
            if (customerCreateModal != null)
            {
                await customerCreateModal.ShowModal(EventCallback);
            }
        }

        private async Task ShowEditModal(CustomerDto customer)
        {
            if (customerEditModal != null)
            {
                await customerEditModal.ShowModal(customer, EventCallback);
            }
        }

        #region Delete 
        private void OnDeleteClicked(CustomerDto customer)
        {
            selectedCustomer = customer;
            isDeleteModalVisible = true;
        }

        private async Task ConfirmDelete()
        {
            if (selectedCustomer != null && selectedCustomer.Id != Guid.Empty)
            {
                await CustomerAppService.DeleteAsync(selectedCustomer.Id);
                isDeleteModalVisible = false;
                await OnInitializedAsync();
            }
        }
        #endregion

    }
}
