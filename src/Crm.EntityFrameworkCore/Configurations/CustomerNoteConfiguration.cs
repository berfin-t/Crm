using Crm.CustomerNotes;
using Crm.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Configurations
{
    public class CustomerNoteConfiguration:IEntityTypeConfiguration<CustomerNote>
    {
        public void Configure(EntityTypeBuilder<CustomerNote> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "CustomerNotes", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Note).HasColumnName(nameof(CustomerNote.Note)).HasMaxLength(1024).IsRequired();
            builder.Property(x => x.NoteDate).HasColumnName(nameof(CustomerNote.NoteDate)).IsRequired();

            builder.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
