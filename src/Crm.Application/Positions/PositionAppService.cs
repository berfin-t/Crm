using Crm.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Crm.Positions
{
    [RemoteService(IsEnabled = false)]
    public class PositionAppService(IPositionRepository positionRepository,
        PositionManager positionManager) : CrmAppService, IPositionAppService
    {
        #region Create
        public async Task<PositionDto> CreateAsync(PositionCreateDto input)
        {
            var position = await positionManager.CreateAsync(
                input.Name, input.Description, input.Salary, input.MinExperience, input.MaxExperience);

            return ObjectMapper.Map<Position, PositionDto>(position);
        }
        #endregion

        #region Get
        [AllowAnonymous]
        public async Task<PositionDto> GetAsync(Guid id)
        {
           return ObjectMapper.Map<Position, PositionDto>(await positionRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<PositionDto>> GetListAllAsync()
        {
           var items = await positionRepository.GetListAsync();
            return ObjectMapper.Map<List<Position>, List<PositionDto>>(items);
        }
        #endregion

        #region GetListPaged
        [AllowAnonymous]
        public async Task<PagedResultDto<PositionDto>> GetListAsync(GetPagedPositionsInput input)
        {
            var totalCount = await positionRepository.GetCountAsync(
                input.Name, input.Description, input.Salary, input.MaxExperience, input.MaxExperience);

            var items = await positionRepository.GetListAsync(
                input.Name, input.Description, input.Salary, input.MaxExperience, input.MaxExperience);

            return new PagedResultDto<PositionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Position>, List<PositionDto>>(items)
            };

        }
        #endregion

        #region Update
        public async Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input)
        {
            var position = await positionManager.UpdateAsync(
                id, input.Name, input.Description, input.Salary, input.MinExperience, input.MaxExperience);

            return ObjectMapper.Map<Position, PositionDto>(position);
        }
        #endregion
    }
}
