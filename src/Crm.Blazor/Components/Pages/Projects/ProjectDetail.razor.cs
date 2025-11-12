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
        #region References
        [Parameter] public string? ProjectSlug { get; set; }
        [Parameter] public Guid ProjectId { get; set; }
        private ProjectDto? project;
        private ProjectDto? selectedProject;
        private bool isProjectModalVisible = false;
        private bool isDeleteModalVisible = false;
        private ProjectEditModal? projectEditModal;
        [Parameter] public List<EmployeeDto>? Employees { get; set; }
        [Parameter] public List<CustomerDto>? Customers { get; set; }
        private long totalTasks;
        private long completedTasks;
        private long teamSize;
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);

        private bool canEditProject;
        private bool canDeleteProject;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            totalTasks = await TaskAppService.GetTotalTaskCountByProjectIdAsync(ProjectId);
            completedTasks = await TaskAppService.GetCompletedTasksByProjectId(ProjectId);
            teamSize = (await EmployeeAppService.GetEmployeesByProjectIdAsync(ProjectId)).Count();

            canEditProject = await AuthorizationService.IsGrantedAsync(CrmPermissions.Projects.Edit);
            canDeleteProject = await AuthorizationService.IsGrantedAsync(CrmPermissions.Projects.Delete);

            project = await ProjectAppService.GetAsync(ProjectId);
            await base.OnInitializedAsync();
        }       

        #region Edit
        private async Task EditProject(ProjectDto project)
        {
            isProjectModalVisible = false;
            await projectEditModal!.ShowModal(project, EventCallback);
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
            await ProjectAppService!.DeleteAsync(ProjectId);
            NavigationManager!.NavigateTo("/projects");
        }
        #endregion
    }
}
