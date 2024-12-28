﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crm.Projects
{
    public class ProjectCreateDto
    {
        [Required]
        public string Name { get; set; } 
        public string? Description { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public ICollection<ProjectStatus> Statues { get; set; }
        [Required]
        public decimal Revenue { get; set; }
        [Required]
        public decimal SuccesRate { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid CustomerId { get; set; } 
    }
}