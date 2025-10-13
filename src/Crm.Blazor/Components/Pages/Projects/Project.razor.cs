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
        #region References
        private ProjectCreateModal? projectCreateModal;
        private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        public List<ProjectDto> AllProjects = new();
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 9;
        public int TotalCount { get; set; } = 0;
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            await LoadMoreProjects();
        }
        public async Task ShowCreateModal()
        {
            if (projectCreateModal != null)
            {
                await projectCreateModal.ShowModal(EventCallback);
            }
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
                AllProjects.AddRange(result.Items);
                TotalCount = (int)result.TotalCount;
                CurrentPage++;                
            }
        }

        //    #region References
        //    public List<ProjectDto> ProjectList = new();
        //    public List<ProjectDto> FilteredProjects = new();
        //    public IEnumerable<ProjectDto>? ReadDataProjects { get; set; } = new List<ProjectDto>();
        //    public IEnumerable<ProjectDto>? ProjectDto;
        //    public string selectedProjectName = string.Empty;
        //    public string selectedProjectId = string.Empty;
        //    public string? selectedAutoCompleteText { get; set; }
        //    private ProjectCreateModal? projectCreateModal;
        //    public EnumStatus? SelectedStatus { get; set; } = null;
        //    private EventCallback EventCallback => EventCallback.Factory.Create(this, OnInitializedAsync);
        //    private readonly Func<ProjectDto, string> getName = item => item.Name;
        //    private readonly Func<ProjectDto, string> getId = item => item.Id.ToString();
        //    public List<ProjectDto> AllProjects = new();
        //    #endregion

        //    public int CurrentPage { get; set; } = 0;
        //    public int PageSize { get; set; } = 9;
        //    public int TotalCount { get; set; } = 0;
        //    public DateTime? startDate { get; set; }
        //    public DateTime? endDate { get; set; }

        //    protected override async Task OnInitializedAsync()
        //    {
        //        AllProjects = (await ProjectAppService.GetListAllAsync()).ToList();
        //        await LoadMoreProjects();
        //        //FilteredProjects = AllProjects.Take(AllProjects.Count < PageSize ? AllProjects.Count : PageSize).ToList();
        //    }

        //    private async Task OnHandleReadData(AutocompleteReadDataEventArgs autocompleteReadDataEventArgs)
        //    {
        //        if(!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
        //        {
        //        await Task.Delay(100);
        //            if(!autocompleteReadDataEventArgs.CancellationToken.IsCancellationRequested)
        //            {
        //                var query = autocompleteReadDataEventArgs.SearchValue ?? string.Empty;

        //                ReadDataProjects = AllProjects
        //                    .Where(x => !string.IsNullOrWhiteSpace(x.Name) &&
        //                                x.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase))
        //                    .ToList();
        //            }
        //        }            
        //    }

        //    public async Task LoadMoreProjects()
        //    {
        //        var input = new GetPagedProjectsInput
        //        {
        //            Sorting = "StartTime DESC",
        //            MaxResultCount = PageSize,
        //            SkipCount = CurrentPage * PageSize
        //        };

        //        var result = await ProjectAppService.GetListAsync(input);
        //        if (result?.Items != null)
        //        {
        //            ProjectList.AddRange(result.Items);
        //            TotalCount = (int)result.TotalCount;
        //            CurrentPage++;
        //            ApplyFilters();
        //        }
        //    }

        //    public void OnEntered(KeyboardEventArgs args)
        //    {
        //        if(args.Code=="Enter" || args.Code=="NumpadEnter")
        //        {
        //            ApplyFilters();
        //        }
        //    }

        //    private void ApplyFilters()
        //    {
        //        FilteredProjects = AllProjects
        //            .Where(p => string.IsNullOrEmpty(selectedAutoCompleteText) || p.Name!.Contains(selectedAutoCompleteText, StringComparison.OrdinalIgnoreCase))
        //    .ToList();

        //    }

        //    public void NavigateToProjectDetail(Guid projectId)
        //    {
        //        Navigation.NavigateTo($"/project-detail/{projectId}");
        //    }       

        //    private int GetCompletionPercentage(DateTime? start, DateTime? end)
        //    {
        //        if (!start.HasValue || !end.HasValue || start >= end)
        //        {
        //            return 0;
        //        }

        //        var totalDuration = (end.Value - start.Value).TotalDays;
        //        var elapsedDuration = (DateTime.Now - start.Value).TotalDays;
        //        var percentage = (elapsedDuration / totalDuration) * 100;

        //        return Math.Clamp((int)percentage, 0, 100);
        //    }

        //    public async Task ShowCreateModal()
        //    {
        //        if (projectCreateModal != null)
        //        {
        //            await projectCreateModal.ShowModal(EventCallback);
        //        }
        //    }

    }
}
