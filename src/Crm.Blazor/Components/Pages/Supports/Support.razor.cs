using Crm.Support;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Supports
{
    public partial class Support
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        private List<SupportTicketDto>? supportTicketList;
        protected override async Task OnInitializedAsync()
        {
            supportTicketList = await SupportTicketAppService.GetListAllAsync();
            await base.OnInitializedAsync();
        }
        private async Task ShowDetail(SupportTicketDto supportTicket)
        {
            NavigationManager.NavigateTo($"/supports/detail/{supportTicket.Id}");
            await Task.CompletedTask;
        }
    }
}
