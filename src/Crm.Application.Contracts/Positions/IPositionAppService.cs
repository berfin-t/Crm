using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Crm.Positions
{
    public interface IPositionAppService : IApplicationService
    {
        Task<PagedResultDto<PositionDto>> GetListAsync(GetPagedPositionsInput input);
        Task<List<PositionDto>> GetListAllAsync();
        Task<PositionDto> GetAsync(Guid id);
        Task<PositionDto> CreateAsync(PositionCreateDto input);
        Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input);
    }
}
