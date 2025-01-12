using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Positions
{
    public class PositionManager(IPositionRepository positionRepository):DomainService
    {
        #region Create
        public virtual async Task<Position> CreateAsync(string name, string description, decimal salary, int minExperience, int maxExperience) {
            var position = new Position(
                GuidGenerator.Create(),
                name,
                description,
                salary,
                minExperience,
                maxExperience
            );
            return await positionRepository.InsertAsync(position);
        }
        #endregion
        #region Update
        public virtual async Task<Position> UpdateAsync(Guid id, string name, string description, decimal salary, int minExperience, int maxExperience)
        {
            var position = await positionRepository.GetAsync(id);
            position.SetName(name);
            position.SetDescription(description);
            position.SetSalary(salary);
            position.SetMinExperience(minExperience);
            position.SetMaxExperience(maxExperience);
            return await positionRepository.UpdateAsync(position);
        }
        #endregion
    }
}
