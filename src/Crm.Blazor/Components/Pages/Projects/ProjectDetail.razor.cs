using Crm.Blazor.Components.Dialogs.Employees;
using Crm.Blazor.Components.Dialogs.Projects;
using Crm.Customers;
using Crm.Employees;
using Crm.Permissions;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        #endregion

        protected override async Task OnInitializedAsync()
        {
            // --- Project Info ---
            project = await ProjectAppService.GetAsync(ProjectId);

            // --- Team Members ---
            projectEmployees = await EmployeeAppService.GetEmployeesByProjectIdAsync(ProjectId);
            teamSize = projectEmployees.Count;

            // --- Tasks ---
            totalTasks = await TaskAppService.GetTotalTaskCountByProjectIdAsync(ProjectId);
            completedTasks = await TaskAppService.GetCompletedTasksByProjectId(ProjectId);

            // --- Permissions ---
            canEditProject = await AuthorizationService.IsGrantedAsync(CrmPermissions.Projects.Edit);
            canDeleteProject = await AuthorizationService.IsGrantedAsync(CrmPermissions.Projects.Delete);

            await base.OnInitializedAsync();
        }

        #region Edit
        private async Task EditProject(ProjectDto project)
        {
            isProjectModalVisible = false;
            await projectEditModal!.ShowModal(project, RefreshCallback);
        }

        private async Task OnSelectProjectForEdit(ProjectDto project)
        {
            selectedProject = project;
            await EditProject(project);
        }
        #endregion

        #region Delete
        private async Task ConfirmDelete()
        {
            await ProjectAppService.DeleteAsync(ProjectId);
            NavigationManager.NavigateTo("/projects");
        }
        #endregion
    }
}
