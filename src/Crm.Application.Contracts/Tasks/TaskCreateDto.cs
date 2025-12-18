using Crm.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Tasks
{
    public class TaskCreateDto
    {
        [Required]
        public string? Name { get; set; }

        public string? Group { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public EnumPriority Priority { get; set; }

        [Required]
        public EnumStatus Status { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}
