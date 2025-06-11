using System;

namespace Crm.Activities
{
    public class ActivityUpdateDto
    {
        public Guid Id { get; set; }
        public EnumType Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
