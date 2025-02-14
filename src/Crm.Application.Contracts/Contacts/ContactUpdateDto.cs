using System;

namespace Crm.Contacts
{
    public class ContactUpdateDto
    {
        public EnumType Type { get; set; }
        public string ContactValue { get; set; }
        public bool IsPrimary { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
