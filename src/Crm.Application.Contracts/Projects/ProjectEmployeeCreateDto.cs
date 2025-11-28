using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Projects
{
    public class ProjectEmployeeCreateDto
    {       
            public List<Guid> EmployeeIds { get; set; } = new();  // Çoklu çalışan
            public Guid CustomerId { get; set; }
            public string Name { get; set; } = null!;
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public EnumStatus Statues { get; set; }
            public decimal Revenue { get; set; }
            public decimal SuccesRate { get; set; }
            public string? Description { get; set; }
        

    }
}
