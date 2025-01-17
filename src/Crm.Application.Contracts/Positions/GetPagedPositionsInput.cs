using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Positions
{
    public class GetPagedPositionsInput
    {
        public GetPagedPositionsInput()
        {
            
        }
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public decimal? Salary { get; set; } = 0;
        public int? MinExperience { get; set; } = 0;
        public int? MaxExperience { get; set; } = 0;
    }
}
