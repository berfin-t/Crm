using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Activities
{
    public class ActivityCreateDto
    {
        [Required]
        public EnumType Type { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }
    }
}
