using Asp.Versioning;
using Crm.Activities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Controllers.Activities
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Activity")]
    [Route("api/app/activities")]
    public class ActivityController:CrmController, IActivityAppService
    {
        protected IActivityAppService _activityAppService;
        public ActivityController(IActivityAppService activityAppService) => _activityAppService = activityAppService;

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ActivityDto> GetAsync(Guid id) => _activityAppService.GetAsync(id);

        [HttpGet]
        [Route("all")]
        public virtual Task<List<ActivityDto>> GetListAllAsync() => _activityAppService.GetListAllAsync();

        [HttpGet]
        [Route("paged")]
        public virtual Task<PagedResultDto<ActivityDto>> GetListAsync(GetPagedActivitiesInput input) => _activityAppService.GetListAsync(input);

        [HttpPost]
        [Route("create")]
        public virtual Task<ActivityDto> CreateAsync(ActivityCreateDto input) => _activityAppService.CreateAsync(input);

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<ActivityDto> UpdateAsync(Guid id, ActivityUpdateDto input) => _activityAppService.UpdateAsync(id, input);
    }
}
