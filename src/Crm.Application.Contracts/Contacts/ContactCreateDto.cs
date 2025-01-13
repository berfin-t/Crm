using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Contacts
{
    public class ContactCreateDto
    {
        public EnumType Type { get; set; }
        public string ContactValue { get; set; }
        public bool IsPrimary { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
