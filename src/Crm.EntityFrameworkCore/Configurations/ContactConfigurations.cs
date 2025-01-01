using Crm.Contacts;
using Crm.Customers;
using Crm.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Configurations
{
    public class ContactConfigurations: IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Contacts", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Type).HasColumnName(nameof(Contact.Type)).IsRequired().HasMaxLength(128);
            builder.Property(x => x.ContactValue).HasColumnName(nameof(Contact.ContactValue)).HasMaxLength(128).IsRequired();
            builder.Property(x => x.IsPrimary).HasColumnName(nameof(Contact.IsPrimary)).IsRequired();

            builder.HasOne<Employee>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
