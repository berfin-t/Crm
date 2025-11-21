using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Employees
{
    public class ProjectEmployeeDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string PhotoPath { get; set; }

    }
}
