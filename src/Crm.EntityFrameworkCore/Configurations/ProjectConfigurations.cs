using Crm.Customers;
using Crm.Employees;
using Crm.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Configurations
{
    public class ProjectConfigurations:IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Projects", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName(nameof(Project.Name)).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Description).HasColumnName(nameof(Project.Description)).HasMaxLength(1024);
            builder.Property(x => x.StartTime).HasColumnName(nameof(Project.StartTime)).IsRequired();
            builder.Property(x => x.EndTime).HasColumnName(nameof(Project.EndTime)).IsRequired();
            builder.Property(x => x.Status).HasColumnName(nameof(Project.Status)).IsRequired();
            builder.Property(x => x.Revenue).HasColumnName(nameof(Project.Revenue)).IsRequired();
            builder.Property(x => x.SuccessRate).HasColumnName(nameof(Project.SuccessRate)).IsRequired();

            builder.HasMany(x => x.ProjectEmployees)
                       .WithOne()
                       .HasForeignKey(pe => pe.ProjectId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Customer>().WithMany().IsRequired().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
