using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Tasks
{
    public class GetPagedTasksInput
    {
        public GetPagedTasksInput() { }
        public string? Title { get; set; } = null;
        public string? Description { get; set; } = null;
        public DateTime? DueDate { get; set; } = null;
        public ICollection<EnumPriority>? Priorities { get; set; } = null;
        public ICollection<EnumStatus>? Statuses { get; set; } = null;
        public Guid? EmployeeId { get; set; } = null;
        public Guid? CustomerId { get; set; } = null;
    }
}
