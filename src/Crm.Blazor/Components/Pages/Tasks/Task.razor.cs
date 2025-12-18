using Blazorise;
using Crm.Blazor.Components.Dialogs.Tasks;
using Crm.Common;
using Crm.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Blazor.Components.Pages.Tasks
{
    public partial class Task
    {
        [Inject] public NavigationManager? NavigationManager { get; set; }

        private List<TaskDto> items = new();
        private TaskDto? selectedTask;
        private TaskDetailModal? taskDetailModal;
        private TaskCreateModal? taskCreateModal;

        private HubConnection? hubConnection;
        private bool showAlert = false;
        private string latestTaskName = string.Empty;

        private EventCallback<TaskDto> EventCallback;

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            EventCallback = Microsoft.AspNetCore.Components.EventCallback.Factory
                .Create<TaskDto>(this, task =>
                {
                    if (!items.Any(x => x.Id == task.Id))
                    {
                        items.Insert(0, task);
                        StateHasChanged();
                    }
                });

            items = (await TaskAppService.GetListAllAsync())
                .OrderByDescending(x => x.Priority)
                .ToList();

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

        private async System.Threading.Tasks.Task ShowCreateModal()
        {
            if (taskCreateModal != null)
            {
                await taskCreateModal.ShowModal(EventCallback);
            }
        }

        private void OnCardClick(TaskDto task)
        {
            selectedTask = task;
            taskDetailModal?.Show();
        }

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

            items = (await TaskAppService.GetListAllAsync())
                .OrderByDescending(x => x.Priority)
                .ToList();

            StateHasChanged();
        }

        private EnumStatus MapStatus(string zone) =>
            zone switch
            {
                "Pending" => EnumStatus.Pending,
                "Active" => EnumStatus.Active,
                "Completed" => EnumStatus.Completed,
                _ => EnumStatus.Pending
            };

        private string GetPriorityBorderStyle(TaskDto task) =>
            task.Priority switch
            {
                EnumPriority.Low => "border-left: 5px solid #22C55E;",
                EnumPriority.Medium => "border-left: 5px solid #FACC15;",
                EnumPriority.High => "border-left: 5px solid #F97316;",
                EnumPriority.Critical => "border-left: 5px solid #EF4444;",
                _ => "border-left: 5px solid #D1D5DB;"
            };
    }
}
