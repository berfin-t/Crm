using Crm.Blazor.Components.Dialogs.Employees;
using Crm.Blazor.Components.Dialogs.Projects;
using Crm.Customers;
using Crm.Employees;
using Crm.Permissions;
using Crm.Projects;
using Crm.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Projects
{
    public partial class ProjectDetail
    {
        #region Parameters & Injects
        [Parameter] public Guid ProjectId { get; set; }

        private ProjectDto? project;
        private ProjectDto? selectedProject;
        private List<ProjectEmployeeDto> projectEmployees = new();

        private bool isProjectModalVisible = false;
        private bool isDeleteModalVisible = false;

        private ProjectEditModal? projectEditModal;

        private long totalTasks;
        private long completedTasks;
        private long teamSize;

        private bool canEditProject;
        private bool canDeleteProject;

        private EventCallback RefreshCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        private bool isActivityModalVisible = false;
        public List<Guid> SelectedEmployeeIds { get; set; } = new();
        private List<EmployeeDto> Employees { get; set; } = new();
        private List<EmployeeDto> FilteredEmployees { get; set; } = new();
        private List<TaskDto> projectTasks = new();
        #endregion

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            project = await ProjectAppService.GetAsync(ProjectId);

            projectEmployees = await EmployeeAppService.GetEmployeesByProjectIdAsync(ProjectId);
            teamSize = projectEmployees.Count;

            totalTasks = await TaskAppService.GetTotalTaskCountByProjectIdAsync(ProjectId);
            completedTasks = await TaskAppService.GetCompletedTasksByProjectId(ProjectId);

            var taskInput = new GetPagedTasksInput
            {
                ProjectId = ProjectId,
                Title = null,
                Description = null,
                Priorities = null,
                Statuses = null,
                DueDate = null,
                EmployeeId = null
            };
            var taskResult = await TaskAppService.GetListAsync(taskInput);
            projectTasks = taskResult.Items.ToList();

            canEditProject = await AuthorizationService.IsGrantedAsync(CrmPermissions.Projects.Edit);
            canDeleteProject = await AuthorizationService.IsGrantedAsync(CrmPermissions.Projects.Delete);

            await base.OnInitializedAsync();
        }


        #region Edit
        private async System.Threading.Tasks.Task EditProject(ProjectDto project)
        {
            isProjectModalVisible = false;
            await projectEditModal!.ShowModal(project, RefreshCallback);
        }

        private async System.Threading.Tasks.Task OnSelectProjectForEdit(ProjectDto project)
        {
            selectedProject = project;
            await EditProject(project);
        }
        #endregion

        #region Delete
        private async System.Threading.Tasks.Task ConfirmDelete()
        {
            await ProjectAppService.DeleteAsync(ProjectId);
            NavigationManager.NavigateTo("/projects");
        }
        #endregion

        private async System.Threading.Tasks.Task ShowAddEmployeeModal()
        {
            isActivityModalVisible = true;
            Employees = await EmployeeAppService.GetListAllAsync();
            FilterEmployees();
        }

        private void FilterEmployees()
        {
            var teamMemberIds = projectEmployees.Select(x => x.EmployeeId).ToList();

            FilteredEmployees = Employees
                .Where(e => !teamMemberIds.Contains(e.Id))
                .ToList();
        }

        private async System.Threading.Tasks.Task UpdateProjectTeam()
        {
            var projectDto = await ProjectAppService.GetAsync(ProjectId);

            var updatedEmployeeIds = projectDto.EmployeeIds.ToList();

            foreach (var empId in SelectedEmployeeIds)
            {
                if (!updatedEmployeeIds.Contains(empId))
                    updatedEmployeeIds.Add(empId);
            }

            var updateDto = new ProjectUpdateDto
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                CustomerId = projectDto.CustomerId,
                StartTime = projectDto.StartTime,
                EndTime = projectDto.EndTime,
                Status = projectDto.Status,
                Revenue = projectDto.Revenue,
                SuccessRate = projectDto.SuccessRate,
                EmployeeIds = updatedEmployeeIds  
            };

            await ProjectAppService.UpdateAsync(ProjectId, updateDto);

            isActivityModalVisible = false;
            SelectedEmployeeIds.Clear();

            projectEmployees = await EmployeeAppService.GetEmployeesByProjectIdAsync(ProjectId);
            FilterEmployees();

            await InvokeAsync(StateHasChanged);
        }


    }
}
