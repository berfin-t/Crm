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
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\+?[0-9\s\-]{11}$")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public Guid PositionId { get; set; }

        [Required]
        public string PhotoPath { get; set; } = string.Empty;

        [Required]
        public EnumGender Gender { get; set; }

        [Required]
        public IdentityUserCreateDto User { get; set; } = new();
    }
}
