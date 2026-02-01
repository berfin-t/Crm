using System;

namespace Crm.Activities
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public EnumType Type { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeSurname { get; set; }

    }
}
