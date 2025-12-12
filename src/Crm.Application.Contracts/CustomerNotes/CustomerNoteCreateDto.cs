using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.CustomerNotes
{
    public class CustomerNoteCreateDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public DateTime NoteDate { get; set; }
    }
}
