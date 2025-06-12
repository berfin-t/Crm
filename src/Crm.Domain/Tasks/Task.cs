using Crm.Common;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Tasks
{
    public class Task: FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Title { get; private set; }
        [CanBeNull]
        public virtual string? Description { get; private set; }
        [NotNull]
        public virtual DateTime DueDate { get; private set; }
        [NotNull]
        public virtual EnumPriority Priority { get; private set; }
        [NotNull]
        public virtual EnumStatus Status { get; private set; }
        public virtual Guid ProjectId { get; private set; }
        public virtual Guid EmployeeId { get; private set; }

        protected Task()
        {
            Title = string.Empty;
            Description = string.Empty;
            DueDate = DateTime.Now;
            Priority = EnumPriority.Low;
            Status = EnumStatus.Pending;
        }

        public Task(Guid id, string title, string description,
            DateTime dueDate, EnumPriority priority, EnumStatus status,
            Guid projectId, Guid employeeId)
        {
            SetTitle(title);
            SetDescription(description);
            SetDueDate(dueDate);
            SetPriority(priority);
            SetStatus(status);
            SetProjectId(projectId);
            SetEmployeeId(employeeId);
        }

        public void SetTitle(string title) => Title = Check.NotNullOrWhiteSpace(title, nameof(title), TaskConsts.MaxTitleLength);
        public void SetDescription(string description) => Description = Check.Length(description, nameof(description), TaskConsts.MaxDescriptionLength);
        public void SetDueDate(DateTime dueDate) => DueDate = Check.NotNull(dueDate, nameof(dueDate));
        public void SetPriority(EnumPriority priority) => Priority = Check.NotNull(priority, nameof(priority));
        public void SetStatus(EnumStatus status) => Status = Check.NotNull(status, nameof(status));
        public void SetProjectId(Guid projectId) => ProjectId = Check.NotDefaultOrNull<Guid>(projectId, nameof(projectId));
        public void SetEmployeeId(Guid employeeId) => EmployeeId = Check.NotDefaultOrNull<Guid>(employeeId, nameof(employeeId));
    }
}
