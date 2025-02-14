using Crm.Customers;
using Crm.Employees;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Activities
{
    public class Activity: FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual EnumType Type { get; private set; }
        [NotNull]
        public virtual string Description { get; private set; }
        [NotNull]
        public virtual DateTime Date { get; private set; }
        [NotNull]
        public virtual Guid CustomerId { get; private set; }
        [NotNull]
        public virtual Guid EmployeeId { get; private set; }

        // Olası bir senaryo için aşağıdaki property'ler eklenebilir.
        //public virtual Guid OwnerId { get; private set; }
        //public virtual Employee? Employee { get; private set; } 
        //public virtual Customer? Customer { get; private set; }
        protected Activity()
        {
            Type = EnumType.Call;
            Description = string.Empty;
            Date = DateTime.Now;
        }

        public Activity(Guid id, EnumType type, string description, DateTime date, Guid customerId, Guid employeeId)
        {
            SetType(type);
            SetDescription(description);
            SetDate(date);
            SetCustomerId(customerId);
            SetEmployeeId(employeeId);
        }

        public void SetType(EnumType type) => Type = Check.NotNull(type, nameof(type));
        public void SetDescription(string description) => Description = Check.NotNullOrWhiteSpace(description, nameof(description));
        public void SetDate(DateTime date) => Date = Check.NotNull(date, nameof(date));
        public void SetCustomerId(Guid customerId) => CustomerId = Check.NotDefaultOrNull<Guid>(customerId, nameof(customerId));
        public void SetEmployeeId(Guid employeeId) => EmployeeId = Check.NotDefaultOrNull<Guid>(employeeId, nameof(employeeId));

    }
}
