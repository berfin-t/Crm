using Crm.Customers;
using Crm.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crm.Tasks;

namespace Crm.Configurations
{
    public class TaskConfigurations:IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Tasks", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnName(nameof(Task.Title)).IsRequired();
            builder.Property(x => x.Description).HasColumnName(nameof(Task.Description)).IsRequired();
            builder.Property(x => x.DueDate).HasColumnName(nameof(Task.DueDate)).IsRequired();
            builder.Property(x => x.Status).HasColumnName(nameof(Task.Status)).IsRequired();
            builder.Property(x => x.Priority).HasColumnName(nameof(Task.Priority)).IsRequired();

            builder.HasOne<Employee>().WithMany().IsRequired().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
