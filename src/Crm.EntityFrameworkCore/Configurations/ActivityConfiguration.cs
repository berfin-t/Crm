using Crm.Activities;
using Crm.Customers;
using Crm.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Configurations
{
    public class ActivityConfiguration:IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Activities", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Type).HasColumnName(nameof(Activity.Type)).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Description).HasColumnName(nameof(Activity.Description)).HasMaxLength(1024);
            builder.Property(x => x.Date).HasColumnName(nameof(Activity.Date)).IsRequired();

            builder.HasOne<Employee>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
