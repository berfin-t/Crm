using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.CustomerNotes
{
    public class GetPagedCustomerNotesInput
    {
        public GetPagedCustomerNotesInput() { }
        public string? Note { get; set; } = null;
        public DateTime? NoteDate { get; set; } = null;
        public Guid? CustomerId { get; set; } = null;
    }
}
