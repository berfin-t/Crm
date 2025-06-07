using Crm.Customers;
using Crm.Employees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Crm.Activities
{
    public class ActivityWithNavigationPropertyDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public ActivityDto Activity { get; set; } = null!;
        public CustomerDto Customer { get; set; } = null!;
        public EmployeeDto Employee { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
