using Crm.Common;
using System;
using System.Collections.Generic;

namespace Crm.Projects
{
    public class GetProjectsInput
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public ICollection<EnumStatus>? Statues { get; set; } = null;
        public decimal? Revenue { get; set; } = null;
        public decimal? SuccesRate { get; set; } = null;
        public Guid? EmployeeId { get; set; } = null;
        public Guid? CustomerId { get; set; } = null;
    }
}
