using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.Activities
{
    public class Activity: FullAuditedAggregateRoot<Guid>
    {
    }
}
