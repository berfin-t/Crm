using Blazorise;
using Crm.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Crm.Blazor.Components.Dialogs.Customers
{
    public partial class CustomerEditModal
    {
        #region reference to the modal component
        private Modal? modalRef;
        private CustomerUpdateDto CustomerUpdateDto { get; set; } = new CustomerUpdateDto();
        private EventCallback EventCallback { get; set; }
        private EnumCustomer selectedType { get; set; }
        #endregion

        #region Modal Methods
        public async Task ShowModal(CustomerDto customer, EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            if (customer != null)
            {
                CustomerUpdateDto.Id = customer.Id;
                CustomerUpdateDto.Name = customer.Name;
                CustomerUpdateDto.Surname = customer.Surname;
                CustomerUpdateDto.Email = customer.Email;
                CustomerUpdateDto.Phone = customer.Phone;
                CustomerUpdateDto.Address = customer.Address;
                CustomerUpdateDto.CompanyName = customer.CompanyName;
                CustomerUpdateDto.CustomerType = customer.CustomerType;
            }

            await modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }
        #endregion

        #region Update Customer
        private async Task UpdateCustomerAsync()
        {            
            await CustomerAppService.UpdateAsync(CustomerUpdateDto.Id, CustomerUpdateDto);
            await HideModal();
            await EventCallback.InvokeAsync();
            
        }
        #endregion
    }
}
