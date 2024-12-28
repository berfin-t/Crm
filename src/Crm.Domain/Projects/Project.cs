using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Projects
{
    public class Project : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }
        [CanBeNull]
        public virtual string? Description { get; private set; }
        [NotNull]
        public virtual DateTime StartTime { get; private set; }
        [NotNull]
        public virtual DateTime EndTime { get; private set; }
        [NotNull]
        public virtual ProjectStatus Status { get; private set; }
        [NotNull]
        public virtual decimal Revenue { get; private set; } // Tahmini kazanç
        [NotNull]
        public virtual decimal SuccessRate { get; private set; } // Başarı oranı
        public virtual Guid UserId { get; private set; } // Sorumlu çalışan 
        public virtual Guid CustomerId { get; private set; }

        protected Project()
        {
            Name = string.Empty;
            Description = string.Empty;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Status = ProjectStatus.Pending;
            Revenue = 0;
            SuccessRate = 0;
        }
        public Project(
            Guid id, string name, string description,
            DateTime startTime, DateTime endTime, ProjectStatus status,
            decimal revenue, decimal successRate, Guid userId,
            Guid customerId
        )
        {
            SetName(name);
            SetDescription(description);
            SetStartTime(startTime);
            SetEndTime(endTime);
            SetStatus(status);
            SetRevenue(revenue);
            SetSuccessRate(successRate);
            SetUserId(userId);
            SetCustomerId(customerId);
        }

        public void SetName(string name) => Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProjectConsts.MinNameLength, ProjectConsts.MaxNameLength);
        public void SetDescription(string description) => Description = Check.Length(description, nameof(description), ProjectConsts.MinDescriptionLength, ProjectConsts.MaxDescriptionLength);
        public void SetStartTime(DateTime startTime) => StartTime = Check.NotNull(startTime, nameof(startTime));
        public void SetEndTime(DateTime endTime) => EndTime = Check.NotNull(endTime, nameof(endTime));
        public void SetStatus(ProjectStatus status) => Status = Check.NotNull(status, nameof(status));
        public void SetRevenue(decimal revenue) => Revenue = Check.NotNull(revenue, nameof(revenue));
        public void SetSuccessRate(decimal successRate) => SuccessRate = Check.NotNull(successRate, nameof(successRate));
        public void SetUserId(Guid userId) => UserId = Check.NotDefaultOrNull<Guid>(userId, nameof(userId));
        public void SetCustomerId(Guid customerId) => CustomerId = Check.NotDefaultOrNull<Guid>(customerId, nameof(customerId));

    }
}

