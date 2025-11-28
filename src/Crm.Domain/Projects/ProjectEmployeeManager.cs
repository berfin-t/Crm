using Crm.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Crm.Projects
{
    public class ProjectEmployeeManager:DomainService
    {
        private readonly IRepository<Project, Guid> projectRepository;
        private readonly IRepository<ProjectEmployee, Guid> projectEmployeeRepository;

        public ProjectEmployeeManager(
            IRepository<Project, Guid> projectRepository,
            IRepository<ProjectEmployee, Guid> projectEmployeeRepository)
        {
            this.projectRepository = projectRepository;
            this.projectEmployeeRepository = projectEmployeeRepository;
        }

        #region Create
        public async Task<Project> CreateAsync(
            List<Guid> employeeIds,       
            Guid customerId,
            string name,
            DateTime startTime,
            DateTime endTime,
            EnumStatus status,
            decimal revenue,
            decimal successRate,
            string? description = null)
        {
            var project = new Project(
                GuidGenerator.Create(),
                name,
                description ?? string.Empty,
                startTime,
                endTime,
                status,
                revenue,
                successRate,
                employeeIds,     
                customerId
            );

            await projectRepository.InsertAsync(project, autoSave: true);

            foreach (var empId in employeeIds)
            {
                var projectEmployee = new ProjectEmployee(
                    GuidGenerator.Create(),
                    project.Id,
                    empId
                );

                await projectEmployeeRepository.InsertAsync(projectEmployee, autoSave: true);
            }

            return project;
        }
        #endregion
    }
}
