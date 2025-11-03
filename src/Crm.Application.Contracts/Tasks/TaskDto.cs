using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string? Name { get; init; }
        public string? Group { get; set; }

        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public EnumPriority Priority { get; set; }
        public EnumStatus Status { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
