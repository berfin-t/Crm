using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Customers
{
    public class CustomerUpdateDto
    {
        public Guid Id { get; set; }
        //public string FullName => $"{Name} {Surname}";
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
    }
}
