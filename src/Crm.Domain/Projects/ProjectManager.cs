using Crm.Common;
using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Crm.Projects
{
    public class ProjectManager(IProjectRepository projectRepository, IEmployeeRepository employeeRepository) : DomainService
    {
        public async Task<Project> CreateAsync(
            List<Guid> employeeIds,
            Guid customerId,
            string name,
            DateTime startTime,
            DateTime endTime,
            EnumStatus status,
            decimal revenue,
            decimal successRate,
            string description)
        {
            var project = new Project(
                Guid.NewGuid(),
                name,
                description,
                startTime,
                endTime,
                status,
                revenue,
                successRate,
                employeeIds,
                customerId
            );

            foreach (var employeeId in employeeIds)
            {
                var employee = await employeeRepository.GetAsync(employeeId);
                if (employee != null)
                {
                    project.AddProjectEmployee(new ProjectEmployee(Guid.NewGuid(), project.Id, employee.Id));
                }
            }

            await projectRepository.InsertAsync(project);
            return project;
        }

        public async Task<Project> UpdateAsync(
            Guid projectId,
            List<Guid> employeeIds,
            Guid customerId,
            string name,
            DateTime startTime,
            DateTime endTime,
            EnumStatus status,
            decimal revenue,
            decimal successRate,
            string description)
        {
            var project = await projectRepository.GetAsync(projectId);

            if (project == null)
                throw new BusinessException("Project not found");

            project.SetName(name);
            project.SetDescription(description);
            project.SetStartTime(startTime);
            project.SetEndTime(endTime);
            project.SetStatus(status);
            project.SetRevenue(revenue);
            project.SetSuccessRate(successRate);
            project.SetCustomerId(customerId);

            var existingEmployees = project.ProjectEmployees.ToList();
            foreach (var pe in existingEmployees)
            {
                project.RemoveProjectEmployee(pe);
            }

            foreach (var employeeId in employeeIds)
            {
                var employee = await employeeRepository.GetAsync(employeeId);
                if (employee != null)
                {
                    project.AddProjectEmployee(new ProjectEmployee(Guid.NewGuid(), project.Id, employee.Id));
                }
            }

            await projectRepository.UpdateAsync(project);
            return project;
        }
    }
}
