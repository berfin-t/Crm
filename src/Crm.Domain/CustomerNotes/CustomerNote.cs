using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Crm.CustomerNotes
{
    public class CustomerNote:FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Note { get; private set; }
        [CanBeNull]
        public virtual DateTime NoteDate { get; private set; }
        public virtual Guid CustomerId { get; private set; }
        protected CustomerNote()
        {
            Note = string.Empty;
            NoteDate = DateTime.Now;
        }
        public CustomerNote(Guid id, string note, DateTime noteDate, Guid customerId)
        {
            SetNote(note);
            SetNoteDate(noteDate);
            SetCustomerId(customerId);
        }
        public void SetNoteDate(DateTime noteDate) => NoteDate = Check.NotNull(noteDate, nameof(noteDate));
        public void SetNote(string note) => Note = Check.NotNullOrWhiteSpace(note, nameof(note), CustomerNoteConsts.MaxNoteLength);
        public void SetCustomerId(Guid customerId) => CustomerId = Check.NotDefaultOrNull<Guid>(customerId, nameof(customerId));
    }
}
