using Crm.Customers;
using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Support
{
    public class SupportTicketWithNavigationProperties
    {
        public SupportTicket SupportTicket { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
