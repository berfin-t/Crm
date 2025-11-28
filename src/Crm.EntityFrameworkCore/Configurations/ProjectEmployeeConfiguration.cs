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
            builder.HasOne(x => x.Project)
                   .WithMany(p => p.ProjectEmployees) // Project sınıfında ICollection<ProjectEmployee> olmalı
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.ProjectEmployees) // Employee sınıfında ICollection<ProjectEmployee> olmalı
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => new { x.ProjectId, x.EmployeeId }).IsUnique();
        }
    }
}
