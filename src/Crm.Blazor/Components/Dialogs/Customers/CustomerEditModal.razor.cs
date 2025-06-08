using Blazorise;
using Crm.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Customers
{
    public partial class CustomerEditModal
    {
        #region reference to the modal component
        private Modal? modalRef;
        private CustomerUpdateDto CustomerUpdateDto { get; set; } = new CustomerUpdateDto();
        private EventCallback EventCallback { get; set; }
        #endregion

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
            }

            await modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Update Customer
        private async Task UpdateCustomerAsync()
        {
            try
            {
                await CustomerAppService.UpdateAsync(CustomerUpdateDto.Id, CustomerUpdateDto);
                await HideModal();
                await EventCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
