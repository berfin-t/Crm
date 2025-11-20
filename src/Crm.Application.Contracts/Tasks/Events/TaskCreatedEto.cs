using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Tasks.Events
{
    public class TaskCreatedEto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
