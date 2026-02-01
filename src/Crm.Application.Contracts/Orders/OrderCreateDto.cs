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

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total Amount must be greater than zero")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Order Code is required")]
        [StringLength(50, ErrorMessage = "Order Code cannot be longer than 50 characters")]
        public string? OrderCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

    }
}
