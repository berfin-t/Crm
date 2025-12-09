using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FullName => $"{Name} {Surname}";
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreationTime { get; set; }
        public EnumCustomer CustomerType { get; set; }

    }
}
