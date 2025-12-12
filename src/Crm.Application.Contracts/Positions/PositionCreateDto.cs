using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Positions
{
    public class PositionCreateDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public int MinExperience { get; set; }

        [Required]
        public int MaxExperience { get; set; }

    }
}
