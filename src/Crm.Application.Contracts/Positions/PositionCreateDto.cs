using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Positions
{
    public class PositionCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public int MinExperience { get; set; }
        public int MaxExperience { get; set; }

    }
}
