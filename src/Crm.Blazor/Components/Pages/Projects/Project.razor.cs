using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Blazor.Components.Dialogs.Projects;
using Crm.Common;
using Crm.Projects;
using Microsoft.AspNetCore.Components;

namespace Crm.Blazor.Components.Pages.Projects
{
    public partial class Project
    {
        public List<ProjectDto> Projects { get; set; } = new();
        public List<ProjectDto> FilteredProjects { get; set; } = new();
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 9;
        public int TotalCount { get; set; } = 0;
        public string selectedProjectName { get; set; } = string.Empty;
        public EnumStatus SelectedStatus { get; set; } = EnumStatus.Active;
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        private ProjectCreateModal projectCreateModal;
        IReadOnlyList<DateTime?> selectedDates;

        protected override async Task OnInitializedAsync()
        {
            await LoadMoreProjects();
        }

        public async Task LoadMoreProjects()
        {
            var input = new GetPagedProjectsInput
            {
                Sorting = "StartTime ASC",
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize
            };

            var result = await ProjectAppService.GetListAsync(input);
            if (result?.Items != null)
            {
                Projects.AddRange(result.Items);
                TotalCount = (int)result.TotalCount;
                CurrentPage++;
                ApplyFilters();
            }
        }

        public void ApplySearch()
        {
            ApplyFilters();
        }

        public async Task ApplyDateFilter()
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            FilteredProjects = Projects
                .Where(p => string.IsNullOrEmpty(selectedProjectName) || p.Name.Contains(selectedProjectName, StringComparison.OrdinalIgnoreCase))
                .Where(p => !startDate.HasValue || !endDate.HasValue || (p.StartTime >= startDate && p.StartTime <= endDate))
                .Where(p => p.Status == SelectedStatus)
                .ToList();
        }

        public void NavigateToProjectDetail(Guid projectId)
        {
            Navigation.NavigateTo($"/project-detail/{projectId}");
        }

        public int GetCompletionPercentage(DateTime? start, DateTime? end)
        {
            if (!start.HasValue || !end.HasValue || start >= end)
                return 0;

            var totalDuration = (end.Value - start.Value).TotalDays;
            var elapsedDuration = (DateTime.Now - start.Value).TotalDays;

            return Math.Clamp((int)((elapsedDuration / totalDuration) * 100), 0, 100);
        }

        public async Task ShowCreateModal()
        {
            if (projectCreateModal != null)
            {
                await projectCreateModal.ShowModal();
            }
        }
    }
}
