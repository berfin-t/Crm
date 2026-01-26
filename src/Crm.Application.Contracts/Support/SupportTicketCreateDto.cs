using Crm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Support
{
    public class SupportTicketCreateDto
    {
        public string CustomerFullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
