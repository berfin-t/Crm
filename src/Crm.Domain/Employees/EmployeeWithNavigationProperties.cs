using Crm.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Employees
{
    public class EmployeeWithNavigationProperties
    {
        public Employee Employee { get; set; } = null!;
        public Position Position { get; set; } = null!;
    }
}
