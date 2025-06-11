using Crm.Positions;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Crm.Employees
{
    public class EmployeeWithNavigationPropertyDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = null!;
        public EmployeeDto Employee { get; set; } = null!;
        public PositionDto Position { get; set; } = null!;
    }
}
