using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Positions
{
    public class Position:FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public virtual decimal Salary { get; private set; }
        public virtual int MinExperience { get; private set; }
        public virtual int MaxExperience { get; private set; }
        public virtual Guid DepartmentId { get; private set; }
        protected Position()
        {
            Name = string.Empty;
            Description = string.Empty;
            Salary = 0;
            MinExperience = 0;
            MaxExperience = 0;
            DepartmentId = Guid.Empty;
        }
        public Position(Guid id, string name, string description, decimal salary, int minExperience, int maxExperience, Guid departmentId)
        {
            SetName(name);
            SetDescription(description);
            SetSalary(salary);
            SetMinExperience(minExperience);
            SetMaxExperience(maxExperience);
            SetDepartmentId(departmentId);
        }
        public void SetName(string name) => Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        public void SetDescription(string description) => Description = description;
        public void SetSalary(decimal salary) => Salary = Check.NotNull(salary, nameof(salary));
        public void SetMinExperience(int minExperience) => MinExperience = Check.NotNull(minExperience, nameof(minExperience));
        public void SetMaxExperience(int maxExperience) => MaxExperience = Check.NotNull(maxExperience, nameof(maxExperience));
        public void SetDepartmentId(Guid departmentId) => DepartmentId = Check.NotNull(departmentId, nameof(departmentId));
    }
}
