using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Employees
{
    public class GetPagedEmployeesInput
    {
        public GetPagedEmployeesInput()
        {
            
        }

        public string? FirstName { get; set; } = null;
        public string?  LastName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public string? Address { get; set; } = null;
        public DateTime? BirthDate { get; set; } = null;
        public Guid? PositionId { get; set; } = null;
    }
}
