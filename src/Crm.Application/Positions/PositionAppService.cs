using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Crm.Positions
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(CrmPermissions.Positions.Default)]
    internal class PositionAppService(IPositionRepository positionRepository,
        PositionManager positionManager) : CrmAppService, IPositionAppService
    {
        public Task<PositionDto> CreateAsync(PositionCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task<PositionDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PositionDto>> GetListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Volo.Abp.Application.Dtos.PagedResultDto<PositionDto>> GetListAsync(GetPagedPositionsInput input)
        {
            throw new NotImplementedException();
        }

        public Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
