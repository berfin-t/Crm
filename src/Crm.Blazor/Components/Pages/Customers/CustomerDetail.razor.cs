using Crm.Activities;
using Crm.CustomerNotes;
using Crm.Customers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Customers
{
    public partial class CustomerDetail
    {
        [Parameter] public Guid Id { get; set; }

        private CustomerDto? customer;
        private List<CustomerNoteDto>? notes;
        private List<ActivityDto>? activities;

        protected override async Task OnInitializedAsync()
        {
            customer = await CustomerAppService.GetAsync(Id);
            notes = await CustomerNoteAppService.GetListByCustomerAsync(Id);
            activities = await ActivityAppService.GetListByCustomerAsync(Id);
        }        

        private Task DownloadPdf()
        {
            Navigation.NavigateTo($"/api/app/customers/{Id}/pdf", true);
            return Task.CompletedTask;
        }

    }
}
