using Blazorise;
using Crm.Blazor.Components.Dialogs.Tasks;
using Crm.Common;
using Crm.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Tasks
{
    public partial class Task : ComponentBase, IAsyncDisposable
    {
        [Inject] public NavigationManager? NavigationManager { get; set; }

        private List<TaskDto> items = new();
        private TaskDto? selectedTask;

        private TaskDetailModal? taskDetailModal;
        private TaskCreateModal? taskCreateModal;

        private HubConnection? hubConnection;

        private bool showAlert = false;
        private string latestTaskName = string.Empty;

        #region Initialization

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await LoadDataAsync();
            await InitializeSignalR();
        }

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            items = (await TaskAppService.GetListAllAsync())
                .OrderByDescending(x => x.Priority)
                .ToList();
        }

        #endregion

        #region SignalR

        private async System.Threading.Tasks.Task InitializeSignalR()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl($"{NavigationManager!.BaseUri}crmhub")
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<TaskDto>("TaskCreated", async task =>
            {
                if (!items.Any(x => x.Id == task.Id))
                {
                    items.Insert(0, task);
                    latestTaskName = task.Name ?? "New Task";
                    showAlert = true;

                    await InvokeAsync(StateHasChanged);
                }
            });

            await hubConnection.StartAsync();
        }

        #endregion

        #region Modal Methods

        private async System.Threading.Tasks.Task ShowCreateModal()
        {
            if (taskCreateModal == null)
                return;

            var createdCallback = EventCallback.Factory.Create<TaskDto>(this, async task =>
            {
                if (!items.Any(x => x.Id == task.Id))
                    items.Insert(0, task);

                await ReloadData();
            });

            await taskCreateModal.ShowModal(createdCallback);
        }

        private void OnCardClick(TaskDto task)
        {
            selectedTask = task;
            taskDetailModal?.Show();
        }

        private async System.Threading.Tasks.Task ReloadData()
        {
            await LoadDataAsync();
            StateHasChanged();
        }

        private EventCallback RefreshCallback =>
            EventCallback.Factory.Create(this, ReloadData);

        #endregion

        #region Drag & Drop

        private async System.Threading.Tasks.Task ItemDropped(DraggableDroppedEventArgs<TaskDto> dropItem)
        {
            dropItem.Item.Group = dropItem.DropZoneName;

            var updateDto = new TaskUpdateDto
            {
                Name = dropItem.Item.Name!,
                Description = dropItem.Item.Description!,
                DueDate = dropItem.Item.DueDate,
                Priority = dropItem.Item.Priority,
                Group = dropItem.DropZoneName,
                ProjectId = dropItem.Item.ProjectId,
                EmployeeId = dropItem.Item.EmployeeId,
                Status = MapStatus(dropItem.DropZoneName)
            };

            await TaskAppService.UpdateAsync(dropItem.Item.Id, updateDto);

            await ReloadData();
        }

        private EnumStatus MapStatus(string zone) =>
            zone switch
            {
                "Pending" => EnumStatus.Pending,
                "Active" => EnumStatus.Active,
                "Completed" => EnumStatus.Completed,
                _ => EnumStatus.Pending
            };

        #endregion

        #region UI Helpers

        private string GetPriorityBorderStyle(TaskDto task) =>
            task.Priority switch
            {
                EnumPriority.Low => "border-left: 5px solid #22C55E;",
                EnumPriority.Medium => "border-left: 5px solid #FACC15;",
                EnumPriority.High => "border-left: 5px solid #F97316;",
                EnumPriority.Critical => "border-left: 5px solid #EF4444;",
                _ => "border-left: 5px solid #D1D5DB;"
            };

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
                await hubConnection.DisposeAsync();
        }

        #endregion
    }
}
