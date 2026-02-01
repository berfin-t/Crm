using Asp.Versioning;
using Crm.Support;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Controllers.Support
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Support")]
    [Route("api/app/suports")]
    public class SupportController:CrmController, ISupportTicketAppService
    {
        protected ISupportTicketAppService _supportTicketAppService;

        public SupportController(ISupportTicketAppService supportTicketAppService) => _supportTicketAppService = supportTicketAppService;

        [HttpGet]
        [Route("all")]
        public virtual Task<List<SupportTicketDto>> GetListAllAsync() => _supportTicketAppService.GetListAllAsync();

        //[HttpPut]
        //[Route("update/{id}")]
        //public virtual Task<SupportTicketDto> UpdateAsync(Guid id, SupportTicketUpdateDto input) => _supportTicketAppService.UpdateAsync(id, input);
    }
}
