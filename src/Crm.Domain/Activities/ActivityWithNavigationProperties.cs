using Crm.Customers;
using Crm.Employees;

namespace Crm.Activities
{
    public class ActivityWithNavigationProperties
    {
        public Activity Activity { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;


    }
}
