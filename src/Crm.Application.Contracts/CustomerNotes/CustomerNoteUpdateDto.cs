using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.CustomerNotes
{
    public class CustomerNoteUpdateDto
    {
        public Guid CustomerId { get; set; }
        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
