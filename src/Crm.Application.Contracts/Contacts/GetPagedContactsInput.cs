using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Contacts
{
    public class GetPagedContactsInput
    {
        public GetPagedContactsInput() { }
        public ICollection<EnumType>? Type { get; set; } = null;
        public string ContactValue { get; set; } = null;
        public bool? IsPrimary { get; set; } = null;
        public Guid? CustomerId { get; set; } = null;
        public Guid? EmployeeId { get; set; } = null;
    }
}
