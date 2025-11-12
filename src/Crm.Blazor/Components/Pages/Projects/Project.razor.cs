using Blazorise.Components;
using Crm.Blazor.Components.Dialogs.Projects;
using Crm.Common;
using Crm.Permissions;
using Crm.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Projects
{
    public partial class Project
    {
        private List<ProjectDto> AllProjects = new();
        private List<ProjectDto> FilteredProjects = new();
        private IEnumerable<ProjectDto> ReadDataProjects { get; set; } = new List<ProjectDto>();
        private ProjectCreateModal? projectCreateModal;

        private string selectedAutoCompleteText = string.Empty;
        private EnumStatus? selectedStatus = null;        
        private int CurrentPage { get; set; } = 0;
        private int PageSize { get; set; } = 9;

        private readonly Func<ProjectDto, string> getName = item => item.Name!;
        private readonly Func<ProjectDto, string> getId = item => item.Id.ToString();

        private bool canCreateProject;

        protected override async Task OnInitializedAsync()
        {
            canCreateProject = await AuthorizationService.IsGrantedAsync(CrmPermissions.Projects.Create);

            AllProjects = (await ProjectAppService.GetListAllAsync()).OrderByDescending(p => p.StartTime).ToList();
            ApplyFilters();
        }

        private async Task OnHandleReadData(AutocompleteReadDataEventArgs args)
        {
            if (args.CancellationToken.IsCancellationRequested) return;

            await Task.Delay(100);

            var query = args.SearchValue ?? string.Empty;
            ReadDataProjects = AllProjects
                .Where(p => !string.IsNullOrWhiteSpace(p.Name) &&
                            p.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        public void LoadMoreProjects()
        {
            CurrentPage++;
            ApplyFilters();
        }

        public void OnEntered(KeyboardEventArgs args)
        {
            if (args.Code == "Enter" || args.Code == "NumpadEnter")
                ApplyFilters();
        }            
        private void ApplyFilters()
        {
            IEnumerable<ProjectDto> filtered = AllProjects;

            // İsim filtresi
            if (!string.IsNullOrWhiteSpace(selectedAutoCompleteText))
            {
                filtered = filtered.Where(p => !string.IsNullOrWhiteSpace(p.Name) &&
                                               p.Name.Contains(selectedAutoCompleteText, StringComparison.OrdinalIgnoreCase));
            }

            // Status filtresi
            if (selectedStatus.HasValue)
            {
                filtered = filtered.Where(p => p.Status == selectedStatus.Value);
            }

            // Sayfalama
            int itemsToTake = CurrentPage > 0 ? PageSize * CurrentPage : PageSize;

            FilteredProjects = filtered
                .OrderByDescending(p => p.StartTime)
                .Take(itemsToTake)
                .ToList();
        }

        public void NavigateToProjectDetail(Guid projectId)
        {
            Navigation.NavigateTo($"/project-detail/{projectId}");
        }

        private int GetCompletionPercentage(DateTime? start, DateTime? end)
        {
            if (!start.HasValue || !end.HasValue || start >= end) return 0;

            var totalDays = (end.Value - start.Value).TotalDays;
            var elapsedDays = (DateTime.Now - start.Value).TotalDays;
            return Math.Clamp((int)((elapsedDays / totalDays) * 100), 0, 100);
        }

        public async Task ShowCreateModal()
        {
            if (projectCreateModal != null)
                await projectCreateModal.ShowModal(EventCallback.Factory.Create(this, OnInitializedAsync));
        }
    }
}

