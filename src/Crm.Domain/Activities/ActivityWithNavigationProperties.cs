using Crm.Customers;
using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Activities
{
    public class ActivityWithNavigationProperties
    {
        public Activity Activity { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;


    }
}
