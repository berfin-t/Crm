using Crm.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Orders
{
    public class OrderCreateDto
    {
        [Required]
        public EnumStatus Status { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [Required]
        [RegularExpression(@"^\+?[0-9\s\-]{10}$")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Order Code is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Order Code must be 6 characters long.")]
        [RegularExpression(@"^[A-Z]{4}\d{2}$", ErrorMessage = "Order Code format: 4 uppercase letters + 2 digits (ör. ABCD12).")]
        public string? OrderCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

    }
}
