using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Crm.Projects
{
    public class ProjectDto:FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }
        public string? Description { get; set; } = null;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
        public decimal Revenue { get; set; }
        public decimal SuccessRate { get; set; }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
