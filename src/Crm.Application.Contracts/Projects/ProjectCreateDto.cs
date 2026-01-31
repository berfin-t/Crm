using Crm.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crm.Projects
{
    public class ProjectCreateDto
    {
        public List<Guid> EmployeeIds { get; set; } = new();

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public EnumStatus Statues { get; set; }

        public decimal Revenue { get; set; }
        public decimal SuccesRate { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
