using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Customers
{
    public class CustomerCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string? Address { get; set; }

        [Required]       
        public string? CompanyName { get; set; }

        [Required]
        public EnumCustomer CustomerType { get; set; }
        }
}
