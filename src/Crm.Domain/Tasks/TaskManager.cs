using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Tasks
{
    public class TaskManager(ITaskRepository taskRepository):DomainService
    {
        #region Create
        public virtual async Task<Task> CreateAsync(string title, string description, DateTime dueDate, EnumPriority priority,EnumStatus status, Guid projectId, Guid employeeId)     
        {
            var task = new Task(
                GuidGenerator.Create(),
                title,
                description,
                dueDate,
                priority,
                status,
                projectId,
                employeeId
            );
            return await taskRepository.InsertAsync(task);
        }
        #endregion
        #region Update
        public virtual async Task<Task> UpdateAsync(Guid id, string title,string description, DateTime dueDate, EnumPriority priority, EnumStatus status, Guid projectId, Guid employeeId)
        {
            var task = await taskRepository.GetAsync(id);
            task.SetTitle(title);
            task.SetDescription(description);
            task.SetDueDate(dueDate);
            task.SetPriority(priority);
            task.SetStatus(status);
            task.SetProjectId(projectId);
            task.SetEmployeeId(employeeId);
            return await taskRepository.UpdateAsync(task);
        }
        #endregion
    }
}
