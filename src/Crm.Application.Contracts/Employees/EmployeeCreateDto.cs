using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Crm.Employees
{
    public class EmployeeCreateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid PositionId { get; set; }
        public string? PhotoPath { get; set; }
        public EnumGender Gender { get; set; }

        [Required]
        public IdentityUserCreateDto User { get; set; } = new();
    }
}
