using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Activities
{
    public class GetPagedActivitiesInput
    {
        public GetPagedActivitiesInput() { }
        public ICollection<EnumType>? Type { get; set; } = null;
        public string Description { get; set; } = null;
        public DateTime? Date { get; set; } = null;
        public Guid? CustomerId { get; set; } = null;
        public Guid? EmployeeId { get; set; } = null;

    }
}
