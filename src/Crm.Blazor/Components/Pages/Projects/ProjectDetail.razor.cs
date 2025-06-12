using Crm.Blazor.Components.Dialogs.Employees;
using Crm.Blazor.Components.Dialogs.Projects;
using Crm.Customers;
using Crm.Employees;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Projects
{
    public partial class ProjectDetail
    {
        #region References
        [Parameter] public Guid ProjectId { get; set; }
        private ProjectDto? project;
        private ProjectDto? selectedProject;
        private bool isProjectModalVisible = false;
        private bool isDeleteModalVisible = false;
        private ProjectEditModal? projectEditModal;
        [Parameter] public List<EmployeeDto>? Employees { get; set; }
        [Parameter] public List<CustomerDto>? Customers { get; set; }
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        #endregion

        protected override async Task OnInitializedAsync()
        {
            project = await ProjectAppService!.GetAsync(ProjectId);
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
