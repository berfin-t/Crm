using Asp.Versioning;
using Crm.Positions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Controllers.Positions
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Position")]
    [Route("api/app/positions")]
    public class PositionController:CrmController, IPositionAppService
    {
        protected IPositionAppService _positionAppService;
        public PositionController(IPositionAppService positionAppService) => _positionAppService = positionAppService;

        //[HttpGet]
        //[Route("{id}")]
        //public virtual Task<PositionDto> GetAsync(Guid id) => _positionAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<PositionDto>> GetListAllAsync() => _positionAppService.GetListAllAsync();

        //[HttpGet]
        //[Route("paged")]
        //public virtual Task<PagedResultDto<PositionDto>> GetListAsync(GetPagedPositionsInput input) => _positionAppService.GetListAsync(input);

        //[HttpPost]
        //[Route("create")]
        //public virtual Task<PositionDto> CreateAsync(PositionCreateDto input) => _positionAppService.CreateAsync(input);

        //[HttpPut]
        //[Route("update/{id}")]
        //public virtual Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input) => _positionAppService.UpdateAsync(id, input);
    }
    
}
