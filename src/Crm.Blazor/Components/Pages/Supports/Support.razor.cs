using Crm.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Supports
{
    public partial class Support
    {
        private List<SupportTicketDto>? supportTicketList;
        protected override async Task OnInitializedAsync()
        {
            supportTicketList = await SupportTicketAppService.GetListAllAsync();
            await base.OnInitializedAsync();
        }
    }
}
