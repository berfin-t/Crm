using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Customers
{
    public class GetPagedCustomersInput
    {
        public GetPagedCustomersInput() { }

        public string? Name { get; set; } = null;
        public string? Surname { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? Address { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public EnumCustomer? CustomerType { get; set; } = null;
    }
}
