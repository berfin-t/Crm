using Crm.Customers;
using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Support
{
    public class SupportTicketWithNavigationPropertyDto
    {
        public SupportTicketDto SupportTicket { get; set; } = null!;
        public CustomerDto Customer { get; set; } = null!;
        public EmployeeDto Employee { get; set; } = null!;
    }
}
