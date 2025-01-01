using Crm.Common;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Projects
{
    public class ProjectManager(IProjectRepository projectRepository):DomainService
    {
        #region Create
        public virtual async Task<Project> CreateAsync(Guid employeeId, Guid customerId,
            string name, DateTime startTime, DateTime endTime,
            EnumStatus status, decimal revenue, decimal successRate, string? description = null)
        {
            var project = new Project(
                GuidGenerator.Create(),
                name,
                description!,
                startTime,
                endTime,
                status,
                revenue,
                successRate,
                employeeId,
                customerId
            );
            return await projectRepository.InsertAsync(project);
        }
        #endregion

        #region Update
        public virtual async Task<Project> UpdateAsync(Guid id, Guid employeeId, Guid customerId,
            string name, DateTime startTime, DateTime endTime,
            EnumStatus status, decimal revenue, decimal successRate, string? description = null)
        {
            var project = await projectRepository.GetAsync(id);
            project.SetName(name);
            project.SetDescription(description!);
            project.SetStartTime(startTime);
            project.SetEndTime(endTime);
            project.SetStatus(status);
            project.SetRevenue(revenue);
            project.SetSuccessRate(successRate);
            project.SetEmployeeId(employeeId);
            project.SetCustomerId(customerId);
            return await projectRepository.UpdateAsync(project);
        }
        #endregion
    }
}
