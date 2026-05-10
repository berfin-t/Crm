using Crm.Permissions;
using Crm.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Supports
{
    public partial class Support
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        private List<SupportTicketDto> supportTicketList = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            await base.OnInitializedAsync();
        }

        private async Task ShowDetail(SupportTicketDto supportTicket)
        {
            NavigationManager.NavigateTo($"/supports/detail/{supportTicket.Id}");
        }

        private async Task LoadDataAsync()
        {
            supportTicketList = (await SupportTicketAppService.GetMyTicketsAsync())
                .OrderByDescending(x => x.Priority)
                .ToList();

        }
    }
}
