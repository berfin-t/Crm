using Crm.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Projects
{
    public class ProjectUpdateDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]

        public DateTime? StartTime { get; set; }
        [Required]

        public DateTime? EndTime { get; set; }
        [Required]

        public EnumStatus Status { get; set; }
        [Required]

        public decimal Revenue { get; set; }
        [Required]

        public decimal SuccessRate { get; set; }
        [Required]

        public Guid EmployeeId { get; set; }
        [Required]

        public Guid CustomerId { get; set; }

    }
}
