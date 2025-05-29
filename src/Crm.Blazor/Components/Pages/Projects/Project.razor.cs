using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Components;
using Crm.Blazor.Components.Dialogs.Projects;
using Crm.Common;
using Crm.Projects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Crm.Blazor.Components.Pages.Projects
{
    public partial class Project
    {
        public List<ProjectDto> ProjectList = new();
        public List<ProjectDto> FilteredProjects = new();
        
        public IEnumerable<ProjectDto> ReadDataProjects;
        public IEnumerable<ProjectDto> ProjectDto;

        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 9;
        public int TotalCount { get; set; } = 0;
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public string selectedProjectName = string.Empty;
        public string selectedProjectId = string.Empty; 
        public EnumStatus SelectedStatus = EnumStatus.Active;
        public string selectedAutoCompleteText { get; set; }
        IReadOnlyList<DateTime?> selectedDates;



        private ProjectCreateModal projectCreateModal;        

        protected override async Task OnInitializedAsync()
        {
            ProjectDto = await ProjectAppService.GetListAllAsync();
            await LoadMoreProjects();

            await base.OnInitializedAsync();
        }

        private async Task OnHandleReadData(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        {
            if(!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100);
                if(!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
                {
                    ReadDataProjects = ProjectDto.Where(x => x.Name.StartsWith(autocompleteReadDataEventArgs.SearchValue, StringComparison.InvariantCultureIgnoreCase));
                }
            }
        }        

        public async Task LoadMoreProjects()
        {
            var input = new GetPagedProjectsInput
            {
                Sorting = "StartTime DESC",
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize
            };

            var result = await ProjectAppService.GetListAsync(input);
            if (result?.Items != null)
            {
                ProjectList.InsertRange(0, result.Items);
                TotalCount = (int)result.TotalCount;
                CurrentPage++;
                ApplyFilters();
            }
        }

        public async Task OnEntered(KeyboardEventArgs args)
        {
            if(args.Code=="Enter" || args.Code=="NumpadEnter")
            {
                ApplySearch();
            }
        }
        public async Task ApplySearch()
        {           

            List<ProjectDto> Deneme = ProjectDto.Where(p => string.IsNullOrEmpty(selectedAutoCompleteText) || p.Name.Contains(selectedAutoCompleteText, StringComparison.OrdinalIgnoreCase)).ToList();

            FilteredProjects = Deneme;
        }        

        private void ApplyFilters()
        {

            FilteredProjects = ProjectList
                .Where(p => string.IsNullOrEmpty(selectedAutoCompleteText) || p.Name.Contains(selectedAutoCompleteText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            
        }

        public void NavigateToProjectDetail(Guid projectId)
        {
            Navigation.NavigateTo($"/project-detail/{projectId}");
        }

        private int GetCompletionPercentage(DateTime? start, DateTime? end)
        {
            if (!start.HasValue || !end.HasValue || start >= end)
            {
                return 0;
            }

            var totalDuration = (end.Value - start.Value).TotalDays;
            var elapsedDuration = (DateTime.Now - start.Value).TotalDays;
            var percentage = (elapsedDuration / totalDuration) * 100;

            return Math.Clamp((int)percentage, 0, 100);
        }

        public async Task ShowCreateModal()
        {
            if (projectCreateModal != null)
            {
                await projectCreateModal.ShowModal(EventCallback);
            }
        }

        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);

    }
}
