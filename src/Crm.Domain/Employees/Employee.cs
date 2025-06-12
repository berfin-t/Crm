using Crm.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Employees
{
    public class Employee:FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string FirstName { get; private set; }
        [NotNull]
        public virtual string LastName { get; private set; }
        [NotNull]
        public virtual string Email { get; private set; }
        [NotNull]
        public virtual string PhoneNumber { get; private set; }
        [NotNull]
        public virtual string Address { get; private set; }
        [NotNull]
        public virtual string PhotoPath { get; private set; }
        [NotNull]
        public virtual EnumGender Gender { get; private set; }
        [NotNull]
        public virtual DateTime BirthDate { get; private set; }
        public virtual Guid PositionId { get; private set; }
        //public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; } = new List<EmployeeTask>();

        protected Employee()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Address = string.Empty;
            BirthDate = DateTime.Now;
            PhotoPath = string.Empty;
            
        }
        public Employee(Guid id, string firstName, string lastName, string email, string phoneNumber, string address, DateTime birthDate, string photoPath, EnumGender gender, Guid positionId)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetPhoneNumber(phoneNumber);
            SetAddress(address);
            SetBirthDate(birthDate);
            SetPositionId(positionId);
            SetPhotoPath(photoPath);
            SetGender(gender);

        }
        public void SetFirstName(string firstName) => FirstName = Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
        public void SetLastName(string lastName) => LastName = Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
        public void SetEmail(string email) => Email = Check.NotNullOrWhiteSpace(email, nameof(email));
        public void SetPhoneNumber(string phoneNumber) => PhoneNumber = Check.NotNullOrWhiteSpace(phoneNumber, nameof(phoneNumber));
        public void SetAddress(string address) => Address = Check.NotNullOrWhiteSpace(address, nameof(address));
        public void SetBirthDate(DateTime birthDate) => BirthDate = Check.NotNull(birthDate, nameof(birthDate));
        public void SetPositionId(Guid positionId) => PositionId = Check.NotDefaultOrNull<Guid>(positionId, nameof(positionId));
        public void SetPhotoPath(string photoPath) => PhotoPath = photoPath;
        public void SetGender(EnumGender gender) => Gender = Check.NotNull(gender, nameof(gender));
    }
}
