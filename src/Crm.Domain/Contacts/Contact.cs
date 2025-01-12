using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Contacts
{
    public class Contact:FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual EnumType Type { get; private set; }
        [NotNull]
        public virtual string ContactValue { get; private set; }
        [NotNull]
        public virtual bool IsPrimary { get; private set; }
        public virtual Guid CustomerId { get; private set; }
        public virtual Guid EmployeeId { get; private set; }

        protected Contact()
        {
            Type = EnumType.Email;
            ContactValue = string.Empty;
            IsPrimary = true;
        }

        public Contact(Guid id, EnumType type, string contactValue, bool isPrimary, Guid customerId, Guid employeeId)
        {
            SetType(type);
            SetContactValue(contactValue);
            SetIsPrimary(isPrimary);
            SetCustomerId(customerId);
            SetEmployeeId(employeeId);
        }

        public void SetType(EnumType type) => Type = Check.NotNull(type, nameof(type));
        public void SetContactValue(string contactValue) => ContactValue = Check.NotNullOrWhiteSpace(contactValue, nameof(contactValue));
        public void SetIsPrimary(bool isPrimary) => IsPrimary = Check.NotNull(isPrimary, nameof(isPrimary));
        public void SetCustomerId(Guid customerId) => CustomerId = Check.NotDefaultOrNull<Guid>(customerId, nameof(customerId));
        public void SetEmployeeId(Guid employeeId) => EmployeeId = Check.NotDefaultOrNull<Guid>(employeeId, nameof(employeeId));
    }
}
