using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Customers
{
    public class Customer: FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }
        [NotNull]
        public virtual string Surname { get; private set; }
        [NotNull]
        public virtual string Email { get; private set; }
        [NotNull]
        public virtual string Phone { get; private set; }
        [CanBeNull]
        public virtual string? Address { get; private set; }
        [CanBeNull]
        public virtual string? CompanyName { get; private set; }

        protected Customer()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            CompanyName = string.Empty;
        }
        public Customer(Guid id, string name, string surname, string email, string phone, string address, string companyName)
        {
            SetName(name);
            SetSurname(surname);
            SetEmail(email);
            SetPhone(phone);
            SetAddress(address);
            SetCompanyName(companyName);
        }

        public void SetName(string name) => Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        public void SetSurname(string surname) => Surname = Check.NotNullOrWhiteSpace(surname, nameof(surname));
        public void SetEmail(string email) => Email = Check.NotNullOrWhiteSpace(email, nameof(email));
        public void SetPhone(string phone) => Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone));
        public void SetAddress(string address) => Address = address;
        public void SetCompanyName(string companyName) => CompanyName = companyName;

    }
}
