using Crm.Employees;
using Crm.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Configurations
{
    public class EmployeeConfigurations: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(CrmConsts.DbTablePrefix + "Employees", CrmConsts.DbSchema);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasColumnName(nameof(Employee.FirstName)).IsRequired();
            builder.Property(x => x.LastName).HasColumnName(nameof(Employee.LastName)).IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnName(nameof(Employee.PhoneNumber)).IsRequired();
            builder.Property(x => x.Address).HasColumnName(nameof(Employee.Address)).IsRequired();
            builder.Property(x => x.BirthDate).HasColumnName(nameof(Employee.BirthDate)).IsRequired();
            builder.Property(x => x.PhotoPath).HasColumnName(nameof(Employee.PhotoPath)).IsRequired();
            builder.Property(x => x.Gender).HasColumnName(nameof(Employee.Gender)).IsRequired();
            builder.Property(x => x.PositionId).HasColumnName(nameof(Employee.PositionId)).IsRequired();

            builder.HasOne<Position>().WithMany().IsRequired().HasForeignKey(x => x.PositionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
