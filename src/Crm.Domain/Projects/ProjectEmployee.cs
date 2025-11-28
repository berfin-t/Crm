using Crm.Employees;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Projects
{
    public class ProjectEmployee : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid ProjectId { get; private set; }
        public virtual Guid EmployeeId { get; private set; }
        public virtual Project Project { get; private set; }
        public virtual Employee Employee { get; private set; }

        protected ProjectEmployee() { }

        public ProjectEmployee(Guid id, Guid projectId, Guid employeeId)
            : base(id)
        {
            SetProjectId(projectId);
            SetEmployeeId(employeeId);
        }

        public void SetProjectId(Guid projectId)
            => ProjectId = Check.NotDefaultOrNull<Guid>(projectId, nameof(projectId));

        public void SetEmployeeId(Guid employeeId)
            => EmployeeId = Check.NotDefaultOrNull<Guid>(employeeId, nameof(employeeId));
    }
}
