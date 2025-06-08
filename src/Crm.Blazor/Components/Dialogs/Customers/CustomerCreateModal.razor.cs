using Blazorise;
using Crm.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Dialogs.Customers
{
    public partial class CustomerCreateModal
    {
        #region Form Fields
        private string? Name { get; set; }
        private string? Surname { get; set; }
        private string? Email { get; set; }
        private string? Phone { get; set; }
        private string? Address { get; set; }
        private string? CompanyName { get; set; }
        #endregion

        #region reference to the modal component
        private EventCallback EventCallback { get; set; }
        private Modal? modalRef;
        #endregion

        public Task ShowModal(EventCallback eventCallback)
        {
            EventCallback = eventCallback;

            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            CompanyName = string.Empty;
            CustomerCreateDto = new CustomerCreateDto();

            return modalRef!.Show();
        }

        private Task HideModal()
        {
            return modalRef!.Hide();
        }

        #region Create Customer
        private CustomerCreateDto CustomerCreateDto { get; set; } = new CustomerCreateDto();

        private async Task CreateCustomerAsync()
        {
            CustomerCreateDto.Name = Name!;
            CustomerCreateDto.Surname = Surname!;
            CustomerCreateDto.Email = Email!;
            CustomerCreateDto.Phone = Phone!;
            CustomerCreateDto.Address = Address;
            CustomerCreateDto.CompanyName = CompanyName;

            try
            {
                await CustomerAppService.CreateAsync(CustomerCreateDto);
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
