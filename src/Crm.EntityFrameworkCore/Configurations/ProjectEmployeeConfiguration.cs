using Crm.Employees;
using Crm.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Configurations
{
    public class ProjectEmployeeConfiguration 
        : IEntityTypeConfiguration<ProjectEmployee>
    {
        public void Configure(EntityTypeBuilder<ProjectEmployee> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "ProjectEmployees", CrmConsts.DbSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProjectId).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();

            builder.HasOne<Project>()
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => new { x.ProjectId, x.EmployeeId }).IsUnique();
        }
    }
}
